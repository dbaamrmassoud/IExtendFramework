using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;

namespace IExtendFramework.Controls
{
	public partial class CornersDialog
	{

		private CornersProperty cnrs = new CornersProperty();
		private void OK_Button_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void Cancel_Button_Click(System.Object sender, System.EventArgs e)
		{
			TheShape.Corners.UpperLeft = cnrs.UpperLeft;
			TheShape.Corners.UpperRight = cnrs.UpperRight;
			TheShape.Corners.LowerLeft = cnrs.LowerLeft;
			TheShape.Corners.LowerRight = cnrs.LowerRight;

			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void tbarUpperLeft_Scroll(System.Object sender, System.EventArgs e)
		{
			TheShape.Corners.UpperLeft = Convert.ToInt16(tbarUpperLeft.Value);
			TheShape.Invalidate();
		}

		private void tbarUpperRight_Scroll(System.Object sender, System.EventArgs e)
		{
			TheShape.Corners.UpperRight = Convert.ToInt16(tbarUpperRight.Value);
			TheShape.Invalidate();
		}

		private void tbarLowerLeft_Scroll(System.Object sender, System.EventArgs e)
		{
			TheShape.Corners.LowerLeft = Convert.ToInt16(tbarLowerLeft.Value);
			TheShape.Invalidate();
		}

		private void tbarLowerRight_Scroll(System.Object sender, System.EventArgs e)
		{
			TheShape.Corners.LowerRight = Convert.ToInt16(tbarLowerRight.Value);
			TheShape.Invalidate();
		}

		private void tbarAll_Scroll(System.Object sender, System.EventArgs e)
		{
			tbarUpperLeft.Value = tbarAll.Value;
			tbarUpperRight.Value = tbarAll.Value;
			tbarLowerLeft.Value = tbarAll.Value;
			tbarLowerRight.Value = tbarAll.Value;
			TheShape.Corners.UpperLeft = Convert.ToInt16(tbarAll.Value);
			TheShape.Corners.UpperRight = Convert.ToInt16(tbarAll.Value);
			TheShape.Corners.LowerLeft = Convert.ToInt16(tbarAll.Value);
			TheShape.Corners.LowerRight = Convert.ToInt16(tbarAll.Value);
			TheShape.Invalidate();

		}

		private void dlgCorners_Load(object sender, System.EventArgs e)
		{
			cnrs.UpperLeft = TheShape.Corners.UpperLeft;
			cnrs.UpperRight = TheShape.Corners.UpperRight;
			cnrs.LowerLeft = TheShape.Corners.LowerLeft;
			cnrs.LowerRight = TheShape.Corners.LowerRight;
		}

		private void HSBarSample_Scroll(System.Object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			TheShape.Value = HSBarSample.Value;
		}
		public CornersDialog()
		{
			Load += dlgCorners_Load;
			InitializeComponent();
		}

	}



}

