using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
	// Token: 0x02000028 RID: 40
	[CompilerGenerated]
	[Guid("000209B1-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Replacement
	{
		// Token: 0x0600017E RID: 382
		void _VtblGap1_9();

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017F RID: 383
		// (set) Token: 0x06000180 RID: 384
		[DispId(15)]
		string Text { [DispId(15)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(15)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
