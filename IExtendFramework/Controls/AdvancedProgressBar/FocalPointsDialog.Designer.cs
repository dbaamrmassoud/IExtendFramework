using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
namespace IExtendFramework.Controls
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class FocalPointsDialog : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
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
			cBlendItems CBlendItems1 = new cBlendItems();
			cFocalPoints CFocalPoints1 = new cFocalPoints();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FocalPointsDialog));
			this.butApply = new System.Windows.Forms.Button();
			this.tbarFocalX = new System.Windows.Forms.TrackBar();
			this.tbarFocalY = new System.Windows.Forms.TrackBar();
			this.panShapeHolder = new System.Windows.Forms.Panel();
			this.TheShape = new ProgressBar();
			this.lblFy = new System.Windows.Forms.Label();
			this.lblFx = new System.Windows.Forms.Label();
			this.lblCx = new System.Windows.Forms.Label();
			this.lblCy = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)this.tbarFocalX).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.tbarFocalY).BeginInit();
			this.panShapeHolder.SuspendLayout();
			this.SuspendLayout();
			//
			//butApply
			//
			this.butApply.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.butApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butApply.Location = new System.Drawing.Point(106, 301);
			this.butApply.Name = "butApply";
			this.butApply.Size = new System.Drawing.Size(67, 23);
			this.butApply.TabIndex = 0;
			this.butApply.Text = "Apply";
			//
			//tbarFocalX
			//
			this.tbarFocalX.LargeChange = 10;
			this.tbarFocalX.Location = new System.Drawing.Point(12, 218);
			this.tbarFocalX.Maximum = 1000;
			this.tbarFocalX.Name = "tbarFocalX";
			this.tbarFocalX.Size = new System.Drawing.Size(200, 45);
			this.tbarFocalX.TabIndex = 3;
			this.tbarFocalX.TickFrequency = 50;
			this.tbarFocalX.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.tbarFocalX.Value = 500;
			//
			//tbarFocalY
			//
			this.tbarFocalY.Location = new System.Drawing.Point(218, 12);
			this.tbarFocalY.Maximum = 1000;
			this.tbarFocalY.Name = "tbarFocalY";
			this.tbarFocalY.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tbarFocalY.Size = new System.Drawing.Size(45, 200);
			this.tbarFocalY.TabIndex = 3;
			this.tbarFocalY.TickFrequency = 50;
			this.tbarFocalY.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.tbarFocalY.Value = 500;
			//
			//panShapeHolder
			//
			this.panShapeHolder.Controls.Add(this.TheShape);
			this.panShapeHolder.Location = new System.Drawing.Point(12, 12);
			this.panShapeHolder.Name = "panShapeHolder";
			this.panShapeHolder.Size = new System.Drawing.Size(200, 200);
			this.panShapeHolder.TabIndex = 4;
			//
			//TheShape
			//
			CBlendItems1.iColor = new System.Drawing.Color[] {
				System.Drawing.Color.White,
				System.Drawing.Color.White
			};
			CBlendItems1.iPoint = new float[] {
				0f,
				1f
			};
			this.TheShape.BarColorBlend = CBlendItems1;
			this.TheShape.BarColorSolid = System.Drawing.Color.Blue;
			this.TheShape.BarColorSolidB = System.Drawing.Color.White;
			this.TheShape.BarLength = ProgressBar.eBarLength.Full;
			this.TheShape.BarLengthValue = Convert.ToInt16(25);
			this.TheShape.BarPadding = new System.Windows.Forms.Padding(0);
			this.TheShape.BarStyleFill = ProgressBar.eBarStyle.Solid;
			this.TheShape.BarStyleHatch = System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard;
			this.TheShape.BarStyleLinear = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.TheShape.BarStyleTexture = null;
			this.TheShape.BarStyleWrapMode = System.Drawing.Drawing2D.WrapMode.Clamp;
			this.TheShape.BarType = ProgressBar.eBarType.Bar;
			this.TheShape.CylonMove = 5f;
			this.TheShape.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TheShape.BorderWidth = Convert.ToInt16(1);
			this.TheShape.Corners.All = Convert.ToInt16(0);
			this.TheShape.Corners.LowerLeft = Convert.ToInt16(0);
			this.TheShape.Corners.LowerRight = Convert.ToInt16(0);
			this.TheShape.Corners.UpperLeft = Convert.ToInt16(0);
			this.TheShape.Corners.UpperRight = Convert.ToInt16(0);
			this.TheShape.CornersApply = ProgressBar.eCornersApply.Both;
			this.TheShape.FillDirection = ProgressBar.eFillDirection.Up_Right;
			CFocalPoints1.CenterPoint = (System.Drawing.PointF)resources.GetObject("CFocalPoints1.CenterPoint");
			CFocalPoints1.FocusScales = (System.Drawing.PointF)resources.GetObject("CFocalPoints1.FocusScales");
			this.TheShape.FocalPoints = CFocalPoints1;
			this.TheShape.CylonInterval = Convert.ToInt16(1);
			this.TheShape.Location = new System.Drawing.Point(0, 0);
			this.TheShape.Name = "TheShape";
			this.TheShape.Orientation = ProgressBar.eOrientation.Horizontal;
			this.TheShape.Shape = ProgressBar.eShape.Rectangle;
			this.TheShape.ShapeTextFont = new System.Drawing.Font("Arial Black", 30f);
			this.TheShape.ShapeTextRotate = ProgressBar.eRotateText.None;
			this.TheShape.Size = new System.Drawing.Size(200, 200);
			this.TheShape.TabIndex = 0;
			this.TheShape.TextAlignment = System.Drawing.StringAlignment.Center;
			this.TheShape.TextAlignmentVert = System.Drawing.StringAlignment.Center;
			this.TheShape.TextPlacement = ProgressBar.eTextPlacement.OverBar;
			this.TheShape.TextRotate = ProgressBar.eRotateText.None;
			this.TheShape.TextShow = ProgressBar.eTextShow.None;
			this.TheShape.Value = 100;
			//
			//lblFy
			//
			this.lblFy.Location = new System.Drawing.Point(220, 208);
			this.lblFy.Name = "lblFy";
			this.lblFy.Size = new System.Drawing.Size(37, 17);
			this.lblFy.TabIndex = 5;
			this.lblFy.Text = "0.5";
			this.lblFy.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			//lblFx
			//
			this.lblFx.Location = new System.Drawing.Point(206, 230);
			this.lblFx.Name = "lblFx";
			this.lblFx.Size = new System.Drawing.Size(37, 17);
			this.lblFx.TabIndex = 5;
			this.lblFx.Text = "0.5";
			this.lblFx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//lblCx
			//
			this.lblCx.AutoSize = true;
			this.lblCx.Location = new System.Drawing.Point(14, 272);
			this.lblCx.Name = "lblCx";
			this.lblCx.Size = new System.Drawing.Size(48, 13);
			this.lblCx.TabIndex = 6;
			this.lblCx.Text = "Center X";
			//
			//lblCy
			//
			this.lblCy.AutoSize = true;
			this.lblCy.Location = new System.Drawing.Point(132, 272);
			this.lblCy.Name = "lblCy";
			this.lblCy.Size = new System.Drawing.Size(48, 13);
			this.lblCy.TabIndex = 7;
			this.lblCy.Text = "Center Y";
			//
			//butCancel
			//
			this.butCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(179, 301);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(67, 23);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "Cancel";
			//
			//dlgFocalPoints
			//
			this.AcceptButton = this.butApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(258, 336);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butApply);
			this.Controls.Add(this.lblCy);
			this.Controls.Add(this.lblCx);
			this.Controls.Add(this.lblFx);
			this.Controls.Add(this.lblFy);
			this.Controls.Add(this.panShapeHolder);
			this.Controls.Add(this.tbarFocalY);
			this.Controls.Add(this.tbarFocalX);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgFocalPoints";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Adjust CenterPoint & Focus Scales";
			((System.ComponentModel.ISupportInitialize)this.tbarFocalX).EndInit();
			((System.ComponentModel.ISupportInitialize)this.tbarFocalY).EndInit();
			this.panShapeHolder.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.Button butApply;
		private System.Windows.Forms.TrackBar withEventsField_tbarFocalX;
		internal System.Windows.Forms.TrackBar tbarFocalX {
			get { return withEventsField_tbarFocalX; }
			set {
				if (withEventsField_tbarFocalX != null) {
					withEventsField_tbarFocalX.MouseDown -= tbarFocalX_MouseDown;
					withEventsField_tbarFocalX.Scroll -= tbarFocalY_Scroll;
				}
				withEventsField_tbarFocalX = value;
				if (withEventsField_tbarFocalX != null) {
					withEventsField_tbarFocalX.MouseDown += tbarFocalX_MouseDown;
					withEventsField_tbarFocalX.Scroll += tbarFocalY_Scroll;
				}
			}
		}
		private System.Windows.Forms.TrackBar withEventsField_tbarFocalY;
		internal System.Windows.Forms.TrackBar tbarFocalY {
			get { return withEventsField_tbarFocalY; }
			set {
				if (withEventsField_tbarFocalY != null) {
					withEventsField_tbarFocalY.MouseDown -= tbarFocalX_MouseDown;
					withEventsField_tbarFocalY.Scroll -= tbarFocalY_Scroll;
				}
				withEventsField_tbarFocalY = value;
				if (withEventsField_tbarFocalY != null) {
					withEventsField_tbarFocalY.MouseDown += tbarFocalX_MouseDown;
					withEventsField_tbarFocalY.Scroll += tbarFocalY_Scroll;
				}
			}
		}
		internal System.Windows.Forms.Panel panShapeHolder;
		internal System.Windows.Forms.Label lblFy;
		internal System.Windows.Forms.Label lblFx;
		internal System.Windows.Forms.Label lblCx;
		internal System.Windows.Forms.Label lblCy;
		private System.Windows.Forms.Button withEventsField_butCancel;
		internal System.Windows.Forms.Button butCancel {
			get { return withEventsField_butCancel; }
			set {
				if (withEventsField_butCancel != null) {
					withEventsField_butCancel.Click -= butCancel_Click;
				}
				withEventsField_butCancel = value;
				if (withEventsField_butCancel != null) {
					withEventsField_butCancel.Click += butCancel_Click;
				}
			}
		}
		private ProgressBar withEventsField_TheShape;
		internal ProgressBar TheShape {
			get { return withEventsField_TheShape; }
			set {
				if (withEventsField_TheShape != null) {
					withEventsField_TheShape.MouseDown -= TheShape_MouseDown;
					withEventsField_TheShape.MouseMove -= TheShape_MouseDown;
				}
				withEventsField_TheShape = value;
				if (withEventsField_TheShape != null) {
					withEventsField_TheShape.MouseDown += TheShape_MouseDown;
					withEventsField_TheShape.MouseMove += TheShape_MouseDown;
				}
			}

		}
	}
}



