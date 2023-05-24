using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
	// Token: 0x0200002D RID: 45
	[CompilerGenerated]
	[DefaultMember("Name")]
	[Guid("0002096B-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface _Document
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018E RID: 398
		[DispId(0)]
		[IndexerName("Name")]
		string Name { [DispId(0)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x0600018F RID: 399
		void _VtblGap1_159();

		// Token: 0x06000190 RID: 400
		[DispId(1105)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Close([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object SaveChanges, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object OriginalFormat, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object RouteDocument);
	}
}
