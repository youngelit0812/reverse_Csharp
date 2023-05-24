'DIUS4 Tools Script for DIUS4.0
'Writen Chubkoff Alex
'Copiright "Magellan Group" 2019
'------------------------------------------------------------------------------
Start:
		bool_Repair_User = False
		bool_New_User = False
		Email = RegKey.GetValue(2,"Email","")
		Progress("Start DIUS Tools . . .",3,0)
		NewPage(pnlInfo,"USER INFO")
		Call :DIUS4_Connect
		Network.Close(0)

	'If User Email and Hash Correct
	If == srvResult,0 Goto :Start_Tools
	'If Email correct and no correct Hash
	If == srvResult,130 Goto :lbl_AccessError
	'If New User
	If == srvResult,128 Goto :Create_NewUser
'Repair Mode
		bool_Repair_User = True
		Progress("Repair Mode . . .",3,1)
		NewView(pnlInfo,"Access_Control",600,90)
		AddInfo(Access_Control,"Description",250)
		AddEdit(Access_Control,"Input_User_Info",350,"EndEdit_Compare_Passwords")
		AddEvent(Access_Control,"CellEndEdit","")
		Row_UserPass = AddRow(Access_Control)
		SetCell(Row_UserPass,"Description"),"Password:"
		SetCell(Row_UserPass,"Input_User_Info"),""
	Exit

'Сравнение паролей
EndEdit_Compare_Passwords:
	If == bool_Repair_User,False Return
		bool_Repair_User = False
		Progress("Send User Info . . .",3,1)
		str_UserPass = GetCell(Row_UserPass,"Input_User_Info")
		Call :DIUS4_Connect
		srvResult = Network.Send(0)(1,str_UserPass)
	If == srvResult,0 Goto :CreateViewUserInfo
		Progress("Password Error . . .",0,0)
		bool_Repair_User = True
		SetCell(Row_UserPass,"Input_User_Info"),""
		RegKey.SetValue(2,"State",-1)
		Network.Close(0)
Return

'-------------------------------------- Регистрация или восстановление нового пользователя --------------------
'Новый пользователь - Регистрация нового пользователя
Create_NewUser:
		bool_New_User = True
		Progress("Register New User . . .",3,1)
		NewView(pnlInfo,"User_Information",600,186)
		NewButton(User_Information,420,158,"Register New User . . .","btn_Register_New_User")
		AddInfo(User_Information,"Description",250)
		AddEdit(User_Information,"Input_User_Info",350,"")
		row_UserName = AddRow(User_Information)
		SetCell(row_UserName,"Description"),"User Name:"
		SetCell(row_UserName,"Input_User_Info"),""
		row_Surname = AddRow(User_Information)
		SetCell(row_Surname,"Description"),"User Surname or Company Name:"
		SetCell(row_Surname,"Input_User_Info"),""
		row_UserEmail = AddRow(User_Information)
		SetCell(row_UserEmail,"Description"),"User Email Address:"
		SetCell(row_UserEmail,"Input_User_Info"),""
		row_UserPass = AddRow(User_Information)
		SetCell(row_UserPass,"Description"),"User Password:"
		SetCell(row_UserPass,"Input_User_Info"),""
		row_UserPassConfirm = AddRow(User_Information)
		SetCell(row_UserPassConfirm,"Description"),"Confirm Password:"
		SetCell(row_UserPassConfirm,"Input_User_Info"),""
Exit

'Обработка данных нового пользователя и отправка на сервер
btn_Register_New_User:
	If == bool_New_User,False Return
		Progress("Incorrect User Name",3,2)
		str_UserName = GetCell(row_UserName,"Input_User_Info")
	If == str_UserName,"" Return
		Progress("Incorrect User Surname",3,2)
		str_UserSurname = GetCell(row_Surname,"Input_User_Info")
	If == str_UserSurname,"" Return
		Progress("Incorrect Email",3,2)
		Email2 = GetCell(row_UserEmail,"Input_User_Info")
	If == Email2,"" Return
		Progress("Password Length Minimum 8 Symbols",3,2)
		str_UserPass = GetCell(row_UserPass,"Input_User_Info")
	If < str_UserPass.Length(0),8 Return
		'Progress("Password Confirm Not Correct",3,2)
		'str_UserPassConfirm = GetCell(row_UserPassConfirm,"Input_User_Info")
	'If <> str_UserPass,str_UserPassConfirm Return
		Progress("Send User Info . . .",3,3)
	'Connect to Server for send new user info
		str_UserName = $(2),"|0| |1|",str_UserName,str_UserSurname
		Call :DIUS4_Connect
		'Проверка верных данных
		srvResult = Network.Send(0)(4,Email2,str_UserName,str_UserPass,MyDealerId)
		Progress("Incorrect Email",0,0)
	If == srvResult,130 Goto :lbl_AccessError
'Register new email
		Email = Email2
		RegKey.SetValue(2,"Email",Email)

'Create panel with server files info with compare local and server files list
CreateViewUserInfo:
		bool_New_User = False
		bool_Repair_User = False
		NewView(pnlInfo,"Server_Files",920,350)
		AddInfo(Server_Files,"id",0)
		AddInfo(Server_Files,"FileName",250)
		AddInfo(Server_Files,"Description",430)
		AddInfo(Server_Files,"Current_Version",100)
		AddInfo(Server_Files,"Version",100)
		AddInfo(Server_Files,"Hash",0)
		AddInfo(Server_Files,"Binary",0)
'Загружаем список служебных файлов
		Progress("Get User Files . . .",0,0)
		srvResult = Network.Send(0)(1,"UserFilesList")
	If == srvResult,130 Goto :lbl_AccessError
		dt_UserFiles =
		Server_Files.DataSource = dt_UserFiles
'Загрузка тела файла и сохранение
		cycle = 0
lbl_NextUserFile:
		str_tmp = GetCell(Server_Files,cycle,"filename")
		Progress($(1),"Download |0|",str_tmp,dt_UserFiles.TableName(0),cycle)
		srvResult = Network.Send(0)(2,"GetUserFile",str_tmp)
	If == srvResult,130 Goto :lbl_AccessError
		dt_tmp = 
		bytes_Array = GetCell(dt_tmp,0,"binary")
		FileSave(str_tmp,bytes_Array)
		SetCell(Server_Files,cycle,"Current_Version"),GetCell(Server_Files,cycle,"version")
		cycle = Add(cycle,1)
	If <> cycle,dt_UserFiles.TableName(0) Goto :lbl_NextUserFile
'Сохранение данных о файлах в Updates database
		cycle = 0
lbl_NextUserFileStore:
		StoreSqlRow("\Data\updates.dsb","files",5,"filename" GetCell(Server_Files,cycle,"filename"),"id" GetCell(Server_Files,cycle,"id"),"description" GetCell(Server_Files,cycle,"description"),"version" GetCell(Server_Files,cycle,"version"),"hash" GetCell(Server_Files,cycle,"hash")
		cycle = Add(cycle,1)
	If <> cycle,dt_UserFiles.TableName(0) Goto :lbl_NextUserFileStore
		Progress("Download User Files Complete",0,0)
		RegKey.SetValue(2,"State",-1)
		Network.Close(0)
		Protect.Update(0)
	End

'---------------------------------------------------------------------------------------------------
Start_Tools:
'create view user info
		Progress("Load User Info",3,0)
		NewButton(pnlInfo,2,2,"U p d a t e","btn_Update_License","")
		NewView(pnlInfo,"User_Information",920,76)
		AddInfo(User_Information,"id",0)
		AddInfo(User_Information,"UserName",300)
		AddInfo(User_Information,"Email",298)
		AddInfo(User_Information,"Password",0)
		AddInfo(User_Information,"Dealer_Email",300)
		Call :DIUS4_Connect
		srvResult = Network.Send(0)(1,"UserInfo")
	If == srvResult,130 Goto :lbl_AccessError
		User_Information.DataSource = 

'Create view with users licenses info local and server state

		NewView(pnlInfo,"Server_Licenses",920,150)
		AddInfo(Server_Licenses,"Current_Brand",190)
		AddInfo(Server_Licenses,"Brand",190)
		AddInfo(Server_Licenses,"Current_Type",160)
		AddInfo(Server_Licenses,"Type",160)
		AddInfo(Server_Licenses,"Create_Date",100)
		AddInfo(Server_Licenses,"End_Date",100)

'Create panel with server files info with compare local and server files list

		NewView(pnlInfo,"Server_Files",920,300)
		AddInfo(Server_Files,"id",0)
		AddInfo(Server_Files,"FileName",270)
		AddInfo(Server_Files,"Description",430)
		AddInfo(Server_Files,"Current_Version",100)
		AddInfo(Server_Files,"Version",100)
		AddInfo(Server_Files,"Hash",0)
		AddInfo(Server_Files,"Binary",0)
		Call :Refresh_License
		Network.Close(0)
Exit

'------------------------------- Update License Event Button Update -----------------------
btn_Update_License:
'Send user History using firmwares
		Progress("Send History...",3,0)
		dt_tmp = IntSQL("\Data\history.dsb","SELECT * FROM history")
		Call :DIUS4_Connect
		srvResult = Network.Send(0)(2,"Update",dt_tmp)
	If == srvResult,130 Goto :lbl_AccessError

'Update files - dt_tmp with files list, cycleend - total files
		Progress "Update Files...",3,3
		dt_tmp = Server_Files.DataSource,0
		cycleend = dt_tmp.TableName,0
		cycle = 0
ForNext4:
		Current_Version = Version(GetCell Server_Files,cycle,"Current_Version")
		New_Version = Version(GetCell(Server_Files,cycle,"Version"))
		If > New_Version,Current_Version Call :proc_UpdServiceFiles
		cycle = Add(cycle,1)
		If <> cycle,cycleend Goto :ForNext4

		Call :Refresh_License

'Update Licenses
		Progress("Update Licenses...",3,2)
		dt_tmp = Server_Licenses.DataSource(0)
		cycleend = dt_tmp.TableName(0)
		cycle = 0
ForNext3:
		If <> GetCell(Server_Licenses,cycle,"Brand"),GetCell(Server_Licenses,cycle,"Current_Brand") Call :proc_UpdLicenses
		If <> GetCell(Server_Licenses,cycle,"Type"),GetCell(Server_Licenses,cycle,"Current_Type") Call :proc_UpdLicenses
		cycle = Add(cycle,1)
		If <> cycle,cycleend Goto :ForNext3
		Protect.StoreTime(0)

	'Update Containers flash list
		rows_ContainerList = SelectRows(Server_Files.DataSource(0),"filename LIKE '%.dsc%'")
		cycle = 0
ForNext5:
		tmp_row = rows_ContainerList.(cycle)
		str_FileName = tmp_row.("filename")
		Progress $(1),"Update Container |0|",ToContainer(str_FileName),rows_ContainerList.Length(0),cycle
		dt_Container = IntSQL(str_FileName,"SELECT id, type, description, sw, hw, models, ecu, serial, link, hash, count, used FROM firmware ORDER BY id")
		srvResult = Network.Send(0)(3,"GetContainer",ToContainer(str_FileName),dt_Container)
	If == srvResult,130 Goto :lbl_AccessError
		dt_Container = 
		'Update local Container
		cycleend = dt_Container.TableName(0)
		cycle2 = 0
	If == cycle2,cycleend Goto :lbl_Next_Container
ForNext6:
		tmp_row2 = dt_Container.Rows(1,cycle2)
		Progress $(2),"Update |0| SW |1|",ToContainer(str_FileName),tmp_row2.("sw"),cycleend,cycle2
		StoreSqlRow(str_FileName,"firmware",12,"id",tmp_row2.("id"),"type",tmp_row2.("type"),"description",tmp_row2.("description"),"sw",tmp_row2.("sw"),"hw",tmp_row2.("hw"),"models",tmp_row2.("models"),"ecu",tmp_row2.("ecu"),"serial",tmp_row2.("serial"),"link",tmp_row2.("link"),"hash",tmp_row2.("hash"),"count",tmp_row2.("count"),"used",tmp_row2.("used")
		bit_Download = tmp_row2.("download")
	If == tmp_row2.("download"),False Goto :lbl_Next_Firmware
	'Download Firmware
		srvResult = Network.Send(0)(3,"GetFirmware",ToContainer(str_FileName),tmp_row2.("id"))
	If == srvResult,130 Goto :lbl_AccessError
		BytesFirmware = 
		StoreSqlRow(str_FileName,"firmware",2,"id",tmp_row2.("id"),"binary",BytesFirmware)
lbl_Next_Firmware:
		cycle2 = Add(cycle2,1)
	If <> cycle2,cycleend Goto :ForNext6
lbl_Next_Container:
		StoreSqlRow("\Data\updates.dsb","files",2,"filename" str_FileName,"hash" Protect.FileTimeHash(1,str_FileName))
		cycle = Add(cycle,1)
	If <> cycle,rows_ContainerList.Length(0) Goto :ForNext5
		Protect.StoreFileHash(1,"\Data\updates.dsb")
		Progress("Update Complete",0,0)
		Network.Close(0)
		If <> RegKey.GetValue(2,"State",-1),0 Goto :Restart
	Return
Restart:
		Protect.Update(0)
	End

'--------------------- Процедура обновления лицензии ------------------------------------------
'Server_Licenses_dt - Server licenses table for user, dt_tmp - Local license table for user
Refresh_License:
		Progress("Load User Licenses",3,1)
		'Call :DIUS4_Connect
		srvResult = Network.Send(0)(1,"LicensesInfo")
	If == srvResult,130 Goto :lbl_AccessError
		dt_lic = 
		Server_Licenses.DataSource = dt_lic
		dt_tmp = IntSQL("\Data\dius.dsb","SELECT brand.brand, access.type FROM access INNER JOIN brand ON access.id = brand.type_access")
		cycle = dt_lic.TableName(0)
'Copy Data from local license table to view user licenses
ForNext1:
		cycle = Sub(cycle,1)
		cr_tmp = SelectRows(dt_tmp,$(1),"brand LIKE '|0|'",GetCell(dt_lic,cycle,"brand"))
		'If == cr_tmp.Length(0),0 Goto lbl_ForNext1
		tmp_row1 = cr_tmp.(0)
		SetCell(Server_Licenses,cycle,"Current_Brand"),tmp_row1.("brand")
		SetCell(Server_Licenses,cycle,"Current_Type"),tmp_row1.("Type")
lbl_ForNext1:
		If <> cycle,0 Goto :ForNext1
'Server Files Load
		Progress("Load Server Files",3,2)
		srvResult = Network.Send(0)(1,"UserFilesList")
	If == srvResult,130 Goto :lbl_AccessError
		Server_Files.DataSource = 
'Load local files in tmp
		dt_tmp = IntSQL("\Data\updates.dsb","SELECT filename,version FROM files ORDER by id")
		cycleend = dt_tmp.TableName(0)
		cycle = 0
'Copy Data from local files table tmp to view user files "Server_Files"
ForNext2:
		GetCell(dt_tmp,cycle,"version")
		SetCell(Server_Files,cycle,"Current_Version")
		cycle = Add(cycle,1)
		If <> cycle,cycleend Goto :ForNext2
		Progress("Press Update for Update List Firmwares . . .",0,0)
	Return

'------------------- Procedure Update Licenses ------------------------------------------------
proc_UpdLicenses:
		dt_tmp = IntSQL("\Data\dius.dsb",$(1),"SELECT id FROM access WHERE type = '|0|'",GetCell(Server_Licenses,cycle,"Type"))
		index = GetCell(dt_tmp,0,"id")
		StoreSqlRow("\Data\dius.dsb","brand",3 "brand" GetCell(Server_Licenses,cycle,"Brand") "access",True "type_access",index)
		GetCell(Server_Licenses,cycle,"Brand")
		SetCell(Server_Licenses,cycle,"Current_Brand")
		GetCell(Server_Licenses,cycle,"Type")
		SetCell(Server_Licenses,cycle,"Current_Type")
	Return

'------------------Procedure Update Service Files -------------------------------------------
proc_UpdServiceFiles:
		str_tmp = GetCell(Server_Files,cycle,"filename")
		Progress(str_tmp,3,1)
		srvResult = Network.Send(0)(2,"GetUserFile",str_tmp)
	If == srvResult,130 Goto :lbl_AccessError
		dt_tmp = 
		If == dt_tmp.TableName(0),0 Return
		bytes_Array = GetCell(dt_tmp,0,"binary")
		FileSave(str_tmp,bytes_Array)
		SetCell(Server_Files,cycle,"Current_Version"),GetCell(Server_Files,cycle,"version")
		StoreSqlRow("\Data\updates.dsb","files",5,"id" GetCell(Server_Files,cycle,"id"),"filename" GetCell(Server_Files,cycle,"filename"),"description" GetCell(Server_Files,cycle,"description"),"version" GetCell(Server_Files,cycle,"Version"),"hash" Protect.FileTimeHash(1,GetCell(Server_Files,cycle,"filename")))
		If <> str_tmp,"\DIUS 4.0.4.tmp" Return
		RegKey.SetValue(2,"State",-1)
	Return

'-------------------------- Proc DIUS4_Connect -----------------------------------------------------
DIUS4_Connect:
'If no have access, Hash Error
		srvResult = Network.Connect(0)
		msgRestart = "No Internet Connection with Server."
		If == srvResult,127 Goto :Out_MessageRestart
		srvResult = Network.Send(0)(3,Email,Protect.GetCompInfo(0),"dius4")
	Return
Out_MessageRestart:
		Network.Close(0)
		msgResult = MsgBox(0,$(1),"|0| Internet Connect Error. Press F12 For Restart",msgRestart,"Warning",0,3)
	Exit
'---------------------------------------------------------------------------------------------------
'Нет доступа к функции
Proc_AccessError:
		Network.Close(0)
		bool_New_User = True
		bool_Repair_User = True
		msgResult = MsgBox(0,"No Have Access For This Function","No Access",0,2)
	Return

'Нет доступа к функции
lbl_AccessError:
		Network.Close(0)
		bool_New_User = true
		bool_Repair_User = True
		msgResult = MsgBox(0,"Please connect with Dealer for Restore License","No Access",0,2)
		'Call :DIUS4_Connect
	Exit

----------------------------------------------------------------------------------------------------------------
RESET:
		Progress("Reset . . .",0,0)
		Network.Close(0)
	End
