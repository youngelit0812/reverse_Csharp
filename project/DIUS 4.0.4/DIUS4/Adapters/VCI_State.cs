using System;

namespace DIUS4.Adapters
{
	// Token: 0x02000009 RID: 9
	public struct VCI_State
	{
		// Token: 0x04000010 RID: 16
		public int Type;

		// Token: 0x04000011 RID: 17
		public int Index;

		// Token: 0x04000012 RID: 18
		public int Chanel;

		// Token: 0x04000013 RID: 19
		public byte Filter;

		// Token: 0x04000014 RID: 20
		public int AccCode;

		// Token: 0x04000015 RID: 21
		public int AccMask;

		// Token: 0x04000016 RID: 22
		public int BaudRateCAN;

		// Token: 0x04000017 RID: 23
		public bool Mode;

		// Token: 0x04000018 RID: 24
		public bool IsOpen;

		// Token: 0x04000019 RID: 25
		public bool ActiveCAN;

		// Token: 0x0400001A RID: 26
		public bool ActiveKLINE;
	}
}
