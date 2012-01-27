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
	public partial class AdvancedForm
	{
		#region "Borderless Form Helper"

		private const int HTLEFT = 10;
		private const int HTRIGHT = 11;
		private const int HTTOP = 12;
		private const int HTTOPLEFT = 13;
		private const int HTTOPRIGHT = 14;
		private const int HTBOTTOM = 15;
		private const int HTBOTTOMLEFT = 16;
		private const int HTBOTTOMRIGHT = 17;

		private const int WM_NCHITTEST = 0x84;
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if (m.Msg == WM_NCHITTEST) {
				Point pt = new Point(m.LParam.ToInt32());
				pt = PointToClient(pt);
				if (pt.X < titleBar1.FrameWidth && pt.Y < titleBar1.FrameWidth) {
					m.Result = new IntPtr(HTTOPLEFT);
				} else if (pt.X > (Width - titleBar1.FrameWidth) && pt.Y < titleBar1.FrameWidth) {
					m.Result = new IntPtr(HTTOPRIGHT);
				} else if (pt.Y < titleBar1.FrameWidth) {
					m.Result = new IntPtr(HTTOP);
				} else if (pt.X < titleBar1.FrameWidth && pt.Y > (Height - titleBar1.FrameWidth - titleBar1.FrameHeightAdj)) {
					m.Result = new IntPtr(HTBOTTOMLEFT);
				} else if (pt.X > (Width - titleBar1.FrameWidth) && pt.Y > (Height - titleBar1.FrameWidth - titleBar1.FrameHeightAdj)) {
					m.Result = new IntPtr(HTBOTTOMRIGHT);
				} else if (pt.Y > (Height - titleBar1.FrameWidth - titleBar1.FrameHeightAdj)) {
					m.Result = new IntPtr(HTBOTTOM);
				} else if (pt.X < titleBar1.FrameWidth) {
					m.Result = new IntPtr(HTLEFT);
				} else if (pt.X > (Width - titleBar1.FrameWidth)) {
					m.Result = new IntPtr(HTRIGHT);
				} else {
					base.WndProc(ref m);
				}
			} else {
				base.WndProc(ref m);
			}
		}

		private void Form1_Activated(object sender, System.EventArgs e)
		{
			titleBar1.IsFormActive = true;
		}

		private void Form1_Deactivate(object sender, System.EventArgs e)
		{
			titleBar1.IsFormActive = false;
		}

		#endregion

		public AdvancedForm()
		{
			Deactivate += Form1_Deactivate;
			Activated += Form1_Activated;
			// The Me.InitializeComponent call is required for Windows Forms designer support.
			this.InitializeComponent();

			//
			//
		}
	}
}
