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
	//
	// Created by SharpDevelop.
	// User: elijah
	// Date: 10/17/2011
	// Time: 10:59 AM
	// 
	// To change this template use Tools | Options | Coding | Edit Standard Headers.
	//
	partial class AdvancedForm : System.Windows.Forms.Form
	{

		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.titleBar1 = new TitleBar();
			this.SuspendLayout();
			//
			//titleBar1
			//
			this.titleBar1.BackColor = System.Drawing.SystemColors.Control;
			this.titleBar1.ControlBoxAffects = TitleBar.eControlBoxAffects.ParentForm;
			this.titleBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.titleBar1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.titleBar1.IsFormActive = true;
			this.titleBar1.Location = new System.Drawing.Point(0, 0);
			this.titleBar1.Name = "titleBar1";
			this.titleBar1.resizeDir = TitleBar.ResizeDirection.None;
			this.titleBar1.Size = new System.Drawing.Size(576, 30);
			this.titleBar1.TabIndex = 0;
			this.titleBar1.TitleImage = null;
			this.titleBar1.TitleImageSize = new System.Drawing.Size(24, 24);
			//
			//TitleBarForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(573, 174);
			this.Controls.Add(this.titleBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "TitleBarForm";
			this.Text = "Doesn't Matter";
			this.ResumeLayout(false);
		}
		public TitleBar titleBar1;
	}
}
