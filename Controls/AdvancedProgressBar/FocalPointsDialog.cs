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
using System.Drawing;

namespace IExtendFramework.Controls
{
	public partial class FocalPointsDialog
	{

		private cFocalPoints fpp = new cFocalPoints();
		private void dlgEditSelected_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			if (e.KeyCode == Keys.Enter)
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void tbarFocalX_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right) {
				((TrackBar)sender).Value = 0;
				UpdateFocusScales();
			}
		}

		private void tbarFocalY_Scroll(System.Object sender, System.EventArgs e)
		{
			UpdateFocusScales();
		}

		public void UpdateFocusScales()
		{
			TheShape.FocalPoints.FocusScales = new PointF(Convert.ToSingle(tbarFocalX.Value / 1000), Convert.ToSingle(tbarFocalY.Value / 1000));
			TheShape.Invalidate();
			lblFx.Text = TheShape.FocalPoints.FocusScales.X.ToString();
			lblFy.Text = TheShape.FocalPoints.FocusScales.Y.ToString();
		}

		private void TheShape_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				TheShape.FocalPoints.CenterPoint = new PointF(Convert.ToSingle(e.X / TheShape.Width), Convert.ToSingle(e.Y / TheShape.Height));
			} else if (e.Button == System.Windows.Forms.MouseButtons.Right) {
				TheShape.FocalPoints.CenterPoint = new PointF(0.5F, 0.5F);
			}
			TheShape.Invalidate();
			UpdateCenterLabels(TheShape.FocalPoints.CenterPoint.X, TheShape.FocalPoints.CenterPoint.Y);
		}

		public void UpdateCenterLabels(double cx, double cy)
		{
			lblCx.Text = "Center X=" + Math.Round(cx, 4);
			lblCy.Text = "Center Y=" + Math.Round(cy, 4);
		}

		private void dlgFocalPoints_Shown(object sender, System.EventArgs e)
		{
			fpp = new cFocalPoints(TheShape.FocalPoints.CenterPoint.X, TheShape.FocalPoints.CenterPoint.Y, TheShape.FocalPoints.FocusScales.X, TheShape.FocalPoints.FocusScales.Y);
			tbarFocalX.Value = Convert.ToInt32(fpp.FocusScales.X * 1000);
			tbarFocalY.Value = Convert.ToInt32(fpp.FocusScales.Y * 1000);
			UpdateCenterLabels(fpp.CenterPoint.X, fpp.CenterPoint.Y);
			lblFx.Text = fpp.FocusScales.X.ToString();
			lblFy.Text = fpp.FocusScales.Y.ToString();
		}

		private void butCancel_Click(System.Object sender, System.EventArgs e)
		{
			TheShape.FocalPoints = new cFocalPoints(fpp.CenterPoint.X, fpp.CenterPoint.Y, fpp.FocusScales.X, fpp.FocusScales.Y);
		}
		public FocalPointsDialog()
		{
			Shown += dlgFocalPoints_Shown;
			KeyDown += dlgEditSelected_KeyDown;
			InitializeComponent();
		}
	}
}


