using System;

namespace DIUS4.Adapters
{
	// Token: 0x02000011 RID: 17
	public enum CmdULA : byte
	{
		// Token: 0x04000076 RID: 118
		CAN_Open = 79,
		// Token: 0x04000077 RID: 119
		CAN_Close = 67,
		// Token: 0x04000078 RID: 120
		CAN_Message = 84,
		// Token: 0x04000079 RID: 121
		CAN_Speed = 83,
		// Token: 0x0400007A RID: 122
		CAN250 = 53,
		// Token: 0x0400007B RID: 123
		CAN500,
		// Token: 0x0400007C RID: 124
		FilterType = 70,
		// Token: 0x0400007D RID: 125
		Mask = 77,
		// Token: 0x0400007E RID: 126
		Filter = 109,
		// Token: 0x0400007F RID: 127
		Name = 78,
		// Token: 0x04000080 RID: 128
		Version = 86,
		// Token: 0x04000081 RID: 129
		Restart = 68,
		// Token: 0x04000082 RID: 130
		Connect = 170
	}
}
