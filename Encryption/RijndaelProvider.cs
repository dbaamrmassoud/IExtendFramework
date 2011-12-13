using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace IExtendFramework.Encryption
{

    public class RijndaelProvider
    {
        // define the rijndael provider

        private RijndaelManaged _Rijndael = new RijndaelManaged();
        // define the string handler

        private UTF8Encoding m_utf8 = new UTF8Encoding();
        // define the local property arrays
        private byte[] m_key;

        private byte[] m_iv;
        public RijndaelProvider(byte[] key, byte[] iv)
        {
            this.m_key = key;
            this.m_iv = iv;
        }

        public byte[] Encrypt(byte[] input)
        {
            return Transform(input, _Rijndael.CreateEncryptor(m_key, m_iv));
        }

        public byte[] Decrypt(byte[] input)
        {
            return Transform(input, _Rijndael.CreateDecryptor(m_key, m_iv));
        }

        public string Encrypt(string text)
        {
            byte[] input = m_utf8.GetBytes(text);
            byte[] output = Transform(input, _Rijndael.CreateEncryptor(m_key, m_iv));
            return m_utf8.GetString(output);
        }

        public string Decrypt(string text)
        {
            byte[] input = m_utf8.GetBytes(text);
            byte[] output = Transform(input, _Rijndael.CreateDecryptor(m_key, m_iv));
            return m_utf8.GetString(output);
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

