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
    public class XorProvider
    {

        public static string Encrypt(string line, int xcode)
        {
            char letter = '\0';
            string strCode = xcode.ToString();
            short i = 0;
            short charsInString = 0;
            short Code = 0;
            string encrypted = "";


            //save text with encryption scheme
            Code = Convert.ToInt16(strCode);
            charsInString = (short) line.Length;
            for (i = 0; i <= charsInString - 1; i++) {
                letter = line.Substring(i, 1).ToCharArray()[0]; // one character, as a char
                //convert to number w/ Asc, then use Xor to encrypt
                encrypted += (Strings.Asc(letter) ^ Code);
                //and save in file
                //separate numbers with a space
                encrypted += " ";
            }
            return encrypted;
        }

        public static string Decrypt(string line, int xcode)
        {
            string AllText = null;
            short i = 0;
            char ch = '\0';
            string strCode = xcode.ToString();
            short Code = 0;
            short Number = 0;
            string[] Numbers = null;
            string Decrypted = "";

            Code = Convert.ToInt16(strCode);
            //read encrypted numbers
            AllText = line;
            AllText = AllText.Trim();
            //split numbers in to an array based on space
            Numbers = AllText.Split(new char[] {' '}, StringSplitOptions.None);
            //loop through array
            for (i = 0; i <= Numbers.Length - 1; i++) {
                if (string.IsNullOrEmpty(Numbers[i]))
                    continue;
                if (Numbers[i] == "\"")
                    continue;
                Number = Convert.ToInt16(Numbers[i]);
                //convert string to number
                try {
                    ch = Strings.Chr(Number ^ Code);
                    //convert with Xor
                } catch (Exception ex) {
                    Interaction.MsgBox(ex.Message);
                }
                Decrypted = Decrypted + ch;
                //and build string
            }
            return Decrypted;
        }
    }
}

