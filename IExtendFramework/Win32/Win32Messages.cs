/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/5/2011
 * Time: 5:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace IExtendFramework.Win32
{
    /// <summary>
    /// Some useful Win32 Messages in Hexadecimal
    /// </summary>
    public class Win32Messages
    {
        private Win32Messages()
        {
        }
        
        public static readonly int WM_SYSCOMMAND = 0x112;
        public static readonly int SC_MINIMIZE = 0xF020;
        
        public static readonly int SC_CLOSE = 0xF060;           //close button's code in windows api
        public static readonly int MF_GRAYED = 0x1;             //disabled button status (enabled = false)
        public static readonly int MF_ENABLED = 0x00000000;     //enabled button status
        public static readonly int MF_DISABLED = 0x00000002;    //disabled button status

    }
}
