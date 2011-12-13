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

        private TripleDESCryptoServiceProvider _tripleDES = new TripleDESCryptoServiceProvider();
        // define the string handler

        private UTF8Encoding _utf8 = new UTF8Encoding();
        // define the local property arrays
        private byte[] _key;

        private byte[] _iv;
        public TripleDESProvider(byte[] key, byte[] iv)
        {
            this._key = key;
            this._iv = iv;
        }

        public byte[] Encrypt(byte[] input)
        {
            return Transform(input, _tripleDES.CreateEncryptor(_key, _iv));
        }

        public byte[] Decrypt(byte[] input)
        {
            return Transform(input, _tripleDES.CreateDecryptor(_key, _iv));
        }

        public string Encrypt(string text)
        {
            byte[] input = _utf8.GetBytes(text);
            byte[] output = Transform(input, _tripleDES.CreateEncryptor(_key, _iv));
            return _utf8.GetString(output);
        }

        public string Decrypt(string text)
        {
            byte[] input = _utf8.GetBytes(text);
            byte[] output = Transform(input, _tripleDES.CreateDecryptor(_key, _iv));
            return _utf8.GetString(output);
        }

        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
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
