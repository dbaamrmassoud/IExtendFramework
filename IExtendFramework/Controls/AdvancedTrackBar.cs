using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
namespace IExtendFramework.Controls
{

    /// <summary>
    /// Customizable TrackBar Control
    /// </summary>
    /// <remarks>v1.4</remarks>
    [ToolboxItem(true), ToolboxBitmap(typeof(AdvancedTrackBar), "Controls.bmp")]
    [DefaultEvent("ValueChanged")]
    public partial class AdvancedTrackBar
    {
        private Timer withEventsField_MouseTimer = new Timer();
        private Timer MouseTimer {
            get { return withEventsField_MouseTimer; }
            set {
                if (withEventsField_MouseTimer != null) {
                    withEventsField_MouseTimer.Tick -= MouseTimer_Tick;
                }
                withEventsField_MouseTimer = value;
                if (withEventsField_MouseTimer != null) {
                    withEventsField_MouseTimer.Tick += MouseTimer_Tick;
                }
            }
        }
        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler(object sender, System.EventArgs e);

        #region "Initiate"

        private eMouseState MouseState = eMouseState.Up;
        private bool IsOverSlider = false;
        private bool IsOverDownButton = false;
        private bool IsOverUpButton = false;
        private GraphicsPath gpSlider = new GraphicsPath();
        private int intSlideIndent = 13;
        private float sngSliderPos = 35;
        private Rectangle rectValueBox = new Rectangle(0, 0, 30, 20);
        private Rectangle rectSlider = new Rectangle(0, 0, 250, 21);
        private Rectangle rectDownButton = new Rectangle(0, 2, 15, 26);
        private Rectangle rectUpButton = new Rectangle(235, 2, 15, 26);
        private Rectangle rectLabel;

        private bool Init = true;

        private StringFormat sf = new StringFormat();

        public AdvancedTrackBar()
        {
            GotFocus += AdvancedTrackBar_LostFocus;
            LostFocus += AdvancedTrackBar_LostFocus;
            Resize += TBSlider_Resize;
            KeyUp += AdvancedTrackBar_KeyUp;
            MouseUp += TBSlider_MouseUp;
            MouseMove += TBSlider_MouseMove;
            MouseLeave += AdvancedTrackBar_MouseLeave;
            MouseDown += TBSlider_MouseDown;
            Load += TBSlider_Load;
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            rectLabel = new Rectangle(0, 0, this.Width, 20);
            CurrSliderHiLtColor = _ColorUpHiLt;
            CurrSliderBorderColor = _ColorUpBorder;
            CurrSliderColor = _ColorUp;
            
            // Add any initialization after the InitializeComponent() call.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void TBSlider_Load(object sender, System.EventArgs e)
        {
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            UpdateRects();
            Init = false;
        }

        #endregion

        #region "Enum"

        public enum eTickType
        {
            None,
            Up_Right,
            Down_Left,
            Both,
            Middle
        }

        public enum eMouseState
        {
            Up,
            Down
        }

        public enum eShape
        {
            Ellipse,
            Rectangle
        }

        public enum eValueBox
        {
            None,
            Left,
            Right
        }

        public enum eBrushStyle
        {
            Image,
            Linear,
            Linear2,
            Path
        }

        #endregion

        #region "Properties"

        #region "Hidden"

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool BorderStyle {
            //always false
            get { return false; }
            //empty
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Font Font {
            //always false
            get { return null; }
            //empty
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor {
            //always false
            get { return Color.Transparent; }
            //empty
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding {
            get { return _LabelPadding; }
            set { base.Padding = value; }
        }
        #endregion

        #region "Control"

        private int _Value;
        [Category("Appearance Controls")]
        [Description("Current Value for the Slider")]
        [Bindable(true)]
        public int Value {
            get { return _Value; }
            set {
                if (_Value != value) {
                    if (value < _MinValue) {
                        _Value = _MinValue;
                    } else {
                        if (value > _MaxValue) {
                            _Value = _MaxValue;
                        } else {
                            _Value = value;
                        }
                    }
                    UpdateRects();
                    this.Invalidate();
                    if (ValueChanged != null) {
                        ValueChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        private eBrushStyle _BrushStyle = eBrushStyle.Path;
        [Category("Appearance Slider")]
        [Description("Use a Linear or Path type Brush on the Slider")]
        public eBrushStyle BrushStyle {
            get { return _BrushStyle; }
            set {
                _BrushStyle = value;
                Invalidate();
            }
        }

        private LinearGradientMode _BrushDirection = LinearGradientMode.Horizontal;
        [Category("Appearance Slider")]
        [Description("The LinearGradientMode for the Linear Fill Type Brush")]
        public LinearGradientMode BrushDirection {
            get { return _BrushDirection; }
            set {
                _BrushDirection = value;
                Invalidate();
            }
        }

        private Orientation _Orientation = Orientation.Horizontal;
        [Category("Appearance Controls")]
        [Description("Horizontal or Vertical Orientation")]
        public Orientation Orientation {
            get { return _Orientation; }
            set {
                _Orientation = value;
                Orient = Convert.ToInt32((Convert.ToBoolean(value) ? 1 : -1));
                this.Size = new Size(this.Height, this.Width);
                this.SliderSize = new Size(_SliderSize.Height, _SliderSize.Width);
                UpdateRects();
                this.Invalidate();
            }
        }

        private int _MinValue = 0;
        [Category("Appearance Controls")]
        [Description("Minimum Value allowed for the Slider")]
        [RefreshProperties(RefreshProperties.All)]
        public int MinValue {
            get { return _MinValue; }
            set {
                if (!Init) {
                    if (value >= _MaxValue)
                        value = _MaxValue + 10;
                    if (_Value < value)
                        _Value = value;
                }
                _MinValue = value;
                UpdateRects();
                Invalidate();
            }
        }

        private int _MaxValue = 50;
        [Category("Appearance Controls")]
        [Description("Maximum Value allowed for the Slider")]
        [RefreshProperties(RefreshProperties.All)]
        public int MaxValue {
            get { return _MaxValue; }
            set {
                if (!Init) {
                    if (value <= _MinValue)
                        value = _MinValue - 10;
                    if (_Value > value)
                        _Value = value;
                }
                _MaxValue = value;
                //Try
                //    MouseTimer.Interval = CInt(100 - ((_MaxValue - _MinValue) / 10))

                //Catch ex As Exception
                //    MouseTimer.Interval = 10

                //End Try

                UpdateRects();
                Invalidate();
            }
        }

        private int _ChangeLarge = 10;
        [Category("Appearance Controls")]
        [Description("How far to adjust the value when clicking to the right or left of the slider or when the Arrow Keys are pressed while holding the Shift Key too.")]
        public int ChangeLarge {
            get { return _ChangeLarge; }
            set { _ChangeLarge = Math.Abs(value); }
        }

        private int _ChangeSmall = 1;
        [Category("Appearance Controls")]
        [Description("How far to adjust the value when clicking the Arrow buttons or when the Arrow Keys are pressed")]
        public int ChangeSmall {
            get { return _ChangeSmall; }
            set { _ChangeSmall = Math.Abs(value); }
        }

        private bool _BorderShow = false;
        [Category("Appearance Controls")]
        [Description("Show or not show the border around the control")]
        public bool BorderShow {
            get { return _BorderShow; }
            set {
                _BorderShow = value;
                this.Invalidate();
            }
        }

        private bool _ShowFocus = true;
        [Category("Appearance Controls")]
        [Description("Show or not show when the control has focus")]
        public bool ShowFocus {
            get { return _ShowFocus; }
            set { _ShowFocus = value; }
        }

        #endregion

        #region "FloatValue"

        private bool _FloatValue = true;
        [Category("Appearance FloatValue")]
        [Description("Show or not show the value above the slider while dragging it back and forth")]
        public bool FloatValue {
            get { return _FloatValue; }
            set { _FloatValue = value; }
        }

        private Font _FloatValueFont = new Font("Arial", 8, FontStyle.Bold);
        [Category("Appearance FloatValue")]
        [Description("Font to use for the value above the slider ")]
        public Font FloatValueFont {
            get { return _FloatValueFont; }
            set {
                _FloatValueFont = value;
                this.Invalidate();
            }
        }

        #endregion

        #region "Label"

        private string _Label;
        [Category("Appearance Label")]
        [Description("Text to appear as a label on the control")]
        public string Label {
            get { return _Label; }
            set {
                _Label = value;
                this.Invalidate();
            }
        }

        private Font _LabelFont = new Font("Arial", 12, FontStyle.Bold);
        [Category("Appearance Label")]
        [Description("Font to use for the Label Text")]
        public Font LabelFont {
            get { return _LabelFont; }
            set {
                _LabelFont = value;
                UpdateRects();
                this.Invalidate();
            }
        }

        private StringFormat _Labelsf = new StringFormat();
        private StringAlignment _LabelAlighnment = StringAlignment.Near;
        [Category("Appearance Label")]
        [Description("Alignment for the Label Text")]
        public StringAlignment LabelAlighnment {
            get { return _LabelAlighnment; }
            set {
                _LabelAlighnment = value;
                _Labelsf.Alignment = value;
                _Labelsf.Trimming = StringTrimming.EllipsisCharacter;
                this.Invalidate();
            }
        }

        private bool _LabelShow = false;
        [Category("Appearance Label")]
        [Description("Show or not show the Label Text")]
        public bool LabelShow {
            get { return _LabelShow; }
            set {
                _LabelShow = value;
                UpdateRects();

                this.Invalidate();
            }
        }

        private Padding _LabelPadding = new Padding(3);
        [Category("Appearance Label")]
        [Description("Pad the Label Text from the edge of the Control")]
        [DefaultValue("3, 3, 3, 3")]
        public Padding LabelPadding {
            get { return _LabelPadding; }
            set {
                _LabelPadding = value;
                this.Padding = value;
                UpdateRects();
                this.Invalidate();
            }
        }
        #endregion

        #region "Slider"

        private int _SliderWidthHigh = 1;
        [Category("Appearance Slider")]
        [Description("How wide to make the High side of the Slider Line")]
        public int SliderWidthHigh {
            get { return _SliderWidthHigh; }
            set {
                _SliderWidthHigh = value;
                this.Invalidate();
            }
        }

        private int _SliderWidthLow = 1;
        [Category("Appearance Slider")]
        [Description("How wide to make Low side of the Slider Line")]
        public int SliderWidthLow {
            get { return _SliderWidthLow; }
            set {
                _SliderWidthLow = value;
                this.Invalidate();
            }
        }

        private Bitmap _SliderImage = null;
        [Category("Appearance Slider")]
        [Description("Slider Image")]
        public Bitmap SliderImage {
            get { return _SliderImage; }
            set {
                _SliderImage = value;
                this.Invalidate();
            }
        }

        private LineCap _SliderCapStart = LineCap.Round;
        [Category("Appearance Slider")]
        [Description("Cap style to use for the start of the Slider Line")]
        public LineCap SliderCapStart {
            get { return _SliderCapStart; }
            set {
                _SliderCapStart = value;
                this.Invalidate();
            }
        }

        private LineCap _SliderCapEnd = LineCap.Round;
        [Category("Appearance Slider")]
        [Description("Cap style to use for the end of the Slider Line")]
        public LineCap SliderCapEnd {
            get { return _SliderCapEnd; }
            set {
                _SliderCapEnd = value;
                this.Invalidate();
            }
        }

        private Size _SliderSize = new Size(20, 10);
        [Category("Appearance Slider")]
        [Description("Size of the Slider")]
        public Size SliderSize {
            get { return _SliderSize; }
            set {
                _SliderSize = value;
                if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                    intSlideIndent = Convert.ToInt32(value.Width / 2) + 5;
                } else {
                    intSlideIndent = Convert.ToInt32(value.Height / 2) + 5;
                }
                UpdateRects();
                this.Invalidate();
            }
        }

        private eShape _SliderShape;
        [Category("Appearance Slider")]
        [Description("Shape for the Slider")]
        public eShape SliderShape {
            get { return _SliderShape; }
            set {
                _SliderShape = value;
                SetSliderPath();
                this.Invalidate();
            }
        }

        private PointF _SliderHighlightPt = new PointF(-5f, -2.5f);
        [Category("Appearance Slider")]
        [Description("Point on the Slider for the Highlight Color")]
        [TypeConverter(typeof(PointFConverter))]
        public PointF SliderHighlightPt {
            get { return _SliderHighlightPt; }
            set {
                _SliderHighlightPt = value;
                this.Invalidate();
            }
        }

        private PointF _SliderFocalPt = new PointF(0f, 0f);
        [Category("Appearance Slider")]
        [Description("Focus of the Center Point")]
        [TypeConverter(typeof(PointFConverter))]
        public PointF SliderFocalPt {
            get { return _SliderFocalPt; }
            set {
                _SliderFocalPt = value;
                this.Invalidate();
            }
        }

        private eTickType _TickType = eTickType.None;
        [Category("Appearance Slider")]
        [Description("Where to draw the Tick Marks")]
        public eTickType TickType {
            get { return _TickType; }
            set {
                _TickType = value;
                this.Invalidate();
            }
        }

        private int _TickInterval = 10;
        [Category("Appearance Slider")]
        [Description("The Interval between the Tick Marks")]
        public int TickInterval {
            get { return _TickInterval; }
            set {
                _TickInterval = value;
                this.Invalidate();
            }
        }

        private int _TickWidth = 5;
        [Category("Appearance Slider")]
        [Description("How long to draw the Tick Marks")]
        public int TickWidth {
            get { return _TickWidth; }
            set {
                _TickWidth = value;
                this.Invalidate();
            }
        }


        #endregion

        #region "ValueBox"

        private eValueBox _ValueBox = eValueBox.None;
        [Category("Appearance ValueBox")]
        [Description("Where to draw the Value Box")]
        public eValueBox ValueBox {
            get { return _ValueBox; }
            set {
                _ValueBox = value;
                SetSliderRect();
                this.Invalidate();
            }
        }

        private Size _ValueBoxSize = new Size(30, 20);
        [Category("Appearance ValueBox")]
        [Description("What size to draw the Value Box")]
        public Size ValueBoxSize {
            get { return _ValueBoxSize; }
            set {
                _ValueBoxSize = value;
                rectValueBox.Width = value.Width;
                rectValueBox.Height = value.Height;
                SetSliderRect();
                this.Invalidate();
            }
        }

        private Font _ValueBoxFont = new Font("Arial", 8.25F);
        [Category("Appearance ValueBox")]
        [Description("What font to use in the Value Box")]
        public Font ValueBoxFont {
            get { return _ValueBoxFont; }
            set {
                _ValueBoxFont = value;
                this.Invalidate();
            }
        }

        private eShape _ValueBoxShape = eShape.Rectangle;
        [Category("Appearance ValueBox")]
        [Description("What Shape to draw the Value Box")]
        public eShape ValueBoxShape {
            get { return _ValueBoxShape; }
            set {
                _ValueBoxShape = value;
                this.Invalidate();
            }
        }

        #endregion

        #region "UpDownButtons"

        private int _UpDownWidth = 30;
        [Category("Appearance UpDownButtons")]
        [Description("Width to draw the Up and Down Buttons if not set to Auto")]
        public int UpDownWidth {
            get { return _UpDownWidth; }
            set {
                if (value < 10)
                    value = 10;
                _UpDownWidth = value;
                SetUpDnButtonsRect();
                this.Invalidate();
            }
        }

        private bool _UpDownAutoWidth = true;
        [Category("Appearance UpDownButtons")]
        [Description("Auto Size the Buttons to the Control")]
        public bool UpDownAutoWidth {
            get { return _UpDownAutoWidth; }
            set {
                _UpDownAutoWidth = value;
                SetUpDnButtonsRect();
                this.Invalidate();
            }
        }

        private bool _UpDownShow = true;
        [Category("Appearance UpDownButtons")]
        [Description("Get or Set if the Up and Down buttons are shown")]
        public bool UpDownShow {
            get { return _UpDownShow; }
            set {
                _UpDownShow = value;
                SetSliderRect();
                this.Invalidate();
            }
        }

        #endregion

        #region "Colors"

        private Color _BorderColor = Color.Black;
        [Category("Appearance Controls")]
        [Description("The Color of the Border around the Control")]
        public Color BorderColor {
            get { return _BorderColor; }
            set {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        private Color _SliderColorLow = Color.Red;
        [Category("Appearance Slider")]
        [Description("The Color of the Slider Line on the Low Value Side")]
        public Color SliderColorLow {
            get { return _SliderColorLow; }
            set {
                _SliderColorLow = value;
                this.Invalidate();
            }
        }

        private Color _SliderColorHigh = Color.DarkGray;
        [Category("Appearance Slider")]
        [Description("The Color of the Slider Line on the High Value Side")]
        public Color SliderColorHigh {
            get { return _SliderColorHigh; }
            set {
                _SliderColorHigh = value;
                this.Invalidate();
            }
        }

        private Color _ColorUpBorder = Color.DarkBlue;
        [Category("Appearance Slider")]
        [Description("Color of the Slider Border when State is Up")]
        public Color ColorUpBorder {
            get { return _ColorUpBorder; }
            set {
                _ColorUpBorder = value;
                CurrSliderBorderColor = _ColorUpBorder;
                this.Invalidate();
            }
        }

        private Color _ColorDownBorder = Color.DarkSlateBlue;
        [Category("Appearance Slider")]
        [Description("Color of the Slider Border when State is Down")]
        public Color ColorDownBorder {
            get { return _ColorDownBorder; }
            set {
                _ColorDownBorder = value;
                this.Invalidate();
            }
        }

        private Color _ColorHoverBorder = Color.Blue;
        [Category("Appearance Slider")]
        [Description("Color of the Slider Border when State is Hovering")]
        public Color ColorHoverBorder {
            get { return _ColorHoverBorder; }
            set {
                _ColorHoverBorder = value;
                this.Invalidate();
            }
        }

        private Color _ColorUp = Color.MediumBlue;
        [Category("Appearance Slider")]
        [Description("Main Color of the Slider when State is Up")]
        public Color ColorUp {
            get { return _ColorUp; }
            set {
                _ColorUp = value;
                CurrSliderColor = _ColorUp;

                this.Invalidate();
            }
        }

        private Color _ColorDown = Color.CornflowerBlue;
        [Category("Appearance Slider")]
        [Description("Main Color of the Slider when State is Down")]
        public Color ColorDown {
            get { return _ColorDown; }
            set {
                _ColorDown = value;
                this.Invalidate();
            }
        }

        private Color _ColorHover = Color.RoyalBlue;
        [Category("Appearance Slider")]
        [Description("Main Color of the Slider when State is Hovering")]
        public Color ColorHover {
            get { return _ColorHover; }
            set {
                _ColorHover = value;
                this.Invalidate();
            }
        }

        private Color _ColorUpHiLt = Color.AliceBlue;
        [Category("Appearance Slider")]
        [Description("Highlight Color of the Slider when State is Up")]
        public Color ColorUpHiLt {
            get { return _ColorUpHiLt; }
            set {
                _ColorUpHiLt = value;
                CurrSliderHiLtColor = _ColorUpHiLt;
                this.Invalidate();
            }
        }

        private Color _ColorDownHiLt = Color.AliceBlue;
        [Category("Appearance Slider")]
        [Description("Highlight Color of the Slider when State is Down")]
        public Color ColorDownHiLt {
            get { return _ColorDownHiLt; }
            set {
                _ColorDownHiLt = value;
                this.Invalidate();
            }
        }

        private Color _ColorHoverHiLt = Color.White;
        [Category("Appearance Slider")]
        [Description("Highlight Color of the Slider when State is Hovering")]
        public Color ColorHoverHiLt {
            get { return _ColorHoverHiLt; }
            set {
                _ColorHoverHiLt = value;
                this.Invalidate();
            }
        }

        private Color _ArrowColorUp = Color.LightSteelBlue;
        [Category("Appearance UpDownButtons")]
        [Description("Color of the Button Arrow when the State is Up")]
        public Color ArrowColorUp {
            get { return _ArrowColorUp; }
            set {
                _ArrowColorUp = value;
                this.Invalidate();
            }
        }

        private Color _ArrowColorDown = Color.GhostWhite;
        [Category("Appearance UpDownButtons")]
        [Description("Color of the Button Arrow when the State is Down")]
        public Color ArrowColorDown {
            get { return _ArrowColorDown; }
            set {
                _ArrowColorDown = value;
                this.Invalidate();
            }
        }

        private Color _ArrowColorHover = Color.DarkBlue;
        [Category("Appearance UpDownButtons")]
        [Description("Color of the Button Arrow when the State is Hovering")]
        public Color ArrowColorHover {
            get { return _ArrowColorHover; }
            set {
                _ArrowColorHover = value;
                this.Invalidate();
            }
        }

        private Color _AButColorA = Color.CornflowerBlue;
        [Category("Appearance UpDownButtons")]
        [Description("Color of the Up Down Button")]
        public Color AButColorA {
            get { return _AButColorA; }
            set {
                _AButColorA = value;
                this.Invalidate();
            }
        }

        private Color _AButColorB = Color.Lavender;
        [Category("Appearance UpDownButtons")]
        [Description("HighLightColor of the Up Down Button")]
        public Color AButColorB {
            get { return _AButColorB; }
            set {
                _AButColorB = value;
                this.Invalidate();
            }
        }

        private Color _AButColorBorder = Color.SteelBlue;
        [Category("Appearance UpDownButtons")]
        [Description("Color of the Border for the Up Down Button")]
        public Color AButColorBorder {
            get { return _AButColorBorder; }
            set {
                _AButColorBorder = value;
                this.Invalidate();
            }
        }

        private Color _ValueBoxBackColor = Color.White;
        [Category("Appearance ValueBox")]
        [Description("Background Color for the Value Box")]
        public Color ValueBoxBackColor {
            get { return _ValueBoxBackColor; }
            set {
                _ValueBoxBackColor = value;
                this.Invalidate();
            }
        }

        private Color _ValueBoxBorder = Color.MediumBlue;
        [Category("Appearance ValueBox")]
        [Description("Color of the Border for the Value Box")]
        public Color ValueBoxBorder {
            get { return _ValueBoxBorder; }
            set {
                _ValueBoxBorder = value;
                this.Invalidate();
            }
        }

        private Color _ValueBoxFontColor = Color.MediumBlue;
        [Category("Appearance ValueBox")]
        [Description("Color of the Font for the Value Box")]
        public Color ValueBoxFontColor {
            get { return _ValueBoxFontColor; }
            set {
                _ValueBoxFontColor = value;
                this.Invalidate();
            }
        }

        private Color _LabelColor = Color.MediumBlue;
        [Category("Appearance Label")]
        [Description("Color of the Label Text")]
        public Color LabelColor {
            get { return _LabelColor; }
            set {
                _LabelColor = value;
                this.Invalidate();
            }
        }

        private Color _FloatValueFontColor = Color.MediumBlue;
        [Category("Appearance FloatValue")]
        [Description("Color of the Value floating above the Slider")]
        public Color FloatValueFontColor {
            get { return _FloatValueFontColor; }
            set {
                _FloatValueFontColor = value;
                this.Invalidate();
            }
        }

        private Color _TickColor = Color.DarkGray;
        [Category("Appearance Slider")]
        [Description("Color of the Tick Marks")]
        public Color TickColor {
            get { return _TickColor; }
            set {
                _TickColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #endregion

        #region "Painting"

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            //Setup the Graphics
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //Draw a Border around the control if requested
            if (_BorderShow) {
                g.DrawRectangle(new Pen(_BorderColor), 0, 0, this.Width - 1, this.Height - 1);
            }

            //Add the value increment buttons if requested
            if (_UpDownShow)
                DrawUpDnButtons(ref g);

            //Add the Line and Tick Marks
            DrawSliderLine(ref g);

            //Draw the Label Text if requested
            if (_LabelShow) {
                DrawLabel(ref g);
                //g.DrawRectangle(Pens.Gray, rectLabel)
            }

            //Add the Slider button
            DrawSlider(ref g);

            //Draw the Value above the Slider if requested
            if (_FloatValue && IsOverSlider && MouseState == eMouseState.Down) {
                DrawFloatValue(ref g);
            }

            //Draw the Box displating the value if requested
            if (!(_ValueBox == eValueBox.None)) {
                DrawValueBox(ref g);
            }

            //Draw Focus Rectangle around control if requested
            if (_ShowFocus && this.Focused) {
                ControlPaint.DrawFocusRectangle(g, new Rectangle(2 + Convert.ToInt32(!_BorderShow), 2 + Convert.ToInt32(!_BorderShow), this.Width - ((2 + Convert.ToInt32(!_BorderShow)) * 2), this.Height - ((2 + Convert.ToInt32(!_BorderShow)) * 2)), Color.Black, this.BackColor);
            }

        }

        private void DrawLabel(ref Graphics g)
        {
            if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                _Labelsf.FormatFlags = StringFormatFlags.NoWrap;
            } else {
                _Labelsf.FormatFlags = StringFormatFlags.DirectionVertical;
            }
            g.DrawString(_Label, _LabelFont, new SolidBrush(_LabelColor), rectLabel, _Labelsf);
        }

        private void DrawSlider(ref Graphics g)
        {
            switch (_BrushStyle) {
                case eBrushStyle.Linear:

                    using (LinearGradientBrush br = new LinearGradientBrush(gpSlider.GetBounds(), CurrSliderHiLtColor, CurrSliderColor, _BrushDirection)) {

                        g.FillPath(br, gpSlider);

                    }


                    break;
                case eBrushStyle.Linear2:
                    ColorBlend blend = new ColorBlend();
                    Color[] bColors = new Color[] {
                        CurrSliderColor,
                        CurrSliderColor,
                        CurrSliderHiLtColor,
                        CurrSliderColor,
                        CurrSliderColor
                    };
                    blend.Colors = bColors;

                    float[] bPts = new float[] {
                        0,
                        _SliderFocalPt.X,
                        0.5f,
                        _SliderFocalPt.Y,
                        1
                    };
                    blend.Positions = bPts;

                    using (LinearGradientBrush br = new LinearGradientBrush(gpSlider.GetBounds(), CurrSliderColor, CurrSliderHiLtColor, _BrushDirection)) {
                        br.InterpolationColors = blend;
                        g.FillPath(br, gpSlider);

                    }


                    break;
                case eBrushStyle.Path:

                    using (PathGradientBrush br = new PathGradientBrush(gpSlider)) {
                        br.SurroundColors = new Color[] { CurrSliderColor };
                        br.CenterColor = CurrSliderHiLtColor;
                        br.CenterPoint = new PointF(br.CenterPoint.X + SliderHighlightPt.X, br.CenterPoint.Y + SliderHighlightPt.Y);
                        br.FocusScales = _SliderFocalPt;
                        g.FillPath(br, gpSlider);
                    }


                    break;
                case eBrushStyle.Image:

                    break;

            }

            if (_BrushStyle == eBrushStyle.Image) {
                if (_SliderImage == null) {
                    g.DrawRectangle(new Pen(CurrSliderBorderColor), Rectangle.Round(gpSlider.GetBounds()));
                } else {
                    g.DrawImage(_SliderImage, gpSlider.GetBounds());
                }
            } else {
                g.DrawPath(new Pen(CurrSliderBorderColor), gpSlider);
            }

        }

        private void DrawFloatValue(ref Graphics g)
        {
            SizeF sz = g.MeasureString(_Value.ToString(), _FloatValueFont, new PointF(0, 0), StringFormat.GenericDefault);
            Rectangle rect = default(Rectangle);
            PathGradientBrush pbr = null;
            GraphicsPath gp = new GraphicsPath();
            if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                rect = new Rectangle(Convert.ToInt32(sngSliderPos - (sz.Width / 2)), Convert.ToInt32((rectSlider.Height / 2) + rectSlider.Y - (_SliderSize.Height / 2) - 1 - sz.Height), Convert.ToInt32(sz.Width) + 1, Convert.ToInt32(sz.Height));
            } else {
                rect = new Rectangle(Convert.ToInt32((rectSlider.Width / 2) - (sz.Width / 2)), Convert.ToInt32(sngSliderPos - sz.Height - (_SliderSize.Height / 2) - 3), Convert.ToInt32(sz.Width + 1), Convert.ToInt32(sz.Height + 2));
            }
            gp.AddRectangle(rect);
            pbr = new PathGradientBrush(gp);
            pbr.SurroundColors = new Color[] { Color.Transparent };
            if (this.BackColor == Color.Transparent) {
                pbr.CenterColor = this.Parent.BackColor;
            } else {
                pbr.CenterColor = this.BackColor;
            }
            g.FillRectangle(pbr, rect);
            rect.Y += 2;
            g.DrawString(_Value.ToString(), _FloatValueFont, new SolidBrush(_FloatValueFontColor), rect, sf);
            pbr.Dispose();
            gp.Dispose();
        }

        private void DrawValueBox(ref Graphics g)
        {
            using (Brush bbr = new SolidBrush(_ValueBoxBackColor)) {
                using (Pen pn = new Pen(_ValueBoxBorder)) {
                    using (Brush fbr = new SolidBrush(_ValueBoxFontColor)) {
                        Rectangle rect = new Rectangle(rectValueBox.X, rectValueBox.Y, rectValueBox.Width, rectValueBox.Height);
                        if (ValueBoxShape == eShape.Rectangle) {
                            g.FillRectangle(bbr, rect);
                            g.DrawRectangle(pn, rect.X, rect.Y, rect.Width, rect.Height);
                        } else {
                            g.FillEllipse(bbr, rect);
                            g.DrawEllipse(pn, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        g.DrawString(_Value.ToString(), _ValueBoxFont, fbr, new Rectangle(rect.X, rect.Y + 1, rect.Width + 1, rect.Height + 1), sf);
                    }
                }
            }

        }

        private void DrawUpDnButtons(ref Graphics g)
        {
            using (Pen pn = new Pen(_ArrowColorUp, 2)) {
                pn.EndCap = LineCap.Round;
                pn.StartCap = LineCap.Round;
                pn.LineJoin = LineJoin.Round;
                GraphicsPath gp = new GraphicsPath();
                Point[] pts = null;
                Matrix mx = new Matrix();
                pts = new Point[] {
                    new Point(5, 0),
                    new Point(0, 5),
                    new Point(5, 10)
                };
                gp.AddLines(pts);


                if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                    if (IsOverDownButton) {
                        g.FillRectangle(new LinearGradientBrush(rectDownButton, _AButColorB, _AButColorA, LinearGradientMode.Horizontal), rectDownButton);
                        if (MouseState == eMouseState.Down) {
                            pn.Color = _ArrowColorDown;
                        } else {
                            pn.Color = _ArrowColorHover;
                        }
                        g.DrawRectangle(new Pen(_AButColorBorder), new Rectangle(rectDownButton.X + 1, rectDownButton.Y, rectDownButton.Width - 1, rectDownButton.Height - 1));
                    }
                    var _with1 = rectDownButton;
                    mx.Translate(5, Convert.ToSingle((rectDownButton.Y + (rectDownButton.Height / 2)) - 6));
                    gp.Transform(mx);
                    g.DrawPath(pn, gp);

                    pn.Color = _ArrowColorUp;
                    if (IsOverUpButton) {
                        g.FillRectangle(new LinearGradientBrush(rectUpButton, _AButColorA, _AButColorB, LinearGradientMode.Horizontal), rectUpButton);
                        if (MouseState == eMouseState.Down) {
                            pn.Color = _ArrowColorDown;
                        } else {
                            pn.Color = _ArrowColorHover;
                        }
                        g.DrawRectangle(new Pen(_AButColorBorder), new Rectangle(rectUpButton.X, rectUpButton.Y, rectUpButton.Width - 1, rectUpButton.Height - 1));
                    }
                    var _with2 = rectUpButton;
                    mx = new Matrix(-1, 0, 0, 1, 5, 0);
                    mx.Translate(_with2.X + 9, 0, MatrixOrder.Append);
                    gp.Transform(mx);
                    g.DrawPath(pn, gp);
                } else {
                    if (IsOverDownButton) {
                        g.FillRectangle(new LinearGradientBrush(rectDownButton, _AButColorB, _AButColorA, LinearGradientMode.Vertical), rectDownButton);
                        g.DrawRectangle(new Pen(_AButColorBorder), new Rectangle(rectDownButton.X, rectDownButton.Y, rectDownButton.Width - 1, rectDownButton.Height - 1));
                        if (MouseState == eMouseState.Down) {
                            pn.Color = _ArrowColorDown;
                        } else {
                            pn.Color = _ArrowColorHover;
                        }
                    }
                    var _with3 = rectDownButton;
                    mx.RotateAt(90, new PointF(gp.GetBounds().Width / 2, gp.GetBounds().Height / 2));
                    mx.Translate(Convert.ToSingle((rectDownButton.X + (rectDownButton.Width / 2)) - 3), 4, MatrixOrder.Append);
                    gp.Transform(mx);
                    g.DrawPath(pn, gp);

                    pn.Color = _ArrowColorUp;
                    if (IsOverUpButton) {
                        g.FillRectangle(new LinearGradientBrush(rectUpButton, _AButColorA, _AButColorB, LinearGradientMode.Vertical), rectUpButton);
                        g.DrawRectangle(new Pen(_AButColorBorder), new Rectangle(rectUpButton.X, rectUpButton.Y, rectUpButton.Width - 1, rectUpButton.Height - 1));
                        if (MouseState == eMouseState.Down) {
                            pn.Color = _ArrowColorDown;
                        } else {
                            pn.Color = _ArrowColorHover;
                        }
                    }
                    var _with4 = rectUpButton;
                    mx = new Matrix(1, 0, 0, -1, 0, 10);
                    mx.Translate(0, _with4.Y + 6, MatrixOrder.Append);
                    gp.Transform(mx);
                    g.DrawPath(pn, gp);
                }
                mx.Dispose();
                gp.Dispose();
            }

        }

        private void DrawSliderLine(ref Graphics g)
        {
            using (Pen pn = new Pen(_SliderColorLow, _SliderWidthLow)) {
                using (Pen tpn = new Pen(_TickColor)) {
                    int @switch = Convert.ToInt32((_Orientation == System.Windows.Forms.Orientation.Horizontal ? 1 : -1));
                    int _SliderWidth = Math.Max(_SliderWidthLow, _SliderWidthHigh);
                    float t1 = 0;
                    float t2 = 0;
                    int lAdj = 0;

                    switch (_TickType) {
                        case eTickType.Middle:
                            t1 = Convert.ToSingle(-_TickWidth / 2);
                            t2 = Convert.ToSingle(_TickWidth / 2);

                            break;
                        case eTickType.Up_Right:
                            t1 = (-5 - _TickWidth - _SliderWidth) * @switch;
                            t2 = (-5 - _SliderWidth) * @switch;

                            break;
                        case eTickType.Down_Left:
                        case eTickType.Both:
                            t1 = (5 + _TickWidth + _SliderWidth) * @switch;
                            t2 = (5 + _SliderWidth) * @switch;

                            break;
                    }

                    if (_LabelShow) {
                        lAdj += rectLabel.Height + _LabelPadding.Top;
                    }

                    int Tickpos = 0;
                    if (Orientation == System.Windows.Forms.Orientation.Horizontal) {
                        pn.StartCap = _SliderCapStart;
                        if (_Value == _MaxValue) {
                            pn.EndCap = _SliderCapEnd;
                        } else {
                            pn.EndCap = LineCap.Flat;
                        }

                        if (_TickType != eTickType.None) {
                            for (int i = 0; i <= _MaxValue; i += _TickInterval) {
                                Tickpos = Convert.ToInt32(rectSlider.X + (rectSlider.Width * ((i - _MinValue) / (_MaxValue - _MinValue))));
                                g.DrawLine(tpn, Tickpos, Convert.ToSingle(rectSlider.Height / 2) + t1 + lAdj, Tickpos, Convert.ToSingle(rectSlider.Height / 2) + t2 + lAdj);
                                if (_TickType == eTickType.Both) {
                                    g.DrawLine(tpn, Tickpos, Convert.ToSingle(rectSlider.Height / 2) - t1 + lAdj, Tickpos, Convert.ToSingle(rectSlider.Height / 2) - t2 + lAdj);
                                }
                            }
                        }

                        g.DrawLine(pn, Convert.ToSingle(rectSlider.X), Convert.ToSingle(rectSlider.Height / 2) + rectSlider.Y, sngSliderPos, Convert.ToSingle(rectSlider.Height / 2) + rectSlider.Y);
                        if (_Value == _MinValue) {
                            pn.StartCap = _SliderCapStart;
                        } else {
                            pn.StartCap = LineCap.Flat;
                        }
                        pn.EndCap = _SliderCapEnd;
                        pn.Color = _SliderColorHigh;
                        pn.Width = _SliderWidthHigh;
                        g.DrawLine(pn, sngSliderPos, Convert.ToSingle(rectSlider.Height / 2 + rectSlider.Y), Convert.ToSingle(rectSlider.X + rectSlider.Width), Convert.ToSingle(rectSlider.Height / 2) + rectSlider.Y);

                    } else {
                        pn.StartCap = _SliderCapEnd;
                        if (_Value == _MaxValue) {
                            pn.EndCap = _SliderCapEnd;
                        } else {
                            pn.EndCap = LineCap.Flat;
                        }

                        if (_TickType != eTickType.None) {
                            for (int i = 0; i <= _MaxValue; i += _TickInterval) {
                                Tickpos = Convert.ToInt32(rectSlider.Y + (rectSlider.Height * ((i - _MinValue) / (_MaxValue - _MinValue))));
                                g.DrawLine(tpn, Convert.ToSingle(rectSlider.Width / 2) + t1, Tickpos, Convert.ToSingle(rectSlider.Width / 2) + t2, Tickpos);
                                if (_TickType == eTickType.Both) {
                                    g.DrawLine(tpn, Convert.ToSingle(rectSlider.Width / 2) - t1, Tickpos, Convert.ToSingle(rectSlider.Width / 2) - t2, Tickpos);
                                }
                            }
                        }

                        pn.Color = _SliderColorHigh;
                        pn.Width = _SliderWidthHigh;
                        g.DrawLine(pn, Convert.ToSingle(rectSlider.Width / 2), Convert.ToSingle(rectSlider.Y), Convert.ToSingle(rectSlider.Width / 2), sngSliderPos);
                        if (_Value == _MinValue) {
                            pn.StartCap = _SliderCapStart;
                        } else {
                            pn.StartCap = LineCap.Flat;
                        }
                        pn.EndCap = _SliderCapStart;
                        pn.Color = _SliderColorLow;
                        pn.Width = _SliderWidthLow;
                        g.DrawLine(pn, Convert.ToSingle(rectSlider.Width / 2), sngSliderPos, Convert.ToSingle(rectSlider.Width / 2), Convert.ToSingle(rectSlider.Y + rectSlider.Height));
                    }
                }
            }

        }

        #endregion

        #region "Building"

        private void SetSliderPath()
        {
            gpSlider.Reset();
            RectangleF rect = default(RectangleF);
            if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                rect = new RectangleF(Convert.ToSingle(sngSliderPos - (_SliderSize.Width / 2)), Convert.ToSingle(rectSlider.Y + (rectSlider.Height / 2) - (_SliderSize.Height) / 2), _SliderSize.Width, _SliderSize.Height);
            } else {
                rect = new RectangleF(Convert.ToSingle((rectSlider.Width - _SliderSize.Width) / 2), Convert.ToSingle(sngSliderPos - (_SliderSize.Height / 2)), _SliderSize.Width, _SliderSize.Height);
            }
            if (_SliderShape == eShape.Rectangle) {
                gpSlider.AddRectangle(rect);
            } else {
                gpSlider.AddEllipse(rect);
            }
            InvRect = Rectangle.Round(gpSlider.GetBounds());
            InvRect.Inflate(2, 2);
        }

        private void UpdateSlider(int xPos)
        {
            RectangleF rect = gpSlider.GetBounds();
            rect.Inflate(20, 20);
            rect.Offset(-10, -10);
            this.Invalidate(Rectangle.Round(rect));
            sngSliderPos = xPos;
            if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                if (sngSliderPos - rectSlider.X < 0)
                    sngSliderPos = rectSlider.X;
                if (sngSliderPos > rectSlider.X + rectSlider.Width)
                    sngSliderPos = rectSlider.X + rectSlider.Width;
            } else {
                if (sngSliderPos - rectSlider.Y < 0)
                    sngSliderPos = rectSlider.Y;
                if (sngSliderPos > rectSlider.Y + rectSlider.Height)
                    sngSliderPos = rectSlider.Y + rectSlider.Height;
            }
            SetSliderPath();
            this.Invalidate(Rectangle.Round(rect));
        }

        private void SetUpDnButtonsRect()
        {
            int UDWidth = 0;
            int UDY = 0;

            if (Orientation == System.Windows.Forms.Orientation.Horizontal) {
                if (_UpDownAutoWidth) {
                    UDWidth = rectSlider.Height - 4;
                    UDY = 3;
                } else {
                    UDWidth = _UpDownWidth;
                    UDY = Convert.ToInt32((rectSlider.Height - UDWidth) / 2);
                }

                if (_LabelShow)
                    UDY += rectLabel.Height + _LabelPadding.Top + _LabelPadding.Bottom;

                rectDownButton = new Rectangle(1, UDY, 15, UDWidth);
                rectUpButton = new Rectangle(this.Width - 17, UDY, 15, UDWidth);
            } else {
                if (_UpDownAutoWidth) {
                    UDWidth = rectSlider.Width - 4;
                    UDY = 2;
                } else {
                    UDWidth = _UpDownWidth;
                    UDY = Convert.ToInt32((rectSlider.Width - UDWidth) / 2);
                }

                rectDownButton = new Rectangle(UDY, 2, UDWidth, 15);
                rectUpButton = new Rectangle(UDY, this.Height - 17, UDWidth, 15);
            }
        }

        private void SetLabelRect()
        {
            if (Orientation == System.Windows.Forms.Orientation.Horizontal) {
                rectLabel = new Rectangle(_LabelPadding.Left, _LabelPadding.Top, this.Width - _LabelPadding.Horizontal - 1, this.LabelFont.Height);
            } else {
                rectLabel = new Rectangle(this.Width - this.LabelFont.Height - _LabelPadding.Top, _LabelPadding.Left, this.LabelFont.Height, this.Height - _LabelPadding.Horizontal - 1);
            }
        }

        private void SetSliderRect()
        {
            try {
                int ButtonOffset = 17;
                if (!_UpDownShow)
                    ButtonOffset = 0;
                var _with5 = rectSlider;
                if (Orientation == System.Windows.Forms.Orientation.Horizontal) {
                    int _SliderWidth = Math.Max(_SliderWidthLow, _SliderWidthHigh);

                    switch (_ValueBox) {
                        case eValueBox.None:
                            _with5.X = ButtonOffset + intSlideIndent;
                            _with5.Width = this.Width - ((ButtonOffset * 2) + 1) - (intSlideIndent * 2);
                            break;
                        case eValueBox.Left:
                            rectValueBox.X = ButtonOffset + 1;
                            rectValueBox.Y = Convert.ToInt32(((rectSlider.Height - rectValueBox.Height) / 2));
                            _with5.Width = Convert.ToInt32(this.Width - ((ButtonOffset * 2) + 1) - rectValueBox.Width - (intSlideIndent * 2) - (_SliderWidth / 2));
                            _with5.X = Convert.ToInt32(rectValueBox.Width + ButtonOffset + intSlideIndent + (_SliderWidth / 2));
                            break;
                        case eValueBox.Right:
                            rectValueBox.X = this.Width - ButtonOffset - 2 - rectValueBox.Width;
                            rectValueBox.Y = Convert.ToInt32(((rectSlider.Height - rectValueBox.Height) / 2));
                            _with5.Width = Convert.ToInt32(this.Width - ((ButtonOffset * 2) + 1) - rectValueBox.Width - (intSlideIndent * 2) - (_SliderWidth / 2));
                            _with5.X = ButtonOffset + intSlideIndent;
                            break;
                    }
                    if (_LabelShow) {
                        _with5.Y = rectLabel.Height + _LabelPadding.Top + _LabelPadding.Bottom;
                        _with5.Height = this.Height - rectLabel.Height - _LabelPadding.Top;
                        rectValueBox.Y += rectLabel.Height + _LabelPadding.Top + _LabelPadding.Bottom;
                    } else {
                        _with5.Y = 0;
                        _with5.Height = this.Height - 1;
                    }
                    UpdateSlider(Convert.ToInt32(rectSlider.X + (rectSlider.Width * ((_Value - _MinValue) / (_MaxValue - _MinValue)))));

                } else {
                    switch (_ValueBox) {
                        case eValueBox.None:
                            _with5.Y = ButtonOffset + intSlideIndent;
                            _with5.Height = this.Height - ((ButtonOffset * 2) + 1) - (intSlideIndent * 2);
                            break;
                        case eValueBox.Left:
                            rectValueBox.X = Convert.ToInt32(((rectSlider.Width - rectValueBox.Width) / 2));
                            rectValueBox.Y = ButtonOffset + 1;
                            _with5.Height = Convert.ToInt32(this.Height - ((ButtonOffset * 2) + 1) - rectValueBox.Height - (intSlideIndent * 2));
                            _with5.Y = Convert.ToInt32(rectValueBox.Height + ButtonOffset + intSlideIndent);
                            break;
                        case eValueBox.Right:
                            rectValueBox.X = Convert.ToInt32(((rectSlider.Width - rectValueBox.Width) / 2));
                            rectValueBox.Y = this.Height - ButtonOffset - 2 - rectValueBox.Height;
                            _with5.Height = Convert.ToInt32(this.Height - ((ButtonOffset * 2) + 1) - rectValueBox.Height - (intSlideIndent * 2));
                            _with5.Y = ButtonOffset + intSlideIndent;
                            break;
                    }
                    if (_LabelShow) {
                        _with5.X = 0;
                        _with5.Width = this.Width - rectLabel.Width - _LabelPadding.Top;
                    } else {
                        _with5.X = 0;
                        _with5.Width = this.Width - 1;
                    }
                    int adj = 0;
                    if (_MinValue < 0)
                        adj = Math.Abs(_MinValue);

                    UpdateSlider(Convert.ToInt32(rectSlider.Y + (rectSlider.Height * (((_MaxValue + adj) - _Value - adj - (_MinValue + adj)) / ((_MaxValue + adj) - (_MinValue + adj))))));

                }

            } catch (Exception) {
            }
        }

        private void UpdateRects()
        {
            SetLabelRect();
            SetSliderRect();
            SetSliderPath();
            SetUpDnButtonsRect();
        }

        #endregion

        #region "Mouse"

        private Rectangle InvRect;
        private Color CurrSliderColor;
        private Color CurrSliderBorderColor;
        private Color CurrSliderHiLtColor;
        private int Orient = 1;
        private int MouseHoldDownTicker = 0;

        private int MouseHoldDownChange = 0;
        private void TBSlider_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseState = eMouseState.Down;
            MouseHoldDownTicker = 0;
            MouseTimer.Interval = 100;

            if (_UpDownShow) {
                if (IsOverDownButton) {
                    MouseHoldDownChange = Orient * _ChangeSmall;
                    this.Value += MouseHoldDownChange;
                    MouseTimer.Start();
                }
                if (IsOverUpButton) {
                    MouseHoldDownChange = -(Orient * _ChangeSmall);
                    this.Value += MouseHoldDownChange;
                    MouseTimer.Start();
                }
            }
            IsOverSlider = gpSlider.IsVisible(e.X, e.Y);
            int pos = 0;
            if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                pos = e.X;
            } else {
                pos = e.Y;
            }
            if (IsOverSlider) {
                CurrSliderColor = _ColorDown;
                CurrSliderBorderColor = _ColorDownBorder;
                CurrSliderHiLtColor = _ColorDownHiLt;
            } else if (rectSlider.Contains(e.Location)) {
                if (pos < sngSliderPos) {
                    MouseHoldDownChange = _ChangeLarge * Orient;
                    this.Value += MouseHoldDownChange;
                } else {
                    MouseHoldDownChange = -(_ChangeLarge * Orient);
                    this.Value += MouseHoldDownChange;
                }
                MouseTimer.Start();
            }
            this.Invalidate();
        }

        private void AdvancedTrackBar_MouseLeave(object sender, System.EventArgs e)
        {
            IsOverDownButton = false;
            IsOverUpButton = false;
            CurrSliderColor = _ColorUp;
            CurrSliderBorderColor = _ColorUpBorder;
            CurrSliderHiLtColor = _ColorUpHiLt;
            this.Invalidate();
        }

        private void TBSlider_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!IsOverSlider) {
                IsOverDownButton = rectDownButton.Contains(e.Location);
                IsOverUpButton = rectUpButton.Contains(e.Location);
            }
            Rectangle rect = rectDownButton;
            rect.Inflate(1, 1);
            Invalidate(rect);
            rect = rectUpButton;
            rect.Inflate(1, 1);
            Invalidate(rect);

            if (MouseState == eMouseState.Up)
                IsOverSlider = gpSlider.IsVisible(e.X, e.Y);

            if (IsOverSlider & MouseState == eMouseState.Down) {
                if (_Orientation == System.Windows.Forms.Orientation.Horizontal) {
                    this.Value = (int)(((sngSliderPos - rectSlider.X) / (rectSlider.Width / (_MaxValue - _MinValue))) + _MinValue);
                    UpdateSlider(e.X);
                } else {
                    int adj = 0;
                    if (_MinValue < 0)
                        adj = Math.Abs(_MinValue);
                    this.Value = ((_MaxValue + adj) - Convert.ToInt32(((sngSliderPos - rectSlider.Y) / (rectSlider.Height / ((_MaxValue + adj) - (_MinValue + adj)))) + (_MinValue + adj))) - adj;
                    UpdateSlider(e.Y);
                }

            } else if (IsOverSlider & MouseState == eMouseState.Up) {
                CurrSliderColor = _ColorHover;
                CurrSliderBorderColor = _ColorHoverBorder;
                CurrSliderHiLtColor = _ColorHoverHiLt;
                this.Invalidate(InvRect);

            } else {
                CurrSliderColor = _ColorUp;
                CurrSliderBorderColor = _ColorUpBorder;
                CurrSliderHiLtColor = _ColorUpHiLt;
                this.Invalidate(InvRect);
            }
            this.Update();
        }

        private void TBSlider_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseTimer.Stop();
            MouseState = eMouseState.Up;
            IsOverDownButton = rectDownButton.Contains(e.Location);
            IsOverUpButton = rectUpButton.Contains(e.Location);
            this.Invalidate();
        }

        #endregion

        #region "KeyDown"

        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            //Because a Usercontrol ignores the arrows in the KeyDown Event
            //and changes focus no matter what in the KeyUp Event
            //This is needed to fix the KeyDown problem
            switch (keyData & Keys.KeyCode) {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }


        private void AdvancedTrackBar_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int adjust = _ChangeSmall;
            if (e.Shift) {
                adjust = _ChangeLarge;
            }

            switch ((Keys) e.KeyValue) {
                case Keys.Up:
                case Keys.Right:
                    this.Value += adjust;

                    break;
                case Keys.Down:
                case Keys.Left:
                    this.Value -= adjust;
                    break;
            }
        }

        #endregion

        #region "Resize"

        private void TBSlider_Resize(object sender, System.EventArgs e)
        {
            UpdateRects();
            this.Refresh();
        }

        #endregion

        #region "Focus"

        private void AdvancedTrackBar_LostFocus(object sender, System.EventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region "Mouse Hold Down Timer"

        private void MouseTimer_Tick(object sender, System.EventArgs e)
        {
            //Check if mouse was just clicked
            if (MouseHoldDownTicker < 5) {
                MouseHoldDownTicker += 1;
                //Interval was set to 100 on MouseDown
                //Tick off 5 times and then reset the Timer Interval
                //  based on the Min/Max span
                if (MouseHoldDownTicker == 5) {
                    MouseTimer.Interval = Convert.ToInt32(Math.Max(10, 100 - ((_MaxValue - _MinValue) / 10)));
                }
            } else {
                //Change the value until the mouse is released
                this.Value += MouseHoldDownChange;
            }
        }

        #endregion

    }
}
namespace IExtendFramework.Controls
{

    #region "PointFConverter"

    internal class PointFConverter : ExpandableObjectConverter
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
                    string[] ConverterParts = new string[3];
                    ConverterParts = Strings.Split(s, ",");
                    if ((ConverterParts != null)) {
                        if ((ConverterParts[0] == null))
                            ConverterParts[0] = "-5";
                        if ((ConverterParts[1] == null))
                            ConverterParts[1] = "-2.5";
                        return new PointF(Convert.ToSingle(ConverterParts[0].Trim()), Convert.ToSingle(ConverterParts[1].Trim()));
                    }
                } catch (Exception){
                    throw new ArgumentException("Can not convert '" + Convert.ToString(value) + "' to type Corners");
                }
            } else {
                return new PointF(-5f, -2.5f);
            }

            return base.ConvertFrom(context, culture, value);

        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {


            if ((object.ReferenceEquals(destinationType, typeof(System.String)) && value is PointF)) {
                PointF ConverterProperty = (PointF)value;
                // build the string representation
                return string.Format("{0}, {1}", ConverterProperty.X, ConverterProperty.Y);
            }
            return base.ConvertTo(context, culture, value, destinationType);

        }

    }
}
//PointFConverter Class

#endregion
