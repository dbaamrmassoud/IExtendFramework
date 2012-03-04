using IExtendFramework.Controls;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace IExtendFramework.Controls
{

    /// <summary>
    /// Label control with built-in effects like AutoFit, MouseOver, Shadow, Outer Glow, and Pulse Glow
    /// </summary>
    /// <remarks>v1.0.5</remarks>
    [ToolboxItem(true)]
    public partial class AdvancedLabel : Label
    {

        #region "Declarations"

        private Timer withEventsField_Pulser = new Timer();
        private Timer Pulser {
            get { return withEventsField_Pulser; }
            set {
                if (withEventsField_Pulser != null) {
                    withEventsField_Pulser.Tick -= Pulser_Tick;
                }
                withEventsField_Pulser = value;
                if (withEventsField_Pulser != null) {
                    withEventsField_Pulser.Tick += Pulser_Tick;
                }
            }
        }

        private bool _MouseIsOver = false;
        #endregion

        #region "New"

        public AdvancedLabel()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.ForeColor = Color.MediumBlue;
            this.Font = new Font("Arial", 12, this.Font.Style);
            this.PulseSpeed = 25;
            this.Size = new Size(75, 21);
            this.TextAlign = ContentAlignment.MiddleCenter;
        }

        #endregion

        #region "Hidden"

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoSize {
            get { return base.AutoSize; }
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Image {
            //always false
            get { return false; }
            //empty
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle {
            get {
                BorderStyle functionReturnValue = default(BorderStyle);
                //return BorderStyle.None;
                return functionReturnValue;
                //always false
            }
            //None
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ImageAlign {
            //always false
            get { return false; }
            //empty
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ImageIndex {
            //always false
            get { return false; }
            //empty
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ImageKey {
            //always false
            get { return false; }
            //empty
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ImageList {
            //always false
            get { return false; }
            //empty
            set { }
        }

        #endregion

        #region "Properties"

        private bool _MouseOver = false;
        /// <summary>
        /// Get or Set if the gLabel will Glow when the mouse is over it
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set if the gLabel will Glow when the mouse is over it")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool MouseOver {
            get { return _MouseOver; }
            set { _MouseOver = value; }
        }

        private Color _MouseOverColor = Color.Crimson;
        /// <summary>
        /// Get or Set what color the gLabel will Glow when the mouse is over it
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set what color the gLabel will Glow when the mouse is over it")]
        [DefaultValue(typeof(Color), "Crimson")]
        public Color MouseOverColor {
            get { return _MouseOverColor; }
            set {
                _MouseOverColor = value;
                this.Invalidate();
            }
        }

        private bool _FeatherState = true;
        /// <summary>
        /// Get or Set if the text is glowing
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set if the glow is feathered")]
        [DefaultValue(true)]
        public bool FeatherState {
            get { return _FeatherState; }
            set {
                _FeatherState = value;
                this.Invalidate();
            }
        }

        private int _Feather = 100;
        /// <summary>
        /// Get or Set the level of feathering (blurring) of the Outer Glow
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set the level of feathering (blurring) of the Outer Glow")]
        [DefaultValue(100)]
        public int Feather {
            get { return _Feather; }
            set {
                _Feather = value;
                if (_Feather > 255)
                    _Feather = 255;
                if (_Feather < 0)
                    _Feather = 0;
                this.Invalidate();
                PulseDirection = _Feather / 25;
                if (PulseDirection < 0)
                    PulseDirection = 1;
            }
        }

        private int _PulseAdj = 0;
        private bool _Pulse = false;
        /// <summary>
        /// Get or Set if the gLabel should be Pulsing or not
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set if the gLabel should be Pulsing or not")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(false)]
        public bool Pulse {
            get { return _Pulse; }
            set {
                _Pulse = value;
                if (value) {
                    _PulseAdj = 0;
                    Pulser.Start();
                } else {
                    Pulser.Stop();
                    _PulseAdj = 0;
                    this.Invalidate();
                    //PulseDirection = _Feather \ 25
                    //If PulseDirection < 0 Then PulseDirection = 1
                }
            }
        }

        /// <summary>
        /// Get or Set how fast to pulse the gLabel
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set how fast to pulse the gLabel")]
        public int PulseSpeed {

            get { return this.Pulser.Interval; }
            set { this.Pulser.Interval = value; }
        }

        private Point _ShadowOffset = new Point(5, 5);
        /// <summary>
        /// Get or Set how far to offset the shadow from the text
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set how far to offset the shadow from the text")]
        [DefaultValue(typeof(Point), "5,5")]
        public Point ShadowOffset {
            get { return _ShadowOffset; }
            set {
                _ShadowOffset = value;
                this.Invalidate();
            }
        }

        private bool _ShadowState = false;
        /// <summary>
        /// Get or Set if the gLabel displays the shadow text
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set if the gLabel displays the shadow text")]
        [DefaultValue(false)]
        public bool ShadowState {
            get { return _ShadowState; }
            set {
                _ShadowState = value;
                this.Invalidate();
            }
        }

        private Color _ShadowColor = Color.Gray;
        /// <summary>
        /// Get or Set what color to use for the shadow text
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set what color to use for the shadow text")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color ShadowColor {
            get { return _ShadowColor; }
            set {
                _ShadowColor = value;
                this.Invalidate();
            }
        }

        private bool _GlowState = true;
        /// <summary>
        /// Get or Set if the text is glowing
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set if the text is glowing")]
        [DefaultValue(true)]
        public bool GlowState {
            get { return _GlowState; }
            set {
                _GlowState = value;
                this.Invalidate();
            }
        }

        private int _Glow = 8;
        /// <summary>
        /// Get or Set how far out the text glows
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set how far out the text glows")]
        [DefaultValue(7)]
        public int Glow {
            get { return _Glow; }
            set {
                _Glow = value;
                this.Invalidate();
            }
        }

        private Color _GlowColor = Color.CornflowerBlue;
        /// <summary>
        /// Get or Set what color to use for the Glow
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set what color to use for the Glow")]
        [DefaultValue(typeof(Color), "SteelBlue")]
        public Color GlowColor {
            get { return _GlowColor; }
            set {
                _GlowColor = value;
                this.Invalidate();
            }
        }

        private bool _Border = false;
        /// <summary>
        /// Get or Set to show or not show the border
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set to show or not show the border")]
        [DefaultValue(false)]
        public bool Border {
            get { return _Border; }
            set {
                _Border = value;
                this.Invalidate();
            }
        }

        private Color _BorderColor = Color.Black;
        /// <summary>
        /// Get or Set what Color to draw the border
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set what Color to draw the border")]
        [DefaultValue(typeof(Color), "Black")]
        public Color BorderColor {
            get { return _BorderColor; }
            set {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        private float _BorderWidth = 1;
        /// <summary>
        /// Get or Set what Width to draw the border
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set what Width to draw the border")]
        [DefaultValue(1)]
        public float BorderWidth {
            get { return _BorderWidth; }
            set {
                _BorderWidth = value;
                this.Invalidate();
            }
        }

        private bool _AutoFit = false;
        /// <summary>
        /// Get or Set if the text is fitted to the DisplayRectangle or uses Font Size
        /// </summary>
        [Category("Appearance")]
        [Description("Get or Set if the text is fitted to the DisplayRectangle or uses Font Size")]
        [DefaultValue(false)]
        public bool AutoFit {
            get { return _AutoFit; }
            set {
                _AutoFit = value;
                this.Invalidate();
            }
        }

        private StringFormat sf = new StringFormat();
        public override ContentAlignment TextAlign {
            get { return base.TextAlign; }
            set {
                base.TextAlign = value;
                switch (this.TextAlign) {
                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                }
                switch (this.TextAlign) {
                    case ContentAlignment.BottomRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.TopRight:
                        sf.Alignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.TopCenter:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.TopLeft:
                        sf.Alignment = StringAlignment.Near;
                        break;
                }

            }
        }

        public enum eFillType
        {
            Solid,
            GradientLinear
        }

        private eFillType _FillType = eFillType.Solid;
        /// <summary>
        /// The eFillType Fill Type to apply to the CButton
        /// </summary>
        [Description("The Fill Type to apply to the CButton")]
        [Category("Appearance")]
        [DefaultValue(eFillType.Solid)]
        public eFillType FillType {
            get { return _FillType; }
            set {
                _FillType = value;
                this.Invalidate();
            }
        }

        private LinearGradientMode _FillTypeLinear = LinearGradientMode.Vertical;
        /// <summary>
        /// The Linear Blend type
        /// </summary>
        [Description("The Linear Blend type"), Category("Appearance")]
        [DefaultValue(LinearGradientMode.Vertical)]
        public LinearGradientMode FillTypeLinear {
            get { return _FillTypeLinear; }
            set {
                _FillTypeLinear = value;
                this.Invalidate();
            }
        }

        private  cBlendItems _ForeColorBlend = new cBlendItems(new Color[] {
                                                                  Color.AliceBlue,
                                                                  Color.RoyalBlue,
                                                                  Color.Navy
                                                              }, new float[] {
                                                                  0,
                                                                  0.5F,
                                                                  1
                                                              });
        /// <summary>
        /// The ColorBlend used to fill the CButton
        /// </summary>
        [Description("The ColorBlend used to fill the CButton"), Category("Appearance"), Editor(typeof(BlendTypeEditor), typeof(UITypeEditor))]
        public cBlendItems ForeColorBlend {
            get { return _ForeColorBlend; }
            set {
                _ForeColorBlend = value;
                this.Invalidate();
            }
        }


        #endregion

        #region "Overrides"

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            if (this.BackColor == Color.Transparent) {
                base.OnPaintBackground(pevent);
            } else {
                pevent.Graphics.Clear(EnabledColor(this.BackColor));
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //Setup Graphics
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic

            //Paint Shadow if Requested
            if (_ShadowState) {
                GraphicsPath shadowpath = new GraphicsPath();
                
                MakeTextPath(ref shadowpath, ref g);
                
                //Offset the Shadow
                using (Matrix mx = new Matrix()) {
                    if (_AutoFit) {
                        mx.Translate(_ShadowOffset.X - 2, _ShadowOffset.Y - 2);
                    } else {
                        mx.Translate(_ShadowOffset.X, _ShadowOffset.Y);
                    }
                    shadowpath.Transform(mx);
                }
                
                //Blur the edge a bit
                int x = Convert.ToInt32(Conversion.Fix(this.Height / 30) + 1);
                for (int i = 1; i <= x; i++) {
                    using (Pen pen = new Pen(EnabledColor(Color.FromArgb(Convert.ToInt32(200 - ((200 / x) * i)), _ShadowColor)), i)) {
                        pen.LineJoin = LineJoin.Round;
                        g.DrawPath(pen, shadowpath);
                    }
                }
                g.FillPath(new SolidBrush(EnabledColor(_ShadowColor)), shadowpath);
            }

            GraphicsPath path = new GraphicsPath();
                            
                MakeTextPath(ref path, ref g);
                                    
                //Paint Glow if Requested
                Color gColor = _GlowColor;
                if (_GlowState | (_MouseOver & _MouseIsOver)) {
                    if (_MouseOver & _MouseIsOver)
                        gColor = _MouseOverColor;
                                
                    if (_FeatherState) {
                        for (int i = 1; i <= _Glow; i += 2) {
                            int aGlow = Convert.ToInt32((_Feather - _PulseAdj) - (((_Feather - _PulseAdj) / _Glow) * i));
                            using (Pen pen = new Pen(Color.FromArgb(aGlow, EnabledColor(gColor)), i)) {
                                pen.LineJoin = LineJoin.Round;
                                g.DrawPath(pen, path);
                            }
                        }
                    } else {
                        using (Pen pen = new Pen(Color.FromArgb(_Feather - _PulseAdj, EnabledColor(gColor)), _Glow)) {
                            pen.LineJoin = LineJoin.Round;
                            g.DrawPath(pen, path);
                        }
                }

                //Paint Label Text
                switch (_FillType) {
                    case eFillType.Solid:
                        g.FillPath(new SolidBrush(EnabledColor(this.ForeColor)), path);

                        break;
                    case eFillType.GradientLinear:
                        using (LinearGradientBrush br = new LinearGradientBrush(new RectangleF(path.GetBounds().X - 1, path.GetBounds().Y - 1, path.GetBounds().Width + 2, path.GetBounds().Height + 2), Color.White, Color.White, FillTypeLinear)) {
                            ColorBlend cb = new ColorBlend();
                            cb.Colors = EnableBlends(_ForeColorBlend.iColor);
                            cb.Positions = _ForeColorBlend.iPoint;

                            br.InterpolationColors = cb;

                            g.FillPath(br, path);
                        }


                        break;
                }
            }

            //Paint Border if Requested
            if (_Border) {
                using (Pen pn = new Pen(EnabledColor(_BorderColor), (_BorderWidth * 2))) {
                    g.ResetTransform();
                    g.DrawRectangle(pn, 0, 0, this.Width - 1, this.Height - 1);
                }

            }

        }

        private void MakeTextPath(ref GraphicsPath path, ref Graphics g)
        {
            if (_AutoFit) {
                try {
                    //Determine the outer margin for the text so there
                    //is enough room for the glow and shadow
                    int pad = 2;
                    if (this.GlowState) {
                        pad += Convert.ToInt32(_Glow - (_Glow / 2) + Conversion.Fix(_Glow / 30) * 3);
                    }
                    if (Border) {
                        pad += Convert.ToInt32(BorderWidth + 1);
                    }
                    Padding TextMargin = new Padding(pad);
                    if (this.ShadowState) {
                        
                        TextMargin.Right += Convert.ToInt32(_ShadowOffset.X);
                        TextMargin.Bottom += Convert.ToInt32(_ShadowOffset.Y + ((_ShadowOffset.Y) / 3));
                    }

                    //Get a rectangle for the area to paint the text
                    Rectangle target = new Rectangle(TextMargin.Left, TextMargin.Top, Math.Max(5, this.ClientSize.Width - TextMargin.Horizontal), Math.Max(5, this.ClientSize.Height - TextMargin.Vertical));

                    //Get the points for the corners of the area
                    PointF[] target_pts = {
                        new PointF(target.Left, target.Top),
                        new PointF(target.Right, target.Top),
                        new PointF(target.Left, target.Bottom)
                    };

                    //Make a GraphicsPath of the Text String
                    //close to the size it needs to be
                    path.AddString(this.Text, this.Font.FontFamily, (int) this.Font.Style, target.Height, new PointF(0, 0), sf);

                    //Get a rectangle for the path of the text
                    RectangleF text_rectf = path.GetBounds();

                    //Transform the Graphics Object with the Matrix
                    //to fit the path rectangle inside the target rectangle
                    g.Transform = new Matrix(text_rectf, target_pts);


                } catch (Exception) {
                }

            } else {
                //create a GraphicsPath of the text
                //Because the GraphicsPath does not match exactly with
                //Drawing a String normally, multiply the font Size by
                //1.26 to get a pretty close representation of the size.
                path.AddString(this.Text, this.Font.FontFamily,(int) this.Font.Style, Convert.ToInt32(this.Font.Size * 1.26), new PointF(this.ClientRectangle.X, this.ClientRectangle.Y), sf);

            }

        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            if (MouseOver) {
                _MouseIsOver = true;
                this.Invalidate();
            }
        }

        protected void OnMouseleave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            if (MouseOver) {
                _MouseIsOver = false;
                this.Invalidate();
            }
        }

        #endregion

        #region "EnabledColor"

        /// <summary>
        /// Convert color to gray if Disabled else return origional color
        /// </summary>
        /// <param name="ColorIn">Color to Check</param>
        private Color EnabledColor(Color ColorIn)
        {
            if (this.Enabled) {
                return ColorIn;
            } else {
                int gray = Convert.ToInt32(ColorIn.R * 0.3 + ColorIn.G * 0.59 + ColorIn.B * 0.11);
                return Color.FromArgb(ColorIn.A, gray, gray, gray);
            }
        }

        /// <summary>
        /// Convert colorblend to grayscale if Disabled else return origional colorblend
        /// </summary>
        /// <param name="ColorIn">Colorblend to Check</param>
        private Color[] EnableBlends(Color[] ColorIn)
        {

            if (this.Enabled) {
                return ColorIn;
            } else {
                List<Color> tcolor = new List<Color>();
                foreach (Color c in ColorIn) {
                    int gray = Convert.ToInt32(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    tcolor.Add(Color.FromArgb(c.A, gray, gray, gray));
                }
                return tcolor.ToArray();
            }
        }

        #endregion

        #region "PulseTimer"

        private int PulseDirection = 1;
        private void Pulser_Tick(object sender, System.EventArgs e)
        {
            _PulseAdj += PulseDirection;
            if (_Feather - _PulseAdj < 10 || _Feather - _PulseAdj > _Feather) {
                PulseDirection *= -1;
                _PulseAdj += PulseDirection;
            }
            this.Invalidate();
        }

        #endregion

    }
}