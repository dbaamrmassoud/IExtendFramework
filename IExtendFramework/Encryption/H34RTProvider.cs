/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/18/2012
 * Time: 3:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.Encryption
{
    /// <summary>
    /// H34RT is an encryption I have made, using bit operations and a char... 
    /// It is mostly for experimentation and learning
    /// </summary>
    public class H34RTProvider
    {
        private H34RTProvider()
        {
        }
        
        /// <summary>
        /// Does not work well with more than 1 character though...
        /// </summary>
        public static AdvancedString Key = "a";
        
        public static int[] Encrypt(string i)
        {
            return EncryptInternal(i);
        }
        
        private static int[] EncryptInternal(string i)
        {
            int[] bytes = new int[i.Length];
            byte[] b1 = Utilities.StringToByte(i);
            for (int i2 = 0; i2 < b1.Length; i2++)
                bytes[i2] = b1[i2];
            
            int keyIndex = 0;
            for(int i2 = 0; i2 < bytes.Length; i2++)
            {
                int b = bytes[i2];
                char c = Key[keyIndex > Key.Length - 1 ? Key.Length - 1 : keyIndex++];
                //foreach (char c in Key)
                //{
                b = (int)(b << c);
                b = (int)(b ^ (c ^ c));
                //}
                
                bytes[i2] = b;
            }
            
            //return Utilities.ByteToString(bytes);
            return bytes;
        }
        
        private static string DecryptInternal(int[] i)
        {
            //byte[] bytes = Utilities.StringToByte(i);
            int[] bytes = i;
            byte[] ret = new byte[i.Length];
            int keyIndex = 0;
            for(int i2 = 0; i2 < bytes.Length; i2++)
            {
                int b = bytes[i2];
                char c = Key[keyIndex > Key.Length - 1 ? Key.Length - 1 : keyIndex++];
                //foreach (char c in Key.Reverse())
                //{
                b = (int)((c ^ c) ^ b);
                b = (int)(b >> c);
                //}
                
                // unchecked... may cause issues
                ret[i2] = unchecked((byte)b);
            }
            
            return Utilities.ByteToString(ret);
        }
        public static string Decrypt(int[] i)
        {
            return DecryptInternal(i);
        }
    }
}
