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
using System.Security.AccessControl;
using System.Text;

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
        
        public static string FormatByteToSize(long bytes)
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
            return FormatByteToSize(bytes) + "/s";
        }

        public static string MD5Hash(string fileName)
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
            {
                Directory.Delete(path, recursive);
            }
        }
        
        public static string ByteToString(byte[] i)
        {
           return new UTF32Encoding().GetString(i);
        }
        
        public static byte[] StringToByte(string i)
        {
            return new UTF32Encoding().GetBytes(i.ToCharArray());
        }
        
        public static string PrettyByteToString(byte[] i)
        {
            string o = "{ ";
            // stick all these bytes in here
            foreach (byte b in i)
                o += b.ToString() + ", ";
            // remove last ', '
            o = o.Substring(0, o.LastIndexOf(","));
            o += " }";
            return o;
        }
    }
}
