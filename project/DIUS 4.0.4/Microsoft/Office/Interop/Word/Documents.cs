using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
	// Token: 0x02000026 RID: 38
	[CompilerGenerated]
	[Guid("0002096C-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Documents : IEnumerable
	{
		// Token: 0x06000173 RID: 371
		void _VtblGap1_5();

		// Token: 0x17000055 RID: 85
		[DispId(0)]
		Document this[[MarshalAs(UnmanagedType.Struct)] [In] ref object Index]
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000175 RID: 373
		void _VtblGap2_9();

		// Token: 0x06000176 RID: 374
		[DispId(19)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		Document Open([MarshalAs(UnmanagedType.Struct)] [In] ref object FileName, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object ConfirmConversions, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object ReadOnly, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object AddToRecentFiles, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PasswordDocument, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object PasswordTemplate, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Revert, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object WritePasswordDocument, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object WritePasswordTemplate, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Format, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Encoding, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object Visible, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object OpenAndRepair, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object DocumentDirection, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object NoEncodingDialog, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object XMLTransform);
	}
}
