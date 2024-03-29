using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace IExtendFramework.Encryption
{
    public class AESProvider
    {
        // define the AES
        
        private static AesCryptoServiceProvider AESCryptoServiceProvider = new AesCryptoServiceProvider();
        // define the string handler
        
        private static UTF8Encoding utf8 = new UTF8Encoding();
        // define the local property arrays
        public static byte[] Key = SampleObjects.CreateAESKey();
        
        public static byte[] IV = SampleObjects.CreateAESIV();
        
        public static byte[] Encrypt(byte[] input)
        {
            return Transform(input, AESCryptoServiceProvider.CreateEncryptor(Key, IV));
        }
        
        public static byte[] Encrypt(byte[] input, byte[] key)
        {
            byte[] tmp = Key;
            Key = key;
            byte[] ret = Encrypt(input);
            Key = tmp;
            return ret;
        }
        
        public static byte[] Encrypt(byte[] input, string key)
        {
            byte[] tmp = Key;
            Key = utf8.GetBytes(key);
            byte[] ret = Encrypt(input);
            Key = tmp;
            return ret;
        }
        
        public static byte[] Decrypt(byte[] input)
        {
            return Transform(input, AESCryptoServiceProvider.CreateDecryptor(Key, IV));
        }
        
        public static string Encrypt(string text)
        {
            byte[] input = utf8.GetBytes(text);
            byte[] output = Transform(input, AESCryptoServiceProvider.CreateEncryptor(Key, IV));
            return utf8.GetString(output);
        }
        
        public static string Decrypt(string text)
        {
            byte[] input = utf8.GetBytes(text);
            byte[] output = Transform(input, AESCryptoServiceProvider.CreateDecryptor(Key, IV));
            return utf8.GetString(output);
        }
        
        public static byte[] Decrypt(byte[] input, string key)
        {
            byte[] tmp = Key;
            Key = utf8.GetBytes(key);
            byte[] ret = Decrypt(input);
            Key = tmp;
            return ret;
        }
        
        public static byte[] Decrypt(byte[] input, byte[] key)
        {
            byte[] tmp = Key;
            Key = key;
            byte[] ret = Decrypt(input);
            Key = tmp;
            return ret;
        }
        
        private static byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            // create the necessary streams
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);
            // transform the bytes as requested
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();
            // Read the memory stream and convert it back into byte array
            memStream.Position = 0;
            byte[] result = new byte[Convert.ToInt32(memStream.Length - 1) + 1];
            memStream.Read(result, 0, Convert.ToInt32(result.Length));
            // close and release the streams
            memStream.Close();
            cryptStream.Close();
            // hand back the encrypted buffer
            return result;
        }
    }
}

