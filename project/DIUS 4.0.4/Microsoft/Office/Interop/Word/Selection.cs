using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
	// Token: 0x02000029 RID: 41
	[CompilerGenerated]
	[DefaultMember("Text")]
	[Guid("00020975-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Selection
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000181 RID: 385
		// (set) Token: 0x06000182 RID: 386
		[DispId(0)]
		[IndexerName("Text")]
		string Text { [DispId(0)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(0)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x06000183 RID: 387
		void _VtblGap1_47();

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000184 RID: 388
		[DispId(262)]
		Find Find { [DispId(262)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
