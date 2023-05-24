using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4.Adapters
{
	// Token: 0x02000012 RID: 18
	internal class ZLG : Adapter
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003A98 File Offset: 0x00001C98
		public override bool Connect()
		{
			bool result;
			if (ControlCAN.VCI_OpenDevice(this.m_devtype, this.m_devind, this.m_cannum) == 1)
			{
				ControlCAN.VCI_BOARD_INFO vci_BOARD_INFO = default(ControlCAN.VCI_BOARD_INFO);
				if (ControlCAN.VCI_ReadBoardInfo(this.m_devtype, this.m_devind, ref vci_BOARD_INFO) == 1)
				{
					this.VciInfo.ID = Encoding.UTF8.GetString(vci_BOARD_INFO.str_Serial_Num).Replace("\0", "");
					this.VciInfo.Description = Encoding.UTF8.GetString(vci_BOARD_INFO.str_hw_Type).Replace("\0", "");
					this.VciInfo.HW = vci_BOARD_INFO.hw_Version.ToString("X3").Insert(1, ".");
					this.VciInfo.SW = vci_BOARD_INFO.fw_Version.ToString("X3").Insert(1, ".");
					this.VciInfo.Manufacturer = "ZLG ZHIYUAN Electronics Co.";
					this.VciInfo.Type = this.m_devtype;
				}
				ControlCAN.VCI_CloseDevice(this.m_devtype, this.m_devind);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public override void Send(VCI_Message Message)
		{
			int devtype = this.m_devtype;
			int devind = this.m_devind;
			int cannum = this.m_cannum;
			ControlCAN.VCI_CAN_OBJ vci_CAN_OBJ = Message.ToVCI_CAN_OBJ();
			ControlCAN.VCI_Transmit(devtype, devind, cannum, ref vci_CAN_OBJ, 1);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public override void Send(Queue Messages)
		{
			int num = Messages.Count - 1;
			ControlCAN.VCI_CAN_OBJ[] array = new ControlCAN.VCI_CAN_OBJ[num + 1];
			int num2 = num;
			for (int i = 0; i <= num2; i++)
			{
				ControlCAN.VCI_CAN_OBJ[] array2 = array;
				int num3 = i;
				object obj = Messages.Dequeue();
				array2[num3] = ((obj != null) ? ((VCI_Message)obj) : default(VCI_Message)).ToVCI_CAN_OBJ();
			}
			ControlCAN.VCI_Transmit(this.m_devtype, this.m_devind, this.m_cannum, ref array[0], array.Count<ControlCAN.VCI_CAN_OBJ>());
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003C54 File Offset: 0x00001E54
		private void ReceiveDataProc()
		{
			while (this.mMustQuit == 0L)
			{
				uint num = (uint)ControlCAN.VCI_GetReceiveNum(this.m_devtype, this.m_devind, this.m_cannum);
				if ((ulong)num > 0UL)
				{
					ControlCAN.VCI_CAN_OBJ[] array = new ControlCAN.VCI_CAN_OBJ[(int)((ulong)num - 1UL) + 1];
					ControlCAN.VCI_Receive(this.m_devtype, this.m_devind, this.m_cannum, ref array[0], (int)num, 200);
					int num2 = (int)((ulong)num - 1UL);
					for (int i = 0; i <= num2; i++)
					{
						this.VciReaderBuffer.Enqueue(array[i].ToCANMessage());
					}
					if (this.VciReaderBuffer.Count > 0)
					{
						base.OutMessage(this.VciReaderBuffer);
						ControlCAN.VCI_ERR_INFO vci_ERR_INFO = default(ControlCAN.VCI_ERR_INFO);
						vci_ERR_INFO.Passive_ErrData = new byte[3];
						ControlCAN.VCI_ReadErrInfo(this.m_devtype, this.m_devind, this.m_cannum, ref vci_ERR_INFO);
					}
				}
			}
			this.m_IsOpen = false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D44 File Offset: 0x00001F44
		private ControlCAN.VCI_INIT_CONFIG AdapterSettings()
		{
			ControlCAN.VCI_INIT_CONFIG result;
			result.AccCode = this.m_Filter;
			result.AccMask = this.m_Mask;
			result.FilterMode = 0;
			result.Mode = 0;
			result.Timing0 = this.m_Timing0;
			result.Timing1 = this.m_Timing1;
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D98 File Offset: 0x00001F98
		public override bool Open()
		{
			this.m_IsOpen = false;
			if (ControlCAN.VCI_OpenDevice(this.m_devtype, this.m_devind, this.m_cannum) == 1)
			{
				int devtype = this.m_devtype;
				int devind = this.m_devind;
				int cannum = this.m_cannum;
				ControlCAN.VCI_INIT_CONFIG vci_INIT_CONFIG = this.AdapterSettings();
				if (ControlCAN.VCI_InitCAN(devtype, devind, cannum, ref vci_INIT_CONFIG) == 1)
				{
					if (ControlCAN.VCI_StartCAN(this.m_devtype, this.m_devind, this.m_cannum) != 1)
					{
						return this.m_IsOpen;
					}
					this.m_IsOpen = true;
					this.mMustQuit = 0L;
					this._CanReceviedThread = new Thread(new ThreadStart(this.ReceiveDataProc))
					{
						IsBackground = true
					};
					this._CanReceviedThread.Start();
				}
			}
			return this.m_IsOpen;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E4C File Offset: 0x0000204C
		public override bool Close()
		{
			Interlocked.Exchange(ref this.mMustQuit, 1L);
			ControlCAN.VCI_CloseDevice(this.m_devtype, this.m_devind);
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E70 File Offset: 0x00002070
		public override void Restart()
		{
			try
			{
				ControlCAN.VCI_ResetCAN(this.m_devtype, this.m_devind, this.m_cannum);
				int devtype = this.m_devtype;
				int devind = this.m_devind;
				int cannum = this.m_cannum;
				ControlCAN.VCI_INIT_CONFIG vci_INIT_CONFIG = this.AdapterSettings();
				ControlCAN.VCI_InitCAN(devtype, devind, cannum, ref vci_INIT_CONFIG);
				ControlCAN.VCI_StartCAN(this.m_devtype, this.m_devind, this.m_cannum);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003EF0 File Offset: 0x000020F0
		private static void FinalizeApp()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003EF4 File Offset: 0x000020F4
		private static void DisposeVciObject(object obj)
		{
			if (obj != null)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
					obj = null;
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003F18 File Offset: 0x00002118
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003F32 File Offset: 0x00002132
		public override string Socket
		{
			get
			{
				return Conversions.ToString(this.m_cannum);
			}
			set
			{
				this.m_cannum = Conversions.ToInteger(value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003F40 File Offset: 0x00002140
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003F5F File Offset: 0x0000215F
		public override string Filter
		{
			get
			{
				return this.m_Filter.ToString("X8");
			}
			set
			{
				this.m_Filter = (int)((long)Conversion.Int(Conversion.Val("&H" + value)) & 65535L);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003F88 File Offset: 0x00002188
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003FA7 File Offset: 0x000021A7
		public override string Mask
		{
			get
			{
				return this.m_Mask.ToString("X8");
			}
			set
			{
				this.m_Mask = (int)Math.Round(Conversion.Val("&H" + value));
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003FC8 File Offset: 0x000021C8
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003FFC File Offset: 0x000021FC
		public override string BitRate
		{
			get
			{
				byte timing = this.m_Timing0;
				string result;
				if (timing != 0)
				{
					if (timing != 1)
					{
						result = "250";
					}
					else
					{
						result = "250";
					}
				}
				else
				{
					result = "500";
				}
				return result;
			}
			set
			{
				if (Operators.CompareString(value, "250", false) == 0)
				{
					this.m_Timing0 = 1;
					this.m_Timing1 = 28;
					return;
				}
				if (Operators.CompareString(value, "500", false) != 0)
				{
					this.m_Timing0 = 1;
					this.m_Timing1 = 28;
					return;
				}
				this.m_Timing0 = 0;
				this.m_Timing1 = 28;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004058 File Offset: 0x00002258
		public override bool IsOpen
		{
			get
			{
				return this.m_IsOpen;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004070 File Offset: 0x00002270
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004085 File Offset: 0x00002285
		public override bool Mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004090 File Offset: 0x00002290
		public ZLG()
		{
			this.m_devtype = 4;
			this.m_devind = 0;
			this.m_cannum = 0;
			this.m_Filter = 0;
			this.m_Mask = -1;
			this.m_Timing0 = 1;
			this.m_Timing1 = 28;
			this.m_IsOpen = false;
			this.mMustQuit = 0L;
		}

		// Token: 0x04000083 RID: 131
		private int m_devtype;

		// Token: 0x04000084 RID: 132
		private int m_devind;

		// Token: 0x04000085 RID: 133
		private int m_cannum;

		// Token: 0x04000086 RID: 134
		private int m_Filter;

		// Token: 0x04000087 RID: 135
		private int m_Mask;

		// Token: 0x04000088 RID: 136
		private byte m_Timing0;

		// Token: 0x04000089 RID: 137
		private byte m_Timing1;

		// Token: 0x0400008A RID: 138
		private bool m_IsOpen;

		// Token: 0x0400008B RID: 139
		private bool m_Mode;

		// Token: 0x0400008C RID: 140
		private long mMustQuit;

		// Token: 0x0400008D RID: 141
		private Thread _CanReceviedThread;

		// Token: 0x0400008E RID: 142
		private bool disposedValue;
	}
}
