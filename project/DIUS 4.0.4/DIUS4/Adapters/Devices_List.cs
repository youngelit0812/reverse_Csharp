using System;

namespace DIUS4.Adapters
{
	// Token: 0x0200000D RID: 13
	public struct Devices_List
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002A50 File Offset: 0x00000C50
		public override string ToString()
		{
			return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", new object[]
			{
				this.Name,
				this.Description,
				this.Address,
				this.Echo,
				this.TesterCmd,
				this.IdentCmd,
				this.Burst,
				this.Tables,
				this.Param,
				this.Mode
			});
		}

		// Token: 0x04000030 RID: 48
		public string Name;

		// Token: 0x04000031 RID: 49
		public string Description;

		// Token: 0x04000032 RID: 50
		public string Address;

		// Token: 0x04000033 RID: 51
		public string Echo;

		// Token: 0x04000034 RID: 52
		public int TesterCmd;

		// Token: 0x04000035 RID: 53
		public int IdentCmd;

		// Token: 0x04000036 RID: 54
		public int Burst;

		// Token: 0x04000037 RID: 55
		public string Tables;

		// Token: 0x04000038 RID: 56
		public string Param;

		// Token: 0x04000039 RID: 57
		public int Mode;
	}
}
