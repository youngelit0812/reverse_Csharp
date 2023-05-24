using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DIUS4.My.Resources;
using Microsoft.VisualBasic.CompilerServices;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace DIUS4
{
	// Token: 0x02000017 RID: 23
	[DesignerGenerated]
	public partial class Disclaimer : RadForm
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000085DC File Offset: 0x000067DC
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000085E4 File Offset: 0x000067E4
		internal virtual Office2010BlackTheme Office2010Black { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000085ED File Offset: 0x000067ED
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000085F5 File Offset: 0x000067F5
		internal virtual RadLabel RadLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060000E6 RID: 230 RVA: 0x000085FE File Offset: 0x000067FE
		public Disclaimer()
		{
			this.InitializeComponent();
			this.RadLabel1.Text = Resources.Disclaimer;
		}
	}
}
