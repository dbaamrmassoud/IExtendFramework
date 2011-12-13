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
	partial class AdvancedTrackBar : System.Windows.Forms.UserControl
	{

		//UserControl1 overrides dispose to clean up the component list.
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
			this.SuspendLayout();
			//
			//Controls
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(5f, 9f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Controls";
			this.Size = new System.Drawing.Size(250, 30);
			this.ResumeLayout(false);

		}

	}
}
