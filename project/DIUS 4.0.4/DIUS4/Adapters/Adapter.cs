using System;
using System.Collections;
using System.Windows.Forms;

namespace DIUS4.Adapters
{
	// Token: 0x02000008 RID: 8
	public abstract class Adapter
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000023AB File Offset: 0x000005AB
		protected Adapter()
		{
			this.VciInfo = default(VCI_Info);
			this.VciInfo.Type = -1;
			this.VciState = default(VCI_State);
			this.VciReaderBuffer = new Queue();
		}

		// Token: 0x06000022 RID: 34
		public abstract void Send(VCI_Message Message);

		// Token: 0x06000023 RID: 35
		public abstract void Send(Queue Messages);

		// Token: 0x06000024 RID: 36
		public abstract bool Connect();

		// Token: 0x06000025 RID: 37
		public abstract bool Open();

		// Token: 0x06000026 RID: 38
		public abstract bool Close();

		// Token: 0x06000027 RID: 39
		public abstract void Restart();

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000028 RID: 40
		public abstract bool IsOpen { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000029 RID: 41
		// (set) Token: 0x0600002A RID: 42
		public abstract string Socket { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002B RID: 43
		// (set) Token: 0x0600002C RID: 44
		public abstract string Filter { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002D RID: 45
		// (set) Token: 0x0600002E RID: 46
		public abstract string Mask { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002F RID: 47
		// (set) Token: 0x06000030 RID: 48
		public abstract bool Mode { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		public abstract string BitRate { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000033 RID: 51 RVA: 0x000023E4 File Offset: 0x000005E4
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x0000241C File Offset: 0x0000061C
		public event Adapter.OnReceiveMessage CanMessageEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000035 RID: 53 RVA: 0x00002454 File Offset: 0x00000654
		// (remove) Token: 0x06000036 RID: 54 RVA: 0x0000248C File Offset: 0x0000068C
		public event Adapter.SelectEventHandler SelectAdapterEvent;

		// Token: 0x06000037 RID: 55 RVA: 0x000024C4 File Offset: 0x000006C4
		internal void OutMessage(Queue CANMessage)
		{
			Adapter.OnReceiveMessage canMessageEventEvent = this.CanMessageEventEvent;
			if (canMessageEventEvent != null)
			{
				canMessageEventEvent(CANMessage);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000024E4 File Offset: 0x000006E4
		public bool AutoDetect()
		{
			Application.DoEvents();
			try
			{
				if (this.IsOpen)
				{
					this.Close();
				}
				if (this.Connect())
				{
					return true;
				}
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x0400000B RID: 11
		public VCI_Info VciInfo;

		// Token: 0x0400000C RID: 12
		public VCI_State VciState;

		// Token: 0x0400000D RID: 13
		public Queue VciReaderBuffer;

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x060001A8 RID: 424
		public delegate void OnReceiveMessage(Queue CANMessage);

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x060001AC RID: 428
		public delegate void SelectEventHandler(object sender, MouseEventArgs e);

		// Token: 0x02000033 RID: 51
		public enum CanError : byte
		{
			// Token: 0x0400010F RID: 271
			OK,
			// Token: 0x04000110 RID: 272
			No_Responding,
			// Token: 0x04000111 RID: 273
			General_Regect = 16,
			// Token: 0x04000112 RID: 274
			Service_Not_Supported,
			// Token: 0x04000113 RID: 275
			SubFunction_Not_Supported,
			// Token: 0x04000114 RID: 276
			Busy = 33,
			// Token: 0x04000115 RID: 277
			Conditions_Not_Correct,
			// Token: 0x04000116 RID: 278
			Routine_Not_Complete,
			// Token: 0x04000117 RID: 279
			Out_Of_Range = 49,
			// Token: 0x04000118 RID: 280
			Security_Access_Denied = 51,
			// Token: 0x04000119 RID: 281
			Invalid_Key = 53,
			// Token: 0x0400011A RID: 282
			Exceed_Number_Of_Attempts,
			// Token: 0x0400011B RID: 283
			Time_Delay_Not_Expired,
			// Token: 0x0400011C RID: 284
			Download_Not_Accepted = 64,
			// Token: 0x0400011D RID: 285
			Improper_Download_Type,
			// Token: 0x0400011E RID: 286
			Not_Download_Number_Of_Bytes = 67,
			// Token: 0x0400011F RID: 287
			UploadNotAccepted = 80,
			// Token: 0x04000120 RID: 288
			Improper_Upload_Type,
			// Token: 0x04000121 RID: 289
			Not_Upload_From_Specified_Address,
			// Token: 0x04000122 RID: 290
			Not_Upload_Number_Of_Bytes,
			// Token: 0x04000123 RID: 291
			Transfer_Suspended = 113,
			// Token: 0x04000124 RID: 292
			Transfer_Aborted,
			// Token: 0x04000125 RID: 293
			Illegal_Address_In_Block_Transfer = 116,
			// Token: 0x04000126 RID: 294
			Illegal_Byte_Count_In_Block_Transfer,
			// Token: 0x04000127 RID: 295
			Illegal_Block_Transfer_Type,
			// Token: 0x04000128 RID: 296
			Transfer_Data_Checksum_Error,
			// Token: 0x04000129 RID: 297
			Correctly_Received,
			// Token: 0x0400012A RID: 298
			Incorrectly_Byte_Count,
			// Token: 0x0400012B RID: 299
			Negative_Response = 127,
			// Token: 0x0400012C RID: 300
			Service_Not_Supported_In_Active_Diagnostic_Mode,
			// Token: 0x0400012D RID: 301
			Done = 144
		}
	}
}
