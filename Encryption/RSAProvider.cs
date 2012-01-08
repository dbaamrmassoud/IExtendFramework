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

    public class RSAProvider
    {

        // define the RSA provider

        private static RSACryptoServiceProvider m_rsa = new RSACryptoServiceProvider();
        // define the string handler

        private static UTF8Encoding m_utf8 = new UTF8Encoding();

        public static string Encrypt(string text)
        {
            byte[] input = m_utf8.GetBytes(text);
            byte[] output = m_rsa.Encrypt(input, false); // no OAEP padding
            return m_utf8.GetString(output);
        }

        public static byte[] Encrypt(byte[] input)
        {
            byte[] output = m_rsa.Encrypt(input, false); // no OAEP padding
            return output;
        }

        public static string Decrypt(string text)
        {
            byte[] input = m_utf8.GetBytes(text);
            byte[] output = null;
            output = m_rsa.Decrypt(input, false); // no OAEP padding
            return m_utf8.GetString(output);
        }

        public static byte[] Decrypt(byte[] input)
        {
            byte[] output = m_rsa.Decrypt(input, false); // no OAEP Padding
            return output;
        }
    }
}
