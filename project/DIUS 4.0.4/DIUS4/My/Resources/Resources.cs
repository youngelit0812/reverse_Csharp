using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4.My.Resources
{
	// Token: 0x02000005 RID: 5
	[StandardModule]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[HideModuleName]
	internal sealed class Resources
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002116 File Offset: 0x00000316
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					Resources.resourceMan = new ResourceManager("DIUS4.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000214F File Offset: 0x0000034F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002157 File Offset: 0x00000357
		internal static Bitmap access
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("access", Resources.resourceCulture));
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002177 File Offset: 0x00000377
		internal static Bitmap BRP
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("BRP", Resources.resourceCulture));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002197 File Offset: 0x00000397
		internal static Bitmap buds2
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("buds2", Resources.resourceCulture));
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021B7 File Offset: 0x000003B7
		internal static string Disclaimer
		{
			get
			{
				return Resources.ResourceManager.GetString("Disclaimer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021CD File Offset: 0x000003CD
		internal static Bitmap dius_files
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("dius_files", Resources.resourceCulture));
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021ED File Offset: 0x000003ED
		internal static Bitmap dius_tools
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("dius_tools", Resources.resourceCulture));
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000220D File Offset: 0x0000040D
		internal static Bitmap firmware
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("firmware", Resources.resourceCulture));
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000222D File Offset: 0x0000042D
		internal static Bitmap m3c
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("m3c", Resources.resourceCulture));
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000224D File Offset: 0x0000044D
		internal static Bitmap ula
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("ula", Resources.resourceCulture));
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000226D File Offset: 0x0000046D
		internal static Bitmap ula_device
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("ula_device", Resources.resourceCulture));
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000228D File Offset: 0x0000048D
		internal static Bitmap ula_firmware
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("ula_firmware", Resources.resourceCulture));
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022AD File Offset: 0x000004AD
		internal static Bitmap users
		{
			get
			{
				return (Bitmap)RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("users", Resources.resourceCulture));
			}
		}

		// Token: 0x04000006 RID: 6
		private static ResourceManager resourceMan;

		// Token: 0x04000007 RID: 7
		private static CultureInfo resourceCulture;
	}
}
