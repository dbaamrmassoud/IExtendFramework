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
	partial class DropdownColorBlender : System.Windows.Forms.UserControl
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
			this.panProps = new System.Windows.Forms.Panel();
			this.lblPos = new System.Windows.Forms.Label();
			this.TabControl1 = new System.Windows.Forms.TabControl();
			this.TabPage1 = new System.Windows.Forms.TabPage();
			this.Panel20 = new System.Windows.Forms.Panel();
			this.Panel7 = new System.Windows.Forms.Panel();
			this.Panel22 = new System.Windows.Forms.Panel();
			this.Panel6 = new System.Windows.Forms.Panel();
			this.Panel29 = new System.Windows.Forms.Panel();
			this.Panel23 = new System.Windows.Forms.Panel();
			this.Panel8 = new System.Windows.Forms.Panel();
			this.Panel24 = new System.Windows.Forms.Panel();
			this.Panel9 = new System.Windows.Forms.Panel();
			this.Panel25 = new System.Windows.Forms.Panel();
			this.Panel10 = new System.Windows.Forms.Panel();
			this.Panel26 = new System.Windows.Forms.Panel();
			this.Panel11 = new System.Windows.Forms.Panel();
			this.Panel27 = new System.Windows.Forms.Panel();
			this.Panel12 = new System.Windows.Forms.Panel();
			this.Panel28 = new System.Windows.Forms.Panel();
			this.Panel13 = new System.Windows.Forms.Panel();
			this.Panel18 = new System.Windows.Forms.Panel();
			this.Panel19 = new System.Windows.Forms.Panel();
			this.Panel21 = new System.Windows.Forms.Panel();
			this.Panel14 = new System.Windows.Forms.Panel();
			this.Panel17 = new System.Windows.Forms.Panel();
			this.Panel15 = new System.Windows.Forms.Panel();
			this.Panel16 = new System.Windows.Forms.Panel();
			this.TabPage3 = new System.Windows.Forms.TabPage();
			this.nudRed = new System.Windows.Forms.NumericUpDown();
			this.nudGreen = new System.Windows.Forms.NumericUpDown();
			this.nudBlue = new System.Windows.Forms.NumericUpDown();
			this.ColorBox = new System.Windows.Forms.ComboBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.TabPage2 = new System.Windows.Forms.TabPage();
			this.txbAlpha = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.tbarAlpha = new System.Windows.Forms.TrackBar();
			this.txbCurrColor = new System.Windows.Forms.TextBox();
			this.panCurrColor = new System.Windows.Forms.Panel();
			this.butClear = new System.Windows.Forms.Button();
			this.butApply = new System.Windows.Forms.Button();
			this.panProps.SuspendLayout();
			this.TabControl1.SuspendLayout();
			this.TabPage1.SuspendLayout();
			this.TabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.nudRed).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.nudGreen).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.nudBlue).BeginInit();
			this.TabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.tbarAlpha).BeginInit();
			this.SuspendLayout();
			//
			//panProps
			//
			this.panProps.Controls.Add(this.lblPos);
			this.panProps.Controls.Add(this.TabControl1);
			this.panProps.Controls.Add(this.txbCurrColor);
			this.panProps.Controls.Add(this.panCurrColor);
			this.panProps.Location = new System.Drawing.Point(3, 43);
			this.panProps.Name = "panProps";
			this.panProps.Size = new System.Drawing.Size(186, 115);
			this.panProps.TabIndex = 6;
			//
			//lblPos
			//
			this.lblPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblPos.Location = new System.Drawing.Point(151, 5);
			this.lblPos.Name = "lblPos";
			this.lblPos.Size = new System.Drawing.Size(34, 16);
			this.lblPos.TabIndex = 12;
			this.lblPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//TabControl1
			//
			this.TabControl1.Controls.Add(this.TabPage1);
			this.TabControl1.Controls.Add(this.TabPage3);
			this.TabControl1.Controls.Add(this.TabPage2);
			this.TabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.TabControl1.ItemSize = new System.Drawing.Size(59, 18);
			this.TabControl1.Location = new System.Drawing.Point(4, 24);
			this.TabControl1.Name = "TabControl1";
			this.TabControl1.SelectedIndex = 0;
			this.TabControl1.Size = new System.Drawing.Size(182, 90);
			this.TabControl1.TabIndex = 11;
			//
			//TabPage1
			//
			this.TabPage1.Controls.Add(this.Panel20);
			this.TabPage1.Controls.Add(this.Panel7);
			this.TabPage1.Controls.Add(this.Panel22);
			this.TabPage1.Controls.Add(this.Panel6);
			this.TabPage1.Controls.Add(this.Panel29);
			this.TabPage1.Controls.Add(this.Panel23);
			this.TabPage1.Controls.Add(this.Panel8);
			this.TabPage1.Controls.Add(this.Panel24);
			this.TabPage1.Controls.Add(this.Panel9);
			this.TabPage1.Controls.Add(this.Panel25);
			this.TabPage1.Controls.Add(this.Panel10);
			this.TabPage1.Controls.Add(this.Panel26);
			this.TabPage1.Controls.Add(this.Panel11);
			this.TabPage1.Controls.Add(this.Panel27);
			this.TabPage1.Controls.Add(this.Panel12);
			this.TabPage1.Controls.Add(this.Panel28);
			this.TabPage1.Controls.Add(this.Panel13);
			this.TabPage1.Controls.Add(this.Panel18);
			this.TabPage1.Controls.Add(this.Panel19);
			this.TabPage1.Controls.Add(this.Panel21);
			this.TabPage1.Controls.Add(this.Panel14);
			this.TabPage1.Controls.Add(this.Panel17);
			this.TabPage1.Controls.Add(this.Panel15);
			this.TabPage1.Controls.Add(this.Panel16);
			this.TabPage1.Location = new System.Drawing.Point(4, 22);
			this.TabPage1.Name = "TabPage1";
			this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.TabPage1.Size = new System.Drawing.Size(174, 64);
			this.TabPage1.TabIndex = 0;
			this.TabPage1.Text = "Swatches";
			this.TabPage1.UseVisualStyleBackColor = true;
			//
			//Panel20
			//
			this.Panel20.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(128)), Convert.ToInt32(Convert.ToByte(0)));
			this.Panel20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel20.Location = new System.Drawing.Point(48, 6);
			this.Panel20.Name = "Panel20";
			this.Panel20.Size = new System.Drawing.Size(17, 17);
			this.Panel20.TabIndex = 35;
			//
			//Panel7
			//
			this.Panel7.BackColor = System.Drawing.Color.White;
			this.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel7.Location = new System.Drawing.Point(6, 6);
			this.Panel7.Name = "Panel7";
			this.Panel7.Size = new System.Drawing.Size(17, 17);
			this.Panel7.TabIndex = 29;
			//
			//Panel22
			//
			this.Panel22.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(255)));
			this.Panel22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel22.Location = new System.Drawing.Point(153, 44);
			this.Panel22.Name = "Panel22";
			this.Panel22.Size = new System.Drawing.Size(17, 17);
			this.Panel22.TabIndex = 43;
			//
			//Panel6
			//
			this.Panel6.BackColor = System.Drawing.Color.Black;
			this.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel6.Location = new System.Drawing.Point(6, 44);
			this.Panel6.Name = "Panel6";
			this.Panel6.Size = new System.Drawing.Size(17, 17);
			this.Panel6.TabIndex = 30;
			//
			//Panel29
			//
			this.Panel29.BackColor = System.Drawing.Color.Silver;
			this.Panel29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel29.Location = new System.Drawing.Point(6, 25);
			this.Panel29.Name = "Panel29";
			this.Panel29.Size = new System.Drawing.Size(17, 17);
			this.Panel29.TabIndex = 28;
			//
			//Panel23
			//
			this.Panel23.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(255)));
			this.Panel23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel23.Location = new System.Drawing.Point(111, 44);
			this.Panel23.Name = "Panel23";
			this.Panel23.Size = new System.Drawing.Size(17, 17);
			this.Panel23.TabIndex = 44;
			//
			//Panel8
			//
			this.Panel8.BackColor = System.Drawing.Color.Red;
			this.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel8.Location = new System.Drawing.Point(27, 6);
			this.Panel8.Name = "Panel8";
			this.Panel8.Size = new System.Drawing.Size(17, 17);
			this.Panel8.TabIndex = 33;
			//
			//Panel24
			//
			this.Panel24.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(255)));
			this.Panel24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel24.Location = new System.Drawing.Point(132, 44);
			this.Panel24.Name = "Panel24";
			this.Panel24.Size = new System.Drawing.Size(17, 17);
			this.Panel24.TabIndex = 45;
			//
			//Panel9
			//
			this.Panel9.BackColor = System.Drawing.Color.Lime;
			this.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel9.Location = new System.Drawing.Point(90, 6);
			this.Panel9.Name = "Panel9";
			this.Panel9.Size = new System.Drawing.Size(17, 17);
			this.Panel9.TabIndex = 32;
			//
			//Panel25
			//
			this.Panel25.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(192)));
			this.Panel25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel25.Location = new System.Drawing.Point(90, 44);
			this.Panel25.Name = "Panel25";
			this.Panel25.Size = new System.Drawing.Size(17, 17);
			this.Panel25.TabIndex = 42;
			//
			//Panel10
			//
			this.Panel10.BackColor = System.Drawing.Color.Blue;
			this.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel10.Location = new System.Drawing.Point(132, 6);
			this.Panel10.Name = "Panel10";
			this.Panel10.Size = new System.Drawing.Size(17, 17);
			this.Panel10.TabIndex = 31;
			//
			//Panel26
			//
			this.Panel26.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(192)));
			this.Panel26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel26.Location = new System.Drawing.Point(69, 44);
			this.Panel26.Name = "Panel26";
			this.Panel26.Size = new System.Drawing.Size(17, 17);
			this.Panel26.TabIndex = 39;
			//
			//Panel11
			//
			this.Panel11.BackColor = System.Drawing.Color.Cyan;
			this.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel11.Location = new System.Drawing.Point(111, 6);
			this.Panel11.Name = "Panel11";
			this.Panel11.Size = new System.Drawing.Size(17, 17);
			this.Panel11.TabIndex = 24;
			//
			//Panel27
			//
			this.Panel27.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(224)), Convert.ToInt32(Convert.ToByte(192)));
			this.Panel27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel27.Location = new System.Drawing.Point(48, 44);
			this.Panel27.Name = "Panel27";
			this.Panel27.Size = new System.Drawing.Size(17, 17);
			this.Panel27.TabIndex = 40;
			//
			//Panel12
			//
			this.Panel12.BackColor = System.Drawing.Color.Fuchsia;
			this.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel12.Location = new System.Drawing.Point(153, 6);
			this.Panel12.Name = "Panel12";
			this.Panel12.Size = new System.Drawing.Size(17, 17);
			this.Panel12.TabIndex = 23;
			//
			//Panel28
			//
			this.Panel28.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(192)));
			this.Panel28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel28.Location = new System.Drawing.Point(27, 44);
			this.Panel28.Name = "Panel28";
			this.Panel28.Size = new System.Drawing.Size(17, 17);
			this.Panel28.TabIndex = 41;
			//
			//Panel13
			//
			this.Panel13.BackColor = System.Drawing.Color.Maroon;
			this.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel13.Location = new System.Drawing.Point(27, 25);
			this.Panel13.Name = "Panel13";
			this.Panel13.Size = new System.Drawing.Size(17, 17);
			this.Panel13.TabIndex = 22;
			//
			//Panel18
			//
			this.Panel18.BackColor = System.Drawing.Color.Purple;
			this.Panel18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel18.Location = new System.Drawing.Point(153, 25);
			this.Panel18.Name = "Panel18";
			this.Panel18.Size = new System.Drawing.Size(17, 17);
			this.Panel18.TabIndex = 34;
			//
			//Panel19
			//
			this.Panel19.BackColor = System.Drawing.Color.Yellow;
			this.Panel19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel19.Location = new System.Drawing.Point(69, 6);
			this.Panel19.Name = "Panel19";
			this.Panel19.Size = new System.Drawing.Size(17, 17);
			this.Panel19.TabIndex = 27;
			//
			//Panel21
			//
			this.Panel21.BackColor = System.Drawing.Color.Teal;
			this.Panel21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel21.Location = new System.Drawing.Point(111, 25);
			this.Panel21.Name = "Panel21";
			this.Panel21.Size = new System.Drawing.Size(17, 17);
			this.Panel21.TabIndex = 36;
			//
			//Panel14
			//
			this.Panel14.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(128)), Convert.ToInt32(Convert.ToByte(64)), Convert.ToInt32(Convert.ToByte(0)));
			this.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel14.Location = new System.Drawing.Point(48, 25);
			this.Panel14.Name = "Panel14";
			this.Panel14.Size = new System.Drawing.Size(17, 17);
			this.Panel14.TabIndex = 26;
			//
			//Panel17
			//
			this.Panel17.BackColor = System.Drawing.Color.Navy;
			this.Panel17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel17.Location = new System.Drawing.Point(132, 25);
			this.Panel17.Name = "Panel17";
			this.Panel17.Size = new System.Drawing.Size(17, 17);
			this.Panel17.TabIndex = 38;
			//
			//Panel15
			//
			this.Panel15.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(0)));
			this.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel15.Location = new System.Drawing.Point(69, 25);
			this.Panel15.Name = "Panel15";
			this.Panel15.Size = new System.Drawing.Size(17, 17);
			this.Panel15.TabIndex = 25;
			//
			//Panel16
			//
			this.Panel16.BackColor = System.Drawing.Color.Green;
			this.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel16.Location = new System.Drawing.Point(90, 25);
			this.Panel16.Name = "Panel16";
			this.Panel16.Size = new System.Drawing.Size(17, 17);
			this.Panel16.TabIndex = 37;
			//
			//TabPage3
			//
			this.TabPage3.Controls.Add(this.nudRed);
			this.TabPage3.Controls.Add(this.nudGreen);
			this.TabPage3.Controls.Add(this.nudBlue);
			this.TabPage3.Controls.Add(this.ColorBox);
			this.TabPage3.Controls.Add(this.Label2);
			this.TabPage3.Controls.Add(this.Label4);
			this.TabPage3.Controls.Add(this.Label3);
			this.TabPage3.Location = new System.Drawing.Point(4, 22);
			this.TabPage3.Name = "TabPage3";
			this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.TabPage3.Size = new System.Drawing.Size(174, 64);
			this.TabPage3.TabIndex = 2;
			this.TabPage3.Text = "Color";
			this.TabPage3.UseVisualStyleBackColor = true;
			//
			//nudRed
			//
			this.nudRed.Location = new System.Drawing.Point(21, 41);
			this.nudRed.Maximum = new decimal(new int[] {
				255,
				0,
				0,
				0
			});
			this.nudRed.Name = "nudRed";
			this.nudRed.Size = new System.Drawing.Size(40, 20);
			this.nudRed.TabIndex = 8;
			this.nudRed.Value = new decimal(new int[] {
				255,
				0,
				0,
				0
			});
			//
			//nudGreen
			//
			this.nudGreen.Location = new System.Drawing.Point(67, 41);
			this.nudGreen.Maximum = new decimal(new int[] {
				255,
				0,
				0,
				0
			});
			this.nudGreen.Name = "nudGreen";
			this.nudGreen.Size = new System.Drawing.Size(40, 20);
			this.nudGreen.TabIndex = 8;
			this.nudGreen.Value = new decimal(new int[] {
				255,
				0,
				0,
				0
			});
			//
			//nudBlue
			//
			this.nudBlue.Location = new System.Drawing.Point(113, 41);
			this.nudBlue.Maximum = new decimal(new int[] {
				255,
				0,
				0,
				0
			});
			this.nudBlue.Name = "nudBlue";
			this.nudBlue.Size = new System.Drawing.Size(40, 20);
			this.nudBlue.TabIndex = 8;
			this.nudBlue.Value = new decimal(new int[] {
				255,
				0,
				0,
				0
			});
			//
			//ColorBox
			//
			this.ColorBox.Font = new System.Drawing.Font("Tahoma", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.ColorBox.FormattingEnabled = true;
			this.ColorBox.Location = new System.Drawing.Point(5, 3);
			this.ColorBox.Name = "ColorBox";
			this.ColorBox.Size = new System.Drawing.Size(164, 24);
			this.ColorBox.TabIndex = 4;
			//
			//Label2
			//
			this.Label2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label2.ForeColor = System.Drawing.Color.Red;
			this.Label2.Location = new System.Drawing.Point(18, 28);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(42, 14);
			this.Label2.TabIndex = 10;
			this.Label2.Text = "Red";
			this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//Label4
			//
			this.Label4.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label4.ForeColor = System.Drawing.Color.Blue;
			this.Label4.Location = new System.Drawing.Point(110, 28);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(42, 14);
			this.Label4.TabIndex = 10;
			this.Label4.Text = "Blue";
			this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//Label3
			//
			this.Label3.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label3.ForeColor = System.Drawing.Color.Green;
			this.Label3.Location = new System.Drawing.Point(64, 28);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(42, 14);
			this.Label3.TabIndex = 10;
			this.Label3.Text = "Green";
			this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//TabPage2
			//
			this.TabPage2.BackColor = System.Drawing.Color.White;
			this.TabPage2.Controls.Add(this.txbAlpha);
			this.TabPage2.Controls.Add(this.Label1);
			this.TabPage2.Controls.Add(this.tbarAlpha);
			this.TabPage2.Location = new System.Drawing.Point(4, 22);
			this.TabPage2.Name = "TabPage2";
			this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.TabPage2.Size = new System.Drawing.Size(174, 64);
			this.TabPage2.TabIndex = 3;
			this.TabPage2.Text = "Transparency";
			//
			//txbAlpha
			//
			this.txbAlpha.Location = new System.Drawing.Point(115, 12);
			this.txbAlpha.Name = "txbAlpha";
			this.txbAlpha.Size = new System.Drawing.Size(27, 20);
			this.txbAlpha.TabIndex = 13;
			this.txbAlpha.Text = "255";
			//
			//Label1
			//
			this.Label1.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.Location = new System.Drawing.Point(29, 14);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(81, 17);
			this.Label1.TabIndex = 9;
			this.Label1.Text = "Alpha Value";
			this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//tbarAlpha
			//
			this.tbarAlpha.AutoSize = false;
			this.tbarAlpha.BackColor = System.Drawing.Color.White;
			this.tbarAlpha.Location = new System.Drawing.Point(6, 35);
			this.tbarAlpha.Maximum = 255;
			this.tbarAlpha.Name = "tbarAlpha";
			this.tbarAlpha.Size = new System.Drawing.Size(164, 21);
			this.tbarAlpha.TabIndex = 12;
			this.tbarAlpha.TickStyle = System.Windows.Forms.TickStyle.None;
			this.tbarAlpha.Value = 255;
			//
			//txbCurrColor
			//
			this.txbCurrColor.Location = new System.Drawing.Point(26, 2);
			this.txbCurrColor.Name = "txbCurrColor";
			this.txbCurrColor.Size = new System.Drawing.Size(124, 20);
			this.txbCurrColor.TabIndex = 7;
			//
			//panCurrColor
			//
			this.panCurrColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panCurrColor.Location = new System.Drawing.Point(4, 2);
			this.panCurrColor.Name = "panCurrColor";
			this.panCurrColor.Size = new System.Drawing.Size(21, 21);
			this.panCurrColor.TabIndex = 7;
			//
			//butClear
			//
			this.butClear.Location = new System.Drawing.Point(195, 129);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(42, 23);
			this.butClear.TabIndex = 8;
			this.butClear.Text = "Clear";
			this.butClear.UseVisualStyleBackColor = true;
			//
			//butApply
			//
			this.butApply.Location = new System.Drawing.Point(243, 129);
			this.butApply.Name = "butApply";
			this.butApply.Size = new System.Drawing.Size(42, 23);
			this.butApply.TabIndex = 8;
			this.butApply.Text = "Apply";
			this.butApply.UseVisualStyleBackColor = true;
			//
			//DropdownColorBlender
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.butApply);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.panProps);
			this.Name = "DropdownColorBlender";
			this.Size = new System.Drawing.Size(293, 161);
			this.panProps.ResumeLayout(false);
			this.panProps.PerformLayout();
			this.TabControl1.ResumeLayout(false);
			this.TabPage1.ResumeLayout(false);
			this.TabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.nudRed).EndInit();
			((System.ComponentModel.ISupportInitialize)this.nudGreen).EndInit();
			((System.ComponentModel.ISupportInitialize)this.nudBlue).EndInit();
			this.TabPage2.ResumeLayout(false);
			this.TabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.tbarAlpha).EndInit();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Panel panProps;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		private System.Windows.Forms.NumericUpDown withEventsField_nudBlue;
		internal System.Windows.Forms.NumericUpDown nudBlue {
			get { return withEventsField_nudBlue; }
			set {
				if (withEventsField_nudBlue != null) {
					withEventsField_nudBlue.ValueChanged -= nud_ValueChanged;
				}
				withEventsField_nudBlue = value;
				if (withEventsField_nudBlue != null) {
					withEventsField_nudBlue.ValueChanged += nud_ValueChanged;
				}
			}
		}
		internal System.Windows.Forms.TextBox txbCurrColor;
		internal System.Windows.Forms.Panel panCurrColor;
		private System.Windows.Forms.NumericUpDown withEventsField_nudGreen;
		internal System.Windows.Forms.NumericUpDown nudGreen {
			get { return withEventsField_nudGreen; }
			set {
				if (withEventsField_nudGreen != null) {
					withEventsField_nudGreen.ValueChanged -= nud_ValueChanged;
				}
				withEventsField_nudGreen = value;
				if (withEventsField_nudGreen != null) {
					withEventsField_nudGreen.ValueChanged += nud_ValueChanged;
				}
			}
		}
		private System.Windows.Forms.NumericUpDown withEventsField_nudRed;
		internal System.Windows.Forms.NumericUpDown nudRed {
			get { return withEventsField_nudRed; }
			set {
				if (withEventsField_nudRed != null) {
					withEventsField_nudRed.ValueChanged -= nud_ValueChanged;
				}
				withEventsField_nudRed = value;
				if (withEventsField_nudRed != null) {
					withEventsField_nudRed.ValueChanged += nud_ValueChanged;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel20;
		internal System.Windows.Forms.Panel Panel20 {
			get { return withEventsField_Panel20; }
			set {
				if (withEventsField_Panel20 != null) {
					withEventsField_Panel20.Click -= Panel7_Click;
					withEventsField_Panel20.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel20 = value;
				if (withEventsField_Panel20 != null) {
					withEventsField_Panel20.Click += Panel7_Click;
					withEventsField_Panel20.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel22;
		internal System.Windows.Forms.Panel Panel22 {
			get { return withEventsField_Panel22; }
			set {
				if (withEventsField_Panel22 != null) {
					withEventsField_Panel22.Click -= Panel7_Click;
					withEventsField_Panel22.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel22 = value;
				if (withEventsField_Panel22 != null) {
					withEventsField_Panel22.Click += Panel7_Click;
					withEventsField_Panel22.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.ComboBox withEventsField_ColorBox;
		internal System.Windows.Forms.ComboBox ColorBox {
			get { return withEventsField_ColorBox; }
			set {
				if (withEventsField_ColorBox != null) {
					withEventsField_ColorBox.SelectedIndexChanged -= ColorBox_SelectedIndexChanged;
				}
				withEventsField_ColorBox = value;
				if (withEventsField_ColorBox != null) {
					withEventsField_ColorBox.SelectedIndexChanged += ColorBox_SelectedIndexChanged;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel23;
		internal System.Windows.Forms.Panel Panel23 {
			get { return withEventsField_Panel23; }
			set {
				if (withEventsField_Panel23 != null) {
					withEventsField_Panel23.Click -= Panel7_Click;
					withEventsField_Panel23.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel23 = value;
				if (withEventsField_Panel23 != null) {
					withEventsField_Panel23.Click += Panel7_Click;
					withEventsField_Panel23.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel24;
		internal System.Windows.Forms.Panel Panel24 {
			get { return withEventsField_Panel24; }
			set {
				if (withEventsField_Panel24 != null) {
					withEventsField_Panel24.Click -= Panel7_Click;
					withEventsField_Panel24.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel24 = value;
				if (withEventsField_Panel24 != null) {
					withEventsField_Panel24.Click += Panel7_Click;
					withEventsField_Panel24.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel25;
		internal System.Windows.Forms.Panel Panel25 {
			get { return withEventsField_Panel25; }
			set {
				if (withEventsField_Panel25 != null) {
					withEventsField_Panel25.Click -= Panel7_Click;
					withEventsField_Panel25.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel25 = value;
				if (withEventsField_Panel25 != null) {
					withEventsField_Panel25.Click += Panel7_Click;
					withEventsField_Panel25.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel26;
		internal System.Windows.Forms.Panel Panel26 {
			get { return withEventsField_Panel26; }
			set {
				if (withEventsField_Panel26 != null) {
					withEventsField_Panel26.Click -= Panel7_Click;
					withEventsField_Panel26.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel26 = value;
				if (withEventsField_Panel26 != null) {
					withEventsField_Panel26.Click += Panel7_Click;
					withEventsField_Panel26.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel27;
		internal System.Windows.Forms.Panel Panel27 {
			get { return withEventsField_Panel27; }
			set {
				if (withEventsField_Panel27 != null) {
					withEventsField_Panel27.Click -= Panel7_Click;
					withEventsField_Panel27.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel27 = value;
				if (withEventsField_Panel27 != null) {
					withEventsField_Panel27.Click += Panel7_Click;
					withEventsField_Panel27.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel28;
		internal System.Windows.Forms.Panel Panel28 {
			get { return withEventsField_Panel28; }
			set {
				if (withEventsField_Panel28 != null) {
					withEventsField_Panel28.Click -= Panel7_Click;
					withEventsField_Panel28.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel28 = value;
				if (withEventsField_Panel28 != null) {
					withEventsField_Panel28.Click += Panel7_Click;
					withEventsField_Panel28.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel18;
		internal System.Windows.Forms.Panel Panel18 {
			get { return withEventsField_Panel18; }
			set {
				if (withEventsField_Panel18 != null) {
					withEventsField_Panel18.Click -= Panel7_Click;
					withEventsField_Panel18.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel18 = value;
				if (withEventsField_Panel18 != null) {
					withEventsField_Panel18.Click += Panel7_Click;
					withEventsField_Panel18.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel21;
		internal System.Windows.Forms.Panel Panel21 {
			get { return withEventsField_Panel21; }
			set {
				if (withEventsField_Panel21 != null) {
					withEventsField_Panel21.Click -= Panel7_Click;
					withEventsField_Panel21.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel21 = value;
				if (withEventsField_Panel21 != null) {
					withEventsField_Panel21.Click += Panel7_Click;
					withEventsField_Panel21.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel17;
		internal System.Windows.Forms.Panel Panel17 {
			get { return withEventsField_Panel17; }
			set {
				if (withEventsField_Panel17 != null) {
					withEventsField_Panel17.Click -= Panel7_Click;
					withEventsField_Panel17.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel17 = value;
				if (withEventsField_Panel17 != null) {
					withEventsField_Panel17.Click += Panel7_Click;
					withEventsField_Panel17.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel16;
		internal System.Windows.Forms.Panel Panel16 {
			get { return withEventsField_Panel16; }
			set {
				if (withEventsField_Panel16 != null) {
					withEventsField_Panel16.Click -= Panel7_Click;
					withEventsField_Panel16.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel16 = value;
				if (withEventsField_Panel16 != null) {
					withEventsField_Panel16.Click += Panel7_Click;
					withEventsField_Panel16.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel15;
		internal System.Windows.Forms.Panel Panel15 {
			get { return withEventsField_Panel15; }
			set {
				if (withEventsField_Panel15 != null) {
					withEventsField_Panel15.Click -= Panel7_Click;
					withEventsField_Panel15.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel15 = value;
				if (withEventsField_Panel15 != null) {
					withEventsField_Panel15.Click += Panel7_Click;
					withEventsField_Panel15.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel14;
		internal System.Windows.Forms.Panel Panel14 {
			get { return withEventsField_Panel14; }
			set {
				if (withEventsField_Panel14 != null) {
					withEventsField_Panel14.Click -= Panel7_Click;
					withEventsField_Panel14.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel14 = value;
				if (withEventsField_Panel14 != null) {
					withEventsField_Panel14.Click += Panel7_Click;
					withEventsField_Panel14.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel19;
		internal System.Windows.Forms.Panel Panel19 {
			get { return withEventsField_Panel19; }
			set {
				if (withEventsField_Panel19 != null) {
					withEventsField_Panel19.Click -= Panel7_Click;
					withEventsField_Panel19.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel19 = value;
				if (withEventsField_Panel19 != null) {
					withEventsField_Panel19.Click += Panel7_Click;
					withEventsField_Panel19.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel13;
		internal System.Windows.Forms.Panel Panel13 {
			get { return withEventsField_Panel13; }
			set {
				if (withEventsField_Panel13 != null) {
					withEventsField_Panel13.Click -= Panel7_Click;
					withEventsField_Panel13.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel13 = value;
				if (withEventsField_Panel13 != null) {
					withEventsField_Panel13.Click += Panel7_Click;
					withEventsField_Panel13.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel12;
		internal System.Windows.Forms.Panel Panel12 {
			get { return withEventsField_Panel12; }
			set {
				if (withEventsField_Panel12 != null) {
					withEventsField_Panel12.Click -= Panel7_Click;
					withEventsField_Panel12.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel12 = value;
				if (withEventsField_Panel12 != null) {
					withEventsField_Panel12.Click += Panel7_Click;
					withEventsField_Panel12.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel11;
		internal System.Windows.Forms.Panel Panel11 {
			get { return withEventsField_Panel11; }
			set {
				if (withEventsField_Panel11 != null) {
					withEventsField_Panel11.Click -= Panel7_Click;
					withEventsField_Panel11.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel11 = value;
				if (withEventsField_Panel11 != null) {
					withEventsField_Panel11.Click += Panel7_Click;
					withEventsField_Panel11.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel10;
		internal System.Windows.Forms.Panel Panel10 {
			get { return withEventsField_Panel10; }
			set {
				if (withEventsField_Panel10 != null) {
					withEventsField_Panel10.Click -= Panel7_Click;
					withEventsField_Panel10.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel10 = value;
				if (withEventsField_Panel10 != null) {
					withEventsField_Panel10.Click += Panel7_Click;
					withEventsField_Panel10.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel9;
		internal System.Windows.Forms.Panel Panel9 {
			get { return withEventsField_Panel9; }
			set {
				if (withEventsField_Panel9 != null) {
					withEventsField_Panel9.Click -= Panel7_Click;
					withEventsField_Panel9.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel9 = value;
				if (withEventsField_Panel9 != null) {
					withEventsField_Panel9.Click += Panel7_Click;
					withEventsField_Panel9.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel8;
		internal System.Windows.Forms.Panel Panel8 {
			get { return withEventsField_Panel8; }
			set {
				if (withEventsField_Panel8 != null) {
					withEventsField_Panel8.Click -= Panel7_Click;
					withEventsField_Panel8.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel8 = value;
				if (withEventsField_Panel8 != null) {
					withEventsField_Panel8.Click += Panel7_Click;
					withEventsField_Panel8.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel29;
		internal System.Windows.Forms.Panel Panel29 {
			get { return withEventsField_Panel29; }
			set {
				if (withEventsField_Panel29 != null) {
					withEventsField_Panel29.Click -= Panel7_Click;
					withEventsField_Panel29.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel29 = value;
				if (withEventsField_Panel29 != null) {
					withEventsField_Panel29.Click += Panel7_Click;
					withEventsField_Panel29.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel7;
		internal System.Windows.Forms.Panel Panel7 {
			get { return withEventsField_Panel7; }
			set {
				if (withEventsField_Panel7 != null) {
					withEventsField_Panel7.Click -= Panel7_Click;
					withEventsField_Panel7.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel7 = value;
				if (withEventsField_Panel7 != null) {
					withEventsField_Panel7.Click += Panel7_Click;
					withEventsField_Panel7.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.Panel withEventsField_Panel6;
		internal System.Windows.Forms.Panel Panel6 {
			get { return withEventsField_Panel6; }
			set {
				if (withEventsField_Panel6 != null) {
					withEventsField_Panel6.Click -= Panel7_Click;
					withEventsField_Panel6.MouseEnter -= Panel10_MouseEnter;
				}
				withEventsField_Panel6 = value;
				if (withEventsField_Panel6 != null) {
					withEventsField_Panel6.Click += Panel7_Click;
					withEventsField_Panel6.MouseEnter += Panel10_MouseEnter;
				}
			}
		}
		private System.Windows.Forms.TabControl withEventsField_TabControl1;
		internal System.Windows.Forms.TabControl TabControl1 {
			get { return withEventsField_TabControl1; }
			set {
				if (withEventsField_TabControl1 != null) {
					withEventsField_TabControl1.MouseLeave -= TabControl1_MouseLeave;
				}
				withEventsField_TabControl1 = value;
				if (withEventsField_TabControl1 != null) {
					withEventsField_TabControl1.MouseLeave += TabControl1_MouseLeave;
				}
			}
		}
		internal System.Windows.Forms.TabPage TabPage1;
		internal System.Windows.Forms.TabPage TabPage3;
		private System.Windows.Forms.TrackBar withEventsField_tbarAlpha;
		internal System.Windows.Forms.TrackBar tbarAlpha {
			get { return withEventsField_tbarAlpha; }
			set {
				if (withEventsField_tbarAlpha != null) {
					withEventsField_tbarAlpha.ValueChanged -= nud_ValueChanged;
				}
				withEventsField_tbarAlpha = value;
				if (withEventsField_tbarAlpha != null) {
					withEventsField_tbarAlpha.ValueChanged += nud_ValueChanged;
				}
			}
		}
		private System.Windows.Forms.TextBox withEventsField_txbAlpha;
		internal System.Windows.Forms.TextBox txbAlpha {
			get { return withEventsField_txbAlpha; }
			set {
				if (withEventsField_txbAlpha != null) {
					withEventsField_txbAlpha.TextChanged -= txbAlpha_TextChanged;
				}
				withEventsField_txbAlpha = value;
				if (withEventsField_txbAlpha != null) {
					withEventsField_txbAlpha.TextChanged += txbAlpha_TextChanged;
				}
			}
		}
		internal System.Windows.Forms.TabPage TabPage2;
		private System.Windows.Forms.Button withEventsField_butClear;
		internal System.Windows.Forms.Button butClear {
			get { return withEventsField_butClear; }
			set {
				if (withEventsField_butClear != null) {
					withEventsField_butClear.Click -= butClear_Click;
				}
				withEventsField_butClear = value;
				if (withEventsField_butClear != null) {
					withEventsField_butClear.Click += butClear_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_butApply;
		internal System.Windows.Forms.Button butApply {
			get { return withEventsField_butApply; }
			set {
				if (withEventsField_butApply != null) {
					withEventsField_butApply.Click -= butApply_Click;
				}
				withEventsField_butApply = value;
				if (withEventsField_butApply != null) {
					withEventsField_butApply.Click += butApply_Click;
				}
			}
		}

		internal System.Windows.Forms.Label lblPos;
	}



}
