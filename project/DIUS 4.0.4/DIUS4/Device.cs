using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DIUS4.Adapters;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace DIUS4
{
	// Token: 0x02000014 RID: 20
	public class Device : Exception
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000040E4 File Offset: 0x000022E4
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000040EC File Offset: 0x000022EC
		private virtual RadButton mBtn { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000040F5 File Offset: 0x000022F5
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004100 File Offset: 0x00002300
		private virtual System.Windows.Forms.Timer Opros
		{
			[CompilerGenerated]
			get
			{
				return this._Opros;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Opros_Tick);
				System.Windows.Forms.Timer opros = this._Opros;
				if (opros != null)
				{
					opros.Tick -= value2;
				}
				this._Opros = value;
				opros = this._Opros;
				if (opros != null)
				{
					opros.Tick += value2;
				}
			}
		}

		// Token: 0x06000097 RID: 151
		[DllImport("aim3see.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int Calculate(int Level, int Seed, int Magic);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004143 File Offset: 0x00002343
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000414B File Offset: 0x0000234B
		public RadProgressBar Progress { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004154 File Offset: 0x00002354
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000415C File Offset: 0x0000235C
		public string Name { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004165 File Offset: 0x00002365
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000416D File Offset: 0x0000236D
		public int Echo { get; set; }

		// Token: 0x17000034 RID: 52
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004178 File Offset: 0x00002378
		public string Label
		{
			set
			{
				Operators.CompareString(value, "Start", false);
				bool flag = false;
				this.Proc_Goto(ref value, ref flag);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000041B9 File Offset: 0x000023B9
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000419E File Offset: 0x0000239E
		public int Address
		{
			get
			{
				return this._Addr;
			}
			set
			{
				this._Addr = value;
				this._Burst = (byte)((this._Addr & 15) + 1);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004210 File Offset: 0x00002410
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000041C4 File Offset: 0x000023C4
		public string Protocol
		{
			get
			{
				string result;
				if (this._Mode)
				{
					result = "ISO-15765";
				}
				else
				{
					result = "ISO-14230";
				}
				return result;
			}
			set
			{
				if (Operators.CompareString(value, "ISO-14230", false) != 0)
				{
					if (Operators.CompareString(value, "ISO-15765", false) == 0)
					{
						this._Mode = true;
					}
				}
				else
				{
					this._Mode = false;
				}
				this.SetValue("Protocol", value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004234 File Offset: 0x00002434
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000423C File Offset: 0x0000243C
		public RadPageView Panel
		{
			get
			{
				return Device.gPanel;
			}
			set
			{
				RadPageViewPage radPageViewPage = new RadPageViewPage
				{
					Name = this.Name,
					Text = this.Name,
					Dock = DockStyle.Fill
				};
				value.Controls.Add(radPageViewPage);
				Device.gPanel = new RadPageView
				{
					Dock = DockStyle.Fill
				};
				radPageViewPage.Controls.Add(Device.gPanel);
			}
		}

		// Token: 0x17000038 RID: 56
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000429B File Offset: 0x0000249B
		public RadButton Button
		{
			set
			{
				this.mBtn = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000042A4 File Offset: 0x000024A4
		public DataTable Script
		{
			set
			{
				this.sCommands = new DataTable();
				this.sCommands = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004313 File Offset: 0x00002513
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000042B8 File Offset: 0x000024B8
		public bool Run
		{
			get
			{
				return this._Run;
			}
			set
			{
				if (!this._Run && value)
				{
					this.Command = default(Device.BASE_CMD);
					Device.tParser = new Thread(new ThreadStart(this.Parser_Thread))
					{
						IsBackground = true
					};
					this._Run = true;
					Device.tParser.Start();
				}
				this._Run = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004329 File Offset: 0x00002529
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000431B File Offset: 0x0000251B
		public bool Init
		{
			get
			{
				return this.Opros.Enabled;
			}
			set
			{
				this.Opros.Enabled = true;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004336 File Offset: 0x00002536
		public bool State
		{
			get
			{
				return this.mBtn.Enabled;
			}
		}

		// Token: 0x1700003D RID: 61
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004343 File Offset: 0x00002543
		public RadGridView Table
		{
			set
			{
				this.sTable = value;
				this.sTable.Rows.Clear();
				this.sTable.CellEndEdit += this.Event_Table;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004373 File Offset: 0x00002573
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000437B File Offset: 0x0000257B
		public Queue Events { get; set; }

		// Token: 0x060000AF RID: 175 RVA: 0x00004384 File Offset: 0x00002584
		public Device()
		{
			this.MessagesIn = new Queue();
			this.WaitForSend = new Queue();
			this.CAN_SendData = new byte[0];
			this.Block = 1;
			this.Command = default(Device.BASE_CMD);
			this._Values = new Hashtable();
			this._Selects = new Hashtable();
			this._Tables = new Hashtable();
			this.mStack = new Stack();
			this.mParam = new Stack();
			this.mDecode = new Decode();
			this.Opros = new System.Windows.Forms.Timer
			{
				Interval = 250,
				Enabled = false
			};
			RadMessageBox.SetThemeName("Office2010Black");
			RadMessageBox.ShowInTaskbar = true;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000443B File Offset: 0x0000263B
		public void SetValue(string Name, object Data)
		{
			if (this._Values.Contains(Name))
			{
				this._Values[Name] = RuntimeHelpers.GetObjectValue(Data);
				return;
			}
			this._Values.Add(Name, RuntimeHelpers.GetObjectValue(Data));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004470 File Offset: 0x00002670
		public object GetValue(string Name)
		{
			object result;
			if (this._Values.Contains(Name))
			{
				result = this._Values[Name];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000044A0 File Offset: 0x000026A0
		private void Opros_Tick(object sender, EventArgs e)
		{
			if (!this._Run)
			{
				this.Opros.Enabled = false;
				VCI_Message vci_Message = new VCI_Message(8)
				{
					ID = (uint)this.Address
				};
				vci_Message.Data[0] = this._Burst;
				vci_Message.Data[(true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0] = 1;
				vci_Message.Data[2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] = (byte)Math.Round(Conversion.Val("&H3E"));
				this.Adapter.Send(vci_Message);
				int num;
				do
				{
					Application.DoEvents();
					Thread.Sleep(30);
					if (this.MessagesIn.Count > 0)
					{
						num = 0;
						this.mBtn.Enabled = true;
						object obj = this.MessagesIn.Peek();
						VCI_Message vci_Message2 = (obj != null) ? ((VCI_Message)obj) : default(VCI_Message);
						if ((double)vci_Message2.Data[((-((this._Mode > false) ? 1 : 0)) ? 1 : 0) + 2] == Conversion.Val("&H7E"))
						{
							object obj2 = this.MessagesIn.Dequeue();
							VCI_Message vci_Message3 = (obj2 != null) ? ((VCI_Message)obj2) : default(VCI_Message);
							this.Command.State = 0;
						}
						else if ((double)vci_Message2.Data[((-((this._Mode > false) ? 1 : 0)) ? 1 : 0) + 2] == Conversion.Val("&H7F"))
						{
							object obj3 = this.MessagesIn.Dequeue();
							VCI_Message vci_Message4 = (obj3 != null) ? ((VCI_Message)obj3) : default(VCI_Message);
							this.Command.State = 0;
						}
					}
					else
					{
						num++;
						if (num > 10)
						{
							num = 0;
							this.mBtn.Enabled = false;
							this.Command.State = 255;
						}
					}
				}
				while (num != 0);
				this.Opros.Enabled = true;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000464C File Offset: 0x0000284C
		private void Parser_Thread()
		{
			this.MessagesIn.Clear();
			for (;;)
			{
				Application.DoEvents();
				int state = this.Command.State;
				if (state <= 48)
				{
					if (state != 0)
					{
						if (state == 48)
						{
							goto IL_8E;
						}
					}
					else
					{
						this.$STATIC$Parser_Thread$2001$ping = 0;
						this.$STATIC$Parser_Thread$2001$ping2 = 0;
						ref int ptr = ref this._CmdAddr;
						this._CmdAddr = ptr + 1;
						this.Execute();
					}
				}
				else
				{
					if (state == 120)
					{
						goto IL_8E;
					}
					if (state == 255)
					{
						break;
					}
				}
				IL_113:
				if (!this._Run)
				{
					return;
				}
				continue;
				IL_8E:
				if (this.MessagesIn.Count != 0)
				{
					this.$STATIC$Parser_Thread$2001$ping = 0;
					this.GetMessage();
					goto IL_113;
				}
				this.$STATIC$Parser_Thread$2001$ping++;
				Application.DoEvents();
				Thread.Sleep(20);
				if (this.$STATIC$Parser_Thread$2001$ping != 250)
				{
					goto IL_113;
				}
				this.$STATIC$Parser_Thread$2001$ping = 0;
				this.Execute();
				this.$STATIC$Parser_Thread$2001$ping2++;
				if (this.$STATIC$Parser_Thread$2001$ping2 == 3)
				{
					this.$STATIC$Parser_Thread$2001$ping = 0;
					this.$STATIC$Parser_Thread$2001$ping2 = 0;
					this.Command.State = 255;
					goto IL_113;
				}
				goto IL_113;
			}
			this._Run = false;
			RadMessageBox.Show("Device no respond. Last Function command: " + Conversions.ToString(this._CmdAddr), "Device no respond");
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004778 File Offset: 0x00002978
		private void Execute()
		{
			int cmdAddr = this._CmdAddr;
			this.Command = new Device.BASE_CMD
			{
				Name = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["name"]),
				Command = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["command"]),
				Param = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["param"]),
				ForParam = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"]),
				Format = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["format"]),
				Out = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["out"]),
				State = 0,
				Data = new byte[0]
			};
			string command = this.Command.Command;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(command);
			if (num > 1884380558U)
			{
				if (num <= 2531704439U)
				{
					if (num <= 2038653189U)
					{
						if (num != 1942912716U)
						{
							if (num != 2038653189U)
							{
								return;
							}
							if (Operators.CompareString(command, "EXIT", false) != 0)
							{
								return;
							}
							this._Run = false;
							return;
						}
						else
						{
							if (Operators.CompareString(command, "RET", false) != 0)
							{
								return;
							}
							this._CmdAddr = Conversions.ToInteger(this.mStack.Pop());
							return;
						}
					}
					else if (num != 2104842452U)
					{
						if (num != 2531704439U)
						{
							return;
						}
						if (Operators.CompareString(command, "GET", false) != 0)
						{
							return;
						}
						int index = Conversions.ToInteger(this.Command.ForParam.Split(new char[]
						{
							'|'
						})[0]);
						int index2 = Conversions.ToInteger(this.Command.ForParam.Split(new char[]
						{
							'|'
						})[1]);
						try
						{
							string param = this.Command.Param;
							if (Operators.CompareString(param, "INT_TIME", false) != 0)
							{
								if (Operators.CompareString(param, "WORD_TIME", false) != 0)
								{
									if (Operators.CompareString(param, "FROM_ASC", false) != 0)
									{
										if (Operators.CompareString(param, "FROM_DATE", false) != 0)
										{
											if (Operators.CompareString(param, "FROM_HEX", false) == 0)
											{
												int num2 = Conversions.ToInteger(this.Command.ForParam.Split(new char[]
												{
													'|'
												})[2]);
												if ((double)this.mDecode.HEX8ToByte(Conversions.ToString(this.sTable.Rows[index].Cells[index2].Value)).Count<byte>() != (double)num2 / 2.0)
												{
													throw new Exception();
												}
												object obj = this.mDecode.HEX8ToByte(Conversions.ToString(this.sTable.Rows[index].Cells[index2].Value));
												this.Store_Value(ref obj);
											}
										}
										else if (Information.IsNothing(RuntimeHelpers.GetObjectValue(this.sTable.Rows[index].Cells[index2].Value)))
										{
											object obj = new byte[3];
											this.Store_Value(ref obj);
										}
										else
										{
											if (this.sTable.Rows[index].Cells[index2].Value.ToString().Length != 8)
											{
												throw new Exception();
											}
											object obj = this.mDecode.FromDate(Conversions.ToString(this.sTable.Rows[index].Cells[index2].Value));
											this.Store_Value(ref obj);
										}
									}
									else
									{
										int num3 = Conversions.ToInteger(this.Command.ForParam.Split(new char[]
										{
											'|'
										})[2]);
										string text;
										if (Information.IsNothing(RuntimeHelpers.GetObjectValue(this.sTable.Rows[index].Cells[index2].Value)))
										{
											text = "";
										}
										else
										{
											text = Conversions.ToString(this.sTable.Rows[index].Cells[index2].Value);
										}
										if (text.Length > num3)
										{
											text = text.Substring(0, num3);
										}
										else if (text.Length < num3)
										{
											int length = text.Length;
											int num4 = num3 - 1;
											for (int i = length; i <= num4; i++)
											{
												text += "\0";
											}
										}
										object obj = this.mDecode.FromASC(text);
										this.Store_Value(ref obj);
									}
								}
								else
								{
									object obj = this.mDecode.TimeToWord(Conversions.ToString(this.sTable.Rows[index].Cells[index2].Value));
									this.Store_Value(ref obj);
								}
							}
							else
							{
								object obj = this.mDecode.TimeToInt(Conversions.ToString(this.sTable.Rows[index].Cells[index2].Value));
								this.Store_Value(ref obj);
							}
						}
						catch (Exception ex)
						{
							this._Run = false;
							Interaction.MsgBox("Error Data, Please input correct Data", MsgBoxStyle.OkOnly, null);
							this.sTable.Rows[index].Cells[2].Value = RuntimeHelpers.GetObjectValue(this.sTable.Rows[index].Cells[1].Value);
						}
					}
					else
					{
						if (Operators.CompareString(command, "ADD", false) != 0)
						{
							return;
						}
						this.Add_Value();
						return;
					}
				}
				else if (num <= 3247583453U)
				{
					if (num != 3011136198U)
					{
						if (num != 3247583453U)
						{
							return;
						}
						if (Operators.CompareString(command, "CREATE", false) != 0)
						{
							return;
						}
						string param2 = this.Command.Param;
						if (Operators.CompareString(param2, "PANEL", false) == 0)
						{
							this.pnl = new RadPageViewPage
							{
								Name = this.Command.Out,
								AutoScroll = true,
								Dock = DockStyle.Fill
							};
							Device.gPanel.Pages.Add(this.pnl);
							return;
						}
						if (Operators.CompareString(param2, "TABLE", false) == 0)
						{
							this.Create_Table(Conversions.ToInteger(this.Command.Format.ToString().Split(new char[]
							{
								'|'
							})[0]), Conversions.ToInteger(this.Command.Format.ToString().Split(new char[]
							{
								'|'
							})[1]), this.Command.ForParam);
							return;
						}
						if (Operators.CompareString(param2, "SELECT", false) != 0)
						{
							if (Operators.CompareString(param2, "BUTTON", false) != 0)
							{
								return;
							}
							RadButton radButton = new RadButton
							{
								Text = this.Command.Format,
								Tag = this.Command.Out,
								Left = Conversions.ToInteger(this.Command.ForParam.ToString().Split(new char[]
								{
									'|'
								})[0]),
								Top = Conversions.ToInteger(this.Command.ForParam.ToString().Split(new char[]
								{
									'|'
								})[1]),
								Font = new Font("Segoe UI", 14f, FontStyle.Bold),
								ForeColor = Color.Red,
								AutoSize = true
							};
							radButton.Click += this.Event_Distributor;
							Device.gPanel.Pages[Device.gPanel.Pages.Count - 1].Controls.Add(radButton);
							return;
						}
					}
					else
					{
						if (Operators.CompareString(command, "PROGRESS", false) != 0)
						{
							return;
						}
						if (Operators.CompareString(this.Command.ForParam, "", false) != 0)
						{
							this.Progress.Maximum = Conversions.ToInteger(this.Command.ForParam);
						}
						this.Progress.Value1 = Conversions.ToInteger(this.Command.Param);
						this.Progress.Text = this.Command.Out;
						return;
					}
				}
				else if (num != 3589817057U)
				{
					if (num != 4076134438U)
					{
						return;
					}
					if (Operators.CompareString(command, "GOTO", false) != 0)
					{
						return;
					}
					bool flag = false;
					this.Proc_Goto(ref this.Command.Out, ref flag);
					return;
				}
				else
				{
					if (Operators.CompareString(command, "PROC", false) != 0)
					{
						return;
					}
					bool flag = true;
					this.Proc_Goto(ref this.Command.Out, ref flag);
					return;
				}
				return;
			}
			if (num <= 771011135U)
			{
				if (num != 236776447U)
				{
					if (num != 624365154U)
					{
						if (num != 771011135U)
						{
							return;
						}
						if (Operators.CompareString(command, "OUT", false) != 0)
						{
							return;
						}
						this.Out();
						return;
					}
					else
					{
						if (Operators.CompareString(command, "MSG", false) != 0)
						{
							return;
						}
						this.Proc_MEssage();
						return;
					}
				}
				else
				{
					if (Operators.CompareString(command, "EVENT", false) != 0)
					{
						return;
					}
					VCI.msgEvent msgEvent = new VCI.msgEvent(this.Command.Param);
					this.Events.Enqueue(msgEvent);
					return;
				}
			}
			else if (num <= 1427742297U)
			{
				if (num != 1197651565U)
				{
					if (num != 1427742297U)
					{
						return;
					}
					if (Operators.CompareString(command, "DIM", false) != 0)
					{
						return;
					}
					this.Set_Value();
					return;
				}
				else
				{
					if (Operators.CompareString(command, "PAUSE", false) != 0)
					{
						return;
					}
					Thread.Sleep(Conversions.ToInteger(this.Command.Param));
					return;
				}
			}
			else if (num != 1491660422U)
			{
				if (num != 1884380558U)
				{
					return;
				}
				if (Operators.CompareString(command, "STORE", false) != 0)
				{
					return;
				}
				object obj = RuntimeHelpers.GetObjectValue(this.Func_GetValue());
				this.Store_Value(ref obj);
				return;
			}
			else
			{
				if (Operators.CompareString(command, "IF", false) != 0)
				{
					return;
				}
				this.Func_Compare();
				return;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000051D8 File Offset: 0x000033D8
		private void Out()
		{
			this.MessagesIn.Clear();
			byte[] array = this.mDecode.HEX8ToByte(this.Command.Param);
			VCI_Message vci_Message = new VCI_Message(8);
			vci_Message.ID = (uint)this._Addr;
			byte[] array2 = new byte[0];
			string forParam = this.Command.ForParam;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(forParam);
			if (num <= 1421410950U)
			{
				if (num != 105150294U)
				{
					if (num != 622060074U)
					{
						if (num == 1421410950U)
						{
							if (Operators.CompareString(forParam, "ARRAY", false) == 0)
							{
								array2 = (byte[])this._Values[this.Command.Out];
								if (Operators.CompareString(this.Command.Format, "", false) != 0)
								{
									array2 = this.mDecode.HEX8ToByte(this.Command.Format).Concat(array2).ToArray<byte>();
								}
							}
						}
					}
					else if (Operators.CompareString(forParam, "VALUE", false) == 0)
					{
						array2 = this.mDecode.HEX8ToByte(this.Command.Out);
					}
				}
				else if (Operators.CompareString(forParam, "HEX8", false) == 0)
				{
					array2 = this.mDecode.HEX8ToByte(Conversions.ToString(this._Values[this.Command.Out]));
				}
			}
			else if (num <= 2385991020U)
			{
				if (num != 1689094666U)
				{
					if (num == 2385991020U)
					{
						if (Operators.CompareString(forParam, "SEED", false) == 0)
						{
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(this._Values["ReceiveData"], null, "Length", new object[0], null, null, null), 0, false))
							{
								return;
							}
							byte[] array3 = (byte[])this._Values["ReceiveData"];
							if (array3[0] == 0 & array3[1] == 0)
							{
								return;
							}
							array2 = this.mDecode.IntTo2Bytes((int)this.mDecode.BRP_SeedKey(Conversions.ToUInteger(this.Command.Format), this.Command.Out, Conversions.ToUInteger(Operators.AddObject(Operators.MultiplyObject(NewLateBinding.LateIndexGet(this._Values["ReceiveData"], new object[]
							{
								0
							}, null), 256), NewLateBinding.LateIndexGet(this._Values["ReceiveData"], new object[]
							{
								1
							}, null)))));
						}
					}
				}
				else if (Operators.CompareString(forParam, "ASC", false) == 0)
				{
					array2 = this.mDecode.FromASC(Conversions.ToString(this._Values[this.Command.Out]));
				}
			}
			else if (num != 2515041328U)
			{
				if (num == 4210612418U)
				{
					if (Operators.CompareString(forParam, "BLOCK", false) == 0)
					{
						this.TransferData();
						return;
					}
				}
			}
			else if (Operators.CompareString(forParam, "POP", false) == 0)
			{
				array2 = (byte[])this.mParam.Pop();
			}
			byte b;
			vci_Message.Data[(int)b] = this._Burst;
			b += ((true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0);
			if ((int)b + array.Length + array2.Length > 7)
			{
				vci_Message.Data[(int)b] = 16;
				b += 1;
			}
			vci_Message.Data[(int)b] = (byte)(array.Length + array2.Length);
			b += 1;
			int num2 = array.Count<byte>() - 1;
			for (int i = 0; i <= num2; i++)
			{
				vci_Message.Data[(int)b] = array[i];
				b += 1;
				this.Command.Count = i;
			}
			this.Command.CmdLen = array.Count<byte>();
			if (array2.Count<byte>() != 0)
			{
				this.WaitForSend.Clear();
				byte b2 = 33;
				int num3 = array2.Length - 1;
				for (int j = 0; j <= num3; j++)
				{
					if (b == 8)
					{
						if (this.Command.State == 120)
						{
							this.WaitForSend.Enqueue(vci_Message);
						}
						else
						{
							this.Adapter.Send(vci_Message);
						}
						vci_Message = new VCI_Message(8)
						{
							ID = (uint)this._Addr
						};
						this.Command.State = 120;
						b = 0;
						vci_Message.Data[(int)b] = this._Burst;
						b += ((true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0);
						vci_Message.Data[(int)b] = b2;
						b2 += 1;
						b += 1;
						vci_Message.Data[(int)b] = array2[j];
						b += 1;
					}
					else
					{
						vci_Message.Data[(int)b] = array2[j];
						b += 1;
					}
				}
				if (this.Command.State == 120)
				{
					this.WaitForSend.Enqueue(vci_Message);
				}
				else
				{
					this.Adapter.Send(vci_Message);
				}
			}
			else
			{
				this.Adapter.Send(vci_Message);
			}
			this.Command.State = 120;
			string format = this.Command.Format;
			bool flag;
			if (Operators.CompareString(format, "PROC", false) == 0)
			{
				flag = true;
				this.Proc_Goto(ref this.Command.Out, ref flag);
				return;
			}
			if (Operators.CompareString(format, "GOTO", false) != 0)
			{
				return;
			}
			flag = false;
			this.Proc_Goto(ref this.Command.Out, ref flag);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005744 File Offset: 0x00003944
		private void TransferData()
		{
			byte[] array = (byte[])this._Values[this.Command.Out];
			VCI_Message vci_Message = new VCI_Message(8);
			ArrayList arrayList = new ArrayList();
			int num = 60;
			int num2 = (int)((double)array.Length / (double)num);
			int num3;
			int num4;
			for (int i = 0; i <= num2; i++)
			{
				if (array.Length - num3 > num)
				{
					byte[] array2 = new byte[num - 1 + 1];
					Array.Copy(array, num3, array2, 0, num);
					arrayList.Add(array2);
				}
				else
				{
					byte[] array3 = new byte[array.Length - num3 - 1 + 1];
					Array.Copy(array, num3, array3, 0, array.Length - num3);
					arrayList.Add(array3);
				}
				num3 += num;
				num4++;
			}
			array = new byte[0];
			int num5 = arrayList.Count - 1;
			for (int j = 0; j <= num5; j++)
			{
				num3 = 0;
				VCI_Message vci_Message2 = new VCI_Message(8)
				{
					ID = (uint)this._Addr
				};
				vci_Message2.Data[num3] = this._Burst;
				num3 = num3 + 1 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0);
				if (Operators.ConditionalCompareObjectGreater(NewLateBinding.LateGet(arrayList[j], null, "Length", new object[0], null, null, null), 3 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0), false))
				{
					vci_Message2.Data[num3] = 16;
					num3++;
				}
				byte[] array4 = (byte[])arrayList[j];
				vci_Message2.Data[num3] = (byte)(array4.Length + 3);
				num3++;
				vci_Message2.Data[num3] = (byte)Math.Round(Conversion.Val("&h" + this.Command.Param));
				num3++;
				byte[] array5 = this.mDecode.IntTo2Bytes(j + 1);
				vci_Message2.Data[num3] = array5[0];
				num3++;
				vci_Message2.Data[num3] = array5[1];
				num3++;
				byte b = 33;
				int num6 = array4.Length - 1;
				for (int k = 0; k <= num6; k++)
				{
					if (num3 == 8)
					{
						this.WaitForSend.Enqueue(vci_Message2);
						VCI_Message vci_Message3 = new VCI_Message(8)
						{
							ID = (uint)this._Addr
						};
						vci_Message2 = vci_Message3;
						num3 = 0;
						vci_Message2.Data[num3] = this._Burst;
						num3 += ((true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0);
						vci_Message2.Data[num3] = b;
						b += 1;
						num3++;
						vci_Message2.Data[num3] = array4[k];
						num3++;
					}
					else
					{
						vci_Message2.Data[num3] = array4[k];
						num3++;
					}
				}
				this.WaitForSend.Enqueue(vci_Message2);
			}
			num3 = 0;
			Queue queue = new Queue();
			try
			{
				int num7 = num4 - 1;
				int l = 0;
				while (l <= num7)
				{
					Application.DoEvents();
					Adapter adapter = this.Adapter;
					object obj = this.WaitForSend.Dequeue();
					VCI_Message message;
					if (obj == null)
					{
						VCI_Message vci_Message3 = default(VCI_Message);
						message = vci_Message3;
					}
					else
					{
						message = (VCI_Message)obj;
					}
					adapter.Send(message);
					while (this.MessagesIn.Count <= 0)
					{
					}
					object obj2 = this.MessagesIn.Dequeue();
					VCI_Message vci_Message4;
					if (obj2 == null)
					{
						VCI_Message vci_Message3 = default(VCI_Message);
						vci_Message4 = vci_Message3;
					}
					else
					{
						vci_Message4 = (VCI_Message)obj2;
					}
					vci_Message = vci_Message4;
					if (vci_Message.Data[(true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0] == 48)
					{
						if (this.WaitForSend.Count > 9 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0))
						{
							int num8 = 9 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0);
							for (int m = 0; m <= num8; m++)
							{
								queue.Enqueue(RuntimeHelpers.GetObjectValue(this.WaitForSend.Dequeue()));
							}
							this.Adapter.Send(queue);
							num3 = num3 + 11 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0);
						}
						else
						{
							int num9 = this.WaitForSend.Count - 1;
							for (int n = 0; n <= num9; n++)
							{
								Adapter adapter2 = this.Adapter;
								object obj3 = this.WaitForSend.Dequeue();
								VCI_Message message2;
								if (obj3 == null)
								{
									VCI_Message vci_Message3 = default(VCI_Message);
									message2 = vci_Message3;
								}
								else
								{
									message2 = (VCI_Message)obj3;
								}
								adapter2.Send(message2);
							}
						}
						for (;;)
						{
							int num10 = 5;
							if (this.Wait_Message(ref num10))
							{
								goto Block_17;
							}
							object obj4 = this.MessagesIn.Dequeue();
							VCI_Message vci_Message5;
							if (obj4 == null)
							{
								VCI_Message vci_Message3 = default(VCI_Message);
								vci_Message5 = vci_Message3;
							}
							else
							{
								vci_Message5 = (VCI_Message)obj4;
							}
							vci_Message = vci_Message5;
							if (!(vci_Message.Data[2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] == 127 & vci_Message.Data[4 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] == 120))
							{
								break;
							}
							this.Progress.Text = "Please Wait . . .";
						}
						if (vci_Message.Data[2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] != 127)
						{
							this.Progress.Text = "Transfer Data Bytes: " + Conversions.ToString(l * 60);
							if (this.$STATIC$TransferData$2001$a < 50)
							{
								this.Progress.ForeColor = Color.Red;
							}
							else
							{
								this.Progress.ForeColor = Color.Black;
							}
							this.$STATIC$TransferData$2001$a++;
							if (this.$STATIC$TransferData$2001$a == 100)
							{
								this.$STATIC$TransferData$2001$a = 0;
							}
							l++;
							continue;
						}
						break;
						Block_17:
						this.SetValue("ReceiveError", 1);
						goto IL_58E;
					}
					break;
				}
			}
			catch (Exception ex)
			{
			}
			if (vci_Message.Data[2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] == 127)
			{
				this.SetValue("ReceiveError", vci_Message.Data[4 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)]);
				this.Progress.Text = "Transfer Data Error: " + Conversions.ToString(vci_Message.Data[4 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)]);
			}
			else
			{
				this.SetValue("ReceiveError", 0);
				this.Progress.Text = "Transfer Data Done";
			}
			IL_58E:
			this.Progress.ForeColor = Color.Black;
			this.Command.State = 0;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005D18 File Offset: 0x00003F18
		private bool Wait_Message(ref int Seconds)
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)Seconds);
			while (this.MessagesIn.Count <= 0)
			{
				Application.DoEvents();
				if (DateTime.Now.Subtract(now).Ticks > timeSpan.Ticks)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005D70 File Offset: 0x00003F70
		private void Event_Distributor(object sender, EventArgs e)
		{
			if (!this._Run)
			{
				string text = Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
				bool flag = false;
				this.Proc_Goto(ref text, ref flag);
				NewLateBinding.LateSetComplex(sender, null, "tag", new object[]
				{
					text
				}, null, null, true, false);
				ref int ptr = ref this._CmdAddr;
				this._CmdAddr = ptr + 1;
				this.Run = true;
				this.Command.Command = Conversions.ToString(0);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005DF0 File Offset: 0x00003FF0
		private void Add_Value()
		{
			if (this.sTable.InvokeRequired)
			{
				this.sTable.Invoke(new MethodInvoker(this.DelegateAddRow));
			}
			else
			{
				this.sTable.Rows.AddNew();
			}
			this.sTable.Rows[this.sTable.RowCount - 1].Cells[0].Value = RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["param"]);
			this.sTable.Rows[this.sTable.RowCount - 1].Tag = RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["out"]);
			int num = 1;
			while (!Operators.ConditionalCompareObjectNotEqual(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr + 1)["command"], "", false))
			{
				ref int ptr = ref this._CmdAddr;
				this._CmdAddr = ptr + 1;
				this.sTable.Rows[this.sTable.RowCount - 1].Cells[num].Value = RuntimeHelpers.GetObjectValue(this.Func_GetValue());
				num++;
				if (num > 100)
				{
					break;
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005F4D File Offset: 0x0000414D
		protected void DelegateAddRow()
		{
			this.sTable.Rows.AddNew();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005F60 File Offset: 0x00004160
		private void Set_Value()
		{
			string[] array = this.Command.ForParam.ToString().Split(new char[]
			{
				' '
			});
			if (array.Count<string>() == 1)
			{
				if (this._Values.ContainsKey(this.Command.Param))
				{
					if (this._Values.ContainsKey(array[0]))
					{
						this._Values[this.Command.Param] = RuntimeHelpers.GetObjectValue(this._Values[array[0]]);
						return;
					}
					this._Values[this.Command.Param] = array[0];
					return;
				}
				else
				{
					if (this._Values.ContainsKey(array[0]))
					{
						this._Values.Add(this.Command.Param, RuntimeHelpers.GetObjectValue(this._Values[array[0]]));
						return;
					}
					this._Values.Add(this.Command.Param, array[0]);
					return;
				}
			}
			else
			{
				string left = array[0];
				if (Operators.CompareString(left, "+", false) == 0)
				{
					this._Values[this.Command.Param] = ((double)Conversions.ToInteger(this._Values[this.Command.Param]) + Conversions.ToDouble(array[1])).ToString();
					return;
				}
				if (Operators.CompareString(left, "&", false) == 0)
				{
					this._Values[this.Command.Param] = Operators.ConcatenateObject(Operators.ConcatenateObject(this._Values[this.Command.Param], " "), this._Values[array[1]]);
					return;
				}
				if (Operators.CompareString(left, "=", false) != 0)
				{
					return;
				}
				if (this._Values.ContainsKey(this.Command.Param))
				{
					this._Values[this.Command.Param] = RuntimeHelpers.GetObjectValue(this._Values[array[1]]);
					return;
				}
				this._Values.Add(this.Command.Param, RuntimeHelpers.GetObjectValue(this._Values[array[1]]));
				return;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006188 File Offset: 0x00004388
		private bool Func_Compare()
		{
			bool flag = false;
			string text = this.Command.Param;
			string left = this.Command.ForParam.Split(new char[]
			{
				' '
			})[0];
			string text2 = this.Command.ForParam.Split(new char[]
			{
				' '
			})[1].Replace("'", "");
			if (this._Values.ContainsKey(this.Command.Param))
			{
				if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(this._Values[this.Command.Param])))
				{
					this._Values[this.Command.Param] = "";
				}
				text = Conversions.ToString(this._Values[this.Command.Param]);
			}
			if (this._Values.ContainsKey(text2))
			{
				text2 = Conversions.ToString(this._Values[text2]);
			}
			if (Operators.CompareString(left, "=", false) != 0)
			{
				if (Operators.CompareString(left, "<>", false) != 0)
				{
					if (Operators.CompareString(left, "<", false) != 0)
					{
						if (Operators.CompareString(left, ">", false) != 0)
						{
							if (Operators.CompareString(left, ">=", false) == 0)
							{
								flag = (Conversions.ToInteger(text) >= Conversions.ToInteger(text2));
							}
						}
						else
						{
							flag = (Conversions.ToInteger(text) > Conversions.ToInteger(text2));
						}
					}
					else
					{
						flag = (Conversions.ToInteger(text) < Conversions.ToInteger(text2));
					}
				}
				else
				{
					flag = (Operators.CompareString(text, text2, false) != 0);
				}
			}
			else
			{
				flag = (Operators.CompareString(text, text2, false) == 0);
			}
			if (flag)
			{
				string format = this.Command.Format;
				if (Operators.CompareString(format, "GOTO", false) != 0)
				{
					if (Operators.CompareString(format, "PROC", false) != 0)
					{
						if (Operators.CompareString(format, "RET", false) != 0)
						{
							if (Operators.CompareString(format, "EXIT", false) != 0)
							{
								if (Operators.CompareString(format, "END", false) == 0)
								{
									this._Run = false;
									Interaction.Beep();
								}
							}
							else
							{
								this._Run = false;
							}
						}
						else
						{
							this._CmdAddr = Conversions.ToInteger(this.mStack.Pop());
						}
					}
					else
					{
						bool flag2 = true;
						this.Proc_Goto(ref this.Command.Out, ref flag2);
					}
				}
				else
				{
					bool flag2 = false;
					this.Proc_Goto(ref this.Command.Out, ref flag2);
				}
			}
			else
			{
				string format2 = this.Command.Format;
				if (Operators.CompareString(format2, "THEN", false) == 0)
				{
					this._Run = false;
					ref int ptr = ref this._CmdAddr;
					this._CmdAddr = ptr + 1;
					while (Operators.CompareString(this.Command.Command, "ENDIF", false) != 0)
					{
						this.Execute();
					}
					this._Run = true;
				}
			}
			return flag;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000643C File Offset: 0x0000463C
		private void GetMessage()
		{
			int state = this.Command.State;
			if (state != 48)
			{
				if (state == 120)
				{
					object obj = this.MessagesIn.Dequeue();
					VCI_Message vci_Message2;
					if (obj == null)
					{
						VCI_Message vci_Message = default(VCI_Message);
						vci_Message2 = vci_Message;
					}
					else
					{
						vci_Message2 = (VCI_Message)obj;
					}
					VCI_Message vci_Message3 = vci_Message2;
					byte b = vci_Message3.Data[(true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0];
					if (b >= 16 && b <= 31)
					{
						this.Command.State = 48;
						this.Command.Count = 0;
						this.Command.Data = new byte[((int)vci_Message3.Data[(true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0] & 3840) + (int)vci_Message3.Data[2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] - this.Command.CmdLen - 1 + 1];
						for (int i = this.Command.CmdLen + 3 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0); i <= 7; i++)
						{
							this.Command.Data[this.Command.Count] = vci_Message3.Data[i];
							ref int ptr = ref this.Command.Count;
							this.Command.Count = ptr + 1;
						}
						VCI_Message vci_Message = new VCI_Message(8)
						{
							ID = (uint)this._Addr
						};
						vci_Message3 = vci_Message;
						vci_Message3.Data[0] = this._Burst;
						vci_Message3.Data[(true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0] = 48;
						vci_Message3.Data[2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] = 0;
						vci_Message3.Data[3 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] = 1;
						vci_Message3.Data[4 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] = this.Command.Data[3 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)];
						this.Adapter.Send(vci_Message3);
						this.Command.State = 48;
						return;
					}
					if (b >= 1 && b <= 7)
					{
						if (vci_Message3.Data[2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] != 127)
						{
							this.Command.Data = new byte[(int)vci_Message3.Data[(true + -((this._Mode > false) ? 1 : 0)) ? 1 : 0] - this.Command.CmdLen - 1 + 1];
							this.Command.Count = 0;
							int num = 2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0) + this.Command.CmdLen;
							int num2 = 2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0) + this.Command.CmdLen + this.Command.Data.Count<byte>() - 1;
							for (int j = num; j <= num2; j++)
							{
								this.Command.Data[this.Command.Count] = vci_Message3.Data[j];
								ref int ptr = ref this.Command.Count;
								this.Command.Count = ptr + 1;
							}
							this._Values["ReceiveData"] = this.Command.Data;
							this._Values["ReceiveLenData"] = this.Command.Count;
							this._Values["ReceiveError"] = Device.ScriptState.NoError;
							this.Command.State = 0;
							return;
						}
						if (vci_Message3.Data[4 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)] == 120)
						{
							this.Command.State = 120;
							return;
						}
						this._Values["ReceiveError"] = vci_Message3.Data[4 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0)];
						this.Command.State = 0;
						return;
					}
					else if (b == 48)
					{
						while (this.WaitForSend.Count != 0)
						{
							Adapter adapter = this.Adapter;
							object obj2 = this.WaitForSend.Dequeue();
							VCI_Message message;
							if (obj2 == null)
							{
								VCI_Message vci_Message = default(VCI_Message);
								message = vci_Message;
							}
							else
							{
								message = (VCI_Message)obj2;
							}
							adapter.Send(message);
						}
						this.Command.State = 120;
						return;
					}
				}
			}
			else
			{
				int num3 = this.MessagesIn.Count - 1;
				for (int k = 0; k <= num3; k++)
				{
					object obj3 = this.MessagesIn.Dequeue();
					VCI_Message vci_Message4;
					if (obj3 == null)
					{
						VCI_Message vci_Message = default(VCI_Message);
						vci_Message4 = vci_Message;
					}
					else
					{
						vci_Message4 = (VCI_Message)obj3;
					}
					VCI_Message vci_Message3 = vci_Message4;
					int num4 = 2 + ((-((this._Mode > false) ? 1 : 0)) ? 1 : 0);
					while (num4 <= 7 && this.Command.Count + 1 <= this.Command.Data.Count<byte>())
					{
						this.Command.Data[this.Command.Count] = vci_Message3.Data[num4];
						ref int ptr = ref this.Command.Count;
						this.Command.Count = ptr + 1;
						num4++;
					}
				}
				if (this.Command.Count + 1 > this.Command.Data.Count<byte>())
				{
					this.Command.State = 0;
					this._Values["ReceiveData"] = this.Command.Data;
					this._Values["ReceiveLenData"] = this.Command.Data.Length;
					this._Values["ReceiveError"] = Device.ScriptState.NoError;
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006938 File Offset: 0x00004B38
		private void Create_Table(int x, int y, string name)
		{
			this.sTable = new RadGridView
			{
				Size = (Size)new Point(x, y),
				AllowAddNewRow = false,
				AllowDeleteRow = false,
				AllowDragToGroup = true,
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				AutoSizeRows = true,
				AllowColumnResize = true,
				Text = name,
				ForeColor = Color.Black,
				Name = name,
				AutoExpandGroups = true,
				AddNewRowPosition = SystemRowPosition.Top,
				NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell
			};
			if (this.sTable.Width != 0)
			{
				RadLabel value = new RadLabel
				{
					Font = new Font("Segoe UI", 11f, FontStyle.Bold),
					ForeColor = Color.Blue,
					Text = this.Command.ForParam,
					AutoSize = false,
					TextAlignment = ContentAlignment.TopCenter,
					Width = this.sTable.Width - 2,
					Height = 22,
					Top = 1,
					Left = 1
				};
				this.sTable.Controls.Add(value);
			}
			ref int ptr = ref this._CmdAddr;
			this._CmdAddr = ptr + 1;
			do
			{
				object left = this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["command"];
				if (Operators.ConditionalCompareObjectEqual(left, "ADDINFO", false))
				{
					GridViewTextBoxColumn gridViewTextBoxColumn = new GridViewTextBoxColumn();
					gridViewTextBoxColumn.FieldName = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["Param"]);
					gridViewTextBoxColumn.Name = gridViewTextBoxColumn.FieldName;
					gridViewTextBoxColumn.HeaderText = gridViewTextBoxColumn.Name.ToString().Replace("_", " ");
					gridViewTextBoxColumn.Width = Conversions.ToInteger(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"]);
					gridViewTextBoxColumn.ReadOnly = true;
					gridViewTextBoxColumn.IsVisible = false;
					gridViewTextBoxColumn.WrapText = true;
					GridViewTextBoxColumn gridViewTextBoxColumn2 = gridViewTextBoxColumn;
					if (gridViewTextBoxColumn2.Width > 0)
					{
						gridViewTextBoxColumn2.IsVisible = true;
					}
					this.sTable.Columns.Add(gridViewTextBoxColumn2);
				}
				else if (Operators.ConditionalCompareObjectEqual(left, "ADDEDIT", false))
				{
					GridViewTextBoxColumn gridViewTextBoxColumn3 = new GridViewTextBoxColumn();
					gridViewTextBoxColumn3.FieldName = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["Param"]);
					gridViewTextBoxColumn3.Name = gridViewTextBoxColumn3.FieldName;
					gridViewTextBoxColumn3.HeaderText = gridViewTextBoxColumn3.Name.ToString().Replace("_", " ");
					gridViewTextBoxColumn3.Width = Conversions.ToInteger(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"]);
					gridViewTextBoxColumn3.ReadOnly = false;
					gridViewTextBoxColumn3.IsVisible = false;
					gridViewTextBoxColumn3.Tag = RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["out"]);
					gridViewTextBoxColumn3.WrapText = true;
					GridViewTextBoxColumn gridViewTextBoxColumn4 = gridViewTextBoxColumn3;
					if (gridViewTextBoxColumn4.Width > 0)
					{
						gridViewTextBoxColumn4.IsVisible = true;
					}
					this.sTable.Columns.Add(gridViewTextBoxColumn4);
				}
				else if (Operators.ConditionalCompareObjectEqual(left, "ADDSEL", false))
				{
					this.dt = (DataTable)this._Selects[RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["format"])];
					GridViewComboBoxColumn gridViewComboBoxColumn = new GridViewComboBoxColumn();
					gridViewComboBoxColumn.FieldName = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["param"]);
					gridViewComboBoxColumn.Name = gridViewComboBoxColumn.FieldName;
					gridViewComboBoxColumn.HeaderText = gridViewComboBoxColumn.Name.ToString().Replace("_", " ");
					gridViewComboBoxColumn.Width = Conversions.ToInteger(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"]);
					gridViewComboBoxColumn.Tag = RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["out"]);
					gridViewComboBoxColumn.TextAlignment = ContentAlignment.BottomRight;
					gridViewComboBoxColumn.IsVisible = false;
					gridViewComboBoxColumn.ReadOnly = false;
					gridViewComboBoxColumn.DataSource = this.dt;
					gridViewComboBoxColumn.DisplayMember = "Value";
					gridViewComboBoxColumn.ValueMember = "Id";
					GridViewComboBoxColumn gridViewComboBoxColumn2 = gridViewComboBoxColumn;
					if (this.dt.Columns.Count == 2)
					{
						gridViewComboBoxColumn2.ValueMember = "id";
					}
					else
					{
						gridViewComboBoxColumn2.ValueMember = "Value";
					}
					if (gridViewComboBoxColumn2.Width > 0)
					{
						gridViewComboBoxColumn2.IsVisible = true;
					}
					this.sTable.Columns.Add(gridViewComboBoxColumn2);
				}
				else if (Operators.ConditionalCompareObjectEqual(left, "ADDBTN", false))
				{
					RadButton radButton = new RadButton
					{
						Text = Conversions.ToString(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["param"]),
						Tag = RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["out"]),
						Left = Conversions.ToInteger(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"].ToString().Split(new char[]
						{
							'|'
						})[0]),
						Top = Conversions.ToInteger(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"].ToString().Split(new char[]
						{
							'|'
						})[1]),
						Font = new Font("Segoe UI", 14f, FontStyle.Bold),
						ForeColor = Color.Blue,
						AutoSize = true
					};
					radButton.Click += this.Event_Distributor;
					this.sTable.Controls.Add(radButton);
				}
				else if (Operators.ConditionalCompareObjectEqual(left, "ADDEVENT", false))
				{
					this.sTable.Tag = RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["out"]);
					this.sTable.CurrentRowChanged += new CurrentRowChangedEventHandler(this.Event_Distributor);
				}
				ptr = ref this._CmdAddr;
				this._CmdAddr = ptr + 1;
			}
			while (!Operators.ConditionalCompareObjectEqual(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["command"], "ADDEND", false));
			this._Tables.Add(this.sTable.Name, this.sTable);
			this.AddTables_OnPanel(ref this.sTable, ref this.Command.Out);
			this.sTable.CellEndEdit += this.Event_Table;
			this.sTable.CreateRow += this.Create_Row;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007030 File Offset: 0x00005230
		private void AddTables_OnPanel(ref RadGridView Adding, ref string onPanel)
		{
			if (this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init == null)
			{
				Interlocked.CompareExchange<StaticLocalInitFlag>(ref this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init, new StaticLocalInitFlag(), null);
			}
			bool flag = false;
			try
			{
				Monitor.Enter(this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init, ref flag);
				if (this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init.State == 0)
				{
					this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init.State = 2;
					this.$STATIC$AddTables_OnPanel$20211012813110E$_Y = 40;
				}
				else if (this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init.State = 1;
				if (flag)
				{
					Monitor.Exit(this.$STATIC$AddTables_OnPanel$20211012813110E$_Y$Init);
				}
			}
			RadPageViewPage radPageViewPage = new RadPageViewPage();
			try
			{
				foreach (object obj in this.Panel.Controls)
				{
					radPageViewPage = (RadPageViewPage)obj;
					if (Operators.CompareString(radPageViewPage.Name, onPanel, false) == 0)
					{
						break;
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			if (radPageViewPage.Tag == null)
			{
				if (Adding.Height == 0)
				{
					Adding.Height = radPageViewPage.Height - 102;
				}
				if (Adding.Width == 0)
				{
					Adding.Width = radPageViewPage.Width - 22;
				}
				radPageViewPage.Tag = new Point(Adding.Width, 40);
				Adding.Left = 0;
				Adding.Top = 40;
				this.$STATIC$AddTables_OnPanel$20211012813110E$_Y = Adding.Height + 40;
			}
			else
			{
				object tag = radPageViewPage.Tag;
				Point point = (tag != null) ? ((Point)tag) : default(Point);
				if (Adding.Height == 0)
				{
					Adding.Height = radPageViewPage.Height - this.$STATIC$AddTables_OnPanel$20211012813110E$_Y - point.Y - 22;
				}
				if (Adding.Width == 0)
				{
					Adding.Width = radPageViewPage.Width - 22;
				}
				if (radPageViewPage.Width - point.X + 2 > Adding.Width)
				{
					Adding.Left = point.X + 2;
					Adding.Top = point.Y;
					point.X = point.X + 2 + Adding.Width;
					if (this.$STATIC$AddTables_OnPanel$20211012813110E$_Y < Adding.Height + Adding.Top)
					{
						this.$STATIC$AddTables_OnPanel$20211012813110E$_Y = Adding.Height + Adding.Top;
					}
				}
				else
				{
					Adding.Left = 0;
					Adding.Top = this.$STATIC$AddTables_OnPanel$20211012813110E$_Y + 2;
					point.X = Adding.Width;
					point.Y = this.$STATIC$AddTables_OnPanel$20211012813110E$_Y + 2;
					this.$STATIC$AddTables_OnPanel$20211012813110E$_Y = point.Y + Adding.Height;
				}
				radPageViewPage.Tag = point;
			}
			radPageViewPage.Controls.Add(Adding);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000072E0 File Offset: 0x000054E0
		private void Event_Table(object sender, GridViewCellEventArgs e)
		{
			this._Values["int_LASTROW"] = e.RowIndex;
			if (Operators.ConditionalCompareObjectNotEqual(e.Row.Tag, "", false))
			{
				GridViewRowInfo row;
				string tag = Conversions.ToString((row = e.Row).Tag);
				bool flag = false;
				this.Proc_Goto(ref tag, ref flag);
				row.Tag = tag;
				return;
			}
			if (Operators.ConditionalCompareObjectNotEqual(e.Column.Tag, "", false))
			{
				GridViewColumn column;
				string tag = Conversions.ToString((column = e.Column).Tag);
				bool flag = true;
				this.Proc_Goto(ref tag, ref flag);
				column.Tag = tag;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007385 File Offset: 0x00005585
		private void Create_Row(object sender, GridViewCreateRowEventArgs e)
		{
			e.RowInfo.MinHeight = 26;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007394 File Offset: 0x00005594
		private object Func_GetValue()
		{
			string[] array = this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"].ToString().Split(new char[]
			{
				'|'
			});
			object left = this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["param"];
			object result;
			if (Operators.ConditionalCompareObjectEqual(left, "ASC", false))
			{
				result = this.mDecode.ToASC((byte[])this._Values["ReceiveData"], Conversions.ToUInteger(array[0]), Conversions.ToUInteger(array[1]));
			}
			else if (Operators.ConditionalCompareObjectEqual(left, "HEX8", false))
			{
				result = this.mDecode.ByteToHEX8((byte[])this._Values["ReceiveData"], Conversions.ToUInteger(array[0]), Conversions.ToUInteger(array[1]));
			}
			else if (Operators.ConditionalCompareObjectEqual(left, "DATE", false))
			{
				if (Operators.ConditionalCompareObjectEqual(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["forparam"], "NOW", false))
				{
					result = DateTime.Now;
				}
				else
				{
					result = this.mDecode.ToDate((byte[])this._Values["ReceiveData"], Conversions.ToUInteger(array[0]), Conversions.ToUInteger(array[1]));
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, "TIME", false))
			{
				byte[] array2 = new byte[(int)Math.Round(Conversions.ToDouble(array[1]) - 1.0) + 1];
				object instance = null;
				Type typeFromHandle = typeof(Array);
				string memberName = "Copy";
				Hashtable values;
				object obj;
				object[] array3;
				object[] arguments = array3 = new object[]
				{
					(values = this._Values)[RuntimeHelpers.GetObjectValue(obj = "ReceiveData")],
					Conversions.ToInteger(array[0]),
					array2,
					0,
					Conversions.ToInteger(array[1])
				};
				string[] argumentNames = null;
				Type[] typeArguments = null;
				bool[] array4 = new bool[5];
				array4[0] = true;
				array4[2] = true;
				bool[] array5 = array4;
				NewLateBinding.LateCall(instance, typeFromHandle, memberName, arguments, argumentNames, typeArguments, array4, true);
				if (array5[0])
				{
					values[RuntimeHelpers.GetObjectValue(obj)] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array3[0]));
				}
				if (array5[2])
				{
					array2 = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[2]), typeof(byte[]));
				}
				if (array2.Length == 2)
				{
					result = this.mDecode.WordToTime(array2);
				}
				else
				{
					result = this.mDecode.IntToTime(array2);
				}
			}
			else if (Operators.ConditionalCompareObjectEqual(left, "BYTE", false))
			{
				result = NewLateBinding.LateIndexGet(this._Values["ReceiveData"], new object[]
				{
					array[0]
				}, null);
			}
			else
			{
				result = this._Values[RuntimeHelpers.GetObjectValue(this.sCommands.AsEnumerable().ElementAtOrDefault(this._CmdAddr)["param"])];
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007680 File Offset: 0x00005880
		private void Store_Value(ref object mValue)
		{
			string @out = this.Command.Out;
			if (Operators.CompareString(@out, "DEVICE", false) != 0)
			{
				if (Operators.CompareString(@out, "ADAPTER", false) != 0)
				{
					if (this._Values.ContainsKey(this.Command.Out))
					{
						this._Values[this.Command.Out] = RuntimeHelpers.GetObjectValue(mValue);
						return;
					}
					this._Values.Add(this.Command.Out, RuntimeHelpers.GetObjectValue(mValue));
				}
			}
			else
			{
				string format = this.Command.Format;
				if (Operators.CompareString(format, "NUMBER", false) != 0 && Operators.CompareString(format, "FIRMWARE", false) != 0)
				{
					Operators.CompareString(format, "HARDWARE", false);
					return;
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007744 File Offset: 0x00005944
		private void Proc_MEssage()
		{
			string[] array = this.Command.Out.ToString().Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			int num2;
			if (num != 1)
			{
				if (num == 2)
				{
					num2 = (int)RadMessageBox.Show(string.Format(array[0], RuntimeHelpers.GetObjectValue(this._Values[array[1]])), this.Command.Format, (MessageBoxButtons)Conversions.ToInteger(this.Command.Param), (RadMessageIcon)Conversions.ToInteger(this.Command.ForParam));
				}
			}
			else
			{
				num2 = (int)RadMessageBox.Show(array[0], this.Command.Format, (MessageBoxButtons)Conversions.ToInteger(this.Command.Param), (RadMessageIcon)Conversions.ToInteger(this.Command.ForParam));
			}
			this.SetValue("MsgResult", num2);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007818 File Offset: 0x00005A18
		private void Proc_Goto(ref string Label, ref bool _Type)
		{
			try
			{
				if (_Type)
				{
					this.mStack.Push(this._CmdAddr);
				}
				this._CmdAddr = this.sCommands.Rows.IndexOf(this.sCommands.Select(string.Format("name = '{0}:'", Label))[0]) - 1;
				this.Run = true;
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Format("Developer: Error Label name: {0}", Label), MsgBoxStyle.OkOnly, null);
			}
		}

		// Token: 0x0400008F RID: 143
		public Queue MessagesIn;

		// Token: 0x04000090 RID: 144
		public Adapter Adapter;

		// Token: 0x04000091 RID: 145
		public Queue WaitForSend;

		// Token: 0x04000092 RID: 146
		private byte _Burst;

		// Token: 0x04000093 RID: 147
		private int _Addr;

		// Token: 0x04000094 RID: 148
		private bool _Mode;

		// Token: 0x04000095 RID: 149
		internal bool _CanData;

		// Token: 0x04000096 RID: 150
		private byte[] CAN_SendData;

		// Token: 0x04000097 RID: 151
		private int Block;

		// Token: 0x04000098 RID: 152
		internal Device.BASE_CMD Command;

		// Token: 0x04000099 RID: 153
		private Hashtable _Values;

		// Token: 0x0400009A RID: 154
		internal Hashtable _Selects;

		// Token: 0x0400009B RID: 155
		internal Hashtable _Tables;

		// Token: 0x0400009C RID: 156
		private bool _Run;

		// Token: 0x0400009D RID: 157
		private DataTable sCommands;

		// Token: 0x0400009E RID: 158
		private int _CmdAddr;

		// Token: 0x0400009F RID: 159
		private Stack mStack;

		// Token: 0x040000A0 RID: 160
		private Stack mParam;

		// Token: 0x040000A1 RID: 161
		private RadPageViewPage pnl;

		// Token: 0x040000A2 RID: 162
		private static RadPageView gPanel;

		// Token: 0x040000A3 RID: 163
		private Decode mDecode;

		// Token: 0x040000A5 RID: 165
		internal DataTable dt;

		// Token: 0x040000A6 RID: 166
		internal RadGridView sTable;

		// Token: 0x040000A8 RID: 168
		private static Thread tParser;

		// Token: 0x040000AD RID: 173
		private int $STATIC$Parser_Thread$2001$ping;

		// Token: 0x040000AE RID: 174
		private int $STATIC$Parser_Thread$2001$ping2;

		// Token: 0x040000AF RID: 175
		private int $STATIC$TransferData$2001$a;

		// Token: 0x040000B0 RID: 176
		private int $STATIC$AddTables_OnPanel$20211012813110E$_Y;

		// Token: 0x040000B1 RID: 177
		private StaticLocalInitFlag $STATIC$AddTables_OnPanel$20211012813110E$_Y$Init;

		// Token: 0x0200003B RID: 59
		internal struct BASE_CMD
		{
			// Token: 0x0400016A RID: 362
			public string Name;

			// Token: 0x0400016B RID: 363
			public string Command;

			// Token: 0x0400016C RID: 364
			public int CmdLen;

			// Token: 0x0400016D RID: 365
			public string Param;

			// Token: 0x0400016E RID: 366
			public string ForParam;

			// Token: 0x0400016F RID: 367
			public string Format;

			// Token: 0x04000170 RID: 368
			public string Out;

			// Token: 0x04000171 RID: 369
			public int State;

			// Token: 0x04000172 RID: 370
			public byte[] Data;

			// Token: 0x04000173 RID: 371
			public int Count;
		}

		// Token: 0x0200003C RID: 60
		private enum ScriptState : byte
		{
			// Token: 0x04000175 RID: 373
			NoError,
			// Token: 0x04000176 RID: 374
			Wait = 120,
			// Token: 0x04000177 RID: 375
			CommandError = 127,
			// Token: 0x04000178 RID: 376
			NoDevice = 255,
			// Token: 0x04000179 RID: 377
			RecieveLongData = 48
		}
	}
}
