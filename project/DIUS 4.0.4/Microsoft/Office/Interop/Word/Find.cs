using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
	// Token: 0x02000027 RID: 39
	[CompilerGenerated]
	[Guid("000209B0-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Find
	{
		// Token: 0x06000177 RID: 375
		void _VtblGap1_26();

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000178 RID: 376
		// (set) Token: 0x06000179 RID: 377
		[DispId(22)]
		string Text { [DispId(22)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(22)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x0600017A RID: 378
		void _VtblGap2_4();

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017B RID: 379
		[DispId(25)]
		Replacement Replacement { [DispId(25)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600017C RID: 380
		void _VtblGap3_15();

		// Token: 0x0600017D RID: 381
		[DispId(444)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool Execute([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object FindText, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchCase, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchWholeWord, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchWildcards, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchSoundsLike, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchAllWordForms, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Forward, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Wrap, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Format, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object ReplaceWith, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Replace, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchKashida, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchDiacritics, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchAlefHamza, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object MatchControl);
	}
}
