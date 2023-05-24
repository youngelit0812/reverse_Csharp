using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
	// Token: 0x0200002C RID: 44
	[CompilerGenerated]
	[DefaultMember("Name")]
	[Guid("00020970-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface _Application
	{
		// Token: 0x06000185 RID: 389
		void _VtblGap1_3();

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000186 RID: 390
		[DispId(0)]
		[IndexerName("Name")]
		string Name { [DispId(0)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000187 RID: 391
		[DispId(6)]
		Documents Documents { [DispId(6)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06000188 RID: 392
		void _VtblGap2_3();

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000189 RID: 393
		[DispId(5)]
		Selection Selection { [DispId(5)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600018A RID: 394
		void _VtblGap3_104();

		// Token: 0x0600018B RID: 395
		[DispId(1105)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Quit([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object SaveChanges, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object OriginalFormat, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object RouteDocument);

		// Token: 0x0600018C RID: 396
		void _VtblGap4_74();

		// Token: 0x0600018D RID: 397
		[DispId(448)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void PrintOut([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Background, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Append, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Range, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object OutputFileName, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object From, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object To, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Item, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Copies, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Pages, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PageType, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PrintToFile, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Collate, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object FileName, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object ActivePrinterMacGX, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object ManualDuplexPrint, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PrintZoomColumn, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PrintZoomRow, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PrintZoomPaperWidth, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PrintZoomPaperHeight);
	}
}
