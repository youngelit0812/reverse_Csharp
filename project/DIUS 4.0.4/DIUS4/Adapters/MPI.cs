using System;
using System.Collections;
using System.Threading;
using Ixxat.Vci3;
using Ixxat.Vci3.Bal;
using Ixxat.Vci3.Bal.Can;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4.Adapters
{
	// Token: 0x0200000F RID: 15
	internal class MPI : Adapter
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002B18 File Offset: 0x00000D18
		public event MPI.MsgCANEventHandler MsgCAN;

		// Token: 0x06000044 RID: 68 RVA: 0x00002B4D File Offset: 0x00000D4D
		public MPI()
		{
			this.mSocket = 0U;
			this.m_Filter = 0U;
			this.m_Mask = 0U;
			this.mBitRate = CanBitrate.Cia250KBit;
			this._CANMessageWriteBuffer = new ArrayList();
			this.Decode = new Decode();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B8C File Offset: 0x00000D8C
		private bool InitSocket()
		{
			IBalObject balObject = null;
			bool result;
			try
			{
				balObject = MPI.mDevice.OpenBusAccessLayer();
				MPI.mCanChn = (ICanChannel)balObject.OpenSocket((byte)this.mSocket, typeof(ICanChannel));
				MPI.mCanChn.Initialize(1024, 128, false);
				MPI.mReader = MPI.mCanChn.GetMessageReader();
				MPI.mReader.Threshold = 1;
				MPI.mRxEvent = new AutoResetEvent(false);
				MPI.mReader.AssignEvent(MPI.mRxEvent);
				MPI.mWriter = MPI.mCanChn.GetMessageWriter();
				MPI.mWriter.Threshold = 1;
				MPI.mCanChn.Activate();
				MPI.mCanCtl = (ICanControl)balObject.OpenSocket((byte)this.mSocket, typeof(ICanControl));
				MPI.mCanCtl.InitLine(CanOperatingModes.Standard, this.mBitRate);
				MPI.mCanCtl.SetAccFilter(CanFilter.Std, this.m_Filter, this.m_Mask);
				MPI.mCanCtl.StartLine();
			}
			catch (Exception ex)
			{
				result = true;
			}
			finally
			{
				MPI.DisposeVciObject(balObject);
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public override void Send(VCI_Message Message)
		{
			MPI.mWriter.SendMessage(Message.ToCanMessage());
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override void Send(Queue Messages)
		{
			int num = Messages.Count - 1;
			CanMessage[] array = new CanMessage[num + 1];
			int num2 = num;
			for (int i = 0; i <= num2; i++)
			{
				CanMessage[] array2 = array;
				int num3 = i;
				object obj = Messages.Dequeue();
				array2[num3] = ((obj != null) ? ((VCI_Message)obj) : default(VCI_Message)).ToCanMessage();
			}
			MPI.mWriter.SendMessages(array);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D34 File Offset: 0x00000F34
		private void ReceiveDataProc()
		{
			while (!(-(this.mMustQuit > false)))
			{
				if (MPI.mReader.FillCount > 0)
				{
					CanMessage[] array = new CanMessage[(int)(MPI.mReader.FillCount - 1 + 1)];
					uint num = (uint)(MPI.mReader.ReadMessages(array) - 1);
					for (uint num2 = 0U; num2 <= num; num2 += 1U)
					{
						if (array[(int)num2].FrameType == CanMsgFrameType.Data & array[(int)num2].DataLength > 0)
						{
							this.VciReaderBuffer.Enqueue(this.ToCANMessage(array[(int)num2]));
						}
					}
				}
				if (this.VciReaderBuffer.Count > 0)
				{
					base.OutMessage(this.VciReaderBuffer);
				}
			}
			this.m_IsOpen = false;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public override bool Connect()
		{
			IVciDeviceManager vciDeviceManager = null;
			IVciDeviceList vciDeviceList = null;
			IEnumerator enumerator = null;
			try
			{
				vciDeviceManager = VciServer.GetDeviceManager();
				vciDeviceList = vciDeviceManager.GetDeviceList();
				enumerator = vciDeviceList.GetEnumerator();
				enumerator.MoveNext();
				MPI.mDevice = (IVciDevice)enumerator.Current;
				this.VciInfo.ID = this.SerialDevice();
				this.VciInfo.HW = MPI.mDevice.HardwareVersion.ToString();
				this.VciInfo.Description = MPI.mDevice.Description;
				this.VciInfo.DW = MPI.mDevice.DriverVersion.ToString();
				this.VciInfo.Manufacturer = MPI.mDevice.Manufacturer.ToString();
				this.VciInfo.Type = 100;
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				MPI.DisposeVciObject(vciDeviceManager);
				MPI.DisposeVciObject(vciDeviceList);
				MPI.DisposeVciObject(enumerator);
			}
			return true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EDC File Offset: 0x000010DC
		public override bool Open()
		{
			this.m_IsOpen = false;
			bool isOpen;
			if (!this.Connect())
			{
				isOpen = this.m_IsOpen;
			}
			else if (this.InitSocket())
			{
				isOpen = this.m_IsOpen;
			}
			else
			{
				this.m_IsOpen = true;
				this.mMustQuit = false;
				MPI.rxThread = new Thread(new ThreadStart(this.ReceiveDataProc));
				MPI.rxThread.Start();
				isOpen = this.m_IsOpen;
			}
			return isOpen;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F48 File Offset: 0x00001148
		public override bool Close()
		{
			this.m_IsOpen = false;
			object value = this.mMustQuit;
			Interlocked.Exchange(ref value, true);
			this.mMustQuit = Conversions.ToBoolean(value);
			if (!Information.IsNothing(MPI.rxThread))
			{
				MPI.rxThread.Join();
			}
			MPI.FinalizeApp();
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F9C File Offset: 0x0000119C
		public override void Restart()
		{
			this.m_IsOpen = false;
			try
			{
				MPI.mCanCtl.StopLine();
			}
			catch (Exception ex)
			{
				return;
			}
			MPI.mCanCtl.InitLine(CanOperatingModes.Standard, this.mBitRate);
			MPI.mCanCtl.SetAccFilter(CanFilter.Std, this.m_Filter, this.m_Mask);
			MPI.mCanCtl.StartLine();
			this.m_IsOpen = true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003014 File Offset: 0x00001214
		private static void FinalizeApp()
		{
			MPI.DisposeVciObject(MPI.mCanChn);
			MPI.DisposeVciObject(MPI.mCanCtl);
			MPI.DisposeVciObject(MPI.mDevice);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003034 File Offset: 0x00001234
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003058 File Offset: 0x00001258
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003072 File Offset: 0x00001272
		public override string Socket
		{
			get
			{
				return Conversions.ToString(this.mSocket);
			}
			set
			{
				this.mSocket = Conversions.ToUInteger(value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003080 File Offset: 0x00001280
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003092 File Offset: 0x00001292
		public override string Filter
		{
			get
			{
				return this.m_Filter.ToString("X8");
			}
			set
			{
				this.m_Filter = (uint)((int)Math.Round(Conversion.Val("&H" + value)));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000030B0 File Offset: 0x000012B0
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000030C2 File Offset: 0x000012C2
		public override string Mask
		{
			get
			{
				return this.m_Mask.ToString("X8");
			}
			set
			{
				this.m_Mask = (uint)((int)Math.Round(Conversion.Val("&H" + value)));
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000030E0 File Offset: 0x000012E0
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003128 File Offset: 0x00001328
		public override string BitRate
		{
			get
			{
				CanBitrate value = this.mBitRate;
				string result;
				if (value == CanBitrate.Cia250KBit)
				{
					result = "250";
				}
				else if (value == CanBitrate.Cia500KBit)
				{
					result = "500";
				}
				else
				{
					result = "No Settings";
				}
				return result;
			}
			set
			{
				if (Operators.CompareString(value, "250", false) == 0)
				{
					this.mBitRate = CanBitrate.Cia250KBit;
					return;
				}
				if (Operators.CompareString(value, "500", false) != 0)
				{
					this.mBitRate = CanBitrate.Cia250KBit;
					return;
				}
				this.mBitRate = CanBitrate.Cia500KBit;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003178 File Offset: 0x00001378
		public override bool IsOpen
		{
			get
			{
				return this.m_IsOpen;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003190 File Offset: 0x00001390
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000031A5 File Offset: 0x000013A5
		public override bool Mode
		{
			get
			{
				return this.mMode;
			}
			set
			{
				this.mMode = value;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000031B0 File Offset: 0x000013B0
		public VCI_Message ToCANMessage(CanMessage Message)
		{
			VCI_Message vci_Message = new VCI_Message(8)
			{
				ID = Message.Identifier,
				TimeStamp = Message.TimeStamp,
				TimeFlag = 0,
				SendType = (byte)Message.FrameType,
				RemoteFlag = ((-((Message.SelfReceptionRequest > false) ? 1 : 0)) ? 1 : 0),
				ExternFlag = ((-((Message.ExtendedFrameFormat > false) ? 1 : 0)) ? 1 : 0)
			};
			vci_Message.Data[0] = Message[0];
			vci_Message.Data[1] = Message[1];
			vci_Message.Data[2] = Message[2];
			vci_Message.Data[3] = Message[3];
			vci_Message.Data[4] = Message[4];
			vci_Message.Data[5] = Message[5];
			vci_Message.Data[6] = Message[6];
			vci_Message.Data[7] = Message[7];
			return vci_Message;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000032A0 File Offset: 0x000014A0
		private string SerialDevice()
		{
			string text = "";
			if (Operators.CompareString(MPI.mDevice.UniqueHardwareId.GetType().ToString(), "System.String", false) == 0)
			{
				text = Conversions.ToString(MPI.mDevice.UniqueHardwareId);
			}
			else
			{
				object uniqueHardwareId = MPI.mDevice.UniqueHardwareId;
				byte[] array = ((uniqueHardwareId != null) ? ((Guid)uniqueHardwareId) : default(Guid)).ToByteArray();
				uint num = 0U;
				do
				{
					text += Conversions.ToString(Strings.Chr((int)array[(int)num]));
					num += 1U;
				}
				while (num <= 8U);
			}
			return text;
		}

		// Token: 0x0400005A RID: 90
		private static IVciDevice mDevice;

		// Token: 0x0400005B RID: 91
		private static ICanControl mCanCtl;

		// Token: 0x0400005C RID: 92
		private static ICanChannel mCanChn;

		// Token: 0x0400005D RID: 93
		private static ICanMessageWriter mWriter;

		// Token: 0x0400005E RID: 94
		private static ICanMessageReader mReader;

		// Token: 0x0400005F RID: 95
		private static Thread rxThread;

		// Token: 0x04000060 RID: 96
		private bool mMustQuit;

		// Token: 0x04000061 RID: 97
		private static AutoResetEvent mRxEvent;

		// Token: 0x04000062 RID: 98
		private bool mMode;

		// Token: 0x04000063 RID: 99
		private uint mSocket;

		// Token: 0x04000064 RID: 100
		private uint m_Filter;

		// Token: 0x04000065 RID: 101
		private uint m_Mask;

		// Token: 0x04000066 RID: 102
		private CanBitrate mBitRate;

		// Token: 0x04000067 RID: 103
		private bool m_IsOpen;

		// Token: 0x04000068 RID: 104
		private ArrayList _CANMessageWriteBuffer;

		// Token: 0x04000069 RID: 105
		private Decode Decode;

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x060001B0 RID: 432
		public delegate void MsgCANEventHandler(CanMessage Message);
	}
}
