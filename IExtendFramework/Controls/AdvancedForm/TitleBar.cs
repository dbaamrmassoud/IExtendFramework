using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.ComponentModel;
namespace IExtendFramework.Controls
{

    /// <summary>
    /// Titlebar is a control that can be put on a frameless form to act like a normal title bar
    /// but also makes it easy to add controls to the bar
    /// </summary>
    /// <remarks>Version 1.0.0</remarks>
    [ToolboxItem(true)]
    public partial class TitleBar
    {

        #region "New"

        public TitleBar()
        {
            DoubleClick += TitleBar_DoubleClick;
            MouseMove += Me_MouseMove;
            MouseDown += Me_MouseDown;
            Invalidated += TitleBar_Invalidated;
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }
        #endregion

        #region "Custom Events"

        public event CloseButtonClickedEventHandler CloseButtonClicked;
        public delegate void CloseButtonClickedEventHandler();
        public event MinimizeButtonClickedEventHandler MinimizeButtonClicked;
        public delegate void MinimizeButtonClickedEventHandler();
        public event MaximizeButtonClickedEventHandler MaximizeButtonClicked;
        public delegate void MaximizeButtonClickedEventHandler();

        #endregion

        #region "Resize-Move form"


        private ResizeDirection _resizeDir = ResizeDirection.None;
        public enum ResizeDirection
        {
            None = 0,
            Left = 1,
            TopLeft = 2,
            Top = 3,
            TopRight = 4,
            Right = 5,
            BottomRight = 6,
            Bottom = 7,
            BottomLeft = 8
        }

        [Browsable(false)]
        public ResizeDirection resizeDir {
            get { return _resizeDir; }
            set {
                _resizeDir = value;

                //Change cursor
                switch (value) {
                    case ResizeDirection.Left:
                        Cursor = Cursors.SizeWE;

                        break;
                    case ResizeDirection.Right:
                        Cursor = Cursors.SizeWE;

                        break;
                    case ResizeDirection.Top:
                        Cursor = Cursors.SizeNS;

                        break;
                    case ResizeDirection.Bottom:
                        Cursor = Cursors.SizeNS;

                        break;
                    case ResizeDirection.BottomLeft:
                        Cursor = Cursors.SizeNESW;

                        break;
                    case ResizeDirection.TopRight:
                        Cursor = Cursors.SizeNESW;

                        break;
                    case ResizeDirection.BottomRight:
                        Cursor = Cursors.SizeNWSE;

                        break;
                    case ResizeDirection.TopLeft:
                        Cursor = Cursors.SizeNWSE;

                        break;
                    case ResizeDirection.None:
                        Cursor = Cursors.Default;

                        break;
                    default:
                        Cursor = Cursors.Default;
                        break;
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xa1;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTCAPTION = 2;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;

        private const int HTTOPRIGHT = 14;
        private void MoveForm()
        {
            ReleaseCapture();
            SendMessage(Parent.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            Invalidate();
        }

        private void ResizeForm(ResizeDirection direction)
        {
            int dir = -1;
            switch (direction) {
                case ResizeDirection.Left:
                    dir = HTLEFT;
                    break;
                case ResizeDirection.TopLeft:
                    dir = HTTOPLEFT;
                    break;
                case ResizeDirection.Top:
                    dir = HTTOP;
                    break;
                case ResizeDirection.TopRight:
                    dir = HTTOPRIGHT;
                    break;
                case ResizeDirection.Right:
                    dir = HTRIGHT;
                    break;
                case ResizeDirection.BottomRight:
                    dir = HTBOTTOMRIGHT;
                    break;
                case ResizeDirection.Bottom:
                    dir = HTBOTTOM;
                    break;
                case ResizeDirection.BottomLeft:
                    dir = HTBOTTOMLEFT;
                    break;
                case ResizeDirection.None:
                    break;
            }

            if (dir != -1) {
                ReleaseCapture();
                SendMessage(ParentForm.Handle, WM_NCLBUTTONDOWN, dir, 0);
            }
        }
        #endregion

        #region "Properties"

        private string _TitleText = "TitleBar";
        /// <summary>
        /// Text to display on the Tile Bar
        /// </summary>
        [Category("TitleBar")]
        [Description("Text to display on the Title Bar")]
        [DefaultValue("TitleBar")]
        public string TitleText {
            get { return _TitleText; }
            set {
                _TitleText = value;
                Refresh();
            }
        }

        public enum eControlBoxAffects
        {
            ParentForm,
            EventTrigger
        }
        private eControlBoxAffects _ControlBoxAffects = eControlBoxAffects.ParentForm;
        /// <summary>
        /// Get or Set if clicking the Control Box Buttons affects the Form or triggers the custom Events
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set if clicking the Control Box Buttons affects the Form or triggers the custom Events")]
        [DefaultValue(eControlBoxAffects.ParentForm)]
        public eControlBoxAffects ControlBoxAffects {
            get { return _ControlBoxAffects; }
            set { _ControlBoxAffects = value; }
        }

        private bool _ShowCloseBox = true;
        /// <summary>
        /// Get or Set if the Close button is visible
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set if the Close button is visible")]
        [DefaultValue(true)]
        public bool ShowCloseBox {
            get { return _ShowCloseBox; }
            set {
                _ShowCloseBox = value;
                CloseWindowButton.Visible = value;
                Invalidate();
            }
        }

        private bool _ShowMinimizeBox = true;
        /// <summary>
        /// Get or Set if the Minimize button is visible
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set if the Minimize button is visible")]
        [DefaultValue(true)]
        public bool ShowMinimizeBox {
            get { return _ShowMinimizeBox; }
            set {
                _ShowMinimizeBox = value;
                MinimizeWindowButton.Visible = value;
                Invalidate();
            }
        }


        private bool _ShowMaximizeBox = true;
        /// <summary>
        /// Get or Set if the Maximize button is visible
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set if the Maximize button is visible")]
        [DefaultValue(true)]
        public bool ShowMaximizeBox {
            get { return _ShowMaximizeBox; }
            set {
                _ShowMaximizeBox = value;
                MaximizeWindowButton.Visible = value;
                Invalidate();
            }
        }

        private bool _AllowMove = true;
        /// <summary>
        /// Get or Set if the TitleBar allows moving with the mouse.
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set if the TitleBar allows moving with the mouse.")]
        [DefaultValue(true)]
        public bool AllowMove {
            get { return _AllowMove; }
            set { _AllowMove = value; }
        }

        private bool _FrameShow = true;
        /// <summary>
        /// Get or Set if the frame is visible around the parent
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set if the frame is visible around the parent")]
        [DefaultValue(true)]
        public bool FrameShow {
            get { return _FrameShow; }
            set {
                _FrameShow = value;
                Invalidate();
            }
        }

        private int _FrameWidth = 5;
        /// <summary>
        /// Get or Set the width of the frame
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set the width of the frame")]
        [DefaultValue(5)]
        public int FrameWidth {
            get { return _FrameWidth; }
            set {
                _FrameWidth = value;
                Invalidate();
            }
        }

        private int _FrameHeightAdj;
        /// <summary>
        /// Get or Set a padding value for the bottom of the frame
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set a padding value for the bottom of the frame")]
        [DefaultValue(0)]
        public int FrameHeightAdj {
            get { return _FrameHeightAdj; }
            set {
                _FrameHeightAdj = value;

                try {
                    Parent.Invalidate();


                } catch (Exception) {
                }
                Invalidate();
            }
        }

        private Image _titleImage;
        /// <summary>
        /// Get or Set The image to display in the TitleBar
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set The image to display in the TitleBar")]
        public Image TitleImage {
            get { return _titleImage; }
            set {
                _titleImage = value;
                Invalidate();
            }
        }

        private Size _titleImageSize = new Size(24, 24);
        /// <summary>
        /// Get or Set the size to make the image on the TitleBar
        /// </summary>
        [Category("TitleBar")]
        [Description("Get or Set the size to make the image on the TitleBar")]
        [DefaultValue("24,24")]
        public Size TitleImageSize {
            get { return _titleImageSize; }
            set {
                _titleImageSize = value;
                Invalidate();
            }
        }

        private int _IsFormActive = 1;
        [Browsable(false)]
        public bool IsFormActive {
            get { return _IsFormActive == 1; }
            set {
                if (value) {
                    _IsFormActive = 1;
                } else {
                    _IsFormActive = 2;
                }
                Invalidate();
            }
        }

        #endregion

        #region "Invalidate Buttons"

        private void TitleBar_Invalidated(object sender, System.Windows.Forms.InvalidateEventArgs e)
        {
            CloseWindowButton.Invalidate();
            MaximizeWindowButton.Invalidate();
            MinimizeWindowButton.Invalidate();
        }
        #endregion

        #region "Mouse Events"

        private void Me_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Check for single click.
            // Bypass if Double clicked.
            if (e.Clicks == 1) {
                if (resizeDir == ResizeDirection.None) {
                    if (_AllowMove) {
                        if (Parent is Form) {
                            if (FindForm().WindowState == FormWindowState.Normal) {
                                MoveForm();
                            }
                        } else {
                            MoveForm();
                        }

                    }
                } else {
                    if (Parent is Form) {
                        ResizeForm(resizeDir);
                    }
                }
            }

        }

        private void Me_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (Parent is Form) {
                if (e.Location.X < _FrameWidth & e.Location.Y < _FrameWidth) {
                    resizeDir = ResizeDirection.TopLeft;

                    //ElseIf e.Location.X < _FrameWidth And e.Location.Y > Height - _FrameWidth Then
                    //    resizeDir = ResizeDirection.BottomLeft

                    //ElseIf e.Location.X > Width - _FrameWidth And e.Location.Y > Height - _FrameWidth Then
                    //    resizeDir = ResizeDirection.BottomRight

                } else if (e.Location.X > Width - _FrameWidth & e.Location.Y < _FrameWidth) {
                    resizeDir = ResizeDirection.TopRight;

                } else if (e.Location.X < _FrameWidth) {
                    resizeDir = ResizeDirection.Left;

                } else if (e.Location.X > Width - _FrameWidth) {
                    resizeDir = ResizeDirection.Right;

                } else if (e.Location.Y < _FrameWidth) {
                    resizeDir = ResizeDirection.Top;

                    //ElseIf e.Location.Y > Height - _FrameWidth Then
                    //    resizeDir = ResizeDirection.Bottom

                } else {
                    resizeDir = ResizeDirection.None;
                }
            }

        }
        #endregion

        #region "DoubleClick-Maximize/Restore"

        private void TitleBar_DoubleClick(object sender, System.EventArgs e)
        {
            if (FindForm().WindowState == FormWindowState.Maximized) {
                FindForm().WindowState = FormWindowState.Normal;
            } else {
                FindForm().WindowState = FormWindowState.Maximized;
                FindForm().Invalidate();
                Invalidate();
            }
        }

        #endregion

        #region "Theme Functions"

        #region "Enums"

        /// <summary>
        ///  "Window" (i.e., non-client) Parts
        /// </summary>
        public enum UxThemeWindowParts : int
        {

            /// <summary>Caption</summary>
            WP_CAPTION = 1,
            /// <summary>Small Caption</summary>
            WP_SMALLCAPTION = 2,
            /// <summary>Minimised Caption</summary>
            WP_MINCAPTION = 3,
            /// <summary>Small minimised Caption</summary>
            WP_SMALLMINCAPTION = 4,
            /// <summary>Maximised Caption</summary>
            WP_MAXCAPTION = 5,
            /// <summary>Small maximised Caption</summary>
            WP_SMALLMAXCAPTION = 6,
            /// <summary>Frame left</summary>
            WP_FRAMELEFT = 7,
            /// <summary>Frame right</summary>
            WP_FRAMERIGHT = 8,
            /// <summary>Frame bottom</summary>
            WP_FRAMEBOTTOM = 9,
            /// <summary>Small frame left</summary>
            WP_SMALLFRAMELEFT = 10,
            /// <summary>Small frame right</summary>
            WP_SMALLFRAMERIGHT = 11,
            /// <summary>Small frame bottom</summary>
            WP_SMALLFRAMEBOTTOM = 12,
            /// <summary>System button</summary>
            WP_SYSBUTTON = 13,
            /// <summary>MDI System button</summary>
            WP_MDISYSBUTTON = 14,
            /// <summary>Min button</summary>
            WP_MINBUTTON = 15,
            /// <summary>MDI Min button</summary>
            WP_MDIMINBUTTON = 16,
            /// <summary>Max button</summary>
            WP_MAXBUTTON = 17,
            /// <summary>Close button</summary>
            WP_CLOSEBUTTON = 18,
            /// <summary>Small close button</summary>
            WP_SMALLCLOSEBUTTON = 19,
            /// <summary>MDI close button</summary>
            WP_MDICLOSEBUTTON = 20,
            /// <summary>Restore button</summary>
            WP_RESTOREBUTTON = 21,
            /// <summary>MDI Restore button</summary>
            WP_MDIRESTOREBUTTON = 22,
            /// <summary>Help button</summary>
            WP_HELPBUTTON = 23,
            /// <summary>MDI Help button</summary>
            WP_MDIHELPBUTTON = 24,
            /// <summary>Horizontal scroll bar</summary>
            WP_HORZSCROLL = 25,
            /// <summary>Horizontal scroll thumb</summary>
            WP_HORZTHUMB = 26,
            /// <summary>Vertical scroll bar</summary>
            WP_VERTSCROLL = 27,
            /// <summary>Vertical scroll thumb</summary>
            WP_VERTTHUMB = 28,
            /// <summary>Dialog</summary>
            WP_DIALOG = 29,
            /// <summary>Caption sizing hittest template</summary>
            WP_CAPTIONSIZINGTEMPLATE = 30,
            /// <summary>Small caption sizing hittest template</summary>
            WP_SMALLCAPTIONSIZINGTEMPLATE = 31,
            /// <summary>Frame left sizing hittest template</summary>
            WP_FRAMELEFTSIZINGTEMPLATE = 32,
            /// <summary>Small frame left sizing hittest template</summary>
            WP_SMALLFRAMELEFTSIZINGTEMPLATE = 33,
            /// <summary>Frame right sizing hittest template</summary>
            WP_FRAMERIGHTSIZINGTEMPLATE = 34,
            /// <summary>Small frame right sizing hittest template</summary>
            WP_SMALLFRAMERIGHTSIZINGTEMPLATE = 35,
            /// <summary>Frame button sizing hittest template</summary>
            WP_FRAMEBOTTOMSIZINGTEMPLATE = 36,
            /// <summary>Small frame bottom sizing hittest template</summary>
            WP_SMALLFRAMEBOTTOMSIZINGTEMPLATE = 37
        }

        /// <summary>
        /// System Button states
        /// </summary>
        public enum UxThemeSysButtonStates : int
        {
            /// <summary>Normal</summary>
            SBS_NORMAL = 1,
            /// <summary>Hot</summary>
            SBS_HOT = 2,
            /// <summary>Pushed</summary>
            SBS_PUSHED = 3,
            /// <summary>Disabled</summary>
            SBS_DISABLED = 4,
            /// <summary>Inactive</summary>
            SBS_INACTIVE = 5
        }

        public enum DrawTextFlags : uint
        {
            DT_TOP = 0x0,
            DT_LEFT = 0x0,
            DT_CENTER = 0x1,
            DT_RIGHT = 0x2,
            DT_VCENTER = 0x4,
            DT_BOTTOM = 0x8,
            DT_WORDBREAK = 0x10,
            DT_SINGLELINE = 0x20,
            DT_EXPANDTABS = 0x40,
            DT_TABSTOP = 0x80,
            DT_NOCLIP = 0x100,
            DT_EXTERNALLEADING = 0x200,
            DT_CALCRECT = 0x400,
            DT_NOPREFIX = 0x800,
            DT_INTERNAL = 0x1000,
            DT_EDITCONTROL = 0x2000,
            DT_PATH_ELLIPSIS = 0x4000,
            DT_END_ELLIPSIS = 0x8000,
            DT_MODIFYSTRING = 0x10000,
            DT_RTLREADING = 0x20000,
            DT_WORD_ELLIPSIS = 0x40000,
            DT_NOFULLWIDTHCHARBREAK = 0x80000,
            DT_HIDEPREFIX = 0x100000,
            DT_PREFIXONLY = 0x200000
        }
        // UxTheme DrawText Additional Flag
        #endregion
        const int DTT_GRAYED = 0x1;

        #region "Structures"

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;

            public int bottom;
            public RECT(Rectangle rect) : this(rect.Left, rect.Top, rect.Right, rect.Bottom)
            {
            }
            //New

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;

            }
            //New

            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle ToRectangle()
            {
                return new Rectangle(left, top, right - left, bottom - top);
            }
            //ToRectangle

        }
        //RECT
        #endregion

        [DllImport("Uxtheme", EntryPoint = "OpenThemeData", CharSet = CharSet.Unicode)]
        static internal extern IntPtr openThemeData(IntPtr hWnd, string classList);

        [DllImport("Uxtheme", EntryPoint = "DrawThemeBackground", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Error)]
        static internal extern Int32 drawThemeBackground(IntPtr hTheme, IntPtr hDC, Int32 iPartId, Int32 iStateId, ref RECT pRect, IntPtr nullRECT);

        [DllImport("UxTheme.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseThemeData(IntPtr hTheme);

        [DllImport("Uxtheme", EntryPoint = "IsAppThemed")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static internal extern bool isAppThemed();

        private static ButtonState ConvertThemeToClassic(UxThemeSysButtonStates themeButtonState)
        {
            switch (themeButtonState) {
                case UxThemeSysButtonStates.SBS_NORMAL:
                    return ButtonState.Normal;
                case UxThemeSysButtonStates.SBS_HOT:
                    return ButtonState.Normal;
                case UxThemeSysButtonStates.SBS_PUSHED:
                    return ButtonState.Pushed;
                case UxThemeSysButtonStates.SBS_INACTIVE:
                    return ButtonState.Inactive;
                case UxThemeSysButtonStates.SBS_DISABLED:
                    return ButtonState.Inactive;
                default:
                    return ButtonState.Normal;
            }
        }

        #endregion

        #region "Paint"

        private IntPtr intptrWindowTheme;
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //Draw the frame if requested
            if (_FrameShow) {
                if (isAppThemed()) {
                    Graphics g = Parent.CreateGraphics();
                    IntPtr hdcGraphics = g.GetHdc();

                    intptrWindowTheme = openThemeData(Parent.Handle, "Window");

                    //Draw Left Side of the Frame
                    RECT tmp = new RECT(new Rectangle(0, 0, _FrameWidth, Parent.Height - _FrameHeightAdj));
                    drawThemeBackground(intptrWindowTheme, hdcGraphics,(int) UxThemeWindowParts.WP_FRAMELEFT, _IsFormActive, ref tmp, IntPtr.Zero);

                    //Draw Right Side of the Frame
                    tmp = new RECT(new Rectangle(Parent.Width - _FrameWidth, 0, _FrameWidth, Parent.Height - _FrameHeightAdj));
                    drawThemeBackground(intptrWindowTheme, hdcGraphics, (int)UxThemeWindowParts.WP_FRAMERIGHT, _IsFormActive, ref tmp, IntPtr.Zero);

                    //Draw Bottom Side of the Frame
                    tmp = new RECT(new Rectangle(0, Parent.Height - _FrameWidth - _FrameHeightAdj, Parent.Width, _FrameWidth));
                    drawThemeBackground(intptrWindowTheme, hdcGraphics,(int) UxThemeWindowParts.WP_FRAMEBOTTOM, _IsFormActive, ref tmp, IntPtr.Zero);

                    g.ReleaseHdc(hdcGraphics);
                    CloseThemeData(intptrWindowTheme);
                } else {
                    //Draw Left Side of the Frame
                    ControlPaint.DrawBorder3D(Parent.CreateGraphics(), new Rectangle(0, 0, _FrameWidth, Parent.Height - _FrameHeightAdj), Border3DStyle.Raised, Border3DSide.All);

                    //Draw Right Side of the Frame
                    ControlPaint.DrawBorder3D(Parent.CreateGraphics(), new Rectangle(Parent.Width - _FrameWidth, 0, _FrameWidth, Parent.Height - _FrameHeightAdj), Border3DStyle.Raised, Border3DSide.All);

                    //Draw Bottom Side of the Frame
                    ControlPaint.DrawBorder3D(Parent.CreateGraphics(), new Rectangle(0, Parent.Height - _FrameWidth - _FrameHeightAdj, Parent.Width, _FrameWidth), Border3DStyle.Raised, Border3DSide.All);

                }
            }

            //Draw TitleBar

            if (isAppThemed()) {
                IntPtr hdcGraphics = e.Graphics.GetHdc();
                intptrWindowTheme = openThemeData(Handle, "Window");

                RECT tmp = new RECT(new Rectangle(0, 0, Width, Height));
                drawThemeBackground(intptrWindowTheme, hdcGraphics, (int)UxThemeWindowParts.WP_CAPTION, _IsFormActive, ref tmp, IntPtr.Zero);

                e.Graphics.ReleaseHdc(hdcGraphics);
                CloseThemeData(intptrWindowTheme);

            } else {
                Rectangle rect = DisplayRectangle;
                Color focuscolorDark = SystemColors.ActiveCaption;
                Color focuscolorLight = SystemColors.GradientActiveCaption;
                if (!IsFormActive) {
                    focuscolorDark = SystemColors.InactiveCaption;
                    focuscolorLight = SystemColors.GradientInactiveCaption;

                }
                using (Brush br = new LinearGradientBrush(rect, focuscolorDark, focuscolorLight, LinearGradientMode.Horizontal)) {
                    e.Graphics.FillRectangle(br, rect);

                    //Top
                    ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(_FrameWidth - 2, 0, Width - _FrameWidth, _FrameWidth), Border3DStyle.Raised, Border3DSide.All);

                    //Top Left Corner
                    ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, _FrameWidth, _FrameWidth), Border3DStyle.Raised, Border3DSide.Top | Border3DSide.Left | Border3DSide.Middle);

                    //Left Side
                    ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, _FrameWidth - 2, _FrameWidth, Height - _FrameWidth + 2), Border3DStyle.Raised, Border3DSide.Top | Border3DSide.Left | Border3DSide.Right | Border3DSide.Middle);

                    //Right Side
                    ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(Width - _FrameWidth, 0, _FrameWidth, Height), Border3DStyle.Raised, Border3DSide.Top | Border3DSide.Left | Border3DSide.Right | Border3DSide.Middle);

                }
            }

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            int ImagePad = 0;
            if (_titleImage != null) {
                ImagePad = _titleImageSize.Width;
                e.Graphics.DrawImage(_titleImage, 5, 3, _titleImageSize.Width, _titleImageSize.Height);
            }

            e.Graphics.DrawString(_TitleText, Font, new SolidBrush(Color.DimGray), ImagePad + 11, 9);
            e.Graphics.DrawString(_TitleText, Font, new SolidBrush(ForeColor), ImagePad + 10, 8);
        }

        private void IsButtonActive(ref UxThemeSysButtonStates ButtonState)
        {
            if (!IsFormActive & ButtonState == UxThemeSysButtonStates.SBS_NORMAL) {
                ButtonState = UxThemeSysButtonStates.SBS_INACTIVE;
            } else if (ButtonState == UxThemeSysButtonStates.SBS_INACTIVE) {
                ButtonState = UxThemeSysButtonStates.SBS_NORMAL;
            }
        }

        #endregion

        #region "Close Button"


        private UxThemeSysButtonStates CloseButtonState = UxThemeSysButtonStates.SBS_NORMAL;
        private void CloseWindowButton_Click(System.Object sender, System.EventArgs e)
        {
            if (_ControlBoxAffects == eControlBoxAffects.ParentForm)
            {
                try {
                    FindForm().Close();
                } catch (Exception) {
                    
                }
            } else {
                if (CloseButtonClicked != null) {
                    CloseButtonClicked();
                }
            }
        }

        private void CloseWindowButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CloseButtonState = UxThemeSysButtonStates.SBS_PUSHED;
            CloseWindowButton.Invalidate();
        }

        private void CloseWindowButton_MouseEnter(object sender, System.EventArgs e)
        {
            CloseButtonState = UxThemeSysButtonStates.SBS_HOT;
            CloseWindowButton.Invalidate();
        }

        private void CloseWindowButton_MouseLeave(object sender, System.EventArgs e)
        {
            CloseButtonState = UxThemeSysButtonStates.SBS_NORMAL;
            CloseWindowButton.Invalidate();
        }


        private void CloseWindowButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CloseButtonState = UxThemeSysButtonStates.SBS_NORMAL;
            CloseWindowButton.Invalidate();
        }

        private void CloseWindowButton_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            IsButtonActive(ref CloseButtonState);
            if (isAppThemed()) {
                intptrWindowTheme = openThemeData(CloseWindowButton.Handle, "Window");
                
                RECT tmp = new RECT(new Rectangle(0, 0, CloseWindowButton.Width, CloseWindowButton.Height));
                drawThemeBackground(intptrWindowTheme, e.Graphics.GetHdc(),(int) UxThemeWindowParts.WP_CLOSEBUTTON,(int) CloseButtonState, ref tmp, IntPtr.Zero);

                e.Graphics.ReleaseHdc();
                CloseThemeData(intptrWindowTheme);
            } else {
                ControlPaint.DrawCaptionButton(e.Graphics, CloseWindowButton.DisplayRectangle, CaptionButton.Close, ConvertThemeToClassic(CloseButtonState));
            }

        }
        #endregion

        #region "Maximize Button"


        private UxThemeSysButtonStates MaximizeButtonState = UxThemeSysButtonStates.SBS_NORMAL;
        private void MaximizeWindowButton_Click(System.Object sender, System.EventArgs e)
        {
            if (_ControlBoxAffects == eControlBoxAffects.ParentForm) {
                if (FindForm().WindowState == FormWindowState.Maximized) {
                    FindForm().WindowState = FormWindowState.Normal;
                } else {
                    FindForm().WindowState = FormWindowState.Maximized;
                }
            } else {
                if (MaximizeButtonClicked != null) {
                    MaximizeButtonClicked();
                }
            }
            FindForm().Invalidate();
            Invalidate();
        }

        private void MaximizeWindowButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MaximizeButtonState = UxThemeSysButtonStates.SBS_PUSHED;
            MaximizeWindowButton.Invalidate();

        }

        private void MaximizeWindowButton_MouseEnter(object sender, System.EventArgs e)
        {
            MaximizeButtonState = UxThemeSysButtonStates.SBS_HOT;
            MaximizeWindowButton.Invalidate();

        }

        private void MaximizeWindowButton_MouseLeave(object sender, System.EventArgs e)
        {
            MaximizeButtonState = UxThemeSysButtonStates.SBS_NORMAL;
            MaximizeWindowButton.Invalidate();

        }

        private void MaximizeWindowButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MaximizeButtonState = UxThemeSysButtonStates.SBS_NORMAL;
            MaximizeWindowButton.Invalidate();

        }

        private void MaximizeWindowButton_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            intptrWindowTheme = openThemeData(MaximizeWindowButton.Handle, "Window");
            IsButtonActive(ref MaximizeButtonState);
            if (FindForm().WindowState == FormWindowState.Maximized) {
                if (isAppThemed()) {
                    RECT tmp = new RECT(new Rectangle(0, 0, MaximizeWindowButton.Width, MaximizeWindowButton.Height));
                    drawThemeBackground(intptrWindowTheme, e.Graphics.GetHdc(), (int)UxThemeWindowParts.WP_RESTOREBUTTON, (int)MaximizeButtonState, ref tmp, IntPtr.Zero);

                    e.Graphics.ReleaseHdc();
                    CloseThemeData(intptrWindowTheme);
                } else {
                    ControlPaint.DrawCaptionButton(e.Graphics, MaximizeWindowButton.DisplayRectangle, CaptionButton.Restore, ConvertThemeToClassic(MaximizeButtonState));
                }
            } else {
                if (isAppThemed()) {
                    RECT tmp = new RECT(new Rectangle(0, 0, MaximizeWindowButton.Width, MaximizeWindowButton.Height));
                    drawThemeBackground(intptrWindowTheme, e.Graphics.GetHdc(), (int)UxThemeWindowParts.WP_MAXBUTTON, Convert.ToInt32((!IsFormActive & MaximizeButtonState == UxThemeSysButtonStates.SBS_NORMAL ? UxThemeSysButtonStates.SBS_INACTIVE : MaximizeButtonState)), ref tmp, IntPtr.Zero);

                    e.Graphics.ReleaseHdc();
                    CloseThemeData(intptrWindowTheme);
                } else {
                    ControlPaint.DrawCaptionButton(e.Graphics, MaximizeWindowButton.DisplayRectangle, CaptionButton.Maximize, ConvertThemeToClassic(MaximizeButtonState));
                }
            }


        }
        #endregion

        #region "Minimize Button"


        private UxThemeSysButtonStates MinimizeButtonState = UxThemeSysButtonStates.SBS_NORMAL;
        private void MinimizeWindowButton_Click(System.Object sender, System.EventArgs e)
        {
            if (_ControlBoxAffects == eControlBoxAffects.ParentForm) {
                FindForm().WindowState = FormWindowState.Minimized;
            } else {
                if (MinimizeButtonClicked != null) {
                    MinimizeButtonClicked();
                }
            }
        }

        private void MinimizeWindowButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MinimizeButtonState = UxThemeSysButtonStates.SBS_PUSHED;
            MinimizeWindowButton.Invalidate();

        }

        private void MinimizeWindowButton_MouseEnter(object sender, System.EventArgs e)
        {
            MinimizeButtonState = UxThemeSysButtonStates.SBS_HOT;
            MinimizeWindowButton.Invalidate();

        }

        private void MinimizeWindowButton_MouseLeave(object sender, System.EventArgs e)
        {
            MinimizeButtonState = UxThemeSysButtonStates.SBS_NORMAL;
            MinimizeWindowButton.Invalidate();

        }

        private void MinimizeWindowButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MinimizeButtonState = UxThemeSysButtonStates.SBS_NORMAL;
            MinimizeWindowButton.Invalidate();

        }


        private void MinimizeWindowButton_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            IsButtonActive(ref MinimizeButtonState);


            if (isAppThemed()) {
                intptrWindowTheme = openThemeData(MinimizeWindowButton.Handle, "Window");
                RECT tmp = new RECT(new Rectangle(0, 0, MinimizeWindowButton.Width, MinimizeWindowButton.Height));
                drawThemeBackground(intptrWindowTheme, e.Graphics.GetHdc(), (int)UxThemeWindowParts.WP_MINBUTTON, (int)MinimizeButtonState, ref tmp, IntPtr.Zero);

                e.Graphics.ReleaseHdc();
                CloseThemeData(intptrWindowTheme);
            } else {
                ControlPaint.DrawCaptionButton(e.Graphics, MinimizeWindowButton.DisplayRectangle, CaptionButton.Minimize, ConvertThemeToClassic(MinimizeButtonState));
            }

        }
        #endregion

    }
}
