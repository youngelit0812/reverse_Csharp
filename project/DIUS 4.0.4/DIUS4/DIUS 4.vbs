'Starting Script for DIUS4.0 "DIUS_4.vbs"
'Writen Chubkoff Alex
'Copiright "Magellan Group" 2018

'------------------------------------------------------------------------------
	x = 2
	y = 220
		Progress("Please Select Function",0,0)
		NewPage(pnlSelector,"Select . . .")
lbl_AfterRegister:
'If no have access to Internet
		srvResult = Network.Connect(0)
		If == srvResult,127 Goto :lbl_OffLine_Mode

		srvResult = Network.Send(0)(3,RegKey.GetValue(2,"Email",""),Protect.GetCompInfo(0),"dius4")
		'If New User
		If == srvResult,128 Goto :Create_NewUser
		'If Repair user
		If == srvResult,129 Goto :Create_NewUser

		NewButton(pnlSelector,x,2,"DIUS Tools","btn_DIUS_Tools","dius_tools")
		x = Add(x,210)

'Проверка на админа
		srvResult = Network.Send(0)(4,"AdminAccess","dius4","","Admin")
		If == False, Goto :lbl_OffLine_Mode

'Проверка доступа к таблице dealer база dius4
		srvResult = Network.Send(0)(4,"AdminAccess","dius4","dealer","Read")
		If <> True, Goto :lbl_Dius_DIUS4_Files
		NewButton(pnlSelector,x,2,"Admin Control","btn_Admin_Access","access")
		x = Add(x,210)

'Проверка доступа к таблице files2 база dius4
lbl_Dius_DIUS4_Files:
		srvResult = Network.Send(0)(4,"AdminAccess","dius4","files2","Read")
		If <> True, Goto :lbl_Dius_DIUS4_Firmware
		NewButton(pnlSelector,x,2,"DIUS4 Files","btn_Dius4_Files","dius_files")
		x = Add(x,210)

'проверка доступа к dius4 firmware
lbl_Dius_DIUS4_Firmware:
		srvResult = Network.Send(0)(4,"AdminAccess","dius4","containers","Read")
		If <> True, Goto :lbl_Dius_DIUS_Users
		NewButton(pnlSelector,x,2,"DIUS4 Firmware","btn_DIUS4_Firmware","firmware")
		x = Add(x,210)

'Проверка доступа к списку пользователей
lbl_Dius_DIUS_Users:
		srvResult = Network.Send(0)(4,"AdminAccess","dius4","users","Read")
		If <> True, Goto :lbl_Dius_ULA_Firmware
		NewButton(pnlSelector,x,2,"DIUS4 Users","btn_DIUS_Users","users")
		x = Add(x,210)

'Проверка доступа к списку прошивок Lambda
lbl_Dius_ULA_Firmware:
		srvResult = Network.Send(0)(4,"AdminAccess","lambda","firmware","Read")
		If <> True, Goto :lbl_Dius_ULA_List
		NewButton(pnlSelector,x,2,"ULA Firmware","btn_ULA_Firmware","ula_firmware")
		x = Add(x,210)

'Проверка доступа к списку Устройств Lambda
lbl_Dius_ULA_List:
		srvResult = Network.Send(0)(4,"AdminAccess","lambda","list","Read")
		If <> True, Goto :lbl_Dius_BUDS2
		NewButton(pnlSelector,x,2,"ULA Devices","btn_ULA_Devices","ula_device")
		x = Add(x,210)

'Проверка доступа к BUDS2
lbl_Dius_BUDS2:
		srvResult = Network.Send(0)(4,"AdminAccess","buds2","buds2","Read")
		If <> True, Goto :lbl_OffLine_Mode
		NewButton(pnlSelector,x,2,"BUDS2","btn_BUDS2","buds2")

'Проверка доступа к лицензиям
lbl_OffLine_Mode:
		Network.Close(0)
		If == Protect.VerifyTime(0),True Goto :lbl_Dius_Ok
		msgResult = MsgBox(0,"Please Update License in DIUS Tools","Timeout License",0,2)
		Exit

'Выводим кнопки согласно лицензий
lbl_Dius_Ok:
		x = 2
		'Test Access BRP
		UsrAcc = IntSQL("\Data\dius.dsb","SELECT type_access FROM brand WHERE brand = 'BRP'")
		If < GetCell(UsrAcc,0,type_access),1 Goto :lbl_M3C
		NewButton(pnlSelector,x,y,"VDO / BOSCH","btn_BRP","BRP")
		x = Add(x,210)
		'Test Access M3C
lbl_M3C:
		UsrAcc = IntSQL("\Data\dius.dsb","SELECT type_access FROM brand WHERE brand = 'M3C'")
		If < GetCell(UsrAcc,0,type_access),1 Goto :lbl_UlaTest
		NewButton(pnlSelector,x,y,"M3C","btn_M3C","m3c")
		x = Add(x,210)
		'Test Access ULA
lbl_UlaTest:
		UsrAcc = IntSQL("\Data\dius.dsb","SELECT type_access FROM brand WHERE brand = 'ULA'")
		If < GetCell(UsrAcc,0,type_access),1 Goto :lbl_End
		NewButton(pnlSelector,x,y,"Log Analyzer","btn_Ula_Analyzer","ula")
		x = Add(x,210)

lbl_End:
	Exit
'------------------------------------- Buttons Event ------------------------------------------
btn_DIUS_Tools:
		Execute(True,"DIUS 4.0.4.exe","Internal DIUS_Tools.vbs")
		If == RegKey.GetValue(2,"State","-1"),-1 Goto :RESET
		Progress("Please Select Function",0,0)
	Return

btn_Admin_Access:
		Execute(False,"DIUS 4.0.4.exe","Base Admin_Control.vbs")
	Return

btn_Dius4_Files:
		Execute(False,"DIUS 4.0.4.exe","Base DIUS4_Files.vbs")
	Return

btn_DIUS4_Firmware:
		Execute(False,"DIUS 4.0.4.exe","Base DIUS4_Firmware.vbs")
	Return

btn_DIUS_Users:
		Execute(False,"DIUS 4.0.4.exe","Base DIUS4_Users.vbs")
	Return

btn_ULA_Firmware:
		Execute(False,"DIUS 4.0.4.exe","Base ULA_Firmware.vbs")
	Return

btn_ULA_Devices:
		Execute(False,"DIUS 4.0.4.exe","Base ULA_Devices.vbs")
	Return

btn_BUDS2:
		Execute(False,"DIUS 4.0.4.exe","Base BUDS2.vbs")
	Return

btn_BRP:
		Execute(True,"DIUS 4.0.4.exe","Base BRP.vbs")
	Return
btn_Ula_Analyzer:
		Execute(False,"DIUS 4.0.4.exe","Base ULA_Analyzer.vbs")
	Return
btn_M3C:
		Execute(True,"DIUS 4.0.4.exe","Base M3C.vbs")
	Return

Create_NewUser:
		Network.Close(0)
		Execute(True,"DIUS 4.0.4.exe","Internal DIUS_Tools.vbs")
		'If == RegKey.GetValue(2,"Email",""),"" Goto :RESET
		'If == RegKey.GetValue(2,"State","-1"),-1 Goto :RESET
		Goto :RESET

----------------------------------------------------------------------------------------------------------------
RESET:
		Progress("Reset . . .",0,0)
	End
