using System;
using System.Text;
using Ixxat.Vci3.Bal.Can;

namespace DIUS4.Adapters
{
	// Token: 0x0200000B RID: 11
	public struct VCI_Message
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002534 File Offset: 0x00000734
		public VCI_Message(byte len)
		{
			this = default(VCI_Message);
			this.DataLen = len;
			this.Data = new byte[(int)(len - 1 + 1)];
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002554 File Offset: 0x00000754
		internal new string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("{0:X8}, ", this.ID), new object[0]);
			stringBuilder.AppendFormat(string.Format("{0:X8}, ", this.TimeStamp), new object[0]);
			stringBuilder.AppendFormat(string.Format("{0:X2}, ", this.TimeFlag), new object[0]);
			stringBuilder.AppendFormat(string.Format("{0:X2}, ", this.SendType), new object[0]);
			stringBuilder.AppendFormat(string.Format("{0:X2}, ", this.RemoteFlag), new object[0]);
			stringBuilder.AppendFormat(string.Format("{0:X2}, ", this.ExternFlag), new object[0]);
			stringBuilder.AppendFormat(string.Format("{0:X2}, ", this.DataLen), new object[0]);
			int num = this.Data.Length - 1;
			for (int i = 0; i <= num; i++)
			{
				stringBuilder.AppendFormat(string.Format("{0:X2} ", this.Data[i]), new object[0]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002698 File Offset: 0x00000898
		internal string ToCANBus()
		{
			string result;
			if (this.DataLen == 0)
			{
				result = "";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat(string.Format("t{0:X3}{1:X1}", this.ID, this.DataLen), new object[0]);
				int num = 0;
				do
				{
					stringBuilder.AppendFormat(string.Format("{0:X2}", this.Data[num]), new object[0]);
					num++;
				}
				while (num <= 7);
				result = stringBuilder.ToString() + "\r";
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002728 File Offset: 0x00000928
		internal byte[] ToULA_Message()
		{
			byte[] bytes = BitConverter.GetBytes(this.ID);
			return new byte[]
			{
				84,
				bytes[0],
				bytes[1],
				bytes[2],
				bytes[3],
				this.Data[0],
				this.Data[1],
				this.Data[2],
				this.Data[3],
				this.Data[4],
				this.Data[5],
				this.Data[6],
				this.Data[7]
			};
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027C4 File Offset: 0x000009C4
		internal ControlCAN.VCI_CAN_OBJ ToVCI_CAN_OBJ()
		{
			return new ControlCAN.VCI_CAN_OBJ
			{
				ID = this.ID,
				TimeStamp = this.TimeStamp,
				TimeFlag = this.TimeFlag,
				SendType = this.SendType,
				RemoteFlag = this.RemoteFlag,
				ExternFlag = this.ExternFlag,
				DataLen = this.DataLen,
				data0 = this.Data[0],
				data1 = this.Data[1],
				data2 = this.Data[2],
				data3 = this.Data[3],
				data4 = this.Data[4],
				data5 = this.Data[5],
				data6 = this.Data[6],
				data7 = this.Data[7]
			};
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028B0 File Offset: 0x00000AB0
		internal CanMessage ToCanMessage()
		{
			CanMessage result = new CanMessage
			{
				Identifier = this.ID,
				TimeStamp = this.TimeStamp,
				FrameType = (CanMsgFrameType)this.SendType,
				DataLength = this.DataLen
			};
			result[0] = this.Data[0];
			result[1] = this.Data[1];
			result[2] = this.Data[2];
			result[3] = this.Data[3];
			result[4] = this.Data[4];
			result[5] = this.Data[5];
			result[6] = this.Data[6];
			result[7] = this.Data[7];
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000297C File Offset: 0x00000B7C
		internal VCI_Message ToCanMessage(ref byte[] Message)
		{
			int num = 0;
			uint num2;
			do
			{
				num2 = (num2 | (uint)Message[num]) << 8;
				num++;
			}
			while (num <= 2);
			num2 |= (uint)Message[3];
			VCI_Message vci_Message = new VCI_Message(8)
			{
				ID = num2,
				TimeStamp = 0U,
				TimeFlag = 0,
				SendType = 0,
				RemoteFlag = 0,
				ExternFlag = 0
			};
			vci_Message.Data[0] = Message[4];
			vci_Message.Data[1] = Message[5];
			vci_Message.Data[2] = Message[6];
			vci_Message.Data[3] = Message[7];
			vci_Message.Data[4] = Message[8];
			vci_Message.Data[5] = Message[9];
			vci_Message.Data[6] = Message[10];
			vci_Message.Data[7] = Message[11];
			return vci_Message;
		}

		// Token: 0x04000022 RID: 34
		public uint ID;

		// Token: 0x04000023 RID: 35
		public uint TimeStamp;

		// Token: 0x04000024 RID: 36
		public byte TimeFlag;

		// Token: 0x04000025 RID: 37
		public byte SendType;

		// Token: 0x04000026 RID: 38
		public byte RemoteFlag;

		// Token: 0x04000027 RID: 39
		public byte ExternFlag;

		// Token: 0x04000028 RID: 40
		public byte DataLen;

		// Token: 0x04000029 RID: 41
		public byte[] Data;
	}
}
