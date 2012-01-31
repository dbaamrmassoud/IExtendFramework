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

    public class TripleDESProvider
    {

        // define the triple des provider

        private static TripleDESCryptoServiceProvider _tripleDES = new TripleDESCryptoServiceProvider();
        // define the string handler

        private static UTF32Encoding utf32 = new UTF32Encoding();
        // define the local property arrays
        public static byte[] Key = SampleObjects.CreateTripleDESKey();

        public static byte[] IV = SampleObjects.CreateTripleDESIV();

        public static byte[] Encrypt(byte[] input)
        {
            return Transform(input, _tripleDES.CreateEncryptor(Key, IV));
        }

        public static byte[] Decrypt(byte[] input)
        {
            return Transform(input, _tripleDES.CreateDecryptor(Key, IV));
        }

        public static string Encrypt(string text)
        {
            byte[] input = utf32.GetBytes(text);
            byte[] output = Transform(input, _tripleDES.CreateEncryptor(Key, IV));
            return utf32.GetString(output);
        }

        public static string Decrypt(string text)
        {
            byte[] input = utf32.GetBytes(text);
            byte[] output = Transform(input, _tripleDES.CreateDecryptor(Key, IV));
            return utf32.GetString(output);
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
