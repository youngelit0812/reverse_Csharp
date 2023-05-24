using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DIUS4.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4.Adapters
{
	// Token: 0x02000010 RID: 16
	internal class ULA : Adapter
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003334 File Offset: 0x00001534
		public ULA()
		{
			this.mDecode = new Decode();
			this.ULAsrv = new TcpClient
			{
				ReceiveTimeout = 5000,
				NoDelay = true,
				ReceiveBufferSize = 512,
				SendBufferSize = 512
			};
			this.mMustQuit = 0L;
			this.mSocket = "192.168.5.1";
			this.mFilter = "00000000";
			this.mMask = "FFFFFFFF";
			this.mBitRate = "500";
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000033B9 File Offset: 0x000015B9
		public override void Send(VCI_Message Message)
		{
			this.ULAStream.Write(Message.ToULA_Message(), 0, 13);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033D0 File Offset: 0x000015D0
		public override void Send(Queue Messages)
		{
			byte[] array = new byte[13 * Messages.Count - 1 + 1];
			int num = Messages.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				object obj = Messages.Dequeue();
				Array.Copy(((obj != null) ? ((VCI_Message)obj) : default(VCI_Message)).ToULA_Message(), 0, array, i * 13, 13);
			}
			this.ULAStream.Write(array, 0, array.Count<byte>());
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003448 File Offset: 0x00001648
		private void ReceiveDataProc()
		{
			byte[] array = new byte[12];
			IL_D7:
			while (this.mMustQuit == 0L)
			{
				while (this.ULAStream.DataAvailable)
				{
					while (this.ULAStream.ReadByte() == 84)
					{
						this.ULAStream.Read(array, 0, 12);
						VCI_Message vci_Message = new VCI_Message
						{
							DataLen = 8,
							ID = BitConverter.ToUInt32(array, 0),
							Data = new byte[]
							{
								array[4],
								array[5],
								array[6],
								array[7],
								array[8],
								array[9],
								array[10],
								array[11]
							}
						};
						this.VciReaderBuffer.Enqueue(vci_Message);
						if (this.ULAStream.ReadByte() == 0)
						{
							if (this.VciReaderBuffer.Count > 0)
							{
								base.OutMessage(this.VciReaderBuffer);
								goto IL_D7;
							}
							goto IL_D7;
						}
					}
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003538 File Offset: 0x00001738
		public override bool Connect()
		{
			bool result;
			if (!MyProject.Computer.Network.Ping(this.mSocket))
			{
				result = false;
			}
			else
			{
				try
				{
					if (!this.ULAsrv.Connected)
					{
						this.ULAsrv = new TcpClient
						{
							ReceiveTimeout = 5000,
							NoDelay = true,
							ReceiveBufferSize = 512,
							SendBufferSize = 512
						};
						this.ULAsrv.Connect(IPAddress.Parse(this.mSocket), 13400);
						if (!this.ULAsrv.Connected)
						{
							result = false;
						}
						else
						{
							this.ULAStream = this.ULAsrv.GetStream();
							this.ULAStream.WriteByte(170);
							this.ULAStream.WriteByte(86);
							Thread.Sleep(50);
							if (!this.ULAStream.DataAvailable)
							{
								this.Close();
								result = false;
							}
							else if (this.ULAStream.ReadByte() != 86)
							{
								this.Close();
								result = false;
							}
							else
							{
								byte[] array = new byte[9];
								this.ULAStream.Read(array, 0, 9);
								string text = this.mDecode.ToASC(array, 0U, 2U);
								this.VciInfo.SW = text.Insert(1, ".");
								this.VciInfo.HW = Conversions.ToString(Strings.Chr((int)array[2])) + ".0";
								this.VciInfo.ID = this.mDecode.ByteToHEX8(array, 6U, 12U);
								this.VciInfo.Manufacturer = "Magellan Group Co.";
								this.ULAStream.WriteByte(78);
								this.ULAStream.ReadByte();
								byte[] array2 = new byte[3];
								this.ULAStream.Read(array2, 0, 3);
								this.VciInfo.Description = "DIUS " + this.mDecode.ToASC(array2, 0U, 3U);
								this.VciInfo.Type = 101;
								result = true;
							}
						}
					}
					else
					{
						result = true;
					}
				}
				catch (Exception ex)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003764 File Offset: 0x00001964
		public override bool Open()
		{
			bool result;
			if (this.m_IsOpen)
			{
				result = true;
			}
			else
			{
				this.m_IsOpen = false;
				try
				{
					if (!this.ULAsrv.Connected)
					{
						this.ULAsrv = new TcpClient
						{
							ReceiveTimeout = 5000,
							NoDelay = true,
							ReceiveBufferSize = 512,
							SendBufferSize = 512
						};
						this.ULAsrv.Connect(IPAddress.Parse(this.mSocket), 13400);
						this.ULAStream = this.ULAsrv.GetStream();
					}
					if (Operators.CompareString(this.mBitRate, "500", false) == 0)
					{
						byte[] buffer = new byte[]
						{
							83,
							54
						};
						this.ULAStream.Write(buffer, 0, 2);
						byte b = (byte)this.ULAStream.ReadByte();
					}
					else
					{
						byte[] buffer2 = new byte[]
						{
							83,
							53
						};
						this.ULAStream.Write(buffer2, 0, 2);
						byte b2 = (byte)this.ULAStream.ReadByte();
					}
					byte[] buffer3 = new byte[]
					{
						70,
						49
					};
					this.ULAStream.Write(buffer3, 0, 2);
					byte b3 = (byte)this.ULAStream.ReadByte();
					this.ULAStream.Write(this.mDecode.TextToByte2("6D" + this.mMask), 0, 5);
					byte b4 = (byte)this.ULAStream.ReadByte();
					this.ULAStream.Write(this.mDecode.TextToByte2("4D" + this.mFilter), 0, 5);
					byte b5 = (byte)this.ULAStream.ReadByte();
					this.ULAStream.WriteByte(79);
					if (this.ULAStream.ReadByte() != 0)
					{
						result = false;
					}
					else
					{
						this.mMustQuit = 0L;
						ULA.rxThread = new Thread(new ThreadStart(this.ReceiveDataProc))
						{
							IsBackground = true
						};
						ULA.rxThread.Start();
						this.m_IsOpen = true;
						result = this.m_IsOpen;
					}
				}
				catch (Exception ex)
				{
					result = this.m_IsOpen;
				}
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003988 File Offset: 0x00001B88
		public override bool Close()
		{
			try
			{
				Interlocked.Exchange(ref this.mMustQuit, 1L);
				if (this.ULAsrv.Connected)
				{
					this.ULAStream.WriteByte(67);
					this.ULAStream.WriteByte(68);
					this.ULAStream.Close();
					this.ULAsrv.Close();
				}
				this.m_IsOpen = false;
			}
			catch (Exception ex)
			{
			}
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A08 File Offset: 0x00001C08
		public override void Restart()
		{
			this.Close();
			if (this.Connect())
			{
				this.Open();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003A20 File Offset: 0x00001C20
		public override bool IsOpen
		{
			get
			{
				return this.m_IsOpen;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003A28 File Offset: 0x00001C28
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003A3D File Offset: 0x00001C3D
		public override string Socket
		{
			get
			{
				return this.mSocket;
			}
			set
			{
				this.mSocket = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003A46 File Offset: 0x00001C46
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003A4E File Offset: 0x00001C4E
		public override string Filter
		{
			get
			{
				return this.mFilter;
			}
			set
			{
				this.mFilter = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003A58 File Offset: 0x00001C58
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003A6D File Offset: 0x00001C6D
		public override string Mask
		{
			get
			{
				return this.mMask;
			}
			set
			{
				this.mMask = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003A76 File Offset: 0x00001C76
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003A7E File Offset: 0x00001C7E
		public override string BitRate
		{
			get
			{
				return this.mBitRate;
			}
			set
			{
				this.mBitRate = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003A87 File Offset: 0x00001C87
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003A8F File Offset: 0x00001C8F
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

		// Token: 0x0400006A RID: 106
		private Decode mDecode;

		// Token: 0x0400006B RID: 107
		private TcpClient ULAsrv;

		// Token: 0x0400006C RID: 108
		private NetworkStream ULAStream;

		// Token: 0x0400006D RID: 109
		private static Thread rxThread;

		// Token: 0x0400006E RID: 110
		private long mMustQuit;

		// Token: 0x0400006F RID: 111
		private bool m_IsOpen;

		// Token: 0x04000070 RID: 112
		private string mSocket;

		// Token: 0x04000071 RID: 113
		private string mFilter;

		// Token: 0x04000072 RID: 114
		private string mMask;

		// Token: 0x04000073 RID: 115
		private string mBitRate;

		// Token: 0x04000074 RID: 116
		private bool mMode;
	}
}
