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
using System.Runtime.InteropServices;

namespace IExtendFramework.Win32
{
    /// <summary>
    /// Some useful Win32 stuff
    /// </summary>
    public class Win32
    {
        private Win32()
        {
        }
        
        public const int WM_NCLBUTTONDOWN = 0xA1;
        
        public const int HT_CAPTION = 0x2;
        
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        
        public static readonly int WM_SYSCOMMAND = 0x112;
        public static readonly int SC_MINIMIZE = 0xF020;
        
        public static readonly int SC_CLOSE = 0xF060;           //close button's code in windows api
        public static readonly int MF_GRAYED = 0x1;             //disabled button status (enabled = false)
        public static readonly int MF_ENABLED = 0x00000000;     //enabled button status
        public static readonly int MF_DISABLED = 0x00000002;    //disabled button status

        /// <summary>
        /// The LogonUser function attempts to log a user on to the local computer.
        /// The local computer is the computer from which LogonUser was called.
        /// You cannot use LogonUser to log on to a remote computer. You specify 
        /// the user with a user name and domain and authenticate the user with a
        /// plaintext password. If the function succeeds, you receive a handle to 
        /// a token that represents the logged-on user. You can then use this token 
        /// handle to impersonate the specified user or, in most cases, to create 
        /// a process that runs in the context of the specified user.
        /// </summary>
        /// <param name="lpszUsername">he name of the user</param>
        /// <param name="lpszDomain">The name of the domain</param>
        /// <param name="lpszPassword">The user's password</param>
        /// <param name="dwLogonType">The type of logon operation to perform</param>
        /// <param name="dwLogonProvider">The logon provider</param>
        /// <param name="phToken">Token handle of the specified user</param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool LogonUser(
            [MarshalAs(UnmanagedType.LPWStr)] string lpszUsername,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszDomain,            
            IntPtr lpszPassword,          
            LogonType dwLogonType,        
            LogonProvider dwLogonProvider,
            out IntPtr phToken            
        );

        public enum LogonType : int
        {
            /// <summary>
            /// This logon type is intended for users who will be interactively 
            /// using the computer
            /// </summary>
            LOGON32_LOGON_INTERACTIVE = 2,

            // This logon type is intended for high performance servers to 
            // authenticate plaintext passwords
            LOGON32_LOGON_NETWORK = 3,

            // This logon type is intended for batch servers
            LOGON32_LOGON_BATCH = 4,

            // Indicates a service-type logon
            LOGON32_LOGON_SERVICE = 5,

            // This logon type is for GINA DLLs that log on users who will be 
            // interactively using the computer       
            LOGON32_LOGON_UNLOCK = 7,

            // This logon type preserves the name and password in the 
            // authentication package
            LOGON32_LOGON_NETWORK_CLEARTEXT = 8,

            // This logon type allows the caller to clone its current token 
            // and specify new credentials for outbound connections.        
            LOGON32_LOGON_NEW_CREDENTIALS = 9
        }

        public enum LogonProvider : int
        {
            // Use the standard logon provider for the system        
            LOGON32_PROVIDER_DEFAULT = 0,
            // Use the negotiate logon provider
            LOGON32_PROVIDER_WINNT50 = 1,
            // Use the NTLM logon provider
            LOGON32_PROVIDER_WINNT40 = 2
        }
        
        /// <summary>
        /// Locks the computer
        /// </summary>
        [DllImport("user32.dll")]
        public static extern void LockWorkStation();
    }
}
