using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms.Design;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace IExtendFramework.Controls
{
	/// <summary>
	/// A DropDown ColorBlender From ProgBarPlus. (To only be used when ProgressBar calls it!)
	/// </summary>
	/// <remarks></remarks>
	[ToolboxItem(false), ToolboxItemFilter("Prevent", ToolboxItemFilterType.Prevent)]
	public partial class DropdownColorBlender : UserControl
	{

		private IWindowsFormsEditorService _editorService = null;
		cblPointer StartPointer = new cblPointer(0, Color.White, false);
		cblPointer EndPointer = new cblPointer(1, Color.White, false);
		Collection MiddlePointers = new Collection();
		bool MouseMoving = false;
		int CurrPointer;
		int TopMargin = 8;
		//List of Known Colors - Done this way because I haven't found a good
		//way to get the Known Colors in color shade order yet

		string[] Known_Color = Strings.Split("Transparent,Black,DimGray,Gray,DarkGray,Silver,LightGray,Gainsboro,WhiteSmoke,White,RosyBrown,IndianRed,Brown,Firebrick,LightCoral,Maroon,DarkRed,Red,Snow,MistyRose,Salmon,Tomato,DarkSalmon,Coral,OrangeRed,LightSalmon,Sienna,SeaShell,Chocalate,SaddleBrown,SandyBrown,PeachPuff,Peru,Linen,Bisque,DarkOrange,BurlyWood,Tan,AntiqueWhite,NavajoWhite,BlanchedAlmond,PapayaWhip,Mocassin,Orange,Wheat,OldLace,FloralWhite,DarkGoldenrod,Cornsilk,Gold,Khaki,LemonChiffon,PaleGoldenrod,DarkKhaki,Beige,LightGoldenrod,Olive,Yellow,LightYellow,Ivory,OliveDrab,YellowGreen,DarkOliveGreen,GreenYellow,Chartreuse,LawnGreen,DarkSeaGreen,ForestGreen,LimeGreen,PaleGreen,DarkGreen,Green,Lime,Honeydew,SeaGreen,MediumSeaGreen,SpringGreen,MintCream,MediumSpringGreen,MediumAquaMarine,YellowAquaMarine,Turquoise,LightSeaGreen,MediumTurquoise,DarkSlateGray,PaleTurquoise,Teal,DarkCyan,Aqua,Cyan,LightCyan,Azure,DarkTurquoise,CadetBlue,PowderBlue,LightBlue,DeepSkyBlue,SkyBlue,LightSkyBlue,SteelBlue,AliceBlue,DodgerBlue,SlateGray,LightSlateGray,LightSteelBlue,CornflowerBlue,RoyalBlue,MidnightBlue,Lavender,Navy,DarkBlue,MediumBlue,Blue,GhostWhite,SlateBlue,DarkSlateBlue,MediumSlateBlue,MediumPurple,BlueViolet,Indigo,DarkOrchid,DarkViolet,MediumOrchid,Thistle,Plum,Violet,Purple,DarkMagenta,Magenta,Fuchsia,Orchid,MediumVioletRed,DeepPink,HotPink,LavenderBlush,PaleVioletRed,Crimson,Pink,LightPink", ",");
		public DropdownColorBlender(IWindowsFormsEditorService editorService)
		{
			MouseUp += ColorBlender_MouseUp;
			MouseMove += ColorBlender_MouseMove;
			MouseDown += ColorBlender_MouseDown;
			Load += ColorBlender_Load;
			InitializeComponent();
			_editorService = editorService;
		}

		//Added for the random thing that opens when compiling
		public DropdownColorBlender()
		{
			MouseUp += ColorBlender_MouseUp;
			MouseMove += ColorBlender_MouseMove;
			MouseDown += ColorBlender_MouseDown;
			Load += ColorBlender_Load;
			// This call is required by the designer.
			InitializeComponent();
			// Add any initialization after the InitializeComponent() call.

		}

		private void ColorBlender_Load(object sender, System.EventArgs e)
		{
			ColorBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			ColorBox.DropDownStyle = ComboBoxStyle.DropDownList;
			ColorBox.DrawItem += this.ColorList_DrawItem;
			ColorBox.Items.AddRange(Known_Color);
			ColorBox.SelectedIndex = 1;
		}

		#region "Properties"

		private Color[] _BlendColors = new Color[] {
			Color.White,
			Color.Black
		};
		[Category("ColorBlender")]
		[Description("Array of Colors used in ColorBlend")]
		public Color[] BlendColors {
			get { return _BlendColors; }
				//Me.Invalidate()
			set { _BlendColors = value; }
		}

		private float[] _BlendPositions = new float[] {
			0,
			1
		};
		[Category("ColorBlender")]
		[Description("Array of Color Positions used in ColorBlend")]
		public float[] BlendPositions {
			get { return _BlendPositions; }
				//Me.Invalidate()
			set { _BlendPositions = value; }
		}


		private cFocalPoints _FocalPoints = new cFocalPoints(0.5F, 0.5F, 0, 0);
		[Description("The CenterPoint and FocusScales for the ColorBlend"), Category("Appearance ProgBar")]
		public cFocalPoints FocalPoints {
			get { return _FocalPoints; }
			set {
				_FocalPoints = value;
				this.Invalidate();
			}
		}

		private float _BarHeight = 20;
		[Category("ColorBlender")]
		[Description("Height of color blender bar")]
		public float BarHeight {
			get { return _BarHeight; }
			set {
				_BarHeight = value;
				panProps.Location = new Point(10, Convert.ToInt32(value + 20));
				this.Invalidate();
			}
		}

		public enum eBlendGradientType
		{
			Linear,
			Path
		}
		private eBlendGradientType _BlendGradientType = eBlendGradientType.Linear;
		[Category("ColorBlender")]
		[Description("Type of brush used to paint color blend")]
		public eBlendGradientType BlendGradientType {
			get { return _BlendGradientType; }
			set {
				_BlendGradientType = value;
				this.Invalidate();
			}
		}

		public enum eBlendPathShape
		{
			Rectangle,
			Ellipse,
			Triangle
		}
		private eBlendPathShape _BlendPathShape = eBlendPathShape.Rectangle;
		[Category("ColorBlender")]
		[Description("Shape of path for the color blend")]
		public eBlendPathShape BlendPathShape {
			get { return _BlendPathShape; }
			set {
				_BlendPathShape = value;
				this.Invalidate();
			}
		}

		private PointF _BlendPathCenterPoint = new PointF();
		[Category("ColorBlender")]
		[Description("Position of the center of the path ColorBlend")]
		public PointF BlendPathCenterPoint {
			get { return _BlendPathCenterPoint; }
			set {
				_BlendPathCenterPoint = value;
				this.Invalidate();
			}
		}

		private LinearGradientMode _BlendGradientMode = LinearGradientMode.Vertical;
		[Category("ColorBlender")]
		[Description("Type of linear gradient color blend")]
		public LinearGradientMode BlendGradientMode {
			get { return _BlendGradientMode; }
			set {
				_BlendGradientMode = value;
				this.Invalidate();
			}
		}

		#endregion

		#region "Mouse Events"


		private void ColorBlender_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Y > TopMargin + BarHeight - 10 & e.Y < TopMargin + BarHeight + 20 & e.X > 5 & e.X < this.Width - 5) {
				//Check if the cursor is over a MiddlePointer
				int mOver = IsMouseOverPointer(e.X, e.Y);
				if (mOver > -1) {
					if (!(CurrPointer == mOver)) {
						CurrPointer = mOver;
						ClearCurrPointer();
						((cblPointer)(cblPointer)MiddlePointers[CurrPointer]).pIsCurr = true;
						UpdateRGBnuds(((cblPointer)(cblPointer)MiddlePointers[CurrPointer]).pColor);
						lblPos.Text = ((cblPointer)MiddlePointers[CurrPointer]).PosToStrong;
					}

					if (e.Button == System.Windows.Forms.MouseButtons.Left) {
						MouseMoving = true;
					} else if (e.Button == System.Windows.Forms.MouseButtons.Right) {
						MiddlePointers.Remove(CurrPointer);
						lblPos.Text = "";
					}
				} else {
					//Check if the cursor is over a Start or End Pointer
					if (IsMouseOverStartPointer(e.X, e.Y)) {
						ClearCurrPointer();
						CurrPointer = -1;
						StartPointer.pIsCurr = true;
						UpdateRGBnuds(StartPointer.pColor);
						lblPos.Text = StartPointer.PosToStrong;
					} else if (IsMouseOverEndPointer(e.X, e.Y)) {
						ClearCurrPointer();
						CurrPointer = -1;
						EndPointer.pIsCurr = true;
						UpdateRGBnuds(EndPointer.pColor);
						lblPos.Text = EndPointer.PosToStrong;
					} else {
						//If the cursor is not over a cblPointer then Add One
						if (e.Button == System.Windows.Forms.MouseButtons.Left) {
							ClearCurrPointer();
							MiddlePointers.Add(new cblPointer(Convert.ToSingle(((e.X - 10) / (this.Width - 20))), Color.FromArgb(tbarAlpha.Value, Convert.ToInt32(nudRed.Value), Convert.ToInt32(nudGreen.Value), Convert.ToInt32(nudBlue.Value)), true));
							SortCollection(MiddlePointers, "pPos", true);
							CurrPointer = FindCurr();
							lblPos.Text = ((cblPointer)MiddlePointers[CurrPointer]).PosToStrong;
							this.Invalidate();
							MouseMoving = true;
						}
					}
				}
			}
		}

		private void ColorBlender_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				if (MouseMoving) {
					if (e.X >= 11 & e.X <= (this.Width - 11)) {
						((cblPointer)MiddlePointers[CurrPointer]).pPos = Convert.ToSingle(((e.X - 10) / (this.Width - 20)));
						SortCollection(MiddlePointers, "pPos", true);
						CurrPointer = FindCurr();
						lblPos.Text = ((cblPointer)MiddlePointers[CurrPointer]).PosToStrong;
						lblPos.Refresh();
						this.Invalidate();
					}
				}
			}
		}


		private void ColorBlender_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			MouseMoving = false;
			SortCollection(MiddlePointers, "pPos", true);
			CurrPointer = FindCurr();
			this.Invalidate();
		}

		private bool IsMouseOverStartPointer(int X, int Y)
		{
			//Convert to Region.
			using (Region PointerRegion = new Region(BuildPointer(GetpX(0)))) {
				//Is the point inside the region.
				return PointerRegion.IsVisible(X, Y);
			}
		}

		private bool IsMouseOverEndPointer(int X, int Y)
		{
			//Convert to Region.
			using (Region PointerRegion = new Region(BuildPointer(GetpX(1)))) {
				//Is the point inside the region.
				return PointerRegion.IsVisible(X, Y);
			}
		}

		private int IsMouseOverPointer(int X, int Y)
		{

			if (MiddlePointers != null) {
				for (int I = 1; I <= MiddlePointers.Count; I++) {
					//Convert to Region.
					using (Region PointerRegion = new Region(BuildPointer(GetpX(((cblPointer)MiddlePointers[I]).pPos)))) {
						//Is the point inside the region.
						if (PointerRegion.IsVisible(X, Y))
							return I;
					}
				}
				return -1;
			}
			return -1;
		}

		private void ClearCurrPointer()
		{
			foreach (cblPointer ptr in MiddlePointers) {
				ptr.pIsCurr = false;
			}
			StartPointer.pIsCurr = false;
			EndPointer.pIsCurr = false;
		}

		private int FindCurr()
		{
			for (int i = 1; i <= MiddlePointers.Count; i++) {
				if (((cblPointer)MiddlePointers[i]).pIsCurr)
					return i;
			}
			return -1;
		}
		#endregion

		#region "Drawing"

		private void DrawPointer(ref Graphics g, Color bColor, float pt, bool IsCurr)
		{
			using (Brush cpbr = new SolidBrush(bColor)) {
				using (Pen pn = new Pen(Color.LightGray, 2)) {
					float pX = GetpX(pt);
					g.FillPath(cpbr, BuildPointer(pX));
					g.DrawPath(pn, BuildPointer(pX));
					pn.Width = 1;
					pn.Color = Color.Black;
					g.DrawPath(pn, BuildPointer(pX));
					if (IsCurr) {
						g.FillEllipse(Brushes.Red, pX + 10, TopMargin + BarHeight + 8, 10, 4);
					}
				}
			}
		}

		private float GetpX(float pos)
		{
			return ((this.Width - 20) * pos) - 5;
		}

		private GraphicsPath BuildPointer(float cPX)
		{
			cPX += 10;
			GraphicsPath gp = new GraphicsPath();
			gp.AddLine(cPX + 5, TopMargin + BarHeight - 3, cPX + 10, TopMargin + BarHeight);
			gp.AddLine(cPX + 10, TopMargin + BarHeight, cPX + 10, TopMargin + BarHeight + 7);
			gp.AddLine(cPX + 10, TopMargin + BarHeight + 7, cPX, TopMargin + BarHeight + 7);
			gp.AddLine(cPX, TopMargin + BarHeight + 7, cPX, TopMargin + BarHeight);
			gp.CloseFigure();
			return gp;
		}

		public LinearGradientBrush LinearBrush(Rectangle BaseRect, LinearGradientMode Mode)
		{
			LinearGradientBrush br = new LinearGradientBrush(new Rectangle(BaseRect.X - 1, BaseRect.Y - 1, BaseRect.Width + 2, BaseRect.Height + 2), Color.AliceBlue, Color.Blue, Mode);
			ColorBlend blend = new ColorBlend();
			blend.Colors = BlendColors;
			blend.Positions = BlendPositions;
			br.InterpolationColors = blend;
			return br;
		}

		public PathGradientBrush PathBrush(Rectangle BaseRect)
		{
			GraphicsPath gp = GetShapePath(BaseRect);
			PathGradientBrush br = new PathGradientBrush(gp);
			ColorBlend blend = new ColorBlend();
			blend.Colors = BlendColors;
			blend.Positions = BlendPositions;
			//        br.CenterPoint = New PointF(40 + Me.Width - 92, CInt(40 + BarHeight + 25))
			br.FocusScales = this.FocalPoints.FocusScales;
			br.CenterPoint = new PointF((this.FocalPoints.CenterPoint.X * 80) + (this.Width - 92), (this.FocalPoints.CenterPoint.Y * 80) + (BarHeight + 25));
			br.InterpolationColors = blend;
			gp.Dispose();
			return br;
		}

		public GraphicsPath GetShapePath(Rectangle rect)
		{
			GraphicsPath gp = new GraphicsPath();
			switch (BlendPathShape) {

				case eBlendPathShape.Ellipse:
					gp.AddEllipse(rect);

					break;
				case eBlendPathShape.Triangle:
					Point[] pts = new Point[] {
						new Point(Convert.ToInt32(rect.X + (rect.Width / 2)), rect.Y),
						new Point(rect.X + rect.Width, rect.Y + rect.Height),
						new Point(rect.X, rect.Y + rect.Height)
					};
					gp.AddPolygon(pts);

					break;
				case eBlendPathShape.Rectangle:
					gp.AddRectangle(rect);

					break;
			}

			return gp;

		}

		private void BuildABlend()
		{
			List<Color> lColors = new List<Color>();
			lColors.Add(StartPointer.pColor);
			if (MiddlePointers != null) {
				foreach (cblPointer ptr in MiddlePointers) {
					lColors.Add(ptr.pColor);
				}
			}
			lColors.Add(EndPointer.pColor);
			BlendColors = lColors.ToArray();
			lColors = null;

			List<float> lPos = new List<float>();
			lPos.Add(StartPointer.pPos);
			if (MiddlePointers != null) {
				foreach (cblPointer ptr in MiddlePointers) {
					lPos.Add(ptr.pPos);
				}
			}
			lPos.Add(EndPointer.pPos);
			BlendPositions = lPos.ToArray();
			lPos = null;

		}


		public void LoadABlend(cBlendItems cb)
		{
			StartPointer.pColor = cb.iColor[0];
			StartPointer.pPos = cb.iPoint[0];
			EndPointer.pColor = cb.iColor[cb.iColor.Length - 1];
			EndPointer.pPos = cb.iPoint[cb.iColor.Length - 1];

			if (cb.iColor.Length > 2) {
				for (int i = 1; i <= cb.iColor.Length - 2; i++) {
					MiddlePointers.Add(new cblPointer(cb.iPoint[i], cb.iColor[i], false));
				}
			}
		}

		private void UpdatePointerColor()
		{
			Color CurrColor = Color.FromArgb(tbarAlpha.Value, Convert.ToInt32(nudRed.Value), Convert.ToInt32(nudGreen.Value), Convert.ToInt32(nudBlue.Value));
			if (StartPointer.pIsCurr) {
				StartPointer.pColor = CurrColor;
			} else if (EndPointer.pIsCurr) {
				EndPointer.pColor = CurrColor;
			} else {
				int curr = FindCurr();
				if (curr > -1)
					((cblPointer)MiddlePointers[curr]).pColor = CurrColor;
			}
			panCurrColor.BackColor = CurrColor;

			txbCurrColor.Text = GetColorName(CurrColor);
			this.Invalidate();
		}

		public string GetColorName(Color c)
		{
			foreach (string ColorName in Known_Color) {
				if (!(Color.FromName(ColorName).IsSystemColor)) {
					if (Convert.ToInt32(ColorTranslator.ToWin32(Color.FromName(ColorName))) == Convert.ToInt32(ColorTranslator.ToWin32(c))) {
						return (c.Name == "ffffffff" ? "White- ffffffff" : ColorName + "- " + c.Name).ToString();
					}
				}
			}
			return (c.Name == "ff7f007f" ? "Transparent- ff7f007f" : c.Name).ToString();

		}
		#endregion

		#region "Painting"

		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
		{
			//Do Nothing
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			//Go through each cblPointer in the collection to get the current Color and Position arrays
			BuildABlend();

			//Create a canvas to aint on the same size as the control
			Bitmap bitmapBuffer = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(bitmapBuffer);
			g.Clear(this.BackColor);
			g.SmoothingMode = SmoothingMode.AntiAlias;

			// Paint the ColorBlender Bar with the Linear Brush
			Rectangle barRect = new Rectangle(10, TopMargin, this.ClientSize.Width - 20, Convert.ToInt32(BarHeight));
			Brush br = LinearBrush(barRect, LinearGradientMode.Horizontal);
			g.FillRectangle(br, barRect);

			// Paint the ColorBlender Sample with the chosen Brush
			Rectangle sampleRect = new Rectangle(this.Width - 92, Convert.ToInt32(BarHeight + 25), 80, 80);
			if (BlendGradientType == eBlendGradientType.Linear) {
				br = LinearBrush(sampleRect, BlendGradientMode);
			} else {
				br = PathBrush(sampleRect);
			}
			g.FillPath(br, GetShapePath(sampleRect));

			//Draw all the cblPointers in their Color at their Position along the Bar
			using (Pen pn = new Pen(Color.Gray, 1)) {
				pn.DashStyle = DashStyle.Dash;
				g.DrawLine(pn, 10, TopMargin + BarHeight + 7, this.ClientSize.Width - 15, TopMargin + BarHeight + 7);

				pn.Color = Color.Black;
				pn.DashStyle = DashStyle.Solid;

				DrawPointer(ref g, StartPointer.pColor, 0, StartPointer.pIsCurr);
				DrawPointer(ref g, EndPointer.pColor, 1, EndPointer.pIsCurr);

				if (MiddlePointers != null) {
					for (int I = 1; I <= MiddlePointers.Count; I++) {
						DrawPointer(ref g, ((cblPointer)MiddlePointers[I]).pColor, ((cblPointer)MiddlePointers[I]).pPos, I == CurrPointer);
					}
				}

			}

			//Draw the entire image to the control in one shot to eliminate flicker
			e.Graphics.DrawImage((Bitmap)bitmapBuffer.Clone(), 0, 0);

			bitmapBuffer.Dispose();
			br.Dispose();
			g.Dispose();



		}
		#endregion

		#region "SortCollection"


		private void SortCollection(Collection col, string psSortPropertyName, bool pbAscending, string psKeyPropertyName = "")
		{
			object obj = null;
			int i = 0;
			int j = 0;
			int iMinMaxIndex = 0;
			object vMinMax = null;
			object vValue = null;
			bool bSortCondition = false;
			bool bUseKey = false;
			string sKey = null;

			bUseKey = (!string.IsNullOrEmpty(psKeyPropertyName));

			for (i = 1; i <= col.Count - 1; i++) {
				obj = col[i];
				vMinMax = Interaction.CallByName(obj, psSortPropertyName, Constants.vbGet);
				iMinMaxIndex = i;

				for (j = i + 1; j <= col.Count; j++) {
					obj = col[j];
					vValue = Interaction.CallByName(obj, psSortPropertyName, Constants.vbGet);

					if ((pbAscending)) {
						bSortCondition = (Convert.ToSingle(vValue) < Convert.ToSingle(vMinMax));
					} else {
						bSortCondition = (Convert.ToSingle(vValue) > Convert.ToSingle(vMinMax));
					}

					if ((bSortCondition)) {
						vMinMax = vValue;
						iMinMaxIndex = j;
					}

					obj = null;
				}

				if ((iMinMaxIndex != i)) {
					obj = col[iMinMaxIndex];

					col.Remove(iMinMaxIndex);
					if ((bUseKey)) {
						sKey = Convert.ToString(Interaction.CallByName(obj, psKeyPropertyName, Constants.vbGet));
						col.Add(obj, sKey, i);
					} else {
						col.Add(obj, "", i);
					}

					obj = null;
				}

				obj = null;
			}

		}

		#endregion

		#region "Controls"

		private void ColorBox_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			UpdateRGBnuds(Color.FromName(ColorBox.Text));
			this.Invalidate();
		}

		private void Panel7_Click(object sender, System.EventArgs e)
		{
			UpdateRGBnuds(((Panel)sender).BackColor);
			this.Invalidate();
		}

		private Panel CurrSwatch;
		private void Panel10_MouseEnter(object sender, System.EventArgs e)
		{
			try {
				CurrSwatch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			} catch (Exception) {
			}
			CurrSwatch = (Panel)sender;
			CurrSwatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		}

		private void TabControl1_MouseLeave(object sender, System.EventArgs e)
		{
			try {
				CurrSwatch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			} catch (Exception) {
			}

		}

		private void UpdateRGBnuds(Color c)
		{
			tbarAlpha.Value = c.A;
			nudRed.Value = c.R;
			nudGreen.Value = c.G;
			nudBlue.Value = c.B;
			UpdatePointerColor();
		}

		private void nud_ValueChanged(System.Object sender, System.EventArgs e)
		{
			txbAlpha.Text = Convert.ToString(tbarAlpha.Value);
			UpdatePointerColor();
		}

		private void txbAlpha_TextChanged(System.Object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(txbAlpha.Text)) {
				switch (Convert.ToInt32(txbAlpha.Text)) {
					case  // ERROR: Case labels with binary operators are unsupported : LessThan
0:
						txbAlpha.Text = Convert.ToString(0);
						break;
					case  // ERROR: Case labels with binary operators are unsupported : GreaterThan
255:
						txbAlpha.Text = Convert.ToString(255);
						break;
				}
				tbarAlpha.Value = Convert.ToInt32(txbAlpha.Text);
			}
		}

		private void butApply_Click(System.Object sender, System.EventArgs e)
		{
			_editorService.CloseDropDown();
		}

		private void butClear_Click(System.Object sender, System.EventArgs e)
		{
			StartPointer.pColor = Color.White;
			EndPointer.pColor = Color.White;
			MiddlePointers.Clear();
			this.Invalidate();
		}
		#endregion

		#region "ColorBox"

		private void ColorList_DrawItem(object sender, DrawItemEventArgs e)
		{
			// If the item is the edit box item, then draw the rectangle white
			// If the item is the selected item, then draw the rectangle blue
			// Otherwise, draw the rectangle filled in beige
			if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit) {
				e.Graphics.FillRectangle(Brushes.White, e.Bounds);
			} else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) {
				e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
			} else {
				e.Graphics.FillRectangle(Brushes.Beige, e.Bounds);
			}

			// Cast the sender object  to ComboBox type.
			ComboBox TheBox = (ComboBox)sender;
			string itemString = Convert.ToString(TheBox.Items[e.Index]);
			Font MyFont = new Font("Tahoma", 10);
			SolidBrush myBrush = new SolidBrush(Color.FromName(itemString));

			//Draw a Color Swatch
			e.Graphics.FillRectangle(myBrush, e.Bounds.X + 3, e.Bounds.Y + 3, 20, e.Bounds.Height - 5);
			e.Graphics.DrawRectangle(Pens.Black, e.Bounds.X + 3, e.Bounds.Y + 3, 20, e.Bounds.Height - 5);

			// Draw the text in the item.
			e.Graphics.DrawString(itemString, MyFont, Brushes.Black, e.Bounds.X + 25, e.Bounds.Y + 1);

			// Draw the focus rectangle around the selected item.
			e.DrawFocusRectangle();
			myBrush.Dispose();
		}

		#endregion

	}

	#region " cblPointer Class "

	class cblPointer
	{
		private Color _pColor;
		private float _pPos;

		private bool _pIsCurr;
		public cblPointer(float pt, Color c, bool IsCurr)
		{
			pPos = pt;
			pColor = c;
			pIsCurr = IsCurr;
		}

		public float pPos {
			get { return _pPos; }
			set { _pPos = value; }
		}

		public Color pColor {
			get { return _pColor; }
			set { _pColor = value; }
		}

		public bool pIsCurr {
			get { return _pIsCurr; }
			set { _pIsCurr = value; }
		}

		public string PosToStrong {
			get { return Math.Round(_pPos * 100, 2).ToString(); }
		}
	}

	#endregion
}
