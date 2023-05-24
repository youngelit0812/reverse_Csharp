using System;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4.Adapters
{
	// Token: 0x02000013 RID: 19
	[StandardModule]
	internal sealed class ControlCAN
	{
		// Token: 0x06000085 RID: 133
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_OpenDevice(int DeviceType, int DeviceInd, int Reserved);

		// Token: 0x06000086 RID: 134
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_CloseDevice(int DeviceType, int DeviceInd);

		// Token: 0x06000087 RID: 135
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_InitCAN(int DeviceType, int DeviceInd, int CANInd, ref ControlCAN.VCI_INIT_CONFIG InitConfig);

		// Token: 0x06000088 RID: 136
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_ReadBoardInfo(int DeviceType, int DeviceInd, ref ControlCAN.VCI_BOARD_INFO info);

		// Token: 0x06000089 RID: 137
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_ReadErrInfo(int DeviceType, int DeviceInd, int CANInd, ref ControlCAN.VCI_ERR_INFO ErrInfo);

		// Token: 0x0600008A RID: 138
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_ReadCANStatus(int DeviceType, int DeviceInd, int CANInd, ref ControlCAN.VCI_CAN_STATUS CANStatus);

		// Token: 0x0600008B RID: 139
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_GetReference(int DeviceType, int DeviceInd, int CANInd, int RefType, ref long data);

		// Token: 0x0600008C RID: 140
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_SetReference(int DeviceType, int DeviceInd, int CANInd, int RefType, ref long data);

		// Token: 0x0600008D RID: 141
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_GetReceiveNum(int DeviceType, int DeviceInd, int CANInd);

		// Token: 0x0600008E RID: 142
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_ClearBuffer(int DeviceType, int DeviceInd, int CANInd);

		// Token: 0x0600008F RID: 143
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_StartCAN(int DeviceType, int DeviceInd, int CANInd);

		// Token: 0x06000090 RID: 144
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_ResetCAN(int DeviceType, int DeviceInd, int CANInd);

		// Token: 0x06000091 RID: 145
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_Transmit(int DeviceType, int DeviceInd, int CANInd, ref ControlCAN.VCI_CAN_OBJ Sendbuf, int length);

		// Token: 0x06000092 RID: 146
		[DllImport("ControlCAN.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int VCI_Receive(int DeviceType, int DeviceInd, int CANInd, ref ControlCAN.VCI_CAN_OBJ Receive, int length, int WaitTime);

		// Token: 0x02000035 RID: 53
		public struct VCI_BOARD_INFO
		{
			// Token: 0x0400012E RID: 302
			public short hw_Version;

			// Token: 0x0400012F RID: 303
			public short fw_Version;

			// Token: 0x04000130 RID: 304
			public short dr_Version;

			// Token: 0x04000131 RID: 305
			public short in_Version;

			// Token: 0x04000132 RID: 306
			public short irq_num;

			// Token: 0x04000133 RID: 307
			public byte can_num;

			// Token: 0x04000134 RID: 308
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public byte[] str_Serial_Num;

			// Token: 0x04000135 RID: 309
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
			public byte[] str_hw_Type;

			// Token: 0x04000136 RID: 310
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public short[] Reserved;
		}

		// Token: 0x02000036 RID: 54
		public enum ErrorType
		{
			// Token: 0x04000138 RID: 312
			ERR_CAN_OVERFLOW = 1,
			// Token: 0x04000139 RID: 313
			ERR_CAN_ERRALARM,
			// Token: 0x0400013A RID: 314
			ERR_CAN_PASSIVE = 4,
			// Token: 0x0400013B RID: 315
			ERR_CAN_LOSE = 8,
			// Token: 0x0400013C RID: 316
			ERR_CAN_BUSERR = 16,
			// Token: 0x0400013D RID: 317
			ERR_DEVICEOPENED = 256,
			// Token: 0x0400013E RID: 318
			ERR_DEVICEOPEN = 512,
			// Token: 0x0400013F RID: 319
			ERR_DEVICENOTOPEN = 1024,
			// Token: 0x04000140 RID: 320
			ERR_BUFFEROVERFLOW = 2048,
			// Token: 0x04000141 RID: 321
			ERR_DEVICENOTEXIST = 4096,
			// Token: 0x04000142 RID: 322
			ERR_LOADKERNELDLL = 8192,
			// Token: 0x04000143 RID: 323
			ERR_CMDFAILED = 16384,
			// Token: 0x04000144 RID: 324
			ERR_BUFFERCREATE = 32768
		}

		// Token: 0x02000037 RID: 55
		public struct VCI_CAN_OBJ
		{
			// Token: 0x060001B1 RID: 433 RVA: 0x000106CC File Offset: 0x0000E8CC
			public VCI_Message ToCANMessage()
			{
				return new VCI_Message
				{
					ID = this.ID,
					TimeStamp = this.TimeStamp,
					TimeFlag = this.TimeFlag,
					SendType = this.SendType,
					RemoteFlag = this.RemoteFlag,
					ExternFlag = this.ExternFlag,
					DataLen = 8,
					Data = new byte[]
					{
						this.data0,
						this.data1,
						this.data2,
						this.data3,
						this.data4,
						this.data5,
						this.data6,
						this.data7
					}
				};
			}

			// Token: 0x04000145 RID: 325
			public uint ID;

			// Token: 0x04000146 RID: 326
			public uint TimeStamp;

			// Token: 0x04000147 RID: 327
			public byte TimeFlag;

			// Token: 0x04000148 RID: 328
			public byte SendType;

			// Token: 0x04000149 RID: 329
			public byte RemoteFlag;

			// Token: 0x0400014A RID: 330
			public byte ExternFlag;

			// Token: 0x0400014B RID: 331
			public byte DataLen;

			// Token: 0x0400014C RID: 332
			public byte data0;

			// Token: 0x0400014D RID: 333
			public byte data1;

			// Token: 0x0400014E RID: 334
			public byte data2;

			// Token: 0x0400014F RID: 335
			public byte data3;

			// Token: 0x04000150 RID: 336
			public byte data4;

			// Token: 0x04000151 RID: 337
			public byte data5;

			// Token: 0x04000152 RID: 338
			public byte data6;

			// Token: 0x04000153 RID: 339
			public byte data7;

			// Token: 0x04000154 RID: 340
			public byte Reserved0;

			// Token: 0x04000155 RID: 341
			public byte Reserved1;

			// Token: 0x04000156 RID: 342
			public byte Reserved2;
		}

		// Token: 0x02000038 RID: 56
		public struct VCI_CAN_STATUS
		{
			// Token: 0x04000157 RID: 343
			public byte ErrInterrupt;

			// Token: 0x04000158 RID: 344
			public byte regMode;

			// Token: 0x04000159 RID: 345
			public byte regStatus;

			// Token: 0x0400015A RID: 346
			public byte regALCapture;

			// Token: 0x0400015B RID: 347
			public byte regECCapture;

			// Token: 0x0400015C RID: 348
			public byte regEWLimit;

			// Token: 0x0400015D RID: 349
			public byte regRECounter;

			// Token: 0x0400015E RID: 350
			public byte regTECounter;

			// Token: 0x0400015F RID: 351
			public int Reserved;
		}

		// Token: 0x02000039 RID: 57
		public struct VCI_ERR_INFO
		{
			// Token: 0x04000160 RID: 352
			public int ErrCode;

			// Token: 0x04000161 RID: 353
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public byte[] Passive_ErrData;

			// Token: 0x04000162 RID: 354
			public byte ArLost_ErrData;
		}

		// Token: 0x0200003A RID: 58
		public struct VCI_INIT_CONFIG
		{
			// Token: 0x04000163 RID: 355
			public int AccCode;

			// Token: 0x04000164 RID: 356
			public int AccMask;

			// Token: 0x04000165 RID: 357
			public int Reserved;

			// Token: 0x04000166 RID: 358
			public byte FilterMode;

			// Token: 0x04000167 RID: 359
			public byte Timing0;

			// Token: 0x04000168 RID: 360
			public byte Timing1;

			// Token: 0x04000169 RID: 361
			public byte Mode;
		}
	}
}
