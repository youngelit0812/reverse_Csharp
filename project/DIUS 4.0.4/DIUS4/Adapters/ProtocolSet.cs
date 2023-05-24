using System;

namespace DIUS4.Adapters
{
	// Token: 0x0200000C RID: 12
	public struct ProtocolSet
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002A3F File Offset: 0x00000C3F
		public void Init_Devices(int DevicesCount)
		{
			this.Devices = new Devices_List[DevicesCount + 1];
		}

		// Token: 0x0400002A RID: 42
		public string Name;

		// Token: 0x0400002B RID: 43
		public bool Mode;

		// Token: 0x0400002C RID: 44
		public string BitRate;

		// Token: 0x0400002D RID: 45
		public string Filter;

		// Token: 0x0400002E RID: 46
		public string Mask;

		// Token: 0x0400002F RID: 47
		public Devices_List[] Devices;
	}
}
