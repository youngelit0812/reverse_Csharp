namespace DIUS4
{
	// Token: 0x02000017 RID: 23
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class Disclaimer : global::Telerik.WinControls.UI.RadForm
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00008404 File Offset: 0x00006604
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

		// Token: 0x060000E1 RID: 225 RVA: 0x00008444 File Offset: 0x00006644
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.Office2010Black = new global::Telerik.WinControls.Themes.Office2010BlackTheme();
			this.RadLabel1 = new global::Telerik.WinControls.UI.RadLabel();
			((global::System.ComponentModel.ISupportInitialize)this.RadLabel1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this).BeginInit();
			base.SuspendLayout();
			this.RadLabel1.AutoSize = false;
			this.RadLabel1.BackColor = global::System.Drawing.Color.FromArgb(255, 255, 192);
			this.RadLabel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.RadLabel1.Location = new global::System.Drawing.Point(0, 0);
			this.RadLabel1.Name = "RadLabel1";
			this.RadLabel1.Size = new global::System.Drawing.Size(792, 566);
			this.RadLabel1.TabIndex = 0;
			this.RadLabel1.Text = "RadLabel1";
			this.RadLabel1.ThemeName = "ControlDefault";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(792, 566);
			base.Controls.Add(this.RadLabel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.IconScaling = global::Telerik.WinControls.Enumerations.ImageScaling.None;
			base.Name = "Disclaimer";
			base.RootElement.ApplyShapeToControl = true;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.ShowItemToolTips = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DIUS Disclaimer";
			base.ThemeName = "Office2010Black";
			base.TopMost = true;
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			((global::System.ComponentModel.ISupportInitialize)this.RadLabel1).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x040000B3 RID: 179
		private global::System.ComponentModel.IContainer components;
	}
}
