using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using DIUS4.My.Resources;
using Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace DIUS4
{
	// Token: 0x02000019 RID: 25
	public class Functions
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00009C34 File Offset: 0x00007E34
		public Functions(DiusParser Obj)
		{
			this.F = new Dictionary<string, string>();
			this._sql = new SqlFunc();
			RadMessageBox.ShowInTaskbar = true;
			RadMessageBox.ThemeName = "Office2010Black";
			this.dp = Obj;
			this.F.Add("Add", "Add");
			this.F.Add("Sub", "Sbb");
			this.F.Add("Mul", "Mul");
			this.F.Add("Div", "Div");
			this.F.Add("UDiv", "UDiv");
			this.F.Add("$", "ReplaceOnValues");
			this.F.Add("ToContainer", "ToContainer");
			this.F.Add("ToShort", "ToShort");
			this.F.Add("ToMoney", "ToMoney");
			this.F.Add("ToMoney3", "ToMoney3");
			this.F.Add("ToDataRow", "ToDataRow");
			this.F.Add("DateNow", "DateNow");
			this.F.Add("DateNow_Short", "DateNow_Short");
			this.F.Add("Progress", "Progress");
			this.F.Add("MsgBox", "Message");
			this.F.Add("Execute", "Execute");
			this.F.Add("Version", "Version");
			this.F.Add("FileSave", "FileSave");
			this.F.Add("FileLoad", "FileLoad");
			this.F.Add("NewPage", "NewPage");
			this.F.Add("NewButton", "NewButton");
			this.F.Add("NewView", "NewView");
			this.F.Add("NewVCI", "NewVCI");
			this.F.Add("GetCell", "GetCell");
			this.F.Add("SetCell", "SetCell");
			this.F.Add("SetPoint", "SetPoint");
			this.F.Add("AddEvent", "AddEvent");
			this.F.Add("AddGroup", "AddGroup");
			this.F.Add("AddFilter", "AddFilter");
			this.F.Add("AddInfo", "AddInfo");
			this.F.Add("AddSelect", "AddSelect");
			this.F.Add("AddEdit", "AddEdit");
			this.F.Add("AddCheck", "AddCheck");
			this.F.Add("AddRow", "AddRow");
			this.F.Add("SelectRow", "SelectRow");
			this.F.Add("IntSQL", "IntSQL");
			this.F.Add("StoreSqlRow", "StoreSqlRow");
			this.F.Add("StoreSqlAllRow", "StoreSqlAllRow");
			this.F.Add("StoreSqlTable", "StoreSqlTable");
			this.F.Add("SelectRows", "SelectRows");
			this.F.Add("GetChanges", "GetChanges");
			this.F.Add("DOC_Open", "DOC_Open");
			this.F.Add("DOC_Replace", "DOC_Replace");
			this.F.Add("DOC_Print", "DOC_Print");
			this.F.Add("DOC_Close", "DOC_Close");
			this.F.Add("SaveXLS", "SaveXLS");
			this.F.Add("NewByteArray", "NewByteArray");
			this.F.Add("CmdRD", "CmdRD");
			this.s = this.dp.S.Stack;
			this.State = this.dp.S.State;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000A090 File Offset: 0x00008290
		public void Add()
		{
			string text = Conversions.ToString(this.s.Pop());
			string text2 = Conversions.ToString(this.s.Pop());
			if (Versioned.IsNumeric(text) & Versioned.IsNumeric(text2))
			{
				this.s.Push(Conversions.ToString(Conversions.ToDouble(text) + Conversions.ToDouble(text2)));
				return;
			}
			this.s.Push(text + text2);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000A100 File Offset: 0x00008300
		public void Sbb()
		{
			string text = Conversions.ToString(this.s.Pop());
			string text2 = Conversions.ToString(this.s.Pop());
			if (Versioned.IsNumeric(text) & Versioned.IsNumeric(text2))
			{
				this.s.Push(Conversions.ToString(Conversions.ToDouble(text) - Conversions.ToDouble(text2)));
				return;
			}
			this.State.Err = -1;
			this.State.Message = string.Format("{0} - {1} Error!", text, text2);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A180 File Offset: 0x00008380
		public void Mul()
		{
			string text = Conversions.ToString(this.s.Pop());
			string text2 = Conversions.ToString(this.s.Pop());
			if (Versioned.IsNumeric(text) & Versioned.IsNumeric(text2))
			{
				this.s.Push(Conversions.ToString(Conversions.ToDouble(text) * Conversions.ToDouble(text2)));
				return;
			}
			this.State.Err = -1;
			this.State.Message = string.Format("{0} * {1} Error!", text, text2);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A200 File Offset: 0x00008400
		public void Div()
		{
			string text = Conversions.ToString(this.s.Pop());
			string text2 = Conversions.ToString(this.s.Pop());
			if (!(Versioned.IsNumeric(text) & Versioned.IsNumeric(text2)))
			{
				this.State.Err = -1;
				this.State.Message = string.Format("{0} : {1} Error!", text, text2);
				return;
			}
			if (Conversions.ToDouble(text2) == 0.0)
			{
				this.s.Push("0");
				return;
			}
			this.s.Push(Conversions.ToString(Conversions.ToDouble(text) / Conversions.ToDouble(text2)));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public void UDiv()
		{
			string text = Conversions.ToString(this.s.Pop());
			string text2 = Conversions.ToString(this.s.Pop());
			if (!(Versioned.IsNumeric(text) & Versioned.IsNumeric(text2)))
			{
				this.State.Err = -1;
				this.State.Message = string.Format("{0} : {1} Error!", text, text2);
				return;
			}
			if (Conversions.ToDouble(text2) == 0.0)
			{
				this.s.Push("0");
				return;
			}
			this.s.Push(Conversions.ToString((long)Math.Round(Conversions.ToDouble(text)) / (long)Math.Round(Conversions.ToDouble(text2))));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A354 File Offset: 0x00008554
		public void ToContainer()
		{
			string[] array = this.s.Pop().ToString().Split(new char[]
			{
				'.'
			})[0].Split(new char[]
			{
				'\\'
			});
			this.s.Push(array[array.Length - 1]);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A3A8 File Offset: 0x000085A8
		public void ToShort()
		{
			string[] array = this.s.Pop().ToString().Split(new char[]
			{
				'\\'
			});
			this.s.Push(array[array.Length - 1]);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A3E8 File Offset: 0x000085E8
		public void ToMoney()
		{
			this.s.Push(Conversions.ToDecimal(this.s.Pop()).ToString("F2"));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A420 File Offset: 0x00008620
		public void ToMoney3()
		{
			this.s.Push(Conversions.ToDecimal(this.s.Pop()).ToString("F3"));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A458 File Offset: 0x00008658
		public void ToDataRow()
		{
			DataRowView dataRowView = (DataRowView)((GridViewDataRowInfo)this.s.Pop()).DataBoundItem;
			this.s.Push(dataRowView.Row);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000A491 File Offset: 0x00008691
		public void DateNow()
		{
			this.s.Push(DateTime.Now);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public void DateNow_Short()
		{
			this.s.Push(DateTime.Now.Date.ToShortDateString());
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A4D8 File Offset: 0x000086D8
		public void NewPage()
		{
			if (this.s.Peek() is string)
			{
				RadPageViewPage radPageViewPage = new RadPageViewPage
				{
					Name = Conversions.ToString(this.s.Pop()),
					AutoScroll = true,
					Dock = DockStyle.Fill,
					Size = this.dp._Parent.Panel.Size,
					Text = Conversions.ToString(this.s.Pop())
				};
				this.dp._Parent.Panel.Pages.Add(radPageViewPage);
				this.dp.S.V.Add(radPageViewPage.Name, radPageViewPage);
				return;
			}
			this.dp._Parent.Panel.Pages.Add((RadPageViewPage)this.s.Pop());
			this.s.Pop();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000A5C4 File Offset: 0x000087C4
		public void NewButton()
		{
			if (this.s.Peek() is RadPageViewPage)
			{
				RadPageViewPage radPageViewPage = (RadPageViewPage)this.s.Pop();
				RadButton radButton = new RadButton();
				radButton.Left = Conversions.ToInteger(this.s.Pop());
				radButton.Top = Conversions.ToInteger(this.s.Pop());
				radButton.Text = Conversions.ToString(this.s.Pop());
				radButton.Tag = RuntimeHelpers.GetObjectValue(this.s.Pop());
				radButton.Name = Conversions.ToString(radButton.Tag);
				radButton.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
				radButton.ForeColor = Color.DarkRed;
				radButton.AutoSize = true;
				radButton.ThemeName = "Office2010Black";
				RadButton radButton2 = radButton;
				string text = Conversions.ToString(this.s.Pop());
				if (Operators.CompareString(text, "", false) != 0)
				{
					if (Operators.CompareString(text, "Binary", false) == 0)
					{
						radButton2.Image = Image.FromStream(new MemoryStream((byte[])this.s.Pop()));
						radButton2.TextAlignment = ContentAlignment.TopCenter;
						radButton2.ForeColor = Color.White;
						object tag = radPageViewPage.Tag;
						Point point = (tag != null) ? ((Point)tag) : default(Point);
						point.X = Conversions.ToInteger(Operators.AddObject(point.X, this.s.Pop()));
						point.Y = Conversions.ToInteger(Operators.AddObject(point.Y, this.s.Pop()));
						radPageViewPage.Tag = point;
					}
					else
					{
						radButton2.Image = (Image)Resources.ResourceManager.GetObject(text);
						radButton2.TextAlignment = ContentAlignment.TopCenter;
						radButton2.ForeColor = Color.White;
					}
				}
				if (this.dp.S.V.ContainsKey(radButton2.Name))
				{
					this.dp.S.V[radButton2.Name] = radButton2;
				}
				else
				{
					radPageViewPage.Controls.Add(radButton2);
					this.dp.S.V.Add(radButton2.Name, radButton2);
				}
				radButton2.Click += this.dp.Event_Distributor;
				return;
			}
			Control control = (RadGridView)this.s.Pop();
			RadButton radButton3 = new RadButton();
			radButton3.Left = Conversions.ToInteger(this.s.Pop());
			radButton3.Top = Conversions.ToInteger(this.s.Pop());
			radButton3.Text = Conversions.ToString(this.s.Pop());
			radButton3.Name = radButton3.Text.Replace(" ", "_");
			radButton3.Tag = RuntimeHelpers.GetObjectValue(this.s.Pop());
			radButton3.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			radButton3.ForeColor = Color.Blue;
			radButton3.AutoSize = true;
			radButton3.ThemeName = "Office2010Black";
			RadButton radButton4 = radButton3;
			control.Controls.Add(radButton4);
			radButton4.Click += this.dp.Event_Distributor;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public void NewVCI()
		{
			RadPageViewPage radPageViewPage = (RadPageViewPage)this.s.Pop();
			VCI vci = (VCI)this.s.Pop();
			vci.Left = Conversions.ToInteger(this.s.Pop());
			vci.Top = Conversions.ToInteger(this.s.Pop());
			object tag = radPageViewPage.Tag;
			Point point = (tag != null) ? ((Point)tag) : default(Point);
			point.X = Conversions.ToInteger(Operators.AddObject(point.X, this.s.Pop()));
			point.Y = Conversions.ToInteger(Operators.AddObject(point.Y, this.s.Pop()));
			radPageViewPage.Tag = point;
			radPageViewPage.Controls.Add(vci);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public void Progress()
		{
			this.dp.Progress.Text = Conversions.ToString(this.s.Pop());
			string text = Conversions.ToString(this.s.Pop());
			string text2 = Conversions.ToString(this.s.Pop());
			if (Versioned.IsNumeric(text))
			{
				this.dp.Progress.Maximum = Conversions.ToInteger(text);
			}
			if (Versioned.IsNumeric(text2))
			{
				this.dp.Progress.Value1 = Conversions.ToInteger(text2);
			}
			System.Windows.Forms.Application.DoEvents();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000AA68 File Offset: 0x00008C68
		public void SetPoint()
		{
			((RadPageViewPage)this.s.Pop()).Tag = new Point(Conversions.ToInteger(this.s.Pop()), Conversions.ToInteger(this.s.Pop()));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		public void NewView()
		{
			RadPageViewPage radPageViewPage = (RadPageViewPage)this.s.Pop();
			RadGridView radGridView = new RadGridView();
			radGridView.Name = Conversions.ToString(this.s.Pop());
			radGridView.Size = (Size)new Point(Conversions.ToInteger(this.s.Pop()), Conversions.ToInteger(this.s.Pop()));
			radGridView.AllowAddNewRow = false;
			radGridView.AllowDeleteRow = false;
			radGridView.AllowDragToGroup = true;
			radGridView.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
			radGridView.AutoSizeRows = false;
			radGridView.AllowRowResize = false;
			radGridView.AllowAutoSizeColumns = true;
			radGridView.AllowColumnResize = true;
			radGridView.ForeColor = Color.Black;
			radGridView.Text = radGridView.Name.Replace('_', ' ');
			radGridView.ShowRowHeaderColumn = false;
			radGridView.AutoExpandGroups = false;
			radGridView.AddNewRowPosition = SystemRowPosition.Top;
			radGridView.NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell;
			radGridView.ThemeName = "Office2010Black";
			RadGridView radGridView2 = radGridView;
			if (radGridView2.Width > 0)
			{
				RadLabel value = new RadLabel
				{
					Font = new Font("Segoe UI", 11f, FontStyle.Bold),
					ForeColor = Color.Blue,
					Text = radGridView2.Text,
					AutoSize = false,
					TextAlignment = ContentAlignment.TopCenter,
					Width = radGridView2.Width - 2,
					Height = 22,
					Top = 1,
					Left = 1
				};
				radGridView2.Controls.Add(value);
			}
			if (this.$STATIC$NewView$2001$_Y$Init == null)
			{
				Interlocked.CompareExchange<StaticLocalInitFlag>(ref this.$STATIC$NewView$2001$_Y$Init, new StaticLocalInitFlag(), null);
			}
			bool flag = false;
			try
			{
				Monitor.Enter(this.$STATIC$NewView$2001$_Y$Init, ref flag);
				if (this.$STATIC$NewView$2001$_Y$Init.State == 0)
				{
					this.$STATIC$NewView$2001$_Y$Init.State = 2;
					this.$STATIC$NewView$2001$_Y = 40;
				}
				else if (this.$STATIC$NewView$2001$_Y$Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				this.$STATIC$NewView$2001$_Y$Init.State = 1;
				if (flag)
				{
					Monitor.Exit(this.$STATIC$NewView$2001$_Y$Init);
				}
			}
			if (radPageViewPage.Tag == null)
			{
				if (radGridView2.Height < 1)
				{
					radGridView2.Height = radPageViewPage.Height - 92 + radGridView2.Height;
				}
				if (radGridView2.Width < 1)
				{
					radGridView2.Width = radPageViewPage.Width - 22 + radGridView2.Width;
				}
				radPageViewPage.Tag = new Point(radGridView2.Width, 40);
				radGridView2.Left = 0;
				radGridView2.Top = 40;
				this.$STATIC$NewView$2001$_Y = radGridView2.Height + 40;
			}
			else
			{
				object tag = radPageViewPage.Tag;
				Point point = (tag != null) ? ((Point)tag) : default(Point);
				if (radGridView2.Height < 1)
				{
					radGridView2.Height = radPageViewPage.Height - this.$STATIC$NewView$2001$_Y - 12 + radGridView2.Height;
				}
				if (radGridView2.Width < 1)
				{
					radGridView2.Width = radPageViewPage.Width - 22 + radGridView2.Width;
				}
				if (radPageViewPage.Width - point.X + 2 > radGridView2.Width)
				{
					radGridView2.Left = point.X + 2;
					radGridView2.Top = point.Y;
					point.X = point.X + 2 + radGridView2.Width;
					if (this.$STATIC$NewView$2001$_Y < radGridView2.Height + radGridView2.Top)
					{
						this.$STATIC$NewView$2001$_Y = radGridView2.Height + radGridView2.Top;
					}
				}
				else
				{
					radGridView2.Left = 0;
					radGridView2.Top = this.$STATIC$NewView$2001$_Y + 2;
					point.X = radGridView2.Width;
					point.Y = this.$STATIC$NewView$2001$_Y + 2;
					this.$STATIC$NewView$2001$_Y = point.Y + radGridView2.Height;
				}
				radPageViewPage.Tag = point;
			}
			radPageViewPage.Controls.Add(radGridView2);
			if (this.dp.S.V.ContainsKey(radGridView2.Name))
			{
				this.dp.S.V[radGridView2.Name] = radGridView2;
				return;
			}
			this.dp.S.V.Add(radGridView2.Name, radGridView2);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public void AddInfo()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			GridViewTextBoxColumn gridViewTextBoxColumn = new GridViewTextBoxColumn();
			gridViewTextBoxColumn.Name = Conversions.ToString(this.s.Pop());
			gridViewTextBoxColumn.FieldName = gridViewTextBoxColumn.Name;
			gridViewTextBoxColumn.HeaderText = gridViewTextBoxColumn.FieldName.ToString().Replace("_", " ");
			gridViewTextBoxColumn.Width = Conversions.ToInteger(this.s.Pop());
			gridViewTextBoxColumn.ReadOnly = true;
			gridViewTextBoxColumn.IsVisible = false;
			gridViewTextBoxColumn.WrapText = true;
			GridViewTextBoxColumn gridViewTextBoxColumn2 = gridViewTextBoxColumn;
			if (gridViewTextBoxColumn2.Width > 0)
			{
				gridViewTextBoxColumn2.IsVisible = true;
			}
			radGridView.Columns.Add(gridViewTextBoxColumn2);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000AF6C File Offset: 0x0000916C
		public void IntSQL()
		{
			string text = Conversions.ToString(this.s.Pop());
			string sqlCommand = Conversions.ToString(this.s.Pop());
			if (text.Contains("."))
			{
				this.s.Push(this._sql.Get_DataTable(sqlCommand, text));
				return;
			}
			this.s.Push(this._sql.Get_ExtDataTable(sqlCommand, text));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000AFDC File Offset: 0x000091DC
		public void AddEvent()
		{
			object objectValue = RuntimeHelpers.GetObjectValue(this.s.Pop());
			string left = Conversions.ToString(this.s.Pop());
			string text = Conversions.ToString(this.s.Pop());
			if (Operators.CompareString(text, "", false) != 0)
			{
				NewLateBinding.LateSet(objectValue, null, "tag", new object[]
				{
					text
				}, null, null);
			}
			if (Operators.CompareString(left, "SelectRow", false) == 0)
			{
				((RadGridView)objectValue).CurrentRowChanged += new CurrentRowChangedEventHandler(this.dp.Event_Distributor);
				return;
			}
			if (Operators.CompareString(left, "CellEndEdit", false) != 0)
			{
				return;
			}
			((RadGridView)objectValue).CellEndEdit += new GridViewCellEventHandler(this.dp.Event_Distributor);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000B098 File Offset: 0x00009298
		public void GetCell()
		{
			if (this.s.Peek() is RadGridView)
			{
				this.s.Push(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(NewLateBinding.LateGet(((RadGridView)this.s.Pop()).Rows[Conversions.ToInteger(this.s.Pop())].Cells, null, "Item", new object[]
				{
					this.s.Pop()
				}, null, null, null), null, "Value", new object[0], null, null, null)));
				return;
			}
			if (this.s.Peek() is GridViewDataRowInfo)
			{
				GridViewCellInfo gridViewCellInfo = (GridViewCellInfo)NewLateBinding.LateGet(((GridViewDataRowInfo)this.s.Pop()).Cells, null, "Item", new object[]
				{
					this.s.Pop()
				}, null, null, null);
				this.s.Push(RuntimeHelpers.GetObjectValue(gridViewCellInfo.Value));
				return;
			}
			if (this.s.Peek() is DataTable)
			{
				this.s.Push(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(((DataTable)this.s.Pop()).Rows[Conversions.ToInteger(this.s.Pop())], null, "Item", new object[]
				{
					this.s.Pop()
				}, null, null, null)));
				return;
			}
			if (this.s.Peek() is DataRow)
			{
				this.s.Push(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet((DataRow)this.s.Pop(), null, "Item", new object[]
				{
					this.s.Pop()
				}, null, null, null)));
				return;
			}
			this.State.Err = -1;
			this.State.Message = string.Format("Get Cell Value Error in {0}", this.dp.S.Line);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000B290 File Offset: 0x00009490
		public void SetCell()
		{
			if (this.s.Peek() is RadGridView)
			{
				NewLateBinding.LateSetComplex(NewLateBinding.LateGet(((RadGridView)this.s.Pop()).Rows[Conversions.ToInteger(this.s.Pop())].Cells, null, "Item", new object[]
				{
					this.s.Pop()
				}, null, null, null), null, "Value", new object[]
				{
					this.s.Pop()
				}, null, null, false, true);
				return;
			}
			if (this.s.Peek() is GridViewDataRowInfo)
			{
				NewLateBinding.LateSetComplex(NewLateBinding.LateGet(((GridViewDataRowInfo)this.s.Pop()).Cells, null, "Item", new object[]
				{
					this.s.Pop()
				}, null, null, null), null, "Value", new object[]
				{
					this.s.Pop()
				}, null, null, false, true);
				return;
			}
			if (this.s.Peek() is DataTable)
			{
				NewLateBinding.LateSetComplex(((DataTable)this.s.Pop()).Rows[Conversions.ToInteger(this.s.Pop())], null, "Item", new object[]
				{
					this.s.Pop(),
					this.s.Pop()
				}, null, null, false, true);
				return;
			}
			if (this.s.Peek() is DataRow)
			{
				NewLateBinding.LateSetComplex((DataRow)this.s.Pop(), null, "Item", new object[]
				{
					this.s.Pop(),
					this.s.Pop()
				}, null, null, false, true);
				return;
			}
			this.State.Err = -1;
			this.State.Message = string.Format("Set Cell Value Error in {0}", this.dp.S.Line);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000B48C File Offset: 0x0000968C
		public void AddGroup()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			string text = Conversions.ToString(this.s.Pop());
			if (Operators.CompareString(text, "", false) == 0)
			{
				radGridView.GroupDescriptors.Clear();
				return;
			}
			radGridView.GroupDescriptors.Add(new GridGroupByExpression(string.Format("{0} GroupBy {1}", text, text)));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public void AddFilter()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			string text = Conversions.ToString(this.s.Pop());
			if (Operators.CompareString(text, "", false) == 0)
			{
				radGridView.EnableFiltering = false;
				radGridView.FilterDescriptors.Clear();
				return;
			}
			radGridView.FilterDescriptors.Add(text, (FilterOperator)Conversions.ToInteger(this.s.Pop()), RuntimeHelpers.GetObjectValue(this.s.Pop()));
			radGridView.ShowFilteringRow = false;
			radGridView.EnableFiltering = true;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000B580 File Offset: 0x00009780
		public void Message()
		{
			int num = Conversions.ToInteger(this.s.Pop());
			string text = Conversions.ToString(this.s.Pop());
			if (num != 0)
			{
				int num2 = num - 1;
				for (int i = 0; i <= num2; i++)
				{
					text.Replace(string.Format("{0}", i), Conversions.ToString(this.s.Pop()));
				}
			}
			this.s.Push(RadMessageBox.Show(text, Conversions.ToString(this.s.Pop()), (MessageBoxButtons)Conversions.ToInteger(this.s.Pop()), (RadMessageIcon)Conversions.ToInteger(this.s.Pop())));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000B630 File Offset: 0x00009830
		public void Execute()
		{
			if (Operators.ConditionalCompareObjectEqual(this.s.Pop(), true, false))
			{
				this.dp._Parent.Hide();
				using (Process process = Process.Start(Conversions.ToString(this.s.Pop()), Conversions.ToString(this.s.Pop())))
				{
					process.WaitForExit();
				}
				this.dp._Parent.TitleMail.Text = Conversions.ToString(this.dp._Parent.regKey.GetValue("Email", "Not Registered"));
				this.dp._Parent.Show();
				return;
			}
			Process.Start(Conversions.ToString(this.s.Pop()), Conversions.ToString(this.s.Pop()));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000B724 File Offset: 0x00009924
		public void Version()
		{
			string text = Conversions.ToString(this.s.Pop());
			if (Operators.CompareString(text, "", false) == 0)
			{
				this.s.Push(0);
				return;
			}
			this.s.Push(Conversion.Val(string.Format("&H{0}", text.Replace(".", ""))));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000B794 File Offset: 0x00009994
		public void FileSave()
		{
			string text = Conversions.ToString(this.s.Pop());
			using (FileStream fileStream = new FileStream(System.Windows.Forms.Application.StartupPath + text, FileMode.Create))
			{
				byte[] array = (byte[])this.s.Pop();
				fileStream.Write(array, 0, array.Length);
			}
			if (text.Contains(".dsb"))
			{
				this._sql.SetPassword(text);
				return;
			}
			if (text.Contains(".dss"))
			{
				this._sql.SetPassword(text);
				return;
			}
			if (text.Contains(".dsc"))
			{
				this._sql.SetPassword(text);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000B84C File Offset: 0x00009A4C
		public void StoreSqlRow()
		{
			try
			{
				string text = Conversions.ToString(this.s.Pop());
				string tableName = Conversions.ToString(this.s.Pop());
				int num = Conversions.ToInteger(this.s.Pop()) - 1;
				string[] array = new string[num + 1];
				object[] array2 = new object[num + 1];
				int num2 = num;
				for (int i = 0; i <= num2; i++)
				{
					array[i] = Conversions.ToString(this.s.Pop());
					array2[i] = RuntimeHelpers.GetObjectValue(this.s.Pop());
				}
				if (text.Contains("."))
				{
					this._sql.StoreSqlRow(text, tableName, array, array2);
				}
				else
				{
					this._sql.StoreExtSqlRow(text, tableName, array, array2);
				}
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(ex.Message, MsgBoxStyle.OkOnly, null);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000B938 File Offset: 0x00009B38
		public void StoreSqlAllRow()
		{
			string text = Conversions.ToString(this.s.Pop());
			string tableName = Conversions.ToString(this.s.Pop());
			string[] array;
			object[] array2;
			if (this.s.Peek() is GridViewDataRowInfo)
			{
				GridViewDataRowInfo gridViewDataRowInfo = (GridViewDataRowInfo)this.s.Pop();
				array = new string[gridViewDataRowInfo.Cells.Count - 1 + 1];
				array2 = new object[gridViewDataRowInfo.Cells.Count - 1 + 1];
				int num = gridViewDataRowInfo.Cells.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					array[i] = gridViewDataRowInfo.Cells[i].ColumnInfo.FieldName;
					array2[i] = RuntimeHelpers.GetObjectValue(gridViewDataRowInfo.Cells[i].Value);
				}
			}
			else
			{
				DataRow dataRow = (DataRow)this.s.Pop();
				array = new string[dataRow.Table.Columns.Count - 1 + 1];
				array2 = new object[dataRow.Table.Columns.Count - 1 + 1];
				int num2 = dataRow.Table.Columns.Count - 1;
				for (int j = 0; j <= num2; j++)
				{
					array[j] = dataRow.Table.Columns[j].ColumnName;
					array2[j] = RuntimeHelpers.GetObjectValue(dataRow[j]);
				}
			}
			if (text.Contains("."))
			{
				this._sql.StoreSqlRow(text, tableName, array, array2);
				return;
			}
			this._sql.StoreExtSqlRow(text, tableName, array, array2);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public void StoreSqlTable()
		{
			this._sql.StoreDataTable(Conversions.ToString(this.s.Pop()), Conversions.ToString(this.s.Pop()), (DataTable)this.s.Pop());
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000BB20 File Offset: 0x00009D20
		public void SelectRows()
		{
			DataTable dataTable = (DataTable)this.s.Pop();
			string filterExpression = Conversions.ToString(this.s.Pop());
			this.s.Push(dataTable.Select(filterExpression));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000BB64 File Offset: 0x00009D64
		public void AddSelect()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			GridViewComboBoxColumn gridViewComboBoxColumn = new GridViewComboBoxColumn();
			gridViewComboBoxColumn.Name = Conversions.ToString(this.s.Pop());
			gridViewComboBoxColumn.FieldName = gridViewComboBoxColumn.Name;
			gridViewComboBoxColumn.HeaderText = gridViewComboBoxColumn.FieldName.ToString().Replace("_", " ");
			gridViewComboBoxColumn.Width = Conversions.ToInteger(this.s.Pop());
			gridViewComboBoxColumn.WrapText = true;
			gridViewComboBoxColumn.Tag = Conversions.ToString(this.s.Pop());
			gridViewComboBoxColumn.ReadOnly = Conversions.ToBoolean(this.s.Pop());
			GridViewComboBoxColumn gridViewComboBoxColumn2 = gridViewComboBoxColumn;
			DataTable dataTable = (DataTable)this.s.Pop();
			if (dataTable.Columns.Count > 1)
			{
				gridViewComboBoxColumn2.ValueMember = dataTable.Columns[0].ToString();
				gridViewComboBoxColumn2.DisplayMember = dataTable.Columns[1].ToString();
			}
			else
			{
				gridViewComboBoxColumn2.ValueMember = dataTable.Columns[0].ToString();
				gridViewComboBoxColumn2.DisplayMember = dataTable.Columns[0].ToString();
			}
			gridViewComboBoxColumn2.DataSource = dataTable;
			radGridView.Columns.Add(gridViewComboBoxColumn2);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		public void AddEdit()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			GridViewTextBoxColumn gridViewTextBoxColumn = new GridViewTextBoxColumn();
			gridViewTextBoxColumn.Name = Conversions.ToString(this.s.Pop());
			gridViewTextBoxColumn.FieldName = gridViewTextBoxColumn.Name;
			gridViewTextBoxColumn.HeaderText = gridViewTextBoxColumn.FieldName.ToString().Replace("_", " ");
			gridViewTextBoxColumn.Width = Conversions.ToInteger(this.s.Pop());
			gridViewTextBoxColumn.WrapText = true;
			gridViewTextBoxColumn.Tag = Conversions.ToString(this.s.Pop());
			GridViewTextBoxColumn item = gridViewTextBoxColumn;
			radGridView.Columns.Add(item);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000BD48 File Offset: 0x00009F48
		public void AddCheck()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			GridViewCheckBoxColumn gridViewCheckBoxColumn = new GridViewCheckBoxColumn();
			gridViewCheckBoxColumn.Name = Conversions.ToString(this.s.Pop());
			gridViewCheckBoxColumn.FieldName = gridViewCheckBoxColumn.Name;
			gridViewCheckBoxColumn.HeaderText = gridViewCheckBoxColumn.FieldName.ToString().Replace("_", " ");
			gridViewCheckBoxColumn.Width = Conversions.ToInteger(this.s.Pop());
			gridViewCheckBoxColumn.ReadOnly = Conversions.ToBoolean(this.s.Pop());
			GridViewCheckBoxColumn item = gridViewCheckBoxColumn;
			radGridView.Columns.Add(item);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public void AddRow()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			radGridView.Rows.AddNew();
			if (Operators.CompareString(radGridView.Columns[0].ToString().ToLower(), "id", false) == 0)
			{
				radGridView.Rows[radGridView.CurrentRow.Index].Cells["id"].Value = 0;
			}
			this.s.Push(radGridView.CurrentRow);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000BE74 File Offset: 0x0000A074
		public void SelectRow()
		{
			RadGridView radGridView = (RadGridView)this.s.Pop();
			int num = Conversions.ToInteger(this.s.Pop());
			if (radGridView.Rows.Count > num)
			{
				radGridView.Rows[num].IsCurrent = true;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public void FileLoad()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = Conversions.ToString(this.s.Pop()),
				Filter = Conversions.ToString(this.s.Pop())
			};
			byte[] array = new byte[0];
			openFileDialog.ShowDialog();
			if (openFileDialog.FileNames.Count<string>() > 0)
			{
				array = File.ReadAllBytes(openFileDialog.FileName);
			}
			byte[] value = HashAlgorithm.Create().ComputeHash(array);
			this.s.Push(BitConverter.ToString(value).GetHashCode());
			FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
			this.s.Push(fileInfo.CreationTime);
			this.s.Push(openFileDialog.FileName);
			this.s.Push(array);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000BF94 File Offset: 0x0000A194
		public void GetChanges()
		{
			DataTable dataTable = (DataTable)((RadGridView)this.s.Pop()).DataSource;
			DataTable changes = dataTable.GetChanges();
			if (changes == null)
			{
				this.s.Push(new DataTable
				{
					TableName = Conversions.ToString(0)
				});
				return;
			}
			changes.TableName = Conversions.ToString(changes.Rows.Count);
			this.s.Push(changes);
			dataTable.AcceptChanges();
			dataTable.TableName = Conversions.ToString(dataTable.Rows.Count);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000C024 File Offset: 0x0000A224
		public void ReplaceOnValues()
		{
			int num = Conversions.ToInteger(this.s.Pop());
			string text = Conversions.ToString(this.s.Pop());
			int num2 = num - 1;
			for (int i = 0; i <= num2; i++)
			{
				try
				{
					text = text.Replace(string.Format("|{0}|", i), this.s.Pop().ToString());
				}
				catch (Exception ex)
				{
				}
			}
			this.s.Push(text);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		public void DOC_Open()
		{
			this.wdApp = (Microsoft.Office.Interop.Word.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
			Documents documents = this.wdApp.Documents;
			object obj = Operators.ConcatenateObject(System.Windows.Forms.Application.StartupPath, this.s.Pop());
			object objectValue = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue2 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue3 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue4 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue5 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue6 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue7 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue8 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue9 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue10 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue11 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue12 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue13 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue14 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue15 = RuntimeHelpers.GetObjectValue(Missing.Value);
			Document obj2 = documents.Open(ref obj, ref objectValue, ref objectValue2, ref objectValue3, ref objectValue4, ref objectValue5, ref objectValue6, ref objectValue7, ref objectValue8, ref objectValue9, ref objectValue10, ref objectValue11, ref objectValue12, ref objectValue13, ref objectValue14, ref objectValue15);
			this.s.Push(obj2);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		public void DOC_Replace()
		{
			Document document = (Document)this.s.Pop();
			Find find = this.wdApp.Selection.Find;
			find.Text = Conversions.ToString(this.s.Pop());
			find.Replacement.Text = Conversions.ToString(this.s.Pop());
			object obj = WdFindWrap.wdFindContinue;
			object obj2 = WdReplace.wdReplaceAll;
			object objectValue = RuntimeHelpers.GetObjectValue(Type.Missing);
			object obj3 = false;
			object obj4 = false;
			object obj5 = false;
			object objectValue2 = RuntimeHelpers.GetObjectValue(Type.Missing);
			object obj6 = false;
			object obj7 = true;
			object obj8 = false;
			object objectValue3 = RuntimeHelpers.GetObjectValue(Type.Missing);
			object objectValue4 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue5 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue6 = RuntimeHelpers.GetObjectValue(Missing.Value);
			object objectValue7 = RuntimeHelpers.GetObjectValue(Missing.Value);
			find.Execute(ref objectValue, ref obj3, ref obj4, ref obj5, ref objectValue2, ref obj6, ref obj7, ref obj, ref obj8, ref objectValue3, ref obj2, ref objectValue4, ref objectValue5, ref objectValue6, ref objectValue7);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000C300 File Offset: 0x0000A500
		public void DOC_Print()
		{
			try
			{
				_Application application = this.wdApp;
				object objectValue = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue2 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue3 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue4 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue5 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue6 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue7 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue8 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object obj = "1,1-1";
				object objectValue9 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue10 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue11 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue12 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue13 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue14 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue15 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue16 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue17 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue18 = RuntimeHelpers.GetObjectValue(Missing.Value);
				application.PrintOut(ref objectValue, ref objectValue2, ref objectValue3, ref objectValue4, ref objectValue5, ref objectValue6, ref objectValue7, ref objectValue8, ref obj, ref objectValue9, ref objectValue10, ref objectValue11, ref objectValue12, ref objectValue13, ref objectValue14, ref objectValue15, ref objectValue16, ref objectValue17, ref objectValue18);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000C448 File Offset: 0x0000A648
		public void DOC_Close()
		{
			Document document = (Document)this.s.Pop();
			try
			{
				_Document document2 = document;
				object obj = false;
				object objectValue = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue2 = RuntimeHelpers.GetObjectValue(Missing.Value);
				document2.Close(ref obj, ref objectValue, ref objectValue2);
			}
			catch (Exception ex)
			{
			}
			try
			{
				_Application application = this.wdApp;
				object objectValue2 = RuntimeHelpers.GetObjectValue(Missing.Value);
				object objectValue = RuntimeHelpers.GetObjectValue(Missing.Value);
				object obj = RuntimeHelpers.GetObjectValue(Missing.Value);
				application.Quit(ref objectValue2, ref objectValue, ref obj);
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000C500 File Offset: 0x0000A700
		public void SaveXLS()
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = Conversions.ToString(this.s.Pop());
				RadGridView radGridView = (RadGridView)this.s.Pop();
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					if (!saveFileDialog.FileName.Equals(string.Empty))
					{
						ExportToExcelML exportToExcelML = new ExportToExcelML(radGridView);
						exportToExcelML.SheetName = "Firmware List";
						exportToExcelML.SummariesExportOption = SummariesOption.ExportAll;
						exportToExcelML.SheetMaxRows = ExcelMaxRows._1048576;
						try
						{
							exportToExcelML.RunExport(saveFileDialog.FileName);
						}
						catch (Exception ex)
						{
						}
					}
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public void NewByteArray()
		{
			this.s.Push(new byte[0]);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		public void CmdRD()
		{
			byte[] array = this.IntTo3Bytes(Conversions.ToInteger(this.s.Pop()));
			array = new byte[7];
			array[3] = Conversions.ToByte(this.s.Pop());
			Array.Copy(this.IntTo3Bytes(Conversions.ToInteger(this.s.Pop())), 0, array, 4, 3);
			this.s.Push(array);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000831A File Offset: 0x0000651A
		private byte[] IntTo3Bytes(int Int)
		{
			return new byte[]
			{
				(byte)((Int & 16711680) >> 16),
				(byte)((Int & 65280) >> 8),
				(byte)(Int & 255)
			};
		}

		// Token: 0x040000BD RID: 189
		public Dictionary<string, string> F;

		// Token: 0x040000BE RID: 190
		private DiusParser dp;

		// Token: 0x040000BF RID: 191
		internal Network srv;

		// Token: 0x040000C0 RID: 192
		private Stack s;

		// Token: 0x040000C1 RID: 193
		private DiusParser.ParserState State;

		// Token: 0x040000C2 RID: 194
		private Microsoft.Office.Interop.Word.Application wdApp;

		// Token: 0x040000C3 RID: 195
		private SqlFunc _sql;

		// Token: 0x040000C4 RID: 196
		private int $STATIC$NewView$2001$_Y;

		// Token: 0x040000C5 RID: 197
		private StaticLocalInitFlag $STATIC$NewView$2001$_Y$Init;
	}
}
