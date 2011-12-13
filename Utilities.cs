/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/5/2011
 * Time: 1:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace IExtendFramework
{
    /// <summary>
    /// Useful functions
    /// </summary>
    public class Utilities
    {
        private Utilities()
        {
        }
        
        public static string FormatByte(long bytes)
        {
            string result = String.Empty;
            
            if (bytes < 1024)
                result = String.Format("{0} B", bytes);
            else if (bytes < 1024 * 1024)
                result = String.Format("{0:0.00} KB", (float)bytes / (1024));
            else if (bytes < 1024 * 1024 * 1024)
                result = String.Format("{0:0.00} MB", (float)bytes / (1024 * 1024));
            else
                result = String.Format("{0:0.00} GB", (float)bytes / (1024 * 1024 * 1024));

            return result;
        }

        public static string FormatSpeed(long bytes)
        {
            return FormatByte(bytes) + "/s";
        }

        public static string Hash(string fileName)
        {
            string result = string.Empty;
            using (System.IO.FileStream fs = System.IO.File.OpenRead(fileName))
            {
                System.Security.Cryptography.MD5 sscMD5 = System.Security.Cryptography.MD5.Create();
                
                byte[] mHash = sscMD5.ComputeHash(fs);
                
                result = Convert.ToBase64String(mHash);
            }
            return result;
        }

        public static void SafeFileDelete(string path)
        {
            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
        }

        public static void SafeDirectoryDelete(string path, bool recursive)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }
    }
}
