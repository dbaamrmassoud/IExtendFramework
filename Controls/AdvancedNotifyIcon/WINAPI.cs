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
		internal sealed class WINAPI
		{
			//Declaration 

            public delegate bool EnumChildWindowsCallback(IntPtr hwnd, IntPtr lParam);
            public delegate int WindowProcCallback(IntPtr hWnd, UInt32 iMsg, IntPtr wParam, IntPtr lParam);

			public const int WM_USER = 0x0400;
			public const int WM_NOTIFY = 0x004E;
			public const string TOOLTIPS_CLASS = "tooltips_class32";
			public const int TTS_ALWAYSTIP = 0x01;
			public const int TTS_NOPREFIX = 0x02;
			public const int TTS_BALLOON = 0x40;
			public const int WS_POPUP = unchecked((int)0x80000000);
			public const int TTF_SUBCLASS = 0x0010;
			public const int TTF_TRANSPARENT = 0x0100;
			public const int TTF_CENTERTIP = 0x0002;
			public const int TTF_LEFTTIP =0x0002;
			public const int TTF_TRACK = 0x0020;
			public const int TTF_ABSOLUTE = 0x0080;
			public const int TTF_PARSELINKS = 0x01000;
			public const int TTM_ADDTOOL = 0x0400 + 50;
            public const int TTM_DELTOOL = 0x0400 + 51;
            public const int TTM_SETTIPBKCOLOR = 0x0400 + 19;
			public const int TTM_SETTIPTEXTCOLOR = 0x0400 + 20;
			public const int TTM_WINDOWFROMPOINT = 0x0400 + 21;
			public const int TTM_SETDELAYTIME = WINAPI.WM_USER + 3;
			public const int TTM_TRACKACTIVATE = WM_USER + 17;
			public const int TTM_TRACKPOSITION = WM_USER + 18;
			public const int TTM_SETTITLE = WM_USER + 32;
			public const int TTM_ADJUSTRECT = WM_USER + 31;
			public const int TTM_SETTOOLINFO = WM_USER + 54;
			public const int TTM_GETMARGIN = WM_USER + 27;
			public const int TTM_GETBUBBLESIZE = WM_USER + 30;
			public const int TTM_GETTOOLINFO = WM_USER + 53;
			public const int TTM_NEWTOOLRECT = WM_USER + 52;
			public const int TTM_GETTEXT = WM_USER + 56;
			public const int ICC_WIN95_CLASSES = 0x000000FF;
			public const int SWP_NOSIZE = 0x0001;
			public const int SWP_NOMOVE = 0x0000;
			public const int SWP_NOACTIVATE = 0x0010;
			public const int PROCESS_ALL_ACCESS = 0x1F0FFF;
			public const int TB_GETBUTTON = (WM_USER + 23);
			public const int GWL_WNDPROC = (-4);
			public const int WM_PAINT = 0x000F;
			public const int WM_ERASEBKGND = 0x0014;
			public const UInt32 TB_BUTTONCOUNT = (WM_USER + 24);
			public const UInt32 MEM_COMMIT = 0x1000;
			public const UInt32 PAGE_READWRITE = 0x04;
			public const UInt32 TBSTATE_HIDDEN = 0x08;
			public const UInt32 TB_HIDEBUTTON = (WM_USER + 4);
			public const UInt32 TB_GETITEMRECT = (WM_USER + 29);
			public const UInt32 GWL_DLGPROC = 4;
			public const UInt32 SW_SHOW = 5;
			public const string IDI_APPLICATION = "#32512";
			public const string IDI_HAND = "#32513";
			public const string IDI_QUESTION = "#32514";
			public const string IDI_EXCLAMATION = "#32515";
			public const string IDI_ASTERISK = "#32516";
			public const int ICC_LINK_CLASS =0x00008000;
			public const int WS_VISIBLE = 0x10000000;
			public const int WS_CHILD = 0x40000000;
			public const int TTN_FIRST = -520;
			public const int TTN_NEEDTEXT = (TTN_FIRST - 10);
			public const int SPI_GETNONCLIENTMETRICS = 0x0029;


			public readonly static IntPtr HWND_TOPMOST = new IntPtr(-1);


			//Structure for rectangle

			[StructLayout(LayoutKind.Sequential)]
			public struct RECT
			{
				public int left;
				public int top;
				public int right;
				public int bottom;
			}


			[StructLayout(LayoutKind.Sequential)]
			public struct TOOLINFO
			{
				public int cbSize;
				public int uFlags;
				public IntPtr hwnd;
				public IntPtr uId;
				public RECT rect;
				public IntPtr hinst;
				[MarshalAs(UnmanagedType.LPTStr)]
				public string lpszText;
				public System.UInt32 lParam;

			}

			[StructLayout(LayoutKind.Sequential)]
			public struct SIZE
			{
				public int cx;
				public int cy;
			}


			[StructLayout(LayoutKind.Sequential)]
			public struct PAINTSTRUCT
			{
				public IntPtr hdc;
				public int fErase;
				public RECT rcPaint;
				public int fRestore;
				public int fIncUpdate;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                public System.Byte[] rgbReserved;
			}


			[DllImport("User32", SetLastError=true)]
			public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, int lpszName);

			[DllImport("User32", SetLastError=true)]
			public static extern int GetWindowRect(IntPtr hWnd, ref RECT lpRect);

			[DllImport("User32", SetLastError=true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref TOOLINFO lParam);

			[DllImport("User32", SetLastError=true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, out WINAPI.RECT lParam);

			[DllImport("user32")]
			public static extern int EnumChildWindows(IntPtr hWnd, EnumChildWindowsCallback callback, int lParam);

			[DllImport("user32.dll")]
			public static extern bool GetClassName(IntPtr hWnd, StringBuilder className, int maxCount); // Flags.

			[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
			public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref RECT lpPoints, UInt32 cPoints);


			[DllImport("User32", SetLastError=true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

			[DllImport("User32", SetLastError=true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

            [DllImport("User32", SetLastError=true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

			[DllImport("User32", SetLastError=true)]
			public static extern int SetProp(IntPtr hWnd, string lpString, IntPtr lpData);

			[DllImport("User32", SetLastError=true)]
			public static extern IntPtr GetProp(IntPtr hWnd, string lpString);

			[DllImport("User32", SetLastError=false)]
			public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, WindowProcCallback callback);

			[DllImport("User32", SetLastError=false)]
			public static extern int CallWindowProc(IntPtr wndProc, IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

			[DllImport("User32", SetLastError=true)]
			public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

			[DllImport("User32")]
			public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

			[DllImport("User32")]
			public static extern IntPtr GetDC(IntPtr hWnd);

		    [DllImport("Gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		    public static extern int GetTextExtentPoint32(IntPtr hDC, string String, int stringSize, ref WINAPI.SIZE size);


			private WINAPI()
			{
			}

		}

}
