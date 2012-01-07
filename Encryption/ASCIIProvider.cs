using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
namespace IExtendFramework.Encryption
{
    public class ASCIIProvider
    {

        public static string Encrypt(string line, int acode)
        {
            string Encrypted = "";
            char letter = '\0';
            long i = 0;
            long charsInFile = 0;
            //save text with encryption scheme (ASCII code + acode)
            charsInFile = line.Length;
            for (i = 0; i <= charsInFile - 1; i++) {
                letter = line.Substring((int)i, 1).ToCharArray()[0];
                //determine ASCII code and add acode to it
                Encrypted = Encrypted + Strings.Chr(Strings.Asc(letter) + Convert.ToInt32(acode));
            }
            //return encrypted text
            return Encrypted;
        }

        public static string Decrypt(string line, int acode)
        {
            string AllText = null;
            short i = 0;
            short charsInString = 0;
            char letter = '\0';
            string Decrypted = "";

            AllText = line;
            //now, decrypt string by subtracting acode from ASCII code
            charsInString = (short)AllText.Length;
            //get length of string
            //loop once for each char
            for (i = 0; i <= charsInString - 1; i++) {
                letter =AllText.Substring(i, 1).ToCharArray()[0];
                //get character
                Decrypted = Decrypted + Strings.Chr(Strings.Asc(letter) - Convert.ToInt32(acode));
                //subtract acode
            }
            //and build new string
            return Decrypted;
        }
    }
}
