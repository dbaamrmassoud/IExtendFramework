using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;

namespace IExtendFramework.Controls
{
    [ToolboxItem(true)]
    public partial class AdvancedNotifyIcon : Component
    {

        #region Fields

        // To indicate if SysTray toolbar was found
        private bool mFound;
        // SysTray toobar handle
        private IntPtr mHWndNotify;
        // Tooltip position
        Point moPosition = new Point();
        // Embedded .Net Control
        private Control moPanel = null;
        // Wrapped NotifyIcon
        private NotifyIcon moNotifyIcon = null;
        // Tooltip native window
        private NativeWindow moNativeWindow = new NativeWindow();
        // Close Button for the tooltip
        private Button moCloseButton = new Button();
        // Window proc delegate
        private WINAPI.WindowProcCallback wpcallback = null;



        #endregion

        #region Events

        /// <summary>
        /// Occurs when NotifyIcon is clicked
        /// </summary>
        public event EventHandler NotifyClicked;

        /// <summary>
        /// Occurs when NotifyIcon is double clicked
        /// </summary>
        public event EventHandler NotifyDoubleClicked;

        /// <summary>
        /// Occurs when the control is clicked with the mouse
        /// </summary>
        public event MouseEventHandler NotifyMouseClicked;

        /// <summary>
        /// Occurs when the control is clicked with the mouse
        /// </summary>
        public event MouseEventHandler NotifyMouseUp;

        /// <summary>
        /// Occurs when the mouse pointer is over the control and the mouse button is pressed
        /// </summary>
        public event MouseEventHandler NotifyMouseDown;

        /// <summary>
        /// Occurs when the mouse ponter is moved over the component
        /// </summary>
        public event MouseEventHandler NotifyMouseMove;

        /// <summary>
        /// Occurs when the mouse pointer is over the control and the mouse button is double clicked
        /// </summary>
        public event MouseEventHandler NotifyMouseDoubleClicked;

        /// <summary>
        /// Occurs when ballon tip is shown
        /// </summary>
        public event EventHandler BalloonTipShown;

        /// <summary>
        /// Occurs when ballon tip is closed
        /// </summary>
        public event EventHandler BallonTipClosed;

        private void OnBalloonTipShown(object sender, EventArgs args)
        {
            if (BalloonTipShown != null)
                BalloonTipShown(sender, args);
        }

        private void OnBalloonTipClosed(object sender, EventArgs args)
        {
            if (BallonTipClosed != null)
                BallonTipClosed(sender, args);
        }

        private void OnNotifyMouseDoubleClicked(object sender, MouseEventArgs args)
        {
            if (NotifyMouseDoubleClicked != null)
                NotifyMouseDoubleClicked(sender, args);
        }

        private void OnNotifyMouseMove(object sender, MouseEventArgs e)
        {
            if (NotifyMouseMove != null)
            {
                NotifyMouseMove(sender, e);
            }
        }

        private void OnNotifyMouseDown(object sender, MouseEventArgs e)
        {
            if (NotifyMouseDown != null)
            {
                NotifyMouseDown(sender, e);
            }
        }


        private void OnNotifyMouseUp(object sender, MouseEventArgs e)
        {
            if (NotifyMouseUp != null)
            {
                NotifyMouseUp(sender, e);
            }
        }

        private void OnNotifyDoubleClicked(object sender, EventArgs args)
        {
            if (NotifyDoubleClicked != null)
                NotifyDoubleClicked(sender, args);
        }

        private void OnMouseNotifyClicked(object sender, MouseEventArgs args)
        {
            if (NotifyMouseClicked != null)
                NotifyMouseClicked(sender, args);
        }

        private void OnNotifyClicked(object sender, EventArgs args)
        {
            if (NotifyClicked != null)
                NotifyClicked(sender, args);
        }

        #endregion

        #region Public Constructors

        public AdvancedNotifyIcon()
        {
            InitializeComponent();

            // Create WinProc for the tip
            wpcallback = new WINAPI.WindowProcCallback(WindowProc);

            // Add handlers to the NotifyIcon
            moNotifyIcon.MouseMove += this.OnNotifyMouseMove;
            moNotifyIcon.MouseDown += this.OnNotifyMouseDown;
            moNotifyIcon.MouseUp += this.OnNotifyMouseUp;
            moNotifyIcon.Click += this.OnNotifyClicked;
            moNotifyIcon.DoubleClick += this.OnNotifyDoubleClicked;
            moNotifyIcon.MouseClick += this.OnMouseNotifyClicked;
            moNotifyIcon.MouseDoubleClick += this.OnNotifyMouseDoubleClicked;

            //Close button for the tip
            moCloseButton.Width = 16;
            moCloseButton.Height = 16;
            moCloseButton.FlatStyle = FlatStyle.Flat;
            moCloseButton.Text = "x";
            moCloseButton.BackColor = Color.LightYellow;
        }

        public AdvancedNotifyIcon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            // Create WinProc for the tip
            wpcallback = new WINAPI.WindowProcCallback(WindowProc);

            // Add handlers to the NotifyIcon
            moNotifyIcon.MouseMove += this.OnNotifyMouseMove;
            moNotifyIcon.MouseDown += this.OnNotifyMouseDown;
            moNotifyIcon.MouseUp += this.OnNotifyMouseUp;
            moNotifyIcon.Click += this.OnNotifyClicked;
            moNotifyIcon.DoubleClick += this.OnNotifyDoubleClicked;
            moNotifyIcon.MouseClick += this.OnMouseNotifyClicked;
            moNotifyIcon.MouseDoubleClick += this.OnNotifyMouseDoubleClicked;

            //Close button for the tip
            moCloseButton.Width = 16;
            moCloseButton.Height = 16;
            moCloseButton.FlatStyle = FlatStyle.Flat;
            moCloseButton.Text = "x";
            moCloseButton.BackColor = Color.LightYellow;

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Panel to be shown inside notify icon balloon.
        /// </summary>
        public Control InsidePanel
        {
            set
            {
                if (value != null)
                {
                    Control panel = (Control)value;
                    panel.BackColor = Color.LightYellow;
                    moPanel = panel;
                }
            }
            get
            {
                return moPanel;
            }
        }

        /// <summary>
        /// Shortcut menu for the icon
        /// </summary>
        public ContextMenu ContextMenu
        {
            get
            {
                return moNotifyIcon.ContextMenu;
            }
            set
            {
                moNotifyIcon.ContextMenu = value;
            }
        }

        [Browsable(true)]
        [Description("The shortcut menu to be shown when the user clicks the icon")]
        public ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return moNotifyIcon.ContextMenuStrip;
            }
            set
            {
                moNotifyIcon.ContextMenuStrip = value;
            }
        }



        [Browsable(true)]
        [Description("The icon to assosiate with NotifyIcon tooltip")]
        public ToolTipIcon BalloonTipIcon
        {
            get
            {
                return moNotifyIcon.BalloonTipIcon;
            }
            set
            {
                moNotifyIcon.BalloonTipIcon = value;
            }
        }

        [Browsable(true)]
        [Description("Title of the tooltip")]
        public String BalloonTipTitle
        {
            get
            {
                return moNotifyIcon.BalloonTipTitle;
            }
            set
            {
                moNotifyIcon.BalloonTipTitle = value;
            }
        }

        [Browsable(true)]
        [Description("The text that will be displayed when the mouse hovers over the icon")]
        public String Text
        {
            get
            {
                return moNotifyIcon.Text;
            }
            set
            {
                moNotifyIcon.Text = value;
            }
        }

        [Browsable(true)]
        public Object Tag
        {
            get
            {
                return moNotifyIcon.Tag;
            }
            set
            {
                moNotifyIcon.Tag = value;
            }
        }


        [Browsable(true)]
        [Description("Current icon")]
        public Icon Icon
        {
            get
            {
                return moNotifyIcon.Icon;
            }
            set
            {
                moNotifyIcon.Icon = value;
            }
        }


        [Browsable(true)]
        [Description("Determines wether notify icon is visible or hidden")]
        public bool Visible
        {
            get
            {
                return moNotifyIcon.Visible;
            }
            set
            {
                moNotifyIcon.Visible = value;
            }
        }

        #endregion

        #region IExtenderProvider Members

        /// <summary>
        /// Make extend forms
        /// </summary>
        /// <param name="extendee"></param>
        /// <returns></returns>
        public bool CanExtend(object extendee)
        {
            if (extendee is Form)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Shows the Ballon tooltip
        /// </summary>
        /// <param name="timeout">Timeout</param>
        /// <param name="tipTitle">The tooltip title</param>
        /// <param name="tipIcon">The tooltip icon</param>
        public void ShowBalloonTip(int timeout, string tipTitle, ToolTipIcon tipIcon)
        {
            BalloonTipIcon = tipIcon;
            BalloonTipTitle = tipTitle;
            ShowBalloonTip(timeout);
        }


        /// <summary>
        /// Shows the ballon tooltip
        /// </summary>
        /// <param name="timeout"></param>
        public void ShowBalloonTip(int timeout)
        {

            //Check if a balloon already is shown or no user control was defined
            if (moNativeWindow.Handle != IntPtr.Zero || moPanel == null)
                return;

            //Get a handle for the notify icon
            IntPtr loNotifyIconHandle = GetHandler(moNotifyIcon);

            if (loNotifyIconHandle == IntPtr.Zero)
                return;

            //Trying to find notify icon rectangle on a desktop
            if (!GetNotifyIconScreenRect(loNotifyIconHandle))
                return;

            //Create parameters for a new tooltip window
            System.Windows.Forms.CreateParams moCreateParams = new System.Windows.Forms.CreateParams();

            // New window is a tooltip and a balloon
            moCreateParams.ClassName = WINAPI.TOOLTIPS_CLASS;
            moCreateParams.Style = WINAPI.WS_POPUP | WINAPI.TTS_NOPREFIX | WINAPI.TTS_ALWAYSTIP | WINAPI.TTS_BALLOON;
            moCreateParams.Parent = loNotifyIconHandle;

            // Create the tooltip window
            moNativeWindow.CreateHandle(moCreateParams);

            //We save old window proc to be used later and replac it our own
            IntPtr loNativeProc = WINAPI.SetWindowLong(moNativeWindow.Handle, WINAPI.GWL_WNDPROC, wpcallback);

            if (loNativeProc == IntPtr.Zero)
                return;

            if (WINAPI.SetProp(moNativeWindow.Handle, "NATIVEPROC", loNativeProc) == 0)
                return;

            // Make tooltip  the top level window
            if (!WINAPI.SetWindowPos(moNativeWindow.Handle, WINAPI.HWND_TOPMOST, 0, 0, 0, 0, WINAPI.SWP_NOACTIVATE | WINAPI.SWP_NOSIZE))
                return;

            // Tool tip info. Set a TRACK flag to show it in specified position
            WINAPI.TOOLINFO ti = new WINAPI.TOOLINFO();
            ti.cbSize = Marshal.SizeOf(ti);
            ti.uFlags = WINAPI.TTF_TRACK;
            ti.hwnd = loNotifyIconHandle;


            //Approximating window size by use of char size
            WINAPI.SIZE size = new WINAPI.SIZE();
            IntPtr hDC = WINAPI.GetDC(moNativeWindow.Handle);

            if (hDC == IntPtr.Zero)
                return;

            if (WINAPI.GetTextExtentPoint32(hDC, ".", 1, ref size) == 0)
                return;

            int lines = 1 + moPanel.Height / size.cy;
            int cols = moPanel.Width / size.cx;

            ti.lpszText = ".";
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < cols; j++)
                    ti.lpszText += ".";
                ti.lpszText += "\r\n";
            }


            //Add tool to the notify icon
            if (WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_ADDTOOL, 0, ref ti) == 0)
                return;

            WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_SETTIPBKCOLOR,
                ColorTranslator.ToWin32(Color.LightYellow), ref ti);

            WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_SETTIPTEXTCOLOR,
                ColorTranslator.ToWin32(Color.Black), ref ti);

            WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_SETDELAYTIME, Duration.AutoPop, 1000);
            WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_TRACKPOSITION, 0, (moPosition.Y << 16) + moPosition.X);
            WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_SETTITLE, (int)BalloonTipIcon, BalloonTipTitle);
            WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_TRACKACTIVATE, 1, ref ti);

            // Raise shown event
            OnBalloonTipShown(this, EventArgs.Empty);

            // Timeout for the tooltip
            moTimer.Interval = timeout;
            moTimer.Start();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets HWND of the control
        /// </summary>
        /// <param name="object"></param>
        /// <returns>window handler</returns>
        private IntPtr GetHandler(Object @object)
        {
            FieldInfo fieldInfo = @object.GetType().GetField("window", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            NativeWindow nativeWindow = (NativeWindow)fieldInfo.GetValue(@object);
            if (nativeWindow.Handle == IntPtr.Zero)
                return IntPtr.Zero;
            return nativeWindow.Handle;
        }

        /// <summary>
        /// Finds Systray toolbar
        /// </summary>
        /// <param name="hwnd">HWND of the window being enumerated</param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected bool EnumChildWindowsFunc(IntPtr hwnd, IntPtr lParam)
        {
            StringBuilder sb = new StringBuilder(256);
            WINAPI.GetClassName(hwnd, sb, sb.Capacity);
            if (sb.ToString().StartsWith("ToolbarWindow32"))
            {
                mHWndNotify = hwnd;
                mFound = true;
                return false;
            }
            mFound = false;
            return true;
        }

        /// <summary>
        /// Gets the NotifyIcon rectangle
        /// </summary>
        /// <param name="handle">Handle of the NotifyIcon</param>
        /// <returns></returns>
        private bool GetNotifyIconScreenRect(IntPtr handle)
        {
            WINAPI.RECT loNotifyIconRecr = new WINAPI.RECT();

            if (handle == IntPtr.Zero)
            {
                return false;
            }

            // Find tray window
            IntPtr hWndTray = WINAPI.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", 0);
            if (hWndTray == IntPtr.Zero)
                return false;

            // Find SysTray toolbar
            WINAPI.EnumChildWindowsCallback callback = new WINAPI.EnumChildWindowsCallback(EnumChildWindowsFunc);
            WINAPI.EnumChildWindows(hWndTray, callback, 0);
            if (!mFound)
                return false;
            mFound = false;

            hWndTray = mHWndNotify;

            // Get a NotifyIcon rectangle and a tip position
            if (WINAPI.GetWindowRect(handle, ref loNotifyIconRecr) == 0)
                return false;

            if (WINAPI.MapWindowPoints(hWndTray, IntPtr.Zero, ref loNotifyIconRecr, 2) == 0)
                return false;

            // Here we should analyze where SysTray happens to be - up, left, right or bottom
            moPosition.X = loNotifyIconRecr.left;
            moPosition.Y = loNotifyIconRecr.top;

            return true;
        }



        /// <summary>
        /// Tooltip window proc
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="iMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected int WindowProc(IntPtr hWnd, UInt32 iMsg, IntPtr wParam, IntPtr lParam)
        {
            int height = 0;
            int width = 0;

            IntPtr nativeProc = WINAPI.GetProp(hWnd, "NATIVEPROC");

            if (iMsg == WINAPI.WM_PAINT)
            {
                // Drow the original tooltip
                int liWinProcResult = WINAPI.CallWindowProc(nativeProc, hWnd, iMsg, wParam, lParam);

                WINAPI.TOOLINFO ti = new WINAPI.TOOLINFO();
                ti.cbSize = Marshal.SizeOf(ti.GetType());
                ti.hwnd = GetHandler(moNotifyIcon);

                if (ti.hwnd != IntPtr.Zero)
                {
                    // Create graphics object for the tooltip
                    Graphics loGraphics = Graphics.FromHwnd(hWnd);

                    WINAPI.RECT rect = new WINAPI.RECT();
                    WINAPI.RECT rect1 = new WINAPI.RECT();

                    WINAPI.GetWindowRect(hWnd, ref rect);

                    // In rect1 - tooltip rectangle
                    // Calculate text display rectangle to rect
                    rect1 = rect;
                    WINAPI.SendMessage(hWnd, WINAPI.TTM_ADJUSTRECT, 0, out rect);

                    // Width and heigth of the text area
                    width = rect.right - rect.left;
                    height = rect.bottom - rect.top;

                    //Calculate iner rectangle of the text area
                    rect.left = rect.left - rect1.left;
                    rect.top = rect.top - rect1.top;
                    rect.right = rect.left + width;
                    rect.bottom = rect.top + height;

                    //Clear text area: 16 pixs - header, 30 pixs from bottom
                    loGraphics.FillRectangle(new SolidBrush(Color.LightYellow), new Rectangle(rect.left, rect.top + 16, width, height-30));

                    //Add close button and click handler
                    // 16 pixs for the button control
                    moCloseButton.Location = new System.Drawing.Point(rect.right - 16, rect.top);
                    moCloseButton.AutoSize = false;
                    moCloseButton.Click += CloseButtonClick;

                    WINAPI.SetParent(moCloseButton.Handle, hWnd);

                    //Adding panel to the tooltip below the header
                    moPanel.Location = new Point(rect.left, rect.top + 16);
                    WINAPI.SetParent(moPanel.Handle, hWnd);

                    loGraphics = null;

                }

                return liWinProcResult;
            }


            return WINAPI.CallWindowProc(nativeProc, hWnd, iMsg, wParam, lParam);
        }
        
        /// <summary>
        /// Closes the tooltip
        /// </summary>
        private void CloseToolTip()
        {
            WINAPI.TOOLINFO ti = new WINAPI.TOOLINFO();
            ti.cbSize = Marshal.SizeOf(ti.GetType());
            ti.hwnd = GetHandler(moNotifyIcon);
            WINAPI.SendMessage(moNativeWindow.Handle, WINAPI.TTM_DELTOOL, 0, ref ti);
            OnBalloonTipClosed(this, EventArgs.Empty);
            moNativeWindow = new NativeWindow();
        }

        /// <summary>
        /// Closes tooltip on close button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButtonClick(object sender, EventArgs e)
        {
            CloseToolTip();
        }

        /// <summary>
        /// Closes tooltip when timeout happens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moTimer_Tick(object sender, EventArgs e)
        {
            moTimer.Stop();
            CloseToolTip();
        }

        #endregion

        private struct Duration
        {
            public const int Automatic = 0;
            public const int Reshow = 1;
            public const int AutoPop = 2;
            public const int Initial = 3;
        }

    }


}
