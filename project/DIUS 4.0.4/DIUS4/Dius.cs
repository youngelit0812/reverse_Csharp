using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DIUS4.My;
using DIUS4.Shield;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace DIUS4
{
	// Token: 0x0200001D RID: 29
	[DesignerGenerated]
	public partial class Dius : RadForm
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000E50C File Offset: 0x0000C70C
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000E514 File Offset: 0x0000C714
		private virtual DiusParser Dp { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000145 RID: 325 RVA: 0x0000E520 File Offset: 0x0000C720
		public Dius()
		{
			base.Closing += new CancelEventHandler(this.Dius_Closed);
			base.KeyDown += this.Dius_KeyDown;
			base.Shown += this.Dius_Shown;
			this.Script = new StringBuilder();
			this.regKey = Registry.CurrentUser.CreateSubKey("Software\\DIUS4");
			this.sEvents = new Queue();
			this.mProtect = new Protect(ref this.regKey);
			RadMessageBox.SetThemeName("Office2010Black");
			RadMessageBox.ShowInTaskbar = true;
			if (Operators.ConditionalCompareObjectNotEqual(this.regKey.GetValue("State", "0"), 0, false))
			{
				MyProject.Forms.Disclaimer.ShowDialog();
				if (this.regKey.GetValue("Version", "").ToString().Length == 0)
				{
					SqlFunc sqlFunc = new SqlFunc();
					sqlFunc.SetPassword("\\Data\\Updates.dsb");
					DataTable dataTable = sqlFunc.Get_DataTable("SELECT * FROM files WHERE filename <> '\\Data\\updates.dsb'", "\\Data\\updates.dsb");
					int num = dataTable.Rows.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						string text = Conversions.ToString(dataTable.AsEnumerable().ElementAtOrDefault(i)["filename"]);
						if (text.Contains(".ds"))
						{
							sqlFunc.SetPassword(text);
							dataTable.AsEnumerable().ElementAtOrDefault(i)["hash"] = this.mProtect.FileTimeHash(ref text);
						}
					}
					sqlFunc.StoreDataTable("\\Data\\Updates.dsb", "files", dataTable);
					Protect protect = this.mProtect;
					string left = "\\Data\\Updates.dsb";
					protect.StoreFileHash(ref left);
				}
				this.regKey.SetValue("Version", Application.ProductVersion);
				this.regKey.SetValue("State", "0");
			}
			this.InitializeComponent();
			this.List.Location = new Point(1, 49);
			this.List.Width = this.Panel.Width - 11;
			this.TitleMail.Text = Conversions.ToString(this.regKey.GetValue("Email", "Not Registered"));
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			if (commandLineArgs.Count<string>() == 1)
			{
				this.Script.Append(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("DIUS4.DIUS 4.vbs")).ReadToEnd());
				this.TitleBar.Text = "DIUS 4";
			}
			else
			{
				this.ScriptName = commandLineArgs[2];
				this.TitleBar.Text = this.ScriptName.Split(new char[]
				{
					'.'
				})[0].Replace("_", " ");
				string left = commandLineArgs[1];
				if (Operators.CompareString(left, "File", false) != 0)
				{
					if (Operators.CompareString(left, "Base", false) != 0)
					{
						if (Operators.CompareString(left, "Internal", false) != 0)
						{
							Application.Exit();
						}
						else
						{
							this.Script.Append(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("DIUS4." + this.ScriptName)).ReadToEnd());
						}
					}
					else
					{
						DataTable source = new SqlFunc().Get_DataTable(string.Format("SELECT binary FROM script WHERE name = '{0}'", this.ScriptName), "\\Data\\DIUS.dsb");
						this.Script.Append(new StreamReader(new MemoryStream((byte[])source.AsEnumerable().ElementAtOrDefault(0)[0])).ReadToEnd());
					}
				}
				else
				{
					this.Script.Append(new StreamReader(new FileStream(this.ScriptName, FileMode.Open)).ReadToEnd());
				}
			}
			this.Text = this.TitleBar.Text;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		private void lstVariables_SelectedItemChanged(object sender, EventArgs e)
		{
			RadListViewElement radListViewElement = (RadListViewElement)sender;
			if (!Information.IsNothing(radListViewElement.SelectedItem))
			{
				this.prop.SelectedObject = RuntimeHelpers.GetObjectValue(this.Dp.S.V[radListViewElement.SelectedItem.Value.ToString().Split(new char[]
				{
					','
				})[0].Replace("[", "")]);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000E948 File Offset: 0x0000CB48
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void Run()
		{
			this.Dp.S.Flag = 4;
			for (;;)
			{
				Thread.Sleep(50);
				Application.DoEvents();
				int num = this.Dp.S.Flag & 28;
				if (num <= 12)
				{
					if (num != 4)
					{
						if (num == 8 || num == 12)
						{
							break;
						}
					}
					else
					{
						while (this.Dp.S.Flag == 4)
						{
							int num2 = this.Dp.Execute();
							if (num2 == -1)
							{
								Interaction.MsgBox(this.Dp.S.State.Message, MsgBoxStyle.OkOnly, null);
								break;
							}
							if (num2 == 2)
							{
								ProjectData.EndApp();
							}
						}
					}
				}
				else if (num <= 20)
				{
					if (num == 16 || num == 20)
					{
						this.Dp.S.Flag = 4;
						object obj = this.sEvents.Dequeue();
						VCI.msgEvent msgEvent = (obj != null) ? ((VCI.msgEvent)obj) : default(VCI.msgEvent);
						if (msgEvent.Data.Count > 0)
						{
							int num3 = msgEvent.Data.Count - 1;
							for (int i = 0; i <= num3; i++)
							{
								this.Dp.S.Stack.Push(RuntimeHelpers.GetObjectValue(msgEvent.Data.Dequeue()));
							}
						}
						this.Dp.S.Jump.Push(this.Dp.S.Line - 1);
						this.Dp.S.Line = Conversions.ToInteger(this.Dp.S.Labels[msgEvent.Label]);
					}
				}
				else if (num == 24 || num == 28)
				{
					break;
				}
				if (this.sEvents.Count > 0)
				{
					this.Dp.S.Flag = (this.Dp.S.Flag | 16);
				}
			}
			this.Dp.S.Flag = (this.Dp.S.Flag ^ 8);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000EB4A File Offset: 0x0000CD4A
		private void Dius_Closed(object sender, EventArgs e)
		{
			this.Reset();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000EB54 File Offset: 0x0000CD54
		private void Restart()
		{
			if (!this.$STATIC$Restart$2001$First)
			{
				this.$STATIC$Restart$2001$First = true;
				Dius dius = this;
				this.Dp = new DiusParser(ref dius);
				this.Dp.Analyse(this.Script);
			}
			else
			{
				this.Reset();
			}
			this.Dp.S.V.Add("Stack", this.Dp.S.Stack);
			this.Dp.S.V.Add("Jump", this.Dp.S.Jump);
			this.Dp.S.V.Add("Labels", this.Dp.S.Labels);
			this.Dp.S.V.Add("Events", this.sEvents);
			this.Dp.S.V.Add("Panel", this.Dp._Parent.Panel);
			this.Dp.S.V.Add("RegKey", this.regKey);
			this.Dp.S.V.Add("Protect", this.mProtect);
			this.Dp.S.V.Add("Network", new Network(ref this.Dp.S.Stack));
			this.Dp.S.V.Add("MyDealerId", MySettingsProperty.Settings.Dealer);
			this.Dp.S.V.Add("_Progress", this.Progress);
			this.Dp.S.Line = 0;
			this.Dp.S.Stack.Clear();
			this.Run();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000ED40 File Offset: 0x0000CF40
		private void Reset()
		{
			this.Dp.S.Flag = 8;
			this.Dp.S.Line = Conversions.ToInteger(this.Dp.S.Labels["RESET"]);
			this.Dp.S.Flag = 4;
			while (this.Dp.S.Flag == 4)
			{
				if (this.Dp.Execute() == -1)
				{
					Interaction.MsgBox(this.Dp.S.State.Message, MsgBoxStyle.OkOnly, null);
					break;
				}
			}
			int num = this.Dp.S.V.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				string key = this.Dp.S.V.Keys.ElementAtOrDefault(i);
				this.Dp.S.V[key] = null;
			}
			this.Dp.S.V.Clear();
			this.Panel.Controls.Clear();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000EE5C File Offset: 0x0000D05C
		private void Dius_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.Dp.S.Flag == 0)
			{
				int keyValue = e.KeyValue;
				if (keyValue == 112)
				{
					this.Panel.Visible = this.$STATIC$Dius_KeyDown$20211C1281FD$Diag;
					this.$STATIC$Dius_KeyDown$20211C1281FD$Diag = !this.$STATIC$Dius_KeyDown$20211C1281FD$Diag;
					return;
				}
				if (keyValue == 116)
				{
					this.Run();
					return;
				}
				if (keyValue != 123)
				{
					return;
				}
				this.Restart();
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		private void Dius_Shown(object sender, EventArgs e)
		{
			this.Restart();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000F815 File Offset: 0x0000DA15
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000F820 File Offset: 0x0000DA20
		internal virtual RadListView lstVariables
		{
			[CompilerGenerated]
			get
			{
				return this._lstVariables;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.lstVariables_SelectedItemChanged);
				RadListView lstVariables = this._lstVariables;
				if (lstVariables != null)
				{
					lstVariables.SelectedItemChanged -= value2;
				}
				this._lstVariables = value;
				lstVariables = this._lstVariables;
				if (lstVariables != null)
				{
					lstVariables.SelectedItemChanged += value2;
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000F863 File Offset: 0x0000DA63
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000F86B File Offset: 0x0000DA6B
		internal virtual RadPageView Panel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000F874 File Offset: 0x0000DA74
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000F87C File Offset: 0x0000DA7C
		internal virtual RadPropertyGrid prop { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000F885 File Offset: 0x0000DA85
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000F88D File Offset: 0x0000DA8D
		internal virtual ListBox List { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000F896 File Offset: 0x0000DA96
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000F89E File Offset: 0x0000DA9E
		internal virtual Office2010BlackTheme Office2010BlackTheme1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000F8A7 File Offset: 0x0000DAA7
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000F8AF File Offset: 0x0000DAAF
		internal virtual RadProgressBar Progress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000F8B8 File Offset: 0x0000DAB8
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		internal virtual RadTextBox TitleBar { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000F8C9 File Offset: 0x0000DAC9
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
		internal virtual RadTextBox TitleMail { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x040000D7 RID: 215
		private string ScriptName;

		// Token: 0x040000D8 RID: 216
		private StringBuilder Script;

		// Token: 0x040000DA RID: 218
		internal RegistryKey regKey;

		// Token: 0x040000DB RID: 219
		internal Queue sEvents;

		// Token: 0x040000DC RID: 220
		internal Protect mProtect;

		// Token: 0x040000E6 RID: 230
		private bool $STATIC$Restart$2001$First;

		// Token: 0x040000E7 RID: 231
		private bool $STATIC$Dius_KeyDown$20211C1281FD$Diag;

		// Token: 0x02000042 RID: 66
		public enum ss : byte
		{
			// Token: 0x0400019C RID: 412
			GoTo = 1,
			// Token: 0x0400019D RID: 413
			Proc,
			// Token: 0x0400019E RID: 414
			Jump,
			// Token: 0x0400019F RID: 415
			Run,
			// Token: 0x040001A0 RID: 416
			Stop = 8,
			// Token: 0x040001A1 RID: 417
			Event = 16
		}
	}
}
