/*
 * User: elijah
 * Date: 3/6/2012
 * Time: 7:44 AM
 */
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;

namespace IExtendFramework.Win32
{
    /// <summary>
    /// Allows access to a webcam or webcam-like video camera
    /// </summary>
    public static class Webcam
    {
        public const int PREVIEW_RATE = 50; // 50 ms preview rate
        public const short WM_CAP = 0x400;
        public const int WM_CAP_DRIVER_CONNECT = WM_CAP + 10;
        public const int WM_CAP_DRIVER_DISCONNECT = WM_CAP + 11;

        public const int WM_CAP_EDIT_COPY = WM_CAP + 30;
        public const int WM_CAP_SET_PREVIEW = WM_CAP + 50;
        public const int WM_CAP_SET_PREVIEWRATE = WM_CAP + 52;
        public const int WM_CAP_SET_SCALE = WM_CAP + 53;
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;
        public const short SWP_NOMOVE = 0x2;
        public const short SWP_NOSIZE = 1;
        public const short SWP_NOZORDER = 0x4;

        public const short HWND_BOTTOM = 1;
        // Current device ID
        public static int Device = 0;
        // Handle to preview window
        public static int hHwnd;
        
        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, 	[MarshalAs(UnmanagedType.AsAny)]
                                             object lParam);
        [DllImport("user32", EntryPoint = "SetWindowPos", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool DestroyWindow(int hndw);
        
        [DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
        
        [DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);
        
        public static void OpenPreviewWindow(ref PictureBox pictureBox)
        {
            int Height = (int)pictureBox.Height;
            int Width = (int)pictureBox.Width;

            hHwnd = capCreateCaptureWindowA(Device.ToString(), WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, pictureBox.Handle.ToInt32(), 0);

            // Connect to webcam
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, Device, 0) != 0) { // 0 == false
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, PREVIEW_RATE, 0);
                //Start previewing the image from the camera
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);
                // Resize window to fit in picturebox
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, (int)pictureBox.Width, (int)pictureBox.Height, SWP_NOMOVE | SWP_NOZORDER);

            } else {
                // Error connecting to device, so close window
                DestroyWindow(hHwnd);
                pictureBox.BackColor = System.Drawing.Color.Black;
                pictureBox.Refresh();
            }
        }

        public static void ClosePreviewWindow(ref PictureBox pictureBox)
        {
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, Device, 0);
            DestroyWindow(hHwnd);
            pictureBox.Image = null; 
            pictureBox.Refresh();
        }

        public static System.Drawing.Image TakePicture(ref PictureBox pictureBox)
        {
            IDataObject cdata = default(IDataObject);
            System.Drawing.Image image = default(System.Drawing.Image);
            // Copy image to clipboard
            SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
            // Get image from clipboard, convert to Image/Bitmap
            cdata = Clipboard.GetDataObject();
            if (cdata.GetDataPresent(typeof(System.Drawing.Bitmap))) {
                image = (System.Drawing.Image)cdata.GetData(typeof(System.Drawing.Bitmap));
                pictureBox.Image = image;
                //Stop Device Capture
                ClosePreviewWindow(ref pictureBox);
                return image;
            }
            // Wrong clipboard data
            cdata = null;
            return null;
        }
    }
}