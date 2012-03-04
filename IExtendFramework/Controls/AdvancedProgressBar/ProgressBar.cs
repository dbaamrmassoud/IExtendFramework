using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Design; // required for this control

namespace IExtendFramework.Controls
{
    /// <summary>
    /// This is a Progress Bar, From ProgBarPlus.
    /// Originally Created by Scott Snyder.
    /// Edited by Eljiah Frederickson
    /// </summary>
    /// <remarks></remarks>
    [ToolboxItem(true)]
    [Designer(typeof(ProgressBarControlDesigner))]
    public class ProgressBar : System.Windows.Forms.UserControl
    {

        private float CylonPosition = 0;
        private float CylonDirection = 1;
        private float CylonGPosition = 0.5F;

        private float CylonGDelta = 0.001F;
        public struct ProgressBarPath
        {
            public Rectangle Rect;
            public GraphicsPath Path;
        }

        #region "Initialize"

        public ProgressBar() : base()
        {

            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
        }

        //Control overrides dispose to clean up the component list.
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

        //Required by the Control Designer

        private System.ComponentModel.IContainer components;
        // NOTE: The following procedure is required by the Component Designer
        // It can be modified using the Component Designer.  Do not modify it
        // using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimerCylon = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            //
            //TimerCylon
            //
            //
            //ProgressBar
            //
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "ProgressBar1";
            this.Size = new System.Drawing.Size(200, 20);
            this.ResumeLayout(false);

        }
        #endregion

        #region "Property Enumeration"

        public enum eShape
        {
            Rectangle,
            Ellipse,
            TriangleLeft,
            TriangleRight,
            TriangleUp,
            TriangleDown,
            Text
        }

        public enum eBarStyle
        {
            Solid,
            GradientLinear,
            GradientPath,
            Texture,
            Hatch
        }

        public enum eOrientation
        {
            Horizontal,
            Vertical
        }

        public enum eTextPlacement
        {
            OverBar,
            OnBar
        }

        public enum eCornersApply
        {
            Both,
            Border,
            Bar
        }

        public enum eRotateText
        {
            None,
            Left,
            Right
        }

        public enum eFillDirection
        {
            Up_Right,
            Down_Left
        }

        public enum eTextShow
        {
            None,
            TextOnly,
            PercentOnly,
            FormatStrPercent,
            FormatStrText,
            FormatStrTextPerc
        }

        public enum eBarType
        {
            Bar,
            CylonBar,
            CylonGlider
        }

        public enum eBarLength
        {
            Full,
            Fixed
        }

        #endregion

        #region "Properties"

        #region "Corners Expandable Property"

        //Corners Property is defined in the Corners Converter Class
        //to use the ExpandableObjectConverter to simulate the BarPadding Property


        private CornersProperty _Corners = new CornersProperty();
        [Category("Appearance ProgressBar"), Description("Get or Set the Corner Radii"), RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CornersProperty Corners {
            get { return _Corners; }
            set {
                _Corners = value;
                this.Refresh();
            }
        }

        #endregion

        #region "Appearance Properties"

        #region "Color and Fill"
        private Color _BarBackColor = Color.White;
        [Category("Appearance ProgressBar"), Description("Get or Set the Color behind the Bar"), DefaultValue(typeof(Color), "White")]
        public Color BarBackColor {
            get { return _BarBackColor; }
            set {
                _BarBackColor = value;
                this.Invalidate();
            }
        }

        private Color _BarColorSolid = Color.Blue;
        [Description("The Solid Color to fill the Bar"), Category("Appearance ProgressBar")]
        public Color BarColorSolid {
            get { return _BarColorSolid; }
            set {
                _BarColorSolid = value;
                this.Invalidate();
            }
        }

        private Color _BarColorSolidB = Color.White;
        [Description("The Secondary Color for Hatch Style"), Category("Appearance ProgressBar")]
        public Color BarColorSolidB {
            get { return _BarColorSolidB; }
            set {
                _BarColorSolidB = value;
                this.Invalidate();
            }
        }

        private cBlendItems _BarColorBlend = new cBlendItems(new Color[] {
                                                                 Color.Navy,
                                                                 Color.Blue
                                                             }, new float[] {
                                                                 0,
                                                                 1
                                                             });
        [Description("The ColorBlend to fill the shape"), Category("Appearance ProgressBar"), Editor(typeof(BlendTypeEditor), typeof(UITypeEditor))]
        public cBlendItems BarColorBlend {
            get { return _BarColorBlend; }
            set {
                _BarColorBlend = value;
                this.Invalidate();
            }
        }

        private eBarStyle _BarStyleFill = eBarStyle.Solid;
        [Description("The Fill Type to apply to the Shape")]
        [Category("Appearance ProgressBar")]
        public eBarStyle BarStyleFill {
            get { return _BarStyleFill; }
            set {
                _BarStyleFill = value;
                this.Invalidate();
            }
        }

        private LinearGradientMode _BarStyleLinear = LinearGradientMode.Horizontal;
        [Description("The Linear Blend type"), Category("Appearance ProgressBar")]
        public LinearGradientMode BarStyleLinear {
            get { return _BarStyleLinear; }
            set {
                _BarStyleLinear = value;
                this.Invalidate();
            }
        }


        private cFocalPoints _FocalPoints = new cFocalPoints(0.5F, 0.5F, 0, 0);
        [Editor(typeof(FocalTypeEditor), typeof(UITypeEditor)), Description("The CenterPoint and FocusScales for the ColorBlend"), Category("Appearance ProgressBar")]
        public cFocalPoints FocalPoints {
            get { return _FocalPoints; }
            set {
                _FocalPoints = value;
                this.Invalidate();
            }
        }

        private HatchStyle _BarStyleHatch = System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard;
        [Editor(typeof(HatchStyleEditor), typeof(UITypeEditor)), Category("Appearance ProgressBar"), Description("Get or Set the Hatch Style when the BarStyleFill is set to Hatch"), DefaultValue(HatchStyle.SmallCheckerBoard)]
        public HatchStyle BarStyleHatch {
            get { return _BarStyleHatch; }
            set {
                _BarStyleHatch = value;
                this.Invalidate();
            }
        }

        private Image _BarStyleTexture = null;
        [Category("Appearance ProgressBar"), Description("Get or Set the Wrap Style for the Texture Image")]
        public Image BarStyleTexture {
            get { return _BarStyleTexture; }
            set {
                _BarStyleTexture = value;
                this.Invalidate();
            }
        }

        private WrapMode _BarStyleWrapMode = WrapMode.Clamp;
        [Category("Appearance ProgressBar"), Description("Get or Set the Wrapmode of the Image"), DefaultValue(WrapMode.Clamp)]
        public WrapMode BarStyleWrapMode {
            get { return _BarStyleWrapMode; }
            set {
                _BarStyleWrapMode = value;
                this.Invalidate();
            }
        }

        #endregion

        #region "Text"

        private bool _Shadow = false;
        [Category("Appearance ProgressBar"), Description("Add Shadow to text"), DefaultValue(false)]
        public bool TextShadow {
            get { return _Shadow; }
            set {
                _Shadow = value;
                this.Invalidate();
            }
        }

        private Color _ShadowColor = Color.White;
        [Category("Appearance ProgressBar"), Description("Define the Color of the Shadow text"), DefaultValue(typeof(Color), "White")]
        public Color TextShadowColor {
            get { return _ShadowColor; }
            set {
                _ShadowColor = value;
                this.Invalidate();
            }
        }


        private eTextShow _TextShow = eTextShow.None;
        [Category("Appearance ProgressBar"), Description("Get or Set how the Text and/or Percent is displayed"), DefaultValue(eTextShow.None)]
        public eTextShow TextShow {
            get { return _TextShow; }
            set {
                _TextShow = value;
                this.Invalidate();
            }
        }

        private string _TextFormat = "Process {0}% Done";
        [Category("Appearance ProgressBar"), Description("Get or Set the Format String to display Percent and or Text variables." + Constants.vbCr + "FormatStrPercent = Enter {0} where you want the Percent to appear." + Constants.vbCr + "FormatStrText = Enter {0} where you want the TextValue to appear." + Constants.vbCr + "FormatStrTextPercent = Enter {0} where you want the TextValue to appear and" + " Enter {1} where you want the Percent to appear."), DefaultValue("Process {0}% Done")]
        public string TextFormat {
            get { return _TextFormat; }
            set {
                _TextFormat = value;
                this.Invalidate();
            }
        }

        private eTextPlacement _TextPlacement = eTextPlacement.OverBar;
        [Category("Appearance ProgressBar"), Description("Where to put text. Static Over Bar or moving on the bar"), DefaultValue(eTextPlacement.OverBar)]
        public eTextPlacement TextPlacement {
            get { return _TextPlacement; }
            set {
                _TextPlacement = value;
                this.Invalidate();
            }
        }

        private StringAlignment _TextAlignment = StringAlignment.Center;
        [Category("Appearance ProgressBar"), Description("Get or Set the Horizontal Alignment of the text"), DefaultValue(StringAlignment.Center)]
        public StringAlignment TextAlignment {
            get { return _TextAlignment; }
            set {
                _TextAlignment = value;
                this.Invalidate();
            }
        }

        private StringAlignment _TextAlignmentVert = StringAlignment.Center;
        [Category("Appearance ProgressBar"), Description("Get or Set the Vertical Alignment of he text"), DefaultValue(StringAlignment.Center)]
        public StringAlignment TextAlignmentVert {
            get { return _TextAlignmentVert; }
            set {
                _TextAlignmentVert = value;
                this.Invalidate();
            }
        }

        private string _TextValue = "";
        [Category("Appearance ProgressBar"), Description("Get or Set the text to appear on the Bar"), DefaultValue("")]
        public string TextValue {
            get { return _TextValue; }
            set {
                _TextValue = value;
                this.Invalidate();
            }
        }

        private eRotateText _RotateText = eRotateText.None;
        [Category("Appearance ProgressBar"), Description("Get or Set the rotation of the text"), DefaultValue(eRotateText.None)]
        public eRotateText TextRotate {
            get { return _RotateText; }
            set {
                _RotateText = value;
                this.Invalidate();
            }
        }

        private bool _TextWrap = true;
        [Category("Appearance ProgressBar"), Description("Get or Set if the text will wrap"), DefaultValue(true)]
        public bool TextWrap {
            get { return _TextWrap; }
            set {
                _TextWrap = value;
                this.Invalidate();
            }
        }

        #endregion

        #region "Border"

        private bool _Border = false;
        [Category("Appearance ProgressBar"), Description("Add Border around control"), DefaultValue(false)]
        public bool Border {
            get { return _Border; }
            set {
                _Border = value;
                ShowDesignBorder = !value;
                this.Invalidate();
            }
        }

        private Color _BorderColor = Color.Black;
        [Category("Appearance ProgressBar"), Description("Get or Set the Border Color"), DefaultValue(typeof(Color), "Black")]
        public Color BorderColor {
            get { return _BorderColor; }
            set {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        private short _BorderWidth = 1;
        [Category("Appearance ProgressBar"), Description("Get or Set the Width of the Border around control"), DefaultValue(1)]
        public short BorderWidth {
            get { return _BorderWidth; }
            set {
                _BorderWidth = value;
                this.Invalidate();
            }
        }

        #endregion

        #region "Shape"

        private eCornersApply _CornersApply = eCornersApply.Both;
        [Category("Appearance ProgressBar"), Description("Apply corners to Bar and/or Border"), DefaultValue(eCornersApply.Both)]
        public eCornersApply CornersApply {
            get { return _CornersApply; }
            set {
                _CornersApply = value;
                this.Invalidate();
            }
        }

        private eShape _Shape = eShape.Rectangle;
        [Category("Appearance ProgressBar"), Description("Get or Set the Shape of the Control"), RefreshProperties(RefreshProperties.All), DefaultValue(eShape.Rectangle)]
        public eShape Shape {
            get { return _Shape; }
            set {
                _Shape = value;
                this.Invalidate();
            }
        }

        private Font _ShapeTextFont = new Font("Arial Black", 30);
        [Category("Appearance ProgressBar"), Description("Get or Set the Font of the Text Shape"), RefreshProperties(RefreshProperties.All), DefaultValue(typeof(Font), "Arial Black")]
        public Font ShapeTextFont {
            get { return _ShapeTextFont; }
            set {
                _ShapeTextFont = value;
                this.Invalidate();
            }
        }

        private string _ShapeText = "ProgressBar";
        [Category("Appearance ProgressBar"), Description("Get or Set the Font of the Text Shape"), RefreshProperties(RefreshProperties.All), DefaultValue("ProgressBar")]
        public string ShapeText {
            get { return _ShapeText; }
            set {
                _ShapeText = value;
                this.Invalidate();
            }
        }

        private eRotateText _ShapeTextRotate = eRotateText.None;
        [Category("Appearance ProgressBar"), Description("Get or Set the rotation of the text shape"), DefaultValue(eRotateText.None)]
        public eRotateText ShapeTextRotate {
            get { return _ShapeTextRotate; }
            set {
                _ShapeTextRotate = value;
                this.Invalidate();
            }
        }

        #endregion

        #endregion

        #region "Behavior Properties"

        private bool _ShowDesignBorder = true;
        [Category("Behavior"), Description("Show Dashed Border around control at design time"), DefaultValue(true)]
        public bool ShowDesignBorder {
            get { return _ShowDesignBorder; }
            set {
                _ShowDesignBorder = value;
                this.Invalidate();
            }
        }

        #endregion

        #region "Bar Cylon"

        private bool _CylonRun = false;
        [Category("Bar Cylon"), Description("Start and Stop the Timer in Cylon Mode"), DefaultValue(false)]
        public bool CylonRun {
            get { return _CylonRun; }
            set {
                if (BarType != eBarType.Bar) {
                    _CylonRun = value;
                    TimerCylon.Enabled = value;
                } else {
                    _CylonRun = false;
                    TimerCylon.Enabled = false;
                }
            }
        }

        private short _CylonInterval = 1;
        [Category("Bar Cylon"), Description("Get or Set the Timer CylonInterval in Cylon Mode"), DefaultValue(1)]
        public short CylonInterval {
            get { return _CylonInterval; }
            set {
                _CylonInterval = value;
                TimerCylon.Interval = value;
                this.Invalidate();
            }
        }

        private float _CylonMove = 5;
        [Category("Bar Cylon"), Description("Get or Set the Speed the bar moves back and forth"), RefreshProperties(RefreshProperties.All), DefaultValue(5)]
        public float CylonMove {
            get { return _CylonMove; }
            set {
                _CylonMove = value;
                this.Invalidate();
            }
        }

        #endregion

        #region "Bar Properties"

        private eBarType _BarType = eBarType.Bar;
        [Category("Bar"), Description("Get or Set the Minimum Value"), RefreshProperties(RefreshProperties.All), DefaultValue(eBarType.Bar)]
        public eBarType BarType {
            get { return _BarType; }
            set {
                _BarType = value;
                if (value == eBarType.Bar)
                    CylonRun = false;
                this.Invalidate();
            }
        }

        private eBarLength _BarLength = eBarLength.Full;
        [Category("Bar"), Description("Get or Set if the Progress Bar Expands with the Value"), DefaultValue(eBarLength.Full)]
        public eBarLength BarLength {
            get { return _BarLength; }
            set {
                _BarLength = value;
                this.Invalidate();
            }
        }

        private short _BarLengthValue = 25;
        [Category("Bar"), Description("Get or Set Length of the bar in Fixed BarLength mode or Cylon Mode"), DefaultValue(25)]
        public short BarLengthValue {
            get { return _BarLengthValue; }
            set {
                _BarLengthValue = value;
                this.Invalidate();
            }
        }

        private eFillDirection _FillDirection = eFillDirection.Up_Right;
        [Category("Bar"), Description("Get or Set the direction the Progress Bar will fill"), DefaultValue(eFillDirection.Up_Right)]
        public eFillDirection FillDirection {
            get { return _FillDirection; }
            set {
                _FillDirection = value;
                this.Invalidate();
            }
        }

        private eOrientation _Orientation = eOrientation.Horizontal;
        [Category("Bar"), Description("Get or Set the Progress Bar's Orientation"), DefaultValue(eOrientation.Horizontal)]
        public eOrientation Orientation {
            get { return _Orientation; }
            set {
                _Orientation = value;
                this.Invalidate();
            }
        }

        private Padding _BarPadding;
        [Description("The Solid Color to fill the Bar"), Category("Bar")]
        public Padding BarPadding {
            get { return _BarPadding; }
            set {
                _BarPadding = value;
                this.Invalidate();
            }
        }

        private Int32 _Min = 0;
        [Category("Bar"), Description("Get or Set the Minimum Value"), RefreshProperties(RefreshProperties.All), DefaultValue(0)]
        public Int32 Minimum {
            get { return _Min; }
            set {
                _Min = value;
                if (this.Value < value)
                    this.Value = value;
                this.Invalidate();
            }
        }

        private Int32 _Max = 100;
        [Category("Bar"), Description("Get or Set the Maximum Value"), RefreshProperties(RefreshProperties.All), DefaultValue(100)]
        public Int32 Maximum {
            get { return _Max; }
            set {
                _Max = value;
                if (this.Value > value)
                    this.Value = value;
                this.Invalidate();
            }
        }

        private Int32 _Value = 50;
        [Category("Bar"), Description("Get or Set the Bar's Value"), RefreshProperties(RefreshProperties.All), DefaultValue(0)]
        public Int32 Value {
            get { return _Value; }
            set {
                if (value > Maximum)
                    value = Maximum;
                if (value < Minimum)
                    value = Minimum;
                _Value = value;
                this.Refresh();
            }
        }

        public void Increment(int Inc = 1)
        {
            if (Value < Maximum)
                Value += Inc;
        }

        public void Decrement(int Inc = 1)
        {
            if (Value > Minimum)
                Value -= Inc;
        }

        public void ResetBar(bool ToMinimumValue = true)
        {
            if (ToMinimumValue) {
                Value = Minimum;
            } else {
                Value = Maximum;
            }
        }

        [Category("Bar"), Description("Percent to of value")]
        public Int32 ValuePercent {
            get { return Convert.ToInt32(((Value - Minimum) / (Maximum - Minimum)) * 100); }
        }


        #endregion

        #region "Hidden Properties"

        [Browsable(false)]
        public override Color BackColor {
            get { return Color.LightGreen; }
            set { }
        }

        [Browsable(false)]
        public new BorderStyle BorderStyle {
            get { return BorderStyle.None; }
            set { }
        }

        [Browsable(false)]
        public override bool AllowDrop {
            get { return false; }
            set { }
        }

        [Browsable(false)]
        public override AutoValidate AutoValidate {
            get { return AutoValidate.Inherit; }
            set { }
        }

        #endregion

        public override string ToString()
        {
            switch (TextShow) {
                case eTextShow.None:
                    return "";
                case eTextShow.PercentOnly:
                    return ValuePercent + "%";
                case eTextShow.TextOnly:
                    return TextValue;
                case eTextShow.FormatStrPercent:
                    return string.Format(TextFormat, ValuePercent);
                case eTextShow.FormatStrText:
                    return string.Format(TextFormat, TextValue);
                case eTextShow.FormatStrTextPerc:
                    return string.Format(TextFormat, TextValue, ValuePercent);
                default:
                    return "";
            }
        }

        #endregion

        #region "Paint Events"

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            pevent.Graphics.Clear(this.BarBackColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            ProgressBarPath MyPath = new ProgressBarPath();
            MyPath = GetPath(this.DisplayRectangle, true);

            if ((this.BackgroundImage != null)) {
                e.Graphics.DrawImage(this.BackgroundImage, this.DisplayRectangle);
            }

            //Call the appropriate Paint Method to draw the bar
            switch (BarType) {

                case eBarType.Bar:
                    if (Value > 0) {
                        PaintBar(e);
                    }

                    break;
                case eBarType.CylonBar:
                    PaintCylonBar(e);

                    break;
                case eBarType.CylonGlider:
                    PaintCylonGlider(e);

                    break;
            }

            //Create the Border Graphicspath and Draw it
            if (Border) {
                Pen MyPen = new Pen(BorderColor, BorderWidth);
                MyPen.Alignment = PenAlignment.Inset;
                var _with1 = MyPath;

                if (Shape == eShape.Text) {
                    if (ShapeTextRotate != eRotateText.None) {
                        Matrix mtrx = new Matrix();
                        mtrx.Rotate(GetRotateAngle(ShapeTextRotate));
                        _with1.Path.Transform(mtrx);
                    }

                    e.Graphics.Transform = TextMatrix(MyPath);
                }
                e.Graphics.DrawPath(MyPen, _with1.Path);
                e.Graphics.ResetTransform();
                MyPen.Dispose();
            }

            //Make a Region from the Graphicspath to clip the shape
            this.Region = null;
            if (Border)
                if (BorderWidth == 1)
                    MyPath = GetPath(this.DisplayRectangle, false);
            Region mRegion = null;

            if (Shape == eShape.Text) {
                if (ShapeTextRotate != eRotateText.None) {
                    Matrix mtrx = new Matrix();
                    mtrx.Rotate(GetRotateAngle(ShapeTextRotate));
                    MyPath.Path.Transform(mtrx);
                }

                mRegion = new Region(MyPath.Path);
                mRegion.Transform(TextMatrix(MyPath));
            } else {
                mRegion = new Region(MyPath.Path);
            }
            this.Region = mRegion;
            mRegion.Dispose();

            //Add the Text
            if (this.TextShow != eTextShow.None & this.TextPlacement == eTextPlacement.OverBar)
                PutText(e, this.DisplayRectangle);

        }

        private void PaintBar(PaintEventArgs e)
        {
            int OrientationWidth = 0;
            int EndPosition = 0;
            int StartPosition = 0;
            int LengthOfBar = 0;
            Rectangle rect = default(Rectangle);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (Orientation == eOrientation.Horizontal) {
                OrientationWidth = this.Size.Width;
            } else {
                OrientationWidth = this.Size.Height;
            }

            if (BarLength == eBarLength.Full) {
                LengthOfBar = Convert.ToInt32(OrientationWidth * ((Value - Minimum) / (Maximum - Minimum))) + 2;
                //- BorderWidth
                StartPosition = 0;
            } else {
                EndPosition = Convert.ToInt32((OrientationWidth) * ((Value - Minimum) / (Maximum - Minimum)));
                //- BorderWidth
                LengthOfBar = BarLengthValue;
                StartPosition = EndPosition - BarLengthValue;
                if (StartPosition < BorderWidth)
                    StartPosition = 1;
            }

            if (Orientation == eOrientation.Horizontal) {
                if (FillDirection == eFillDirection.Down_Left) {
                    rect = new Rectangle(OrientationWidth - StartPosition - LengthOfBar, 0, LengthOfBar + 1, Height - 1);
                } else {
                    rect = new Rectangle(StartPosition - 1, 0, LengthOfBar, Height - 1);
                }
            } else {
                if (FillDirection == eFillDirection.Down_Left) {
                    rect = new Rectangle(0, StartPosition - 2, Width, LengthOfBar);
                } else {
                    rect = new Rectangle(0, OrientationWidth - StartPosition - LengthOfBar, Width, LengthOfBar + 1);
                }
            }

            rect.X += BarPadding.Left;
            rect.Y += BarPadding.Top;
            rect.Width -= BarPadding.Horizontal;
            rect.Height -= BarPadding.Vertical;

            e.Graphics.FillPath((Brush)GetBrush(ref rect), CornerPath(rect));

            if (this.TextShow != eTextShow.None & this.TextPlacement == eTextPlacement.OnBar)
                PutText(e, rect);

        }

        private void PaintCylonBar(PaintEventArgs e)
        {
            Rectangle rect = default(Rectangle);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (Orientation == eOrientation.Horizontal) {
                rect = new Rectangle(Convert.ToInt32(CylonPosition + BarPadding.Left), 0 + BarPadding.Top, BarLengthValue - 1, this.Height - 1 - BarPadding.Vertical);
            } else {
                rect = new Rectangle(0 + BarPadding.Left, Convert.ToInt32(CylonPosition + BarPadding.Top), this.Width - 1 - BarPadding.Horizontal, BarLengthValue - 1);
            }

            e.Graphics.FillPath((Brush)GetBrush(ref rect), CornerPath(rect));

        }


        private void PaintCylonGlider(PaintEventArgs e)
        {
            Rectangle rect = default(Rectangle);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            LinearGradientBrush br = null;
            rect = new Rectangle(BarPadding.Left, BarPadding.Top, this.Width - BarPadding.Horizontal, this.Height - BarPadding.Vertical);
            rect.Inflate(1, 1);
            if (Orientation == eOrientation.Horizontal) {
                br = new LinearGradientBrush(new Point(rect.X, rect.Y), new Point(rect.Right, rect.Y), Color.White, Color.White);
            } else {
                br = new LinearGradientBrush(new Point(rect.X, rect.Bottom), new Point(rect.X, rect.Top + 1), Color.White, Color.White);
            }
            rect.Inflate(-1, -2);

            ColorBlend blend = new ColorBlend();
            blend.Colors = this.BarColorBlend.iColor;
            blend.Positions = this.BarColorBlend.iPoint;
            if (blend.Positions.Length > 2) {
                blend.Colors = new Color[] {
                    this.BarColorBlend.iColor[0],
                    this.BarColorBlend.iColor[0],
                    this.BarColorBlend.iColor[1],
                    this.BarColorBlend.iColor[2],
                    this.BarColorBlend.iColor[2]
                };
            } else {
                blend.Colors = new Color[] {
                    this.BarColorSolid,
                    this.BarColorSolid,
                    this.BarColorSolidB,
                    this.BarColorSolid,
                    this.BarColorSolid
                };

            }
            blend.Positions = new float[] {
                0,
                Convert.ToSingle(CylonGPosition - 0.3),
                CylonGPosition,
                Convert.ToSingle(CylonGPosition + 0.3),
                1
            };
            br.InterpolationColors = blend;

            e.Graphics.FillPath(br, CornerPath(rect));
        }
        #endregion

        #region "Paint Helpers"

        private GraphicsPath CornerPath(Rectangle rect)
        {
            GraphicsPath gp = new GraphicsPath();
            if (this.Shape == eShape.Rectangle) {
                switch (CornersApply) {
                    case eCornersApply.Bar:
                    case eCornersApply.Both:
                        gp = GetRoundedRectPath(rect, Corners);
                        break;
                    case eCornersApply.Border:
                        gp.AddRectangle(rect);
                        break;
                }
            } else {
                gp.AddRectangle(rect);
            }
            return gp;
        }

        public Matrix TextMatrix(ProgressBarPath mp)
        {
            //Scale the Path to fit the Rectangle
            var _with3 = mp;
            RectangleF text_rectf = _with3.Path.GetBounds();
            PointF[] target_pts = {
                new PointF(_with3.Rect.Left, _with3.Rect.Top),
                new PointF(_with3.Rect.Right, _with3.Rect.Top),
                new PointF(_with3.Rect.Left, _with3.Rect.Bottom)
            };

            return new Matrix(text_rectf, target_pts);
        }

        public ProgressBarPath GetPath(Rectangle PathRect, bool IsBorder, short ShowDotBorder = 0)
        {

            ProgressBarPath pPath = new ProgressBarPath();
            pPath.Path = new GraphicsPath();
            switch (Shape) {
                case eShape.Rectangle:
                    if (IsBorder) {
                        pPath.Rect = new Rectangle(PathRect.X, PathRect.Y, PathRect.Width - 1, PathRect.Height - 1);
                    } else {
                        pPath.Rect = new Rectangle(PathRect.X, PathRect.Y, PathRect.Width - ShowDotBorder, PathRect.Height - ShowDotBorder);
                    }
                    if (CornersApply == eCornersApply.Bar) {
                        pPath.Path.AddRectangle(pPath.Rect);
                    } else {
                        pPath.Path = GetRoundedRectPath(pPath.Rect, Corners);
                    }

                    break;
                case eShape.Ellipse:
                    if (IsBorder) {
                        pPath.Rect = new Rectangle(1, 1, PathRect.Width - 2, PathRect.Height - 2);
                    } else {
                        pPath.Rect = new Rectangle(PathRect.X + ShowDotBorder, PathRect.Y + ShowDotBorder, PathRect.Width - ShowDotBorder * 2, PathRect.Height - ShowDotBorder * 2);
                    }
                    pPath.Path.AddEllipse(pPath.Rect);

                    break;
                case eShape.TriangleLeft:
                    pPath.Rect = new Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height);
                    Point[] myArray = null;
                    if (IsBorder) {
                        myArray = new Point[] {
                            new Point(pPath.Rect.Left, Convert.ToInt32(pPath.Rect.Height / 2)),
                            new Point(pPath.Rect.Right, 1),
                            new Point(pPath.Rect.Right, pPath.Rect.Bottom - 1)
                        };
                    } else {
                        myArray = new Point[] {
                            new Point(pPath.Rect.Left + ShowDotBorder, Convert.ToInt32(pPath.Rect.Height / 2)),
                            new Point(Convert.ToInt32(pPath.Rect.Right - ShowDotBorder / 2), 0 + ShowDotBorder),
                            new Point(Convert.ToInt32(pPath.Rect.Right - ShowDotBorder / 2), pPath.Rect.Bottom - ShowDotBorder)
                        };
                    }
                    pPath.Path.AddPolygon(myArray);

                    break;
                case eShape.TriangleRight:
                    pPath.Rect = new Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height);
                    myArray = null;
                    if (IsBorder) {
                        myArray = new Point[] {
                            new Point(0, 1),
                            new Point(0, pPath.Rect.Bottom - 1),
                            new Point(pPath.Rect.Right, Convert.ToInt32(pPath.Rect.Height / 2))
                        };
                    } else {
                        myArray = new Point[] {
                            new Point(0, 0 + ShowDotBorder),
                            new Point(0, pPath.Rect.Bottom - ShowDotBorder),
                            new Point(pPath.Rect.Right - ShowDotBorder, Convert.ToInt32(pPath.Rect.Height / 2))
                        };
                    }
                    pPath.Path.AddPolygon(myArray);

                    break;
                case eShape.TriangleUp:
                    pPath.Rect = new Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height);
                    myArray = null;
                    if (IsBorder) {
                        myArray = new Point[] {
                            new Point(Convert.ToInt32(pPath.Rect.Width / 2), pPath.Rect.Top),
                            new Point(pPath.Rect.Left + 2, pPath.Rect.Bottom - 1),
                            new Point(pPath.Rect.Right - 2, pPath.Rect.Bottom - 1)
                        };
                    } else {
                        myArray = new Point[] {
                            new Point(Convert.ToInt32(pPath.Rect.Width / 2), Convert.ToInt32(pPath.Rect.Top + ShowDotBorder / 2)),
                            new Point(pPath.Rect.Left + ShowDotBorder * 2, pPath.Rect.Bottom - ShowDotBorder),
                            new Point(pPath.Rect.Right - ShowDotBorder * 2, pPath.Rect.Bottom - ShowDotBorder)
                        };
                    }
                    pPath.Path.AddPolygon(myArray);

                    break;
                case eShape.TriangleDown:
                    pPath.Rect = new Rectangle(PathRect.X, PathRect.Y, PathRect.Width, PathRect.Height);
                    myArray = null;
                    if (IsBorder) {
                        myArray = new Point[] {
                            new Point(Convert.ToInt32(pPath.Rect.Width / 2), pPath.Rect.Bottom),
                            new Point(pPath.Rect.Left + 1, 0),
                            new Point(pPath.Rect.Right - 1, 0)
                        };
                    } else {
                        myArray = new Point[] {
                            new Point(Convert.ToInt32(pPath.Rect.Width / 2), Convert.ToInt32(pPath.Rect.Bottom - ShowDotBorder / 2)),
                            new Point(pPath.Rect.Left + ShowDotBorder, 0),
                            new Point(pPath.Rect.Right - ShowDotBorder, 0)
                        };
                    }
                    pPath.Path.AddPolygon(myArray);

                    break;
                case eShape.Text:

                    if (IsBorder) {
                        pPath.Rect = new Rectangle(1, 1, PathRect.Width - 2, PathRect.Height - 2);
                    } else {
                        pPath.Rect = new Rectangle(PathRect.X + 1, PathRect.Y + 1, Convert.ToInt32(PathRect.Width - 2 - ShowDotBorder / 2), PathRect.Height - 2 - ShowDotBorder);
                    }

                    // Make the StringFormat.
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    // Add the text to the GraphicsPath.
                    pPath.Path.AddString(ShapeText, ShapeTextFont.FontFamily, Convert.ToInt32(FontStyle.Bold), pPath.Rect.Height, new PointF(0, 0), sf);

                    sf.Dispose();
                    break;
            }

            return pPath;
        }

        public void PutText(PaintEventArgs e, Rectangle TextRect)
        {
            StringFormat sf = new StringFormat();
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            sf.Alignment = TextAlignment;
            sf.LineAlignment = TextAlignmentVert;
            if (!TextWrap)
                sf.FormatFlags = StringFormatFlags.NoWrap;
            if (TextShadow) {
                Rectangle ShadowRect = TextRect;
                ShadowRect.Offset(1, 1);
                if (TextRotate != eRotateText.None)
                    ShadowRect = RotateRect(e, ShadowRect, TextRotate);
                e.Graphics.DrawString(this.ToString(), this.Font, new SolidBrush(TextShadowColor), ShadowRect, sf);
                e.Graphics.ResetTransform();
            }

            if (TextRotate != eRotateText.None)
                TextRect = RotateRect(e, TextRect, TextRotate);
            e.Graphics.DrawString(this.ToString(), this.Font, new SolidBrush(this.ForeColor), TextRect, sf);
            e.Graphics.ResetTransform();

        }

        private Rectangle RotateRect(System.Windows.Forms.PaintEventArgs e, Rectangle TabRect, eRotateText Rotate)
        {

            PointF cp = new PointF(TabRect.Left + (TabRect.Width / 2), TabRect.Top + (TabRect.Height / 2));
            e.Graphics.TranslateTransform(cp.X, cp.Y);
            e.Graphics.RotateTransform(GetRotateAngle(Rotate));
            return new Rectangle(-(TabRect.Height / 2), -(TabRect.Width / 2), TabRect.Height, TabRect.Width);

        }

        public short GetRotateAngle(eRotateText Rotate)
        {
            float RotateAngle = 0;
            switch (Rotate) {
                case eRotateText.Left:
                    RotateAngle = 270;
                    break;
                case eRotateText.Right:
                    RotateAngle = 90;
                    break;
            }
            return Convert.ToInt16(RotateAngle);
        }

        private Brush GetBrush(ref Rectangle rect)
        {

            try {
                switch (BarStyleFill) {
                    case eBarStyle.Solid:
                        return new SolidBrush(BarColorSolid);
                    case eBarStyle.Hatch:
                        return new HatchBrush(BarStyleHatch, BarColorSolid, BarColorSolidB);
                    case eBarStyle.GradientLinear:
                        if (this.Orientation == eOrientation.Horizontal) {
                            rect.Inflate(-2, 0);
                        } else {
                            rect.Inflate(0, -2);
                        }
                        LinearGradientBrush br = new LinearGradientBrush(rect, Color.White, Color.White, BarStyleLinear);
                        ColorBlend cb = new ColorBlend();
                        cb.Colors = this.BarColorBlend.iColor;
                        cb.Positions = this.BarColorBlend.iPoint;
                        br.InterpolationColors = cb;

                        return br;
                    case eBarStyle.GradientPath:
                        float OffsetX = 0;
                        float OffsetY = 0;
                        float CylonOffsetX = 0;
                        float CylonOffsetY = 0;
                        if (this.BarType == eBarType.CylonBar) {
                            if (this.Orientation == eOrientation.Horizontal) {
                                CylonOffsetX = CylonPosition;
                            } else {
                                CylonOffsetY = CylonPosition;
                            }
                        } else {
                            if (this.Orientation == eOrientation.Horizontal) {
                                OffsetX = rect.X;
                            } else {
                                OffsetY = rect.Y;
                            }
                        }
                        PathGradientBrush br2 = new PathGradientBrush(GetPath(rect, false).Path);
                        cb = new ColorBlend();
                        cb.Colors = this.BarColorBlend.iColor;
                        cb.Positions = this.BarColorBlend.iPoint;
                        br2.FocusScales = this.FocalPoints.FocusScales;
                        br2.CenterPoint = new PointF(this.FocalPoints.CenterPoint.X * Convert.ToSingle(rect.Width) + OffsetX + this.BarPadding.Left + CylonOffsetX, this.FocalPoints.CenterPoint.Y * Convert.ToSingle(rect.Height) + OffsetY + this.BarPadding.Top + CylonOffsetY);
                        br2.InterpolationColors = cb;

                        return br2;
                    case eBarStyle.Texture:
                        TextureBrush br3 = null;
                        br3 = new TextureBrush(BarStyleTexture);
                        br3.WrapMode = BarStyleWrapMode;
                        return br3;
                }
            } catch (Exception) {
                return new SolidBrush(BarColorSolid);
            }
            return new SolidBrush(BarColorSolid);
        }

        public GraphicsPath GetRoundedRectPath(RectangleF BaseRect, CornersProperty rCorners)
        {

            RectangleF ArcRect = default(RectangleF);
            System.Drawing.Drawing2D.GraphicsPath MyPath = new System.Drawing.Drawing2D.GraphicsPath();
            if (rCorners.All == -1) {
                var _with5 = MyPath;
                // top left arc
                if (rCorners.UpperLeft == 0) {
                    _with5.AddLine(BaseRect.X, BaseRect.Y, BaseRect.X, BaseRect.Y);
                } else {
                    ArcRect = new RectangleF(BaseRect.Location, new SizeF(rCorners.UpperLeft * 2, rCorners.UpperLeft * 2));
                    _with5.AddArc(ArcRect, 180, 90);
                }

                // top right arc
                if (rCorners.UpperRight == 0) {
                    _with5.AddLine(BaseRect.X + (rCorners.UpperLeft), BaseRect.Y, BaseRect.Right - (rCorners.UpperRight), BaseRect.Top);
                } else {
                    ArcRect = new RectangleF(BaseRect.Location, new SizeF(rCorners.UpperRight * 2, rCorners.UpperRight * 2));
                    ArcRect.X = BaseRect.Right - (rCorners.UpperRight * 2);
                    _with5.AddArc(ArcRect, 270, 90);
                }

                // bottom right arc
                if (rCorners.LowerRight == 0) {
                    _with5.AddLine(BaseRect.Right, BaseRect.Top + (rCorners.UpperRight), BaseRect.Right, BaseRect.Bottom - (rCorners.LowerRight));
                } else {
                    ArcRect = new RectangleF(BaseRect.Location, new SizeF(rCorners.LowerRight * 2, rCorners.LowerRight * 2));
                    ArcRect.Y = BaseRect.Bottom - (rCorners.LowerRight * 2);
                    ArcRect.X = BaseRect.Right - (rCorners.LowerRight * 2);
                    _with5.AddArc(ArcRect, 0, 90);
                }

                // bottom left arc
                if (rCorners.LowerLeft == 0) {
                    _with5.AddLine(BaseRect.Right - (rCorners.LowerRight), BaseRect.Bottom, BaseRect.X - (rCorners.LowerLeft), BaseRect.Bottom);
                } else {
                    ArcRect = new RectangleF(BaseRect.Location, new SizeF(rCorners.LowerLeft * 2, rCorners.LowerLeft * 2));
                    ArcRect.Y = BaseRect.Bottom - (rCorners.LowerLeft * 2);
                    _with5.AddArc(ArcRect, 90, 90);
                }

                _with5.CloseFigure();
            } else {
                var _with6 = MyPath;
                if (rCorners.All == 0) {
                    _with6.AddRectangle(BaseRect);

                } else {
                    ArcRect = new RectangleF(BaseRect.Location, new SizeF(rCorners.All * 2, rCorners.All * 2));
                    // top left arc
                    _with6.AddArc(ArcRect, 180, 90);

                    // top right arc
                    ArcRect.X = BaseRect.Right - (rCorners.All * 2);
                    _with6.AddArc(ArcRect, 270, 90);

                    // bottom right arc
                    ArcRect.Y = BaseRect.Bottom - (rCorners.All * 2);
                    _with6.AddArc(ArcRect, 0, 90);

                    // bottom left arc
                    ArcRect.X = BaseRect.Left;
                    _with6.AddArc(ArcRect, 90, 90);

                }
                _with6.CloseFigure();
            }
            return MyPath;
        }
        #endregion

        #region "Cylon"

        private void TimerCylon_Tick(object sender, System.EventArgs e)
        {
            switch (BarType) {
                case eBarType.CylonBar:
                    if (Orientation == eOrientation.Horizontal) {
                        if (CylonPosition + BarLengthValue >= this.Width)
                            CylonDirection = -(_CylonMove);
                        if (CylonPosition <= 0)
                            CylonDirection = _CylonMove;
                    } else {
                        if (CylonPosition + BarLengthValue >= this.Height)
                            CylonDirection = -(_CylonMove);
                        if (CylonPosition <= 0)
                            CylonDirection = _CylonMove;
                    }

                    CylonPosition += CylonDirection;

                    break;
                case eBarType.CylonGlider:
                    CylonGPosition += CylonGDelta * _CylonMove;
                    if ((CylonGPosition > 1) || (CylonGPosition < 0))
                        CylonGDelta = -CylonGDelta;
                    break;
            }

            this.Refresh();
        }

        private System.Windows.Forms.Timer withEventsField_TimerCylon;
        internal System.Windows.Forms.Timer TimerCylon {
            get { return withEventsField_TimerCylon; }
            set {
                if (withEventsField_TimerCylon != null) {
                    withEventsField_TimerCylon.Tick -= TimerCylon_Tick;
                }
                withEventsField_TimerCylon = value;
                if (withEventsField_TimerCylon != null) {
                    withEventsField_TimerCylon.Tick += TimerCylon_Tick;
                }
            }

        }
        #endregion

    }

    #region "Dropdown Editors"

    #region "HatchStyleEditor"

    public class HatchStyleEditor : UITypeEditor
    {

        // Indicate that we display a dropdown.
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        // Edit a line style
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            // Get an IWindowsFormsEditorService object.
            IWindowsFormsEditorService editor_service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (editor_service == null) {
                return base.EditValue(context, provider, value);
            }

            // Pass the UI editor the current property value
            ProgressBar Instance = new ProgressBar();
            if (object.ReferenceEquals(context.Instance.GetType(), typeof(ProgressBar))) {
                Instance = (ProgressBar)context.Instance;
            } else {
                Instance = ((ProgressBarActionList)context.Instance).CurrProgressBar;
            }

            // Convert the value into a BorderStyles value.
            HatchStyle hatch_style = (HatchStyle)value;

            // Make the editing control.
            HatchStyleListBox editor_control = new HatchStyleListBox(hatch_style.ToString(), Instance.BarColorSolid, Instance.BarColorSolidB, editor_service);
            // Display the editing control.
            editor_service.DropDownControl(editor_control);

            // Save the new results.
            return (HatchStyle)System.Enum.Parse(typeof(HatchStyle), editor_control.Text, true);
        }

        public override bool IsDropDownResizable {
            get { return base.IsDropDownResizable; }
        }

        //SmartTag Workaround
        private ITypeDescriptorContext SmartContext;
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            SmartContext = context;
            //store reference for use in PaintValue
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            HatchStyle hatch = (HatchStyle)e.Value;
            // Pass the UI editor the current property value
            ProgressBar Instance = new ProgressBar();
            //e.context only works properly in the Propertygrid.
            //When comming from the SmartTag e.context becomes null and
            //will cause a fatal crash of the IDE.
            //So to get around the null value error I captured a reference to the context
            //in the SmartContext variable in the GetPaintValueSupported function
            if (e.Context != null) {
                Instance = (ProgressBar)e.Context.Instance;
            } else {
                Instance = ((ProgressBarActionList)SmartContext.Instance).CurrProgressBar;
            }

            using (Brush br = new HatchBrush(hatch, Instance.BarColorSolid, Instance.BarColorSolidB)) {
                e.Graphics.FillRectangle(br, e.Bounds);
            }

        }
    }

    #region "HatchStyleListBox Custom Control"

    [ToolboxItem(false)]
    public class HatchStyleListBox : ListBox
    {

        // The editor service displaying this control.

        private IWindowsFormsEditorService m_EditorService;
        public HatchStyleListBox(string hatch_style, Color ColorFore, Color ColorBack, IWindowsFormsEditorService editor_service) : base()
        {
            DrawItem += HatchStyleListBox_DrawItem;
            Click += HatchStyleListBox_Click;

            m_EditorService = editor_service;
            // Make items for each LineStyles value.
            this.Items.Clear();
            string[] hatchNames = System.Enum.GetNames(typeof(HatchStyle));
            Array.Sort(hatchNames);
            foreach (string hs in hatchNames) {
                this.Items.Add(hs);
            }
            this.SelectedIndex = this.FindStringExact(hatch_style);
            this.ColorFore = ColorFore;
            this.ColorBack = ColorBack;
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ItemHeight = 21;
            this.Height = 200;
            this.Width = 200;
        }

        private Color _ColorFore;
        public Color ColorFore {
            get { return _ColorFore; }
            set { _ColorFore = value; }
        }

        private Color _ColorBack;
        public Color ColorBack {
            get { return _ColorBack; }
            set { _ColorBack = value; }
        }

        // When the user selects an item, close the dropdown.
        private void HatchStyleListBox_Click(object sender, System.EventArgs e)
        {
            if (m_EditorService != null) {
                m_EditorService.CloseDropDown();
            }
        }

        // Draw a menu item.
        private void HatchStyleListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index != -1 & this.Items.Count > 0) {
                Graphics g = e.Graphics;
                Rectangle sample = e.Bounds;
                Rectangle sampletext = e.Bounds;

                sample.Width = 40;
                sample.Inflate(0, -3);
                sampletext.Width = sampletext.Width - sample.Width - 2;
                sampletext.X = sample.Right + 2;

                string displayText = this.Items[e.Index].ToString();
                HatchStyle hs = (HatchStyle)System.Enum.Parse(typeof(HatchStyle), displayText, true);
                HatchBrush hb = new HatchBrush(hs, ColorFore, ColorBack);

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                if ((e.State & DrawItemState.Focus) == 0) {
                    g.FillRectangle(new SolidBrush(SystemColors.Window), sampletext);
                    g.DrawString(displayText, this.Font, new SolidBrush(SystemColors.WindowText), sampletext, sf);
                } else {
                    g.FillRectangle(new SolidBrush(SystemColors.Highlight), sampletext);
                    g.DrawString(displayText, this.Font, new SolidBrush(SystemColors.HighlightText), sampletext, sf);
                }
                g.FillRectangle(hb, sample);
                g.DrawRectangle(new Pen(Color.Black, 1), sample);
            }
            e.DrawFocusRectangle();

        }
    }

    #endregion

    #endregion

    #region "BlendTypeEditor - UITypeEditor"

    #region " cBlendItems "

    public class cBlendItems
    {


        public cBlendItems()
        {
        }

        public cBlendItems(Color[] Color, float[] Pt)
        {
            iColor = Color;
            iPoint = Pt;
        }

        private Color[] _iColor;
        [Description("The Color for the Point"), Category("Appearance")]
        public Color[] iColor {
            get { return _iColor; }
            set { _iColor = value; }
        }

        private float[] _iPoint;
        [Description("The Color for the Point"), Category("Appearance")]
        public float[] iPoint {
            get { return _iPoint; }
            set { _iPoint = value; }
        }

        public override string ToString()
        {
            return "BlendItems";
        }

    }

    #endregion

    public class BlendTypeEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if ((context != null)) {
                return UITypeEditorEditStyle.DropDown;
            }
            return (base.GetEditStyle(context));
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (((context != null)) & ((provider != null))) {
                // Access the property browser's UI display service, IWindowsFormsEditorService
                IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if ((editorService != null)) {
                    // Create an instance of the UI editor, passing a reference to the editor service
                    DropdownColorBlender dropDownEditor = new DropdownColorBlender(editorService);

                    // Pass the UI editor the current property value
                    ProgressBar Instance = new ProgressBar();
                    if (object.ReferenceEquals(context.Instance.GetType(), typeof(ProgressBar))) {
                        Instance = (ProgressBar)context.Instance;
                    } else {
                        Instance = ((ProgressBarActionList)context.Instance).CurrProgressBar;
                    }

                    var _with7 = dropDownEditor;

                    switch (Instance.Shape) {
                        case ProgressBar.eShape.Ellipse:
                            _with7.BlendPathShape = DropdownColorBlender.eBlendPathShape.Ellipse;
                            break;
                        case ProgressBar.eShape.Rectangle:
                        case ProgressBar.eShape.Text:
                            _with7.BlendPathShape = DropdownColorBlender.eBlendPathShape.Rectangle;
                            break;
                        default:
                            _with7.BlendPathShape = DropdownColorBlender.eBlendPathShape.Triangle;
                            break;
                    }

                    if (Instance.BarStyleFill == ProgressBar.eBarStyle.GradientPath) {
                        _with7.BlendGradientType = DropdownColorBlender.eBlendGradientType.Path;
                        _with7.FocalPoints = new cFocalPoints(new PointF(Instance.FocalPoints.CenterPoint.X, Instance.FocalPoints.CenterPoint.Y), Instance.FocalPoints.FocusScales);
                    } else {
                        _with7.BlendGradientType = DropdownColorBlender.eBlendGradientType.Linear;
                        _with7.BlendGradientMode = Instance.BarStyleLinear;
                    }


                    _with7.LoadABlend(Instance.BarColorBlend);

                    // Display the UI editor
                    editorService.DropDownControl(dropDownEditor);

                    // Return the new property value from the editor
                    return new cBlendItems(dropDownEditor.BlendColors, dropDownEditor.BlendPositions);
                }
            }
            return base.EditValue(context, provider, value);
        }

        // Indicate that we draw values in the Properties window.
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        // Draw a BorderStyles value.
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            // Erase the area.
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            // Draw the sample.
            cBlendItems cblnd = (cBlendItems)e.Value;
            using (LinearGradientBrush br = new LinearGradientBrush(e.Bounds, Color.Black, Color.Black, LinearGradientMode.Horizontal)) {
                ColorBlend cb = new ColorBlend();
                cb.Colors = cblnd.iColor;
                cb.Positions = cblnd.iPoint;
                br.InterpolationColors = cb;
                e.Graphics.FillRectangle(br, e.Bounds);
            }
        }
    }
    #endregion

    #endregion

    #region "Modal Form Editors"

    #region "FocalTypeEditor"

    #region "cFocalPoints"

    public class cFocalPoints
    {

        private PointF _CenterPoint = new PointF(0.5f, 0.5f);
        public PointF CenterPoint {
            get { return _CenterPoint; }
            set {
                if (value.X < 0)
                    value.X = 0;
                if (value.X > 1)
                    value.X = 1;
                if (value.Y < 0)
                    value.Y = 0;
                if (value.Y > 1)
                    value.Y = 1;
                _CenterPoint = value;
            }
        }

        private PointF _FocusScales = new PointF(0, 0);
        public PointF FocusScales {
            get { return _FocusScales; }
            set {
                if (value.X < 0)
                    value.X = 0;
                if (value.X > 1)
                    value.X = 1;
                if (value.Y < 0)
                    value.Y = 0;
                if (value.Y > 1)
                    value.Y = 1;
                _FocusScales = value;
            }
        }

        public cFocalPoints()
        {
        }

        public cFocalPoints(float Cx, float Cy, float Fx, float Fy)
        {
            this.CenterPoint = new PointF(Cx, Cy);
            this.FocusScales = new PointF(Fx, Fy);
        }

        public cFocalPoints(PointF ptC, PointF ptF)
        {
            this.CenterPoint = ptC;
            this.FocusScales = ptF;
        }

        public override string ToString()
        {
            return "CP=" + _CenterPoint.ToString() + ", FP=" + _FocusScales.ToString();
        }

    }

    #endregion

    public class FocalTypeEditor : UITypeEditor
    {

        // Indicate that we display a modal dialog.
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        // Edit a Selected value.
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            // Get the editor service.
            IWindowsFormsEditorService editor_service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (editor_service == null)
                return value;

            ProgressBar Instance = new ProgressBar();
            if (object.ReferenceEquals(context.Instance.GetType(), typeof(ProgressBar))) {
                Instance = (ProgressBar)context.Instance;
            } else {
                Instance = ((ProgressBarActionList)context.Instance).CurrProgressBar;
            }

            FocalPointsDialog dlg = new FocalPointsDialog();

            // Prepare the editing dialog.
            var _with8 = dlg;
            float ratio = 0;
            int BarWidth = 0;
            int BarHeight = 0;
            if (Instance.BarType == ProgressBar.eBarType.CylonBar) {
                if (Instance.Orientation == ProgressBar.eOrientation.Horizontal) {
                    BarWidth = Instance.BarLengthValue;
                    BarHeight = Instance.Height;
                } else {
                    BarWidth = Instance.Width;
                    BarHeight = Instance.BarLengthValue;
                }
            } else {
                BarWidth = Instance.Width;
                BarHeight = Instance.Height;
            }


            if (BarWidth > BarHeight) {
                _with8.TheShape.Height = Convert.ToInt32(_with8.TheShape.Width * (BarHeight / BarWidth));
                _with8.TheShape.Top = Convert.ToInt32((_with8.panShapeHolder.Height - _with8.TheShape.Height) / 2);
                ratio = Convert.ToSingle(_with8.TheShape.Height / BarHeight);
            } else {
                _with8.TheShape.Width = Convert.ToInt32(_with8.TheShape.Height * (BarWidth / BarHeight));
                _with8.TheShape.Left = Convert.ToInt32((_with8.panShapeHolder.Width - _with8.TheShape.Width) / 2);
                ratio = Convert.ToSingle(_with8.TheShape.Width / BarWidth);
            }

            _with8.TheShape.Shape = Instance.Shape;
            _with8.TheShape.BarStyleFill = Instance.BarStyleFill;
            _with8.TheShape.BarStyleLinear = Instance.BarStyleLinear;
            _with8.TheShape.BarColorSolid = Instance.BarColorSolid;
            _with8.TheShape.BorderWidth = Instance.BorderWidth;
            _with8.TheShape.BorderColor = Instance.BorderColor;
            _with8.TheShape.BorderStyle = Instance.BorderStyle;
            _with8.TheShape.BarColorBlend = Instance.BarColorBlend;
            _with8.TheShape.Corners = new CornersProperty(Convert.ToInt16(Instance.Corners.LowerLeft * ratio), Convert.ToInt16(Instance.Corners.LowerRight * ratio), Convert.ToInt16(Instance.Corners.UpperLeft * ratio), Convert.ToInt16(Instance.Corners.UpperRight * ratio));
            _with8.TheShape.FocalPoints = new cFocalPoints(Instance.FocalPoints.CenterPoint, Instance.FocalPoints.FocusScales);

            // Display the dialog.
            editor_service.ShowDialog(dlg);
            Instance.Refresh();
            // Return the new value.
            return dlg.TheShape.FocalPoints;
        }
    }

    #endregion

    #endregion

    #region "Expandable Border Corners Property Class"

    [TypeConverter(typeof(CornerConverter)), Serializable()]
    public class CornersProperty
    {

        private short _All = -1;
        private short _UpperLeft = 0;
        private short _UpperRight = 0;
        private short _LowerLeft = 0;

        private short _LowerRight = 0;
        public CornersProperty(short LowerLeft, short LowerRight, short UpperLeft, short UpperRight)
        {
            this.LowerLeft = LowerLeft;
            this.LowerRight = LowerRight;
            this.UpperLeft = UpperLeft;
            this.UpperRight = UpperRight;
        }

        public CornersProperty()
        {
            this.LowerLeft = 0;
            this.LowerRight = 0;
            this.UpperLeft = 0;
            this.UpperRight = 0;
        }

        private void CheckForAll(short val)
        {
            if (val == LowerLeft && val == LowerRight && val == UpperLeft && val == UpperRight) {
                if (All != val)
                    All = val;
            } else {
                if (All != -1)
                    All = -1;
            }
        }

        [DescriptionAttribute("Set the Radius of the All four Corners the same"), NotifyParentProperty(true), RefreshProperties(RefreshProperties.All)]
        public short All {
            get { return _All; }
            set {
                _All = value;
                if (value > -1) {
                    this.LowerLeft = value;
                    this.LowerRight = value;
                    this.UpperLeft = value;
                    this.UpperRight = value;
                }
            }
        }

        [DescriptionAttribute("Set the Radius of the Upper Left Corner"), RefreshProperties(RefreshProperties.All)]
        public short UpperLeft {
            get { return _UpperLeft; }
            set {
                _UpperLeft = value;
                CheckForAll(value);
            }
        }

        [DescriptionAttribute("Set the Radius of the Upper Right Corner"), RefreshProperties(RefreshProperties.All)]
        public short UpperRight {
            get { return _UpperRight; }
            set {
                _UpperRight = value;
                CheckForAll(value);
            }
        }

        [DescriptionAttribute("Set the Radius of the Lower Left Corner"), RefreshProperties(RefreshProperties.All)]
        public short LowerLeft {
            get { return _LowerLeft; }
            set {
                _LowerLeft = value;
                CheckForAll(value);
            }
        }

        [DescriptionAttribute("Set the Radius of the Lower Right Corner"), RefreshProperties(RefreshProperties.All)]
        public short LowerRight {
            get { return _LowerRight; }
            set {
                _LowerRight = value;
                CheckForAll(value);
            }
        }

    }
    //Corners Properties

    internal class CornerConverter : ExpandableObjectConverter
    {

        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if ((object.ReferenceEquals(sourceType, typeof(string)))) {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string) {
                try {
                    string s = Convert.ToString(value);
                    string[] cornerParts = new string[5];
                    cornerParts = Strings.Split(s, ",");
                    if ((cornerParts != null)) {
                        if ((cornerParts[0] == null))
                            cornerParts[0] = Convert.ToString(0);
                        if ((cornerParts[1] == null))
                            cornerParts[1] = Convert.ToString(0);
                        if ((cornerParts[2] == null))
                            cornerParts[2] = Convert.ToString(0);
                        if ((cornerParts[3] == null))
                            cornerParts[3] = Convert.ToString(0);
                        return new CornersProperty(Convert.ToInt16(cornerParts[0]), Convert.ToInt16(cornerParts[1]), Convert.ToInt16(cornerParts[2]), Convert.ToInt16(cornerParts[3]));
                    }
                } catch (Exception) {
                    throw new ArgumentException("Can not convert '" + Convert.ToString(value) + "' to type Corners");
                }
            } else {
                return new CornersProperty();
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {

            if ((object.ReferenceEquals(destinationType, typeof(System.String)) && value is CornersProperty)) {
                CornersProperty _Corners = (CornersProperty)value;

                // build the string as "UpperLeft,UpperRight,LowerLeft,LowerRight"
                return string.Format("{0},{1},{2},{3}", _Corners.LowerLeft, _Corners.LowerRight, _Corners.UpperLeft, _Corners.UpperRight);
            }
            return base.ConvertTo(context, culture, value, destinationType);

        }

    }
    //CornerConverter Code

    #endregion

    #region "Control Designer"
    //
    //You have to directly add the System.Design Reference to the Project
    //
    public class ProgressBarControlDesigner : ControlDesigner
    {
        private ProgressBar _ProgressBar = null;

        private DesignerActionListCollection _Lists;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            _ProgressBar = (ProgressBar)component;
        }

        #region "OnPaintAdornments"

        protected override void OnPaintAdornments(PaintEventArgs e)
        {
            if (_ProgressBar.ShowDesignBorder) {
                Graphics g = e.Graphics;
                Pen myPen = new Pen(Color.Gray, 1);
                Rectangle rect = new Rectangle(0, 0, _ProgressBar.Width - 1, _ProgressBar.Height - 1);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                if (_ProgressBar.CornersApply == ProgressBar.eCornersApply.Bar) {
                    g.DrawRectangle(myPen, rect);
                } else {
                    ProgressBar.ProgressBarPath MyPath = new ProgressBar.ProgressBarPath();
                    MyPath = _ProgressBar.GetPath(_ProgressBar.DisplayRectangle, false, 2);
                    var _with9 = MyPath;

                    if (_ProgressBar.Shape == ProgressBar.eShape.Text) {
                        if (_ProgressBar.ShapeTextRotate != ProgressBar.eRotateText.None) {
                            Matrix mtrx = new Matrix();
                            mtrx.Rotate(_ProgressBar.GetRotateAngle(_ProgressBar.ShapeTextRotate));
                            _with9.Path.Transform(mtrx);
                        }

                        g.Transform = _ProgressBar.TextMatrix(MyPath);
                        g.DrawPath(myPen, _with9.Path);
                        g.ResetTransform();
                    } else {
                        e.Graphics.DrawPath(myPen, _with9.Path);

                    }
                }

                myPen.Dispose();
            }
        }
        #endregion

        #region "ActionLists"

        public override System.ComponentModel.Design.DesignerActionListCollection ActionLists {
            get {
                if (_Lists == null) {
                    _Lists = new DesignerActionListCollection();
                    _Lists.Add(new ProgressBarActionList(this.Component));
                }
                return _Lists;
            }
        }

        #endregion

    }

    #region "ProgressBarActionList"

    public class ProgressBarActionList : DesignerActionList
    {

        private ProgressBar _ProgressBarSelector;

        private DesignerActionUIService _DesignerService = null;
        public ProgressBarActionList(IComponent component) : base(component)
        {

            // Save a reference to the control we are designing.
            _ProgressBarSelector = (ProgressBar)component;

            // Save a reference to the DesignerActionUIService
            _DesignerService = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));

            //Makes the Smart Tags open automatically
            this.AutoShow = true;
        }

        #region "Smart Tag Items"

        #region "Properties"

        public ProgressBar.eFillDirection FillDirection {
            get { return _ProgressBarSelector.FillDirection; }
            set { SetControlProperty("FillDirection", value); }
        }

        public ProgressBar.eOrientation Orientation {
            get { return _ProgressBarSelector.Orientation; }
            set { SetControlProperty("Orientation", value); }
        }

        public ProgressBar.eBarType BarType {
            get { return _ProgressBarSelector.BarType; }
            set {
                SetControlProperty("BarType", value);
                _DesignerService.ShowUI(_ProgressBarSelector);
            }
        }

        public float CylonMove {
            get { return _ProgressBarSelector.CylonMove; }
            set { SetControlProperty("CylonMove", value); }
        }

        public short CylonInterval {
            get { return _ProgressBarSelector.CylonInterval; }
            set { SetControlProperty("CylonInterval", value); }
        }

        public bool CylonRun {
            get { return _ProgressBarSelector.CylonRun; }
            set { SetControlProperty("CylonRun", value); }
        }

        public ProgressBar.eBarLength BarLength {
            get { return _ProgressBarSelector.BarLength; }
            set {
                SetControlProperty("BarLength", value);
                _DesignerService.ShowUI(_ProgressBarSelector);
            }
        }

        public short BarLengthValue {
            get { return _ProgressBarSelector.BarLengthValue; }
            set { SetControlProperty("BarLengthValue", value); }
        }

        public ProgressBar.eShape Shape {
            get { return _ProgressBarSelector.Shape; }
            set {
                SetControlProperty("Shape", value);
                _DesignerService.ShowUI(_ProgressBarSelector);
            }
        }

        public ProgressBar.eCornersApply CornersApply {
            get { return _ProgressBarSelector.CornersApply; }
            set { SetControlProperty("CornersApply", value); }
        }

        public string ShapeText {
            get { return _ProgressBarSelector.ShapeText; }
            set { SetControlProperty("ShapeText", value); }
        }

        public Font ShapeTextFont {
            get { return _ProgressBarSelector.ShapeTextFont; }
            set { SetControlProperty("ShapeTextFont", value); }
        }

        public ProgressBar.eRotateText ShapeTextRotate {
            get { return _ProgressBarSelector.ShapeTextRotate; }
            set { SetControlProperty("ShapeTextRotate", value); }
        }

        public ProgressBar.eBarStyle BarStyleFill {
            get { return _ProgressBarSelector.BarStyleFill; }
            set {
                SetControlProperty("BarStyleFill", value);
                _DesignerService.ShowUI(_ProgressBarSelector);
            }
        }

        public Color BarBackColor {
            get { return _ProgressBarSelector.BarBackColor; }
            set { SetControlProperty("BarBackColor", value); }
        }

        public Color BarColorSolid {
            get { return _ProgressBarSelector.BarColorSolid; }
            set {
                SetControlProperty("BarColorSolid", value);
                if (_ProgressBarSelector.BarStyleFill == ProgressBar.eBarStyle.Hatch) {
                    _DesignerService.Refresh(_ProgressBarSelector);
                }
            }
        }

        public Color BarColorSolidB {
            get { return _ProgressBarSelector.BarColorSolidB; }
            set {
                SetControlProperty("BarColorSolidB", value);
                if (_ProgressBarSelector.BarStyleFill == ProgressBar.eBarStyle.Hatch) {
                    _DesignerService.Refresh(_ProgressBarSelector);
                }
            }
        }

        [Editor(typeof(BlendTypeEditor), typeof(UITypeEditor))]
        public cBlendItems BarColorBlend {
            get { return _ProgressBarSelector.BarColorBlend; }
            set { SetControlProperty("BarColorBlend", value); }
        }

        public LinearGradientMode BarStyleLinear {
            get { return _ProgressBarSelector.BarStyleLinear; }
            set { SetControlProperty("BarStyleLinear", value); }
        }

        [Editor(typeof(HatchStyleEditor), typeof(UITypeEditor))]
        public HatchStyle BarStyleHatch {
            get { return _ProgressBarSelector.BarStyleHatch; }
            set { SetControlProperty("BarStyleHatch", value); }
        }

        public Image BarStyleTexture {
            get { return _ProgressBarSelector.BarStyleTexture; }
            set { SetControlProperty("BarStyleTexture", value); }
        }

        public WrapMode BarStyleWrapMode {
            get { return _ProgressBarSelector.BarStyleWrapMode; }
            set { SetControlProperty("BarStyleWrapMode", value); }
        }

        [Editor(typeof(FocalTypeEditor), typeof(UITypeEditor))]
        public cFocalPoints FocalPoints {
            get { return _ProgressBarSelector.FocalPoints; }
            set { SetControlProperty("FocalPoints", value); }
        }

        public bool Border {
            get { return _ProgressBarSelector.Border; }
            set {
                SetControlProperty("Border", value);
                _DesignerService.ShowUI(_ProgressBarSelector);
            }
        }

        public Color BorderColor {
            get { return _ProgressBarSelector.BorderColor; }
            set { SetControlProperty("BorderColor", value); }
        }

        public int BorderWidth {
            get { return _ProgressBarSelector.BorderWidth; }
            set { SetControlProperty("BorderWidth", value); }
        }

        public ProgressBar.eTextShow TextShow {
            get { return _ProgressBarSelector.TextShow; }
            set {
                SetControlProperty("TextShow", value);
                _DesignerService.Refresh(_ProgressBarSelector);
            }
        }

        public Color ForeColor {
            get { return _ProgressBarSelector.ForeColor; }
            set { SetControlProperty("ForeColor", value); }
        }

        public string TextValue {
            get { return _ProgressBarSelector.TextValue; }
            set { SetControlProperty("TextValue", value); }
        }

        public string TextFormat {
            get { return _ProgressBarSelector.TextFormat; }
            set { SetControlProperty("TextFormat", value); }
        }

        public ProgressBar.eTextPlacement TextPlacement {
            get { return _ProgressBarSelector.TextPlacement; }
            set { SetControlProperty("TextPlacement", value); }
        }

        public StringAlignment TextAlignment {
            get { return _ProgressBarSelector.TextAlignment; }
            set { SetControlProperty("TextAlignment", value); }
        }

        public StringAlignment TextAlignmentVert {
            get { return _ProgressBarSelector.TextAlignmentVert; }
            set { SetControlProperty("TextAlignmentVert", value); }
        }

        public bool TextWrap {
            get { return _ProgressBarSelector.TextWrap; }
            set { SetControlProperty("TextWrap", value); }
        }

        public ProgressBar.eRotateText TextRotate {
            get { return _ProgressBarSelector.TextRotate; }
            set { SetControlProperty("TextRotate", value); }
        }

        public bool TextShadow {
            get { return _ProgressBarSelector.TextShadow; }
            set {
                SetControlProperty("TextShadow", value);
                _DesignerService.ShowUI(_ProgressBarSelector);
            }
        }

        public Color TextShadowColor {
            get { return _ProgressBarSelector.TextShadowColor; }
            set { SetControlProperty("TextShadowColor", value); }
        }


        public ProgressBar CurrProgressBar {
            get { return _ProgressBarSelector; }
        }

        #endregion

        #region "Methods"


        public void AdjustCorners()
        {
            //Create a new Corners Dialog and update the controls on the form
            CornersDialog dlg = new CornersDialog();

            int maxcorner = 0;
            float ratio = 0;

            if (_ProgressBarSelector.Width > _ProgressBarSelector.Height) {
                dlg.TheShape.Height = Convert.ToInt32(dlg.TheShape.Width * (_ProgressBarSelector.Height / _ProgressBarSelector.Width));
                dlg.TheShape.Top = Convert.ToInt32((dlg.panShapeHolder.Height - dlg.TheShape.Height) / 2);
                maxcorner = Convert.ToInt32(((dlg.TheShape.Height / 2) - (_ProgressBarSelector.BorderWidth) * 2));
                ratio = Convert.ToSingle(dlg.TheShape.Height / _ProgressBarSelector.Height);
            } else {
                dlg.TheShape.Width = Convert.ToInt32(dlg.TheShape.Height * (_ProgressBarSelector.Width / _ProgressBarSelector.Height));
                dlg.TheShape.Left = Convert.ToInt32((dlg.panShapeHolder.Width - dlg.TheShape.Width) / 2);
                maxcorner = Convert.ToInt32(((dlg.TheShape.Width / 2) - (_ProgressBarSelector.BorderWidth) * 2));
                ratio = Convert.ToSingle(dlg.TheShape.Width / _ProgressBarSelector.Width);
            }

            // Set current Corners values
            dlg.tbarUpperLeft.Maximum = maxcorner;
            dlg.tbarUpperRight.Maximum = maxcorner;
            dlg.tbarLowerLeft.Maximum = maxcorner;
            dlg.tbarLowerRight.Maximum = maxcorner;
            dlg.tbarAll.Maximum = maxcorner;
            dlg.tbarUpperLeft.TickFrequency = Convert.ToInt32(maxcorner / 2);
            dlg.tbarUpperRight.TickFrequency = Convert.ToInt32(maxcorner / 2);
            dlg.tbarLowerLeft.TickFrequency = Convert.ToInt32(maxcorner / 2);
            dlg.tbarLowerRight.TickFrequency = Convert.ToInt32(maxcorner / 2);
            dlg.tbarAll.TickFrequency = Convert.ToInt32(maxcorner / 2);
            if (_ProgressBarSelector.Corners.All > -1) {
                dlg.tbarAll.Value = Convert.ToInt32(Math.Min((_ProgressBarSelector.Corners.UpperLeft * ratio), maxcorner));
            }
            dlg.tbarUpperLeft.Value = Convert.ToInt32(Math.Min((_ProgressBarSelector.Corners.UpperLeft * ratio), maxcorner));
            dlg.tbarUpperRight.Value = Convert.ToInt32(Math.Min((_ProgressBarSelector.Corners.UpperRight * ratio), maxcorner));
            dlg.tbarLowerLeft.Value = Convert.ToInt32(Math.Min((_ProgressBarSelector.Corners.LowerLeft * ratio), maxcorner));
            dlg.tbarLowerRight.Value = Convert.ToInt32(Math.Min((_ProgressBarSelector.Corners.LowerRight * ratio), maxcorner));

            dlg.TheShape.Shape = _ProgressBarSelector.Shape;
            dlg.TheShape.BarStyleFill = _ProgressBarSelector.BarStyleFill;
            dlg.TheShape.BarStyleLinear = _ProgressBarSelector.BarStyleLinear;
            dlg.TheShape.BarLength = _ProgressBarSelector.BarLength;
            dlg.TheShape.BarLengthValue = _ProgressBarSelector.BarLengthValue;
            dlg.TheShape.BarBackColor = _ProgressBarSelector.BarBackColor;
            dlg.TheShape.BarColorSolid = _ProgressBarSelector.BarColorSolid;
            dlg.TheShape.FillDirection = _ProgressBarSelector.FillDirection;
            dlg.TheShape.Orientation = _ProgressBarSelector.Orientation;
            dlg.TheShape.CornersApply = _ProgressBarSelector.CornersApply;
            dlg.TheShape.BarColorSolidB = _ProgressBarSelector.BarColorSolidB;
            dlg.TheShape.Border = _ProgressBarSelector.Border;
            dlg.TheShape.BorderWidth = _ProgressBarSelector.BorderWidth;
            dlg.TheShape.BorderColor = _ProgressBarSelector.BorderColor;
            dlg.TheShape.BarStyleHatch = _ProgressBarSelector.BarStyleHatch;
            dlg.TheShape.BarColorBlend = new cBlendItems(_ProgressBarSelector.BarColorBlend.iColor, _ProgressBarSelector.BarColorBlend.iPoint);
            dlg.TheShape.Corners = new CornersProperty(Convert.ToInt16(_ProgressBarSelector.Corners.LowerLeft * ratio), Convert.ToInt16(_ProgressBarSelector.Corners.LowerRight * ratio), Convert.ToInt16(_ProgressBarSelector.Corners.UpperLeft * ratio), Convert.ToInt16(_ProgressBarSelector.Corners.UpperRight * ratio));
            dlg.TheShape.FocalPoints = _ProgressBarSelector.FocalPoints;
            dlg.HSBarSample.Location = new Point(dlg.HSBarSample.Location.X, dlg.panShapeHolder.Location.Y + dlg.TheShape.Location.Y + dlg.TheShape.Height);

            // Update new Corners values if OK button was pressed
            if (dlg.ShowDialog() == DialogResult.OK) {
                IDesignerHost designerHost = (IDesignerHost)this.Component.Site.GetService(typeof(IDesignerHost));

                if (designerHost != null) {
                    DesignerTransaction t = designerHost.CreateTransaction();
                    try {
                        SetControlProperty("Corners", new CornersProperty(Convert.ToInt16(dlg.TheShape.Corners.LowerLeft / ratio), Convert.ToInt16(dlg.TheShape.Corners.LowerRight / ratio), Convert.ToInt16(dlg.TheShape.Corners.UpperLeft / ratio), Convert.ToInt16(dlg.TheShape.Corners.UpperRight / ratio)));
                        t.Commit();
                    } catch {
                        t.Cancel();
                    }
                }
            }
            _ProgressBarSelector.Refresh();

        }

        #endregion

        // Set a control property. This method makes Undo/Redo
        // work properly and marks the form as modified in the IDE.
        private void SetControlProperty(string property_name, object value)
        {
            TypeDescriptor.GetProperties(_ProgressBarSelector)[property_name].SetValue(_ProgressBarSelector, value);
        }

        #endregion

        // Return the smart tag action items.
        public override System.ComponentModel.Design.DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            items.Add(new DesignerActionHeaderItem("Behavior"));
            items.Add(new DesignerActionPropertyItem("BarType", "Bar Type", "Behavior", "Use Standard, CylonBar, or Glider Progress Bar"));
            if (_ProgressBarSelector.BarType == ProgressBar.eBarType.Bar) {
                items.Add(new DesignerActionPropertyItem("BarLength", "Bar Length Type", "Behavior", "Use Fixed or Full Length Progress Bar"));
                if (_ProgressBarSelector.BarLength == ProgressBar.eBarLength.Fixed) {
                    items.Add(new DesignerActionPropertyItem("BarLengthValue", "Bar Length Value", "Behavior", "Length of the Fixed Progress Bar"));
                }
                items.Add(new DesignerActionPropertyItem("FillDirection", "Progress Direction", "Behavior", "The ProgressBar of the Control"));

            } else {
                if (_ProgressBarSelector.BarType == ProgressBar.eBarType.CylonBar) {
                    items.Add(new DesignerActionPropertyItem("BarLengthValue", "Bar Length Value", "Behavior", "Length of the Fixed Progress Bar"));
                }
                items.Add(new DesignerActionPropertyItem("CylonRun", "Cylon On", "Behavior", "Start the Cylon Timer"));
                items.Add(new DesignerActionPropertyItem("CylonMove", "Move Distance", "Behavior", "How far the bar moves"));
                items.Add(new DesignerActionPropertyItem("CylonInterval", "Timer Tick", "Behavior", "How often the bar moves"));
            }

            items.Add(new DesignerActionPropertyItem("Orientation", "Bar Orientation", "Behavior", "The ProgressBar of the Control"));

            items.Add(new DesignerActionHeaderItem("Color and Fill"));

            items.Add(new DesignerActionPropertyItem("BarStyleFill", "Bar Fill Type", "Color and Fill", "Fill Solid, Gradient, Hatch, or Texture"));

            switch (_ProgressBarSelector.BarStyleFill) {
                case ProgressBar.eBarStyle.Solid:
                    items.Add(new DesignerActionPropertyItem("BarColorSolid", "Primary Solid Color", "Color and Fill", "The Primary Color for Solid Fill"));
                    items.Add(new DesignerActionPropertyItem("BarColorSolidB", "Secondary Solid Color", "Color and Fill", "The Secondary Color for Solid Fill"));

                    break;
                case ProgressBar.eBarStyle.Hatch:
                    items.Add(new DesignerActionPropertyItem("BarColorSolid", "Primary Solid Color", "Color and Fill", "The Primary Color for Hatch Fill"));
                    items.Add(new DesignerActionPropertyItem("BarColorSolidB", "Secondary Solid Color", "Color and Fill", "The Secondary Color for Hatch Fill"));
                    items.Add(new DesignerActionPropertyItem("BarStyleHatch", "Hatch Style", "Color and Fill", "The Hatch Style for Fill"));

                    break;
                case ProgressBar.eBarStyle.GradientLinear:
                    items.Add(new DesignerActionPropertyItem("BarColorBlend", "Blend Colors", "Color and Fill", "Color and Position Arrays for Color Blend"));
                    items.Add(new DesignerActionPropertyItem("BarStyleLinear", "Linear Style", "Color and Fill", "Color and Position Arrays for Color Blend"));

                    break;
                case ProgressBar.eBarStyle.GradientPath:
                    items.Add(new DesignerActionPropertyItem("BarColorBlend", "Blend Colors", "Color and Fill", "Color and Position Arrays for Color Blend"));
                    items.Add(new DesignerActionPropertyItem("FocalPoints", "FocalPoints", "Color and Fill", "The color of the ProgressBar's Border"));

                    break;
                case ProgressBar.eBarStyle.Texture:
                    items.Add(new DesignerActionPropertyItem("BarStyleTexture", "Texture Image", "Color and Fill", "The Image to fill with"));
                    items.Add(new DesignerActionPropertyItem("BarStyleWrapMode", "Texture Wrap Mode", "Color and Fill", "The Wrap Mode for texture fills"));

                    break;
            }
            items.Add(new DesignerActionPropertyItem("BarBackColor", "Background Color", "Color and Fill", "The ProgressBar of the Control"));

            items.Add(new DesignerActionHeaderItem("Border"));
            items.Add(new DesignerActionPropertyItem("Border", "Show Border", "Border", "The show or not show the border"));
            if (_ProgressBarSelector.Border) {
                items.Add(new DesignerActionPropertyItem("BorderColor", "Border Color", "Border", "The color of the ProgressBar's Border"));
                items.Add(new DesignerActionPropertyItem("BorderWidth", "Border Width", "Border", "The width of the ProgressBar's Border"));
            }

            items.Add(new DesignerActionHeaderItem("Shape"));
            items.Add(new DesignerActionPropertyItem("Shape", "Shape", "Shape", "The Shape of the ProgressBar"));

            switch (_ProgressBarSelector.Shape) {
                case ProgressBar.eShape.Rectangle:
                    items.Add(new DesignerActionPropertyItem("CornersApply", "Apply Corners", "Shape", "Apply the Corners to what parts of the ProgressBar"));
                    items.Add(new DesignerActionMethodItem(this, "AdjustCorners", "Adjust Corners ", "Shape", "Adjust Corners", true));
                    break;
                case ProgressBar.eShape.Text:
                    items.Add(new DesignerActionPropertyItem("ShapeTextFont", "Font for Shape", "Shape", "The Font for Shape of the ProgressBar"));
                    items.Add(new DesignerActionPropertyItem("ShapeText", "Text for Shape", "Shape", "The Text for Shape of the ProgressBar"));
                    items.Add(new DesignerActionPropertyItem("ShapeTextRotate", "Rotate Text", "Shape", "The Font for Shape of the ProgressBar"));
                    break;
            }

            items.Add(new DesignerActionHeaderItem("Text"));
            items.Add(new DesignerActionPropertyItem("TextShow", "Show Text", "Text", "The Show or not Show the Text on the ProgressBar"));

            if (_ProgressBarSelector.TextShow != ProgressBar.eTextShow.None) {
                items.Add(new DesignerActionPropertyItem("ForeColor", "Color for Text", "Text", "The Color for Text on the Bar"));

                if (_ProgressBarSelector.TextShow == ProgressBar.eTextShow.FormatStrText | _ProgressBarSelector.TextShow == ProgressBar.eTextShow.FormatStrTextPerc | _ProgressBarSelector.TextShow == ProgressBar.eTextShow.TextOnly) {
                    items.Add(new DesignerActionPropertyItem("TextValue", "Text for Shape", "Text", "The Text for Shape of the ProgressBar"));
                }

                if (_ProgressBarSelector.TextShow == ProgressBar.eTextShow.FormatStrPercent | _ProgressBarSelector.TextShow == ProgressBar.eTextShow.FormatStrText | _ProgressBarSelector.TextShow == ProgressBar.eTextShow.FormatStrTextPerc) {
                    items.Add(new DesignerActionPropertyItem("TextFormat", "Format String", "Text", "The Text for Shape of the ProgressBar"));

                }

                items.Add(new DesignerActionPropertyItem("TextPlacement", "Placement on Bar", "Text", "The Text for Shape of the ProgressBar"));
                items.Add(new DesignerActionPropertyItem("TextAlignment", "Horiz Alignment", "Text", "The Text for Shape of the ProgressBar"));
                items.Add(new DesignerActionPropertyItem("TextAlignmentVert", "Vert Alignment", "Text", "The Text for Shape of the ProgressBar"));
                items.Add(new DesignerActionPropertyItem("TextShadow", "Text Shadow", "Text", "The Text for Shape of the ProgressBar"));
                if (_ProgressBarSelector.TextShadow) {
                    items.Add(new DesignerActionPropertyItem("TextShadowColor", "Text Shadow", "Text", "The Text for Shape of the ProgressBar"));
                }
                items.Add(new DesignerActionPropertyItem("TextRotate", "Rotate", "Text", "The Text for Shape of the ProgressBar"));
                items.Add(new DesignerActionPropertyItem("TextWrap", "Wrap Text", "Text", "The Text for Shape of the ProgressBar"));

            }


            //items.Add(New DesignerActionHeaderItem("Information"))
            //Add Text Item - I gave it an empty Category to make
            //it appear at the end with no Header
            items.Add(new DesignerActionTextItem(Strings.Space(20) + "The ProgressBar Plus" + Constants.vbCr + Strings.Space(14) + "Original Creator: Scott Snyder" + Constants.vbCr + Strings.Space(16) + "Edited by Elijah Frederickson", ""));

            //Another Text item but with a header
            //Dim txt As String = "Width=" & _ProgressBarSelector.Width & _
            // " Height=" & _ProgressBarSelector.Height
            //items.Add( _
            //    New DesignerActionTextItem( _
            //        txt, "Information"))

            return items;
        }

    }

    #endregion

    #endregion


}
