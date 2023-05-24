namespace DIUS4
{
	// Token: 0x0200001D RID: 29
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class Dius : global::Telerik.WinControls.UI.RadForm
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000EF08 File Offset: 0x0000D108
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::Telerik.WinControls.UI.ListViewDetailColumn listViewDetailColumn = new global::Telerik.WinControls.UI.ListViewDetailColumn("Column 0", "Key");
			global::Telerik.WinControls.UI.ListViewDetailColumn listViewDetailColumn2 = new global::Telerik.WinControls.UI.ListViewDetailColumn("Column 1", "Value");
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DIUS4.Dius));
			this.lstVariables = new global::Telerik.WinControls.UI.RadListView();
			this.Panel = new global::Telerik.WinControls.UI.RadPageView();
			this.prop = new global::Telerik.WinControls.UI.RadPropertyGrid();
			this.List = new global::System.Windows.Forms.ListBox();
			this.Office2010BlackTheme1 = new global::Telerik.WinControls.Themes.Office2010BlackTheme();
			this.Progress = new global::Telerik.WinControls.UI.RadProgressBar();
			this.TitleBar = new global::Telerik.WinControls.UI.RadTextBox();
			this.TitleMail = new global::Telerik.WinControls.UI.RadTextBox();
			((global::System.ComponentModel.ISupportInitialize)this.lstVariables).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.Panel).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.prop).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.Progress).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.TitleBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.TitleMail).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this).BeginInit();
			base.SuspendLayout();
			this.lstVariables.AllowRemove = false;
			this.lstVariables.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			listViewDetailColumn.HeaderText = "Key";
			listViewDetailColumn.MaxWidth = 100f;
			listViewDetailColumn.Width = 100f;
			listViewDetailColumn2.HeaderText = "Value";
			listViewDetailColumn2.MaxWidth = 100f;
			listViewDetailColumn2.Width = 100f;
			this.lstVariables.Columns.AddRange(new global::Telerik.WinControls.UI.ListViewDetailColumn[]
			{
				listViewDetailColumn,
				listViewDetailColumn2
			});
			this.lstVariables.GroupItemSize = new global::System.Drawing.Size(100, 20);
			this.lstVariables.HeaderHeight = 22f;
			this.lstVariables.ItemSize = new global::System.Drawing.Size(80, 20);
			this.lstVariables.ItemSpacing = -1;
			this.lstVariables.Location = new global::System.Drawing.Point(1, 49);
			this.lstVariables.Name = "lstVariables";
			this.lstVariables.ShowGridLines = true;
			this.lstVariables.ShowItemToolTips = false;
			this.lstVariables.Size = new global::System.Drawing.Size(404, 283);
			this.lstVariables.TabIndex = 0;
			this.lstVariables.ThemeName = "Office2010Black";
			this.lstVariables.ViewType = global::Telerik.WinControls.UI.ListViewType.DetailsView;
			this.lstVariables.Visible = false;
			this.Panel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.Panel.Location = new global::System.Drawing.Point(-5, 49);
			this.Panel.Name = "Panel";
			this.Panel.Size = new global::System.Drawing.Size(832, 290);
			this.Panel.TabIndex = 1;
			this.Panel.ThemeName = "Office2010Black";
			this.prop.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.prop.AutoScroll = true;
			this.prop.HelpVisible = false;
			this.prop.Location = new global::System.Drawing.Point(411, 49);
			this.prop.Name = "prop";
			this.prop.PropertySort = global::System.Windows.Forms.PropertySort.CategorizedAlphabetical;
			this.prop.ShowItemToolTips = false;
			this.prop.Size = new global::System.Drawing.Size(323, 283);
			this.prop.SortOrder = global::System.Windows.Forms.SortOrder.Ascending;
			this.prop.TabIndex = 0;
			this.prop.ThemeName = "Office2010Black";
			this.prop.ToolbarVisible = true;
			this.prop.Visible = false;
			this.List.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.List.FormattingEnabled = true;
			this.List.HorizontalScrollbar = true;
			this.List.Location = new global::System.Drawing.Point(741, 49);
			this.List.Name = "List";
			this.List.Size = new global::System.Drawing.Size(66, 290);
			this.List.TabIndex = 12;
			this.Progress.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.Progress.Font = new global::System.Drawing.Font("Segoe UI", 14.25f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 204);
			this.Progress.Location = new global::System.Drawing.Point(245, 3);
			this.Progress.Name = "Progress";
			this.Progress.RootElement.BorderHighlightThickness = 1;
			this.Progress.RootElement.FocusBorderWidth = 3;
			this.Progress.Size = new global::System.Drawing.Size(356, 42);
			this.Progress.TabIndex = 13;
			this.Progress.ThemeName = "ControlDefault";
			this.TitleBar.BackColor = global::System.Drawing.Color.FromArgb(146, 146, 146);
			this.TitleBar.Font = new global::System.Drawing.Font("Segoe UI Black", 15.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 204);
			this.TitleBar.ForeColor = global::System.Drawing.Color.Yellow;
			this.TitleBar.Location = new global::System.Drawing.Point(2, 6);
			this.TitleBar.Name = "TitleBar";
			this.TitleBar.Size = new global::System.Drawing.Size(237, 32);
			this.TitleBar.TabIndex = 14;
			this.TitleBar.Text = "Debuger";
			this.TitleBar.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.TitleBar.ThemeName = "Office2010Black";
			((global::Telerik.WinControls.UI.RadTextBoxElement)this.TitleBar.GetChildAt(0)).Text = "Debuger";
			((global::Telerik.WinControls.UI.RadTextBoxElement)this.TitleBar.GetChildAt(0)).BorderHighlightThickness = 0;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleBar.GetChildAt(0).GetChildAt(2)).Width = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleBar.GetChildAt(0).GetChildAt(2)).LeftWidth = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleBar.GetChildAt(0).GetChildAt(2)).TopWidth = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleBar.GetChildAt(0).GetChildAt(2)).RightWidth = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleBar.GetChildAt(0).GetChildAt(2)).BottomWidth = 0f;
			this.TitleMail.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.TitleMail.BackColor = global::System.Drawing.Color.FromArgb(146, 146, 146);
			this.TitleMail.Font = new global::System.Drawing.Font("Segoe UI Semibold", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 204);
			this.TitleMail.ForeColor = global::System.Drawing.Color.Yellow;
			this.TitleMail.Location = new global::System.Drawing.Point(607, 11);
			this.TitleMail.Name = "TitleMail";
			this.TitleMail.Size = new global::System.Drawing.Size(212, 25);
			this.TitleMail.TabIndex = 15;
			this.TitleMail.Text = "Email";
			this.TitleMail.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.TitleMail.ThemeName = "Office2010Black";
			((global::Telerik.WinControls.UI.RadTextBoxElement)this.TitleMail.GetChildAt(0)).Text = "Email";
			((global::Telerik.WinControls.UI.RadTextBoxElement)this.TitleMail.GetChildAt(0)).BorderHighlightThickness = 0;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleMail.GetChildAt(0).GetChildAt(2)).Width = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleMail.GetChildAt(0).GetChildAt(2)).LeftWidth = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleMail.GetChildAt(0).GetChildAt(2)).TopWidth = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleMail.GetChildAt(0).GetChildAt(2)).RightWidth = 0f;
			((global::Telerik.WinControls.Primitives.BorderPrimitive)this.TitleMail.GetChildAt(0).GetChildAt(2)).BottomWidth = 0f;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(823, 335);
			base.Controls.Add(this.TitleMail);
			base.Controls.Add(this.Panel);
			base.Controls.Add(this.TitleBar);
			base.Controls.Add(this.Progress);
			base.Controls.Add(this.List);
			base.Controls.Add(this.prop);
			base.Controls.Add(this.lstVariables);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "Dius";
			base.RootElement.ApplyShapeToControl = true;
			base.ShowIcon = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "";
			base.ThemeName = "Office2010Black";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			((global::System.ComponentModel.ISupportInitialize)this.lstVariables).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.Panel).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.prop).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.Progress).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.TitleBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.TitleMail).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000DD RID: 221
		private global::System.ComponentModel.IContainer components;
	}
}
