using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using DIUS4.Adapters;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Telerik.WinControls.UI;

namespace DIUS4
{
	// Token: 0x0200001E RID: 30
	[DesignerGenerated]
	public class VCI : UserControl
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000F8DC File Offset: 0x0000DADC
		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000F91C File Offset: 0x0000DB1C
		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new Container();
			this.tmrVCIDetect = new System.Windows.Forms.Timer(this.components);
			base.SuspendLayout();
			this.tmrVCIDetect.Interval = 1000;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "VCI";
			base.Size = new Size(200, 200);
			base.ResumeLayout(false);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000F99E File Offset: 0x0000DB9E
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		internal virtual System.Windows.Forms.Timer tmrVCIDetect
		{
			[CompilerGenerated]
			get
			{
				return this._tmrVCIDetect;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.tmrVCIDetect_Tick);
				System.Windows.Forms.Timer tmrVCIDetect = this._tmrVCIDetect;
				if (tmrVCIDetect != null)
				{
					tmrVCIDetect.Tick -= value2;
				}
				this._tmrVCIDetect = value;
				tmrVCIDetect = this._tmrVCIDetect;
				if (tmrVCIDetect != null)
				{
					tmrVCIDetect.Tick += value2;
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000F9EB File Offset: 0x0000DBEB
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		private virtual Adapter _Adapter
		{
			[CompilerGenerated]
			get
			{
				return this.__Adapter;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				Adapter.OnReceiveMessage obj = new Adapter.OnReceiveMessage(this.Adapter_CanMessageEvent);
				Adapter _Adapter = this.__Adapter;
				if (_Adapter != null)
				{
					_Adapter.CanMessageEvent -= obj;
				}
				this.__Adapter = value;
				_Adapter = this.__Adapter;
				if (_Adapter != null)
				{
					_Adapter.CanMessageEvent += obj;
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000FA38 File Offset: 0x0000DC38
		public VCI(DataTable Adapter)
		{
			base.MouseClick += this.VCI_MouseClick;
			this._Protocol = "";
			this._EchoList = new ArrayList();
			this._Devices = new ArrayList();
			this.Devices = new ArrayList();
			this.dtAdapter = Adapter;
			this.InitializeComponent();
			this.lbl = new RadLabel
			{
				Font = new Font("Segoe UI", 11f, FontStyle.Bold),
				ForeColor = Color.White,
				BackColor = Color.Transparent,
				Text = Conversions.ToString(this.dtAdapter.AsEnumerable().ElementAtOrDefault(0)["name"]),
				AutoSize = false,
				TextAlignment = ContentAlignment.TopCenter,
				Width = base.Width - 2,
				Height = 22,
				Top = 5,
				Left = 1
			};
			base.Controls.Add(this.lbl);
			this.BackgroundImage = Image.FromStream(new MemoryStream((byte[])this.dtAdapter.AsEnumerable().ElementAtOrDefault(0)["picture"]));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000FB68 File Offset: 0x0000DD68
		public void Close()
		{
			if (this.tmrVCIDetect.Enabled)
			{
				this.tmrVCIDetect.Enabled = false;
			}
			if (this.Devices.Count != 0)
			{
				int num = this.Devices.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					((Device)this.Devices[i]).Run = false;
					Application.DoEvents();
				}
			}
			if (this._Adapter.IsOpen)
			{
				this._Adapter.Close();
			}
			this._Adapter = null;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000FBFF File Offset: 0x0000DDFF
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000FBF1 File Offset: 0x0000DDF1
		public bool VciDetect
		{
			get
			{
				return this.tmrVCIDetect.Enabled;
			}
			set
			{
				this.tmrVCIDetect.Enabled = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000FCD5 File Offset: 0x0000DED5
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		public string Protocol
		{
			get
			{
				return this._Protocol;
			}
			set
			{
				this._Protocol = value;
				this.tmrVCIDetect.Enabled = false;
				string[] array = this._Protocol.Split(new char[]
				{
					','
				});
				string left = array[0];
				if (Operators.CompareString(left, "ISO-14230", false) != 0)
				{
					if (Operators.CompareString(left, "ISO-15765", false) == 0)
					{
						this._Adapter.Mode = true;
					}
				}
				else
				{
					this._Adapter.Mode = false;
				}
				this._Adapter.BitRate = array[1];
				this._Adapter.Filter = array[2];
				this._Adapter.Mask = array[3];
				this._Address = (int)Math.Round(Conversion.Val("&H" + array[4]));
				this._Count = Conversions.ToInteger(array[5]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000FCDD File Offset: 0x0000DEDD
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000FCE5 File Offset: 0x0000DEE5
		public new Queue Events { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public DataTable DevicesDetect(DataTable DevList)
		{
			DataTable dataTable = DevList.Clone();
			this._EchoList.Clear();
			this._Devices.Clear();
			this.Devices.Clear();
			this._Adapter.Open();
			int num = DevList.Rows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				this._EchoList.Add(RuntimeHelpers.GetObjectValue(DevList.AsEnumerable().ElementAtOrDefault(i)["echo"]));
				VCI_Message vci_Message = new VCI_Message(8)
				{
					ID = (uint)Math.Round(Conversion.Val(Operators.ConcatenateObject("&H", DevList.AsEnumerable().ElementAtOrDefault(i)["address"])))
				};
				vci_Message.Data[0] = Conversions.ToByte(DevList.AsEnumerable().ElementAtOrDefault(i)["burst"]);
				vci_Message.Data[(true + -((this._Adapter.Mode > false) ? 1 : 0)) ? 1 : 0] = 1;
				vci_Message.Data[2 + ((-((this._Adapter.Mode > false) ? 1 : 0)) ? 1 : 0)] = (byte)Math.Round(Conversion.Val("&H3E"));
				this._Adapter.Send(vci_Message);
			}
			Application.DoEvents();
			Thread.Sleep(300);
			int num2 = this._Devices.Count - 1;
			for (int j = 0; j <= num2; j++)
			{
				DataRow dataRow = DevList.Select(string.Format("echo = {0}", RuntimeHelpers.GetObjectValue(this._Devices[j])))[0];
				dataTable.ImportRow(dataRow);
				Device value = new Device
				{
					Name = Conversions.ToString(dataRow["name"]),
					Protocol = Conversions.ToString(dataRow["protocol"]),
					Address = (int)Math.Round(Conversion.Val(Operators.ConcatenateObject("&H", dataRow["address"]))),
					Echo = (int)Math.Round(Conversion.Val(Operators.ConcatenateObject("&H", dataRow["echo"]))),
					Adapter = this._Adapter
				};
				this.Devices.Add(value);
			}
			dataTable.TableName = Conversions.ToString(this._Devices.Count);
			if (this._Devices.Count == 0)
			{
				this._Adapter.Close();
			}
			this._EchoList.Clear();
			return dataTable;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000FF60 File Offset: 0x0000E160
		private void Adapter_CanMessageEvent(Queue CANMessage)
		{
			int num = CANMessage.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				object obj = CANMessage.Dequeue();
				VCI_Message vci_Message = (obj != null) ? ((VCI_Message)obj) : default(VCI_Message);
				try
				{
					foreach (object obj2 in this.Devices)
					{
						Device device = (Device)obj2;
						if ((long)device.Echo == (long)((ulong)vci_Message.ID))
						{
							device.MessagesIn.Enqueue(vci_Message);
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
				if (this._EchoList.Count > 0)
				{
					string text = vci_Message.ID.ToString("X8");
					if (this._EchoList.Contains(text))
					{
						this._EchoList.Remove(text);
						this._Devices.Add(text);
					}
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00010064 File Offset: 0x0000E264
		public Device GetDevice(string Name)
		{
			try
			{
				foreach (object obj in this.Devices)
				{
					Device device = (Device)obj;
					if (Operators.CompareString(device.Name, Name, false) == 0)
					{
						return device;
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
			return null;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000100D0 File Offset: 0x0000E2D0
		private void tmrVCIDetect_Tick(object sender, EventArgs e)
		{
			this.tmrVCIDetect.Enabled = false;
			if (this._AdapterType == 0)
			{
				int num = this.dtAdapter.Rows.Count - 1;
				for (int i = 1; i <= num; i++)
				{
					object left = this.dtAdapter.AsEnumerable().ElementAtOrDefault(i)["type"];
					if (Operators.ConditionalCompareObjectEqual(left, 4, false))
					{
						this._Adapter = new ZLG();
					}
					else if (Operators.ConditionalCompareObjectEqual(left, 100, false))
					{
						this._Adapter = new MPI();
					}
					else if (Operators.ConditionalCompareObjectEqual(left, 101, false))
					{
						this._Adapter = new ULA();
					}
					try
					{
						if (this._Adapter.AutoDetect())
						{
							this._AdapterType = i;
							break;
						}
					}
					catch (Exception ex)
					{
						this._AdapterType = 0;
					}
					Application.DoEvents();
				}
			}
			else
			{
				try
				{
					if (!this._Adapter.AutoDetect())
					{
						this._AdapterType = 0;
					}
				}
				catch (Exception ex2)
				{
					this._AdapterType = 0;
				}
			}
			if (this.$STATIC$tmrVCIDetect_Tick$20211C1280A1$AdapterType != this._AdapterType)
			{
				this.$STATIC$tmrVCIDetect_Tick$20211C1280A1$AdapterType = this._AdapterType;
				VCI.msgEvent msgEvent = new VCI.msgEvent("EVENT_VCI_DETECT");
				if (this._AdapterType != 0)
				{
					msgEvent.Data.Enqueue("---");
					msgEvent.Data.Enqueue("---");
					msgEvent.Data.Enqueue(this._Adapter.VciInfo.ID);
					msgEvent.Data.Enqueue(this._Adapter.VciInfo.HW);
					msgEvent.Data.Enqueue(this._Adapter.VciInfo.SW);
					msgEvent.Data.Enqueue(this._Adapter.VciInfo.Manufacturer);
					msgEvent.Data.Enqueue(this._Adapter.VciInfo.Description);
				}
				else
				{
					msgEvent.Data.Enqueue("");
					msgEvent.Data.Enqueue("");
					msgEvent.Data.Enqueue("");
					msgEvent.Data.Enqueue("");
					msgEvent.Data.Enqueue("");
					msgEvent.Data.Enqueue(RuntimeHelpers.GetObjectValue(this.dtAdapter.Rows[this._AdapterType]["name"]));
					msgEvent.Data.Enqueue("Not Connected");
					this._AdapterType = 0;
				}
				msgEvent.Data.Enqueue(RuntimeHelpers.GetObjectValue(this.dtAdapter.Rows[this._AdapterType]["type"]));
				this.Events.Enqueue(msgEvent);
				this.lbl.Text = Conversions.ToString(this.dtAdapter.Rows[this._AdapterType]["name"]);
				this.BackgroundImage = Image.FromStream(new MemoryStream((byte[])this.dtAdapter.AsEnumerable().ElementAtOrDefault(this.$STATIC$tmrVCIDetect_Tick$20211C1280A1$AdapterType)["picture"]));
			}
			this.tmrVCIDetect.Enabled = true;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00010440 File Offset: 0x0000E640
		private void VCI_MouseClick(object sender, MouseEventArgs e)
		{
			if (this._AdapterType != 0 && this._Devices.Count == 0)
			{
				VCI.msgEvent msgEvent = new VCI.msgEvent("EVENT_VCI_CLICK");
				this.Events.Enqueue(msgEvent);
			}
		}

		// Token: 0x040000E8 RID: 232
		private IContainer components;

		// Token: 0x040000EA RID: 234
		private DataTable dtAdapter;

		// Token: 0x040000EC RID: 236
		private int _AdapterType;

		// Token: 0x040000ED RID: 237
		private string _Protocol;

		// Token: 0x040000EE RID: 238
		private int _Address;

		// Token: 0x040000EF RID: 239
		private int _Count;

		// Token: 0x040000F0 RID: 240
		private ArrayList _EchoList;

		// Token: 0x040000F1 RID: 241
		private ArrayList _Devices;

		// Token: 0x040000F2 RID: 242
		private ArrayList Devices;

		// Token: 0x040000F3 RID: 243
		private RadLabel lbl;

		// Token: 0x040000F5 RID: 245
		private int $STATIC$tmrVCIDetect_Tick$20211C1280A1$AdapterType;

		// Token: 0x02000043 RID: 67
		public struct msgEvent
		{
			// Token: 0x060001B3 RID: 435 RVA: 0x000107F1 File Offset: 0x0000E9F1
			public msgEvent(string strLabel)
			{
				this = default(VCI.msgEvent);
				this.Label = strLabel;
				this.Data = new Queue();
			}

			// Token: 0x040001A2 RID: 418
			public string Label;

			// Token: 0x040001A3 RID: 419
			public Queue Data;
		}
	}
}
