using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace IExtendFramework.Controls
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class TitleBar : System.Windows.Forms.UserControl
	{

		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try {
				if (disposing && components != null) {
					components.Dispose();
				}
			} finally {
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.CloseWindowButton = new System.Windows.Forms.Button();
			this.MaximizeWindowButton = new System.Windows.Forms.Button();
			this.MinimizeWindowButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//CloseWindowButton
			//
			this.CloseWindowButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CloseWindowButton.BackColor = System.Drawing.SystemColors.Control;
			this.CloseWindowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.CloseWindowButton.FlatAppearance.BorderSize = 0;
			this.CloseWindowButton.Location = new System.Drawing.Point(549, 6);
			this.CloseWindowButton.Name = "CloseWindowButton";
			this.CloseWindowButton.Size = new System.Drawing.Size(21, 21);
			this.CloseWindowButton.TabIndex = 0;
			this.CloseWindowButton.UseVisualStyleBackColor = false;
			this.CloseWindowButton.Click += this.CloseWindowButton_Click;
			//
			//MaximizeWindowButton
			//
			this.MaximizeWindowButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.MaximizeWindowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.MaximizeWindowButton.FlatAppearance.BorderSize = 0;
			this.MaximizeWindowButton.Location = new System.Drawing.Point(526, 6);
			this.MaximizeWindowButton.Name = "MaximizeWindowButton";
			this.MaximizeWindowButton.Size = new System.Drawing.Size(21, 21);
			this.MaximizeWindowButton.TabIndex = 1;
			this.MaximizeWindowButton.UseVisualStyleBackColor = true;
			//
			//MinimizeWindowButton
			//
			this.MinimizeWindowButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.MinimizeWindowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.MinimizeWindowButton.FlatAppearance.BorderSize = 0;
			this.MinimizeWindowButton.Location = new System.Drawing.Point(503, 6);
			this.MinimizeWindowButton.Name = "MinimizeWindowButton";
			this.MinimizeWindowButton.Size = new System.Drawing.Size(21, 21);
			this.MinimizeWindowButton.TabIndex = 2;
			this.MinimizeWindowButton.UseVisualStyleBackColor = true;
			//
			//TitleBar
			//
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.MinimizeWindowButton);
			this.Controls.Add(this.MaximizeWindowButton);
			this.Controls.Add(this.CloseWindowButton);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.Name = "TitleBar";
			this.Size = new System.Drawing.Size(576, 30);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button withEventsField_CloseWindowButton;
		internal System.Windows.Forms.Button CloseWindowButton {
			get { return withEventsField_CloseWindowButton; }
			set {
				if (withEventsField_CloseWindowButton != null) {
					withEventsField_CloseWindowButton.Click -= CloseWindowButton_Click;
					withEventsField_CloseWindowButton.MouseDown -= CloseWindowButton_MouseDown;
					withEventsField_CloseWindowButton.MouseEnter -= CloseWindowButton_MouseEnter;
					withEventsField_CloseWindowButton.MouseLeave -= CloseWindowButton_MouseLeave;
					withEventsField_CloseWindowButton.MouseUp -= CloseWindowButton_MouseUp;
					withEventsField_CloseWindowButton.Paint -= CloseWindowButton_Paint;
				}
				withEventsField_CloseWindowButton = value;
				if (withEventsField_CloseWindowButton != null) {
					withEventsField_CloseWindowButton.Click += CloseWindowButton_Click;
					withEventsField_CloseWindowButton.MouseDown += CloseWindowButton_MouseDown;
					withEventsField_CloseWindowButton.MouseEnter += CloseWindowButton_MouseEnter;
					withEventsField_CloseWindowButton.MouseLeave += CloseWindowButton_MouseLeave;
					withEventsField_CloseWindowButton.MouseUp += CloseWindowButton_MouseUp;
					withEventsField_CloseWindowButton.Paint += CloseWindowButton_Paint;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_MaximizeWindowButton;
		internal System.Windows.Forms.Button MaximizeWindowButton {
			get { return withEventsField_MaximizeWindowButton; }
			set {
				if (withEventsField_MaximizeWindowButton != null) {
					withEventsField_MaximizeWindowButton.Click -= MaximizeWindowButton_Click;
					withEventsField_MaximizeWindowButton.MouseDown -= MaximizeWindowButton_MouseDown;
					withEventsField_MaximizeWindowButton.MouseEnter -= MaximizeWindowButton_MouseEnter;
					withEventsField_MaximizeWindowButton.MouseLeave -= MaximizeWindowButton_MouseLeave;
					withEventsField_MaximizeWindowButton.MouseUp -= MaximizeWindowButton_MouseUp;
					withEventsField_MaximizeWindowButton.Paint -= MaximizeWindowButton_Paint;
				}
				withEventsField_MaximizeWindowButton = value;
				if (withEventsField_MaximizeWindowButton != null) {
					withEventsField_MaximizeWindowButton.Click += MaximizeWindowButton_Click;
					withEventsField_MaximizeWindowButton.MouseDown += MaximizeWindowButton_MouseDown;
					withEventsField_MaximizeWindowButton.MouseEnter += MaximizeWindowButton_MouseEnter;
					withEventsField_MaximizeWindowButton.MouseLeave += MaximizeWindowButton_MouseLeave;
					withEventsField_MaximizeWindowButton.MouseUp += MaximizeWindowButton_MouseUp;
					withEventsField_MaximizeWindowButton.Paint += MaximizeWindowButton_Paint;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_MinimizeWindowButton;
		internal System.Windows.Forms.Button MinimizeWindowButton {
			get { return withEventsField_MinimizeWindowButton; }
			set {
				if (withEventsField_MinimizeWindowButton != null) {
					withEventsField_MinimizeWindowButton.Click -= MinimizeWindowButton_Click;
					withEventsField_MinimizeWindowButton.MouseDown -= MinimizeWindowButton_MouseDown;
					withEventsField_MinimizeWindowButton.MouseEnter -= MinimizeWindowButton_MouseEnter;
					withEventsField_MinimizeWindowButton.MouseLeave -= MinimizeWindowButton_MouseLeave;
					withEventsField_MinimizeWindowButton.MouseUp -= MinimizeWindowButton_MouseUp;
					withEventsField_MinimizeWindowButton.Paint -= MinimizeWindowButton_Paint;
				}
				withEventsField_MinimizeWindowButton = value;
				if (withEventsField_MinimizeWindowButton != null) {
					withEventsField_MinimizeWindowButton.Click += MinimizeWindowButton_Click;
					withEventsField_MinimizeWindowButton.MouseDown += MinimizeWindowButton_MouseDown;
					withEventsField_MinimizeWindowButton.MouseEnter += MinimizeWindowButton_MouseEnter;
					withEventsField_MinimizeWindowButton.MouseLeave += MinimizeWindowButton_MouseLeave;
					withEventsField_MinimizeWindowButton.MouseUp += MinimizeWindowButton_MouseUp;
					withEventsField_MinimizeWindowButton.Paint += MinimizeWindowButton_Paint;
				}
			}

		}
	}
}
