using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Telerik.WinControls.UI;

namespace DIUS4
{
	// Token: 0x02000018 RID: 24
	public class DiusParser
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000861C File Offset: 0x0000681C
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00008624 File Offset: 0x00006824
		internal virtual Dius _Parent { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060000E9 RID: 233 RVA: 0x00008630 File Offset: 0x00006830
		public DiusParser(ref Dius Parrent)
		{
			this.Z = new Dictionary<string, string>();
			this.List = new ArrayList();
			this._Parent = Parrent;
			this.Progress = this._Parent.Progress;
			this.S = new DiusParser.DiusScript(0);
			this.Func = new Functions(this);
			this.List.Add("If");
			this.List.Add("Goto");
			this.List.Add("Call");
			this.List.Add("Return");
			this.List.Add("Exit");
			this.List.Add("End");
			this.Z.Add("==", "Equal");
			this.Z.Add("<", "Less");
			this.Z.Add(">", "More");
			this.Z.Add("<>", "NotEqual");
			this.Z.Add(">=", "MoreOrEqual");
			this.Z.Add("<=", "LessOrEqual");
			this.Z.Add("++", "AddObject");
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008784 File Offset: 0x00006984
		public int Analyse(StringBuilder Text)
		{
			int num = 0;
			Text.Replace("\r", "");
			Text.Replace("\t", "");
			string[] array = Text.ToString().Split(new char[]
			{
				'\n'
			});
			int num2 = array.Count<string>() - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (this.Parse(ref array[i], ref num) == -1)
				{
					Interaction.MsgBox(string.Format("Error analyse on string: {0} Text: {1}", i + 1, array[i]), MsgBoxStyle.OkOnly, null);
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008820 File Offset: 0x00006A20
		public int Parse(ref string Text, ref int Row)
		{
			int result;
			if (Text.Count<char>() == 0)
			{
				result = 1;
			}
			else if (Text.StartsWith("'"))
			{
				result = 1;
			}
			else if (Text.Split(new char[]
			{
				' '
			})[0].EndsWith(":"))
			{
				this.S.Labels.Add(Text.Split(new char[]
				{
					' '
				})[0].Replace(":", ""), Conversions.ToString(Row));
				result = 1;
			}
			else
			{
				Queue queue = new Queue();
				while (Text.Contains("\""))
				{
					int num = Text.IndexOf("\"", 0);
					int num2 = Text.IndexOf("\"", num + 1);
					queue.Enqueue(Text.Substring(num, num2 - num + 1).Replace("\"", ""));
					Text = Text.Remove(num, num2 - num + 1);
					if (Text.Count<char>() == num)
					{
						Text += "<S>";
					}
					else
					{
						Text = Text.Insert(num, "<S>");
					}
				}
				Text = Text.Replace(" ", ",");
				Text = Text.Replace("(", ",");
				Text = Text.Replace(")", ",");
				Text = Text.Replace(",,,,", ",");
				Text = Text.Replace(",,,", ",");
				Text = Text.Replace(",,", ",");
				if (Operators.CompareString(Conversions.ToString(Text.Last<char>()), ",", false) == 0)
				{
					Text = Text.Substring(0, Text.Length - 1);
				}
				Row++;
				result = this.Parsing(ref Text, ref queue);
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000089F4 File Offset: 0x00006BF4
		public int Parsing(ref string Parse, ref Queue StrStack)
		{
			string text = "";
			string[] array = Parse.Split(new char[]
			{
				','
			});
			int num = array.Length - 1;
			for (int i = 0; i <= num; i++)
			{
				bool flag = this.List.Contains(array[i]);
				if (flag)
				{
					if (flag)
					{
						text += "C";
					}
				}
				else
				{
					bool flag2 = this.Func.F.ContainsKey(array[i]);
					if (flag2)
					{
						if (flag2)
						{
							text += "F";
						}
					}
					else
					{
						bool flag3 = this.Z.ContainsKey(array[i]);
						if (flag3)
						{
							if (flag3)
							{
								text += "Z";
							}
						}
						else if (Versioned.IsNumeric(array[i]))
						{
							text += "N";
						}
						else if (Versioned.IsNumeric(array[i].Replace(".", ",")))
						{
							text += "N";
							array[i] = array[i].Replace(".", ",");
						}
						else if (Operators.CompareString(array[i], "<S>", false) == 0)
						{
							text += "S";
							array[i] = Conversions.ToString(StrStack.Dequeue());
						}
						else if (Operators.CompareString(array[i], "=", false) == 0)
						{
							text += "=";
						}
						else if (array[i].StartsWith(":"))
						{
							text += "L";
							array[i] = array[i].Replace(":", "");
						}
						else if (array[i].Contains("."))
						{
							text += "M";
						}
						else
						{
							text += "V";
						}
					}
				}
			}
			this.S.Data.Add(array);
			this.S.Code.Add(text);
			return 0;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public int Execute()
		{
			string text = Conversions.ToString(this.S.Code[this.S.Line]);
			string[] array = (string[])this.S.Data[this.S.Line];
			string text2 = Conversions.ToString(this.S.Line) + ": ";
			int num = array.Length - 1;
			for (int i = 0; i <= num; i++)
			{
				text2 = text2 + array[i] + " ";
			}
			this._Parent.List.Items.Add(text2);
			this._Parent.List.SelectedIndex = this._Parent.List.Items.Count - 1;
			try
			{
				int pos = text.Length - 1;
				this.S.Pos = pos;
				while (this.S.Pos >= 0)
				{
					char c = text[this.S.Pos];
					if (c <= 'F')
					{
						if (c != '=')
						{
							if (c != 'C')
							{
								if (c == 'F')
								{
									this.Func.GetType().GetMethod(this.Func.F[array[this.S.Pos]], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(this.Func, null);
									if (this.S.State.Err == 1)
									{
										this.S.Flag = (this.S.Flag & 251);
										return -1;
									}
								}
							}
							else
							{
								string left = array[this.S.Pos];
								if (Operators.CompareString(left, "If", false) == 0)
								{
									if (Operators.ConditionalCompareObjectEqual(this.S.Stack.Pop(), true, false))
									{
										if ((this.S.Flag & 3) == 1)
										{
											this.S.Line = Conversions.ToInteger(Operators.SubtractObject(this.S.Jump.Pop(), 1));
										}
										else
										{
											int line = this.S.Line;
											if (Operators.CompareString(array[array.Count<string>() - 1], "Return", false) == 0)
											{
												this.S.Line = Conversions.ToInteger(this.S.Jump.Pop());
											}
											else
											{
												this.S.Line = Conversions.ToInteger(Operators.SubtractObject(this.S.Jump.Pop(), 1));
												this.S.Jump.Push(line);
											}
										}
									}
									else if (Operators.CompareString(array[array.Count<string>() - 1], "Return", false) != 0)
									{
										this.S.Jump.Pop();
									}
									this.S.Flag = (this.S.Flag & 252);
									ref int ptr = ref this.S.Line;
									this.S.Line = ptr + 1;
									return 0;
								}
								if (Operators.CompareString(left, "Return", false) != 0)
								{
									if (Operators.CompareString(left, "Exit", false) == 0)
									{
										this.S.Flag = (this.S.Flag ^ 4);
										return 1;
									}
									if (Operators.CompareString(left, "End", false) == 0)
									{
										this._Parent.Panel.Visible = true;
										Interaction.Beep();
										this.S.Flag = (this.S.Flag | 8);
										return 2;
									}
								}
								else if (this.S.Pos == 0)
								{
									this.S.Line = Conversions.ToInteger(this.S.Jump.Pop());
								}
							}
						}
						else
						{
							ref int ptr = ref this.S.Pos;
							this.S.Pos = ptr - 1;
							if (Operators.CompareString(Conversions.ToString(text[this.S.Pos]), "V", false) == 0)
							{
								this.GetValue();
							}
							else if (Operators.CompareString(Conversions.ToString(text[this.S.Pos]), "M", false) == 0)
							{
								string[] array2 = array[this.S.Pos].Split(new char[]
								{
									'.'
								});
								if (array2.Count<string>() > 2)
								{
									int num2 = array2.Count<string>() - 1;
									for (int j = 2; j <= num2; j++)
									{
										array2[1] = array2[1] + "." + array2[j];
									}
								}
								Versioned.CallByName(RuntimeHelpers.GetObjectValue(this.S.V[array2[0]]), array2[1], CallType.Set, new object[]
								{
									this.S.Stack.Pop()
								});
							}
						}
					}
					else if (c <= 'S')
					{
						switch (c)
						{
						case 'L':
						{
							this.S.Jump.Push(this.S.Labels[array[this.S.Pos]]);
							ref int ptr = ref this.S.Pos;
							this.S.Pos = ptr - 1;
							if (Operators.CompareString(array[this.S.Pos], "Call", false) == 0)
							{
								this.S.Flag = (this.S.Flag | 2);
								goto IL_825;
							}
							this.S.Flag = (this.S.Flag | 1);
							goto IL_825;
						}
						case 'M':
						{
							string[] array3 = array[this.S.Pos].Split(new char[]
							{
								'.'
							});
							if (Operators.CompareString(array3[1], "", false) == 0)
							{
								this.S.Stack.Push(RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(this.S.V[array3[0]], new object[]
								{
									this.S.Stack.Pop()
								}, null)));
								goto IL_825;
							}
							switch (Conversions.ToInteger(this.S.Stack.Pop()))
							{
							case 0:
								this.S.Stack.Push(RuntimeHelpers.GetObjectValue(Versioned.CallByName(RuntimeHelpers.GetObjectValue(this.S.V[array3[0]]), array3[1], CallType.Method, null)));
								break;
							case 1:
								this.S.Stack.Push(RuntimeHelpers.GetObjectValue(Versioned.CallByName(RuntimeHelpers.GetObjectValue(this.S.V[array3[0]]), array3[1], CallType.Get, new object[]
								{
									this.S.Stack.Pop()
								})));
								break;
							case 2:
								this.S.Stack.Push(RuntimeHelpers.GetObjectValue(Versioned.CallByName(RuntimeHelpers.GetObjectValue(this.S.V[array3[0]]), array3[1], CallType.Get, new object[]
								{
									this.S.Stack.Pop(),
									this.S.Stack.Pop()
								})));
								break;
							case 3:
								this.S.Stack.Push(RuntimeHelpers.GetObjectValue(Versioned.CallByName(RuntimeHelpers.GetObjectValue(this.S.V[array3[0]]), array3[1], CallType.Get, new object[]
								{
									this.S.Stack.Pop(),
									this.S.Stack.Pop(),
									this.S.Stack.Pop()
								})));
								break;
							}
							if (Information.IsNothing(RuntimeHelpers.GetObjectValue(this.S.Stack.Peek())))
							{
								this.S.Stack.Pop();
								goto IL_825;
							}
							goto IL_825;
						}
						case 'N':
							break;
						default:
							if (c != 'S')
							{
								goto IL_825;
							}
							break;
						}
						this.S.Stack.Push(array[this.S.Pos]);
					}
					else if (c != 'V')
					{
						if (c == 'Z')
						{
							this.CompareValues(ref array[this.S.Pos]);
						}
					}
					else
					{
						this.SetValue();
					}
					IL_825:
					this.S.Pos = this.S.Pos + -1;
				}
				if ((this.S.Flag & 1) == 1)
				{
					this.S.Flag = (this.S.Flag & 254);
					this.S.Line = Conversions.ToInteger(this.S.Jump.Pop());
				}
				else if ((this.S.Flag & 2) == 2)
				{
					this.S.Flag = (this.S.Flag & 253);
					int line2 = Conversions.ToInteger(this.S.Jump.Pop());
					this.S.Jump.Push(this.S.Line);
					this.S.Line = line2;
				}
				else
				{
					ref int ptr = ref this.S.Line;
					this.S.Line = ptr + 1;
				}
			}
			catch (Exception ex)
			{
				this.S.State.Err = -1;
				this.S.State.Message = ex.Message;
				this.S.Flag = (this.S.Flag & 251);
				return -1;
			}
			return 0;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000958C File Offset: 0x0000778C
		public void SetValue()
		{
			if (this.S.V.ContainsKey(Conversions.ToString(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
			{
				this.S.Pos
			}, null))))
			{
				this.S.Stack.Push(RuntimeHelpers.GetObjectValue(this.S.V[Conversions.ToString(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
				{
					this.S.Pos
				}, null))]));
				return;
			}
			this.S.Stack.Push(RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
			{
				this.S.Pos
			}, null)));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000096A0 File Offset: 0x000078A0
		public void GetValue()
		{
			if (this.S.V.ContainsKey(Conversions.ToString(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
			{
				this.S.Pos
			}, null))))
			{
				if (this.S.Stack.Count == 0)
				{
					this.S.V[Conversions.ToString(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
					{
						this.S.Pos
					}, null))] = null;
					return;
				}
				this.S.V[Conversions.ToString(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
				{
					this.S.Pos
				}, null))] = RuntimeHelpers.GetObjectValue(this.S.Stack.Pop());
				return;
			}
			else
			{
				if (this.S.Stack.Count == 0)
				{
					this.S.V.Add(Conversions.ToString(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
					{
						this.S.Pos
					}, null)), null);
					return;
				}
				this.S.V.Add(Conversions.ToString(NewLateBinding.LateIndexGet(this.S.Data[this.S.Line], new object[]
				{
					this.S.Pos
				}, null)), RuntimeHelpers.GetObjectValue(this.S.Stack.Pop()));
				return;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009890 File Offset: 0x00007A90
		public void CompareValues(ref string Compare)
		{
			if (Operators.CompareString(Compare, "++", false) == 0)
			{
				string left = Conversions.ToString(this.S.Stack.Pop());
				if (Operators.CompareString(left, "VCI", false) == 0)
				{
					VCI obj = new VCI((DataTable)this.S.Stack.Pop());
					this.S.Stack.Push(obj);
					return;
				}
				if (Operators.CompareString(left, "Analyzer", false) != 0)
				{
					return;
				}
				Analyzer obj2 = new Analyzer();
				this.S.Stack.Push(obj2);
				return;
			}
			else
			{
				string text = Conversions.ToString(this.S.Stack.Pop());
				string text2 = Conversions.ToString(this.S.Stack.Pop());
				if (Versioned.IsNumeric(text))
				{
					double num = Conversions.ToDouble(text);
					double num2 = Conversions.ToDouble(text2);
					string left2 = Compare;
					if (Operators.CompareString(left2, "==", false) == 0)
					{
						this.S.Stack.Push(num == num2);
						return;
					}
					if (Operators.CompareString(left2, "<>", false) == 0)
					{
						this.S.Stack.Push(num != num2);
						return;
					}
					if (Operators.CompareString(left2, ">", false) == 0)
					{
						this.S.Stack.Push(num > num2);
						return;
					}
					if (Operators.CompareString(left2, "<", false) == 0)
					{
						this.S.Stack.Push(num < num2);
						return;
					}
					if (Operators.CompareString(left2, ">=", false) == 0)
					{
						this.S.Stack.Push(num >= num2);
						return;
					}
					if (Operators.CompareString(left2, "<=", false) != 0)
					{
						return;
					}
					this.S.Stack.Push(num <= num2);
					return;
				}
				else
				{
					string left3 = Compare;
					if (Operators.CompareString(left3, "==", false) == 0)
					{
						this.S.Stack.Push(string.Equals(text, text2));
						return;
					}
					if (Operators.CompareString(left3, "<>", false) != 0)
					{
						return;
					}
					this.S.Stack.Push(!string.Equals(text, text2));
					return;
				}
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public void Event_Distributor(object sender, EventArgs e)
		{
			VCI.msgEvent msgEvent = new VCI.msgEvent("");
			string name = sender.GetType().Name;
			if (Operators.CompareString(name, "RadGridView", false) != 0)
			{
				if (Operators.CompareString(name, "GridViewEditManager", false) != 0)
				{
					if (Operators.CompareString(name, "GridDataCellElement", false) != 0)
					{
						msgEvent.Label = Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
					}
					else
					{
						GridViewCellEventArgs gridViewCellEventArgs = (GridViewCellEventArgs)e;
						msgEvent.Label = Conversions.ToString(gridViewCellEventArgs.Column.Tag);
						msgEvent.Data.Enqueue(gridViewCellEventArgs.Row);
					}
				}
				else
				{
					GridViewCellEventArgs gridViewCellEventArgs2 = (GridViewCellEventArgs)e;
					msgEvent.Label = Conversions.ToString(gridViewCellEventArgs2.Column.Tag);
					msgEvent.Data.Enqueue(gridViewCellEventArgs2.Row);
				}
			}
			else
			{
				CurrentRowChangedEventArgs currentRowChangedEventArgs = (CurrentRowChangedEventArgs)e;
				if (Information.IsNothing(currentRowChangedEventArgs.CurrentRow))
				{
					return;
				}
				msgEvent.Label = Conversions.ToString(NewLateBinding.LateGet(sender, null, "tag", new object[0], null, null, null));
				msgEvent.Data.Enqueue(currentRowChangedEventArgs.CurrentRow);
			}
			if (msgEvent.Label.Length != 0)
			{
				this._Parent.sEvents.Enqueue(msgEvent);
			}
		}

		// Token: 0x040000B6 RID: 182
		internal Dictionary<string, string> Z;

		// Token: 0x040000B7 RID: 183
		internal DiusParser.DiusScript S;

		// Token: 0x040000B8 RID: 184
		public Functions Func;

		// Token: 0x040000B9 RID: 185
		private ArrayList List;

		// Token: 0x040000BB RID: 187
		internal RadProgressBar Progress;

		// Token: 0x040000BC RID: 188
		private static Thread Exec;

		// Token: 0x0200003E RID: 62
		[Serializable]
		public struct ParserState
		{
			// Token: 0x0400017E RID: 382
			public int Err;

			// Token: 0x0400017F RID: 383
			public string Message;
		}

		// Token: 0x0200003F RID: 63
		[Serializable]
		public struct DiusScript
		{
			// Token: 0x060001B2 RID: 434 RVA: 0x00010790 File Offset: 0x0000E990
			public DiusScript(int Options)
			{
				this = default(DiusParser.DiusScript);
				this.Code = new ArrayList();
				this.Data = new ArrayList();
				this.Labels = new Dictionary<string, string>();
				this.V = new Dictionary<string, object>();
				this.Stack = new Stack();
				this.R = new Queue();
				this.Jump = new Stack();
			}

			// Token: 0x04000180 RID: 384
			public ArrayList Code;

			// Token: 0x04000181 RID: 385
			public ArrayList Data;

			// Token: 0x04000182 RID: 386
			public Dictionary<string, string> Labels;

			// Token: 0x04000183 RID: 387
			public Dictionary<string, object> V;

			// Token: 0x04000184 RID: 388
			public Stack Stack;

			// Token: 0x04000185 RID: 389
			public Queue R;

			// Token: 0x04000186 RID: 390
			public DiusParser.ParserState State;

			// Token: 0x04000187 RID: 391
			public int Line;

			// Token: 0x04000188 RID: 392
			public int Pos;

			// Token: 0x04000189 RID: 393
			public int Flag;

			// Token: 0x0400018A RID: 394
			public Stack Jump;

			// Token: 0x0400018B RID: 395
			public Stack EventStack;
		}

		// Token: 0x02000040 RID: 64
		public enum ScriptState : byte
		{
			// Token: 0x0400018D RID: 397
			GoTo = 1,
			// Token: 0x0400018E RID: 398
			Proc,
			// Token: 0x0400018F RID: 399
			Jump,
			// Token: 0x04000190 RID: 400
			Run,
			// Token: 0x04000191 RID: 401
			Stop = 8,
			// Token: 0x04000192 RID: 402
			Event = 16
		}
	}
}
