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
    /// </summary>
    public class H34RTProvider
    {
        public H34RTProvider()
        {
        }
        
        public static char Key = 'a';
        
        public static string Encrypt(string i)
        {
            return EncryptInternal(i);
        }
        
        private static string EncryptInternal(string i)
        {
            byte[] bytes = Utilities.StringToByte(i);
            for(int i2 = 0; i2 < bytes.Length; i2++)
                bytes[i2] = (byte) (bytes[i2] << Key);
            
            return Utilities.ByteToString(bytes);
        }
        
        private static string DecryptInternal(string i)
        {
            byte[] bytes = Utilities.StringToByte(i);
            for(int i2 = 0; i2 < bytes.Length; i2++)
                bytes[i2] = (byte) (bytes[i2] >> Key);
            
            return Utilities.ByteToString(bytes);
        }
        public static string Decrypt(string i)
        {
            return DecryptInternal(i);
        }
    }
}
