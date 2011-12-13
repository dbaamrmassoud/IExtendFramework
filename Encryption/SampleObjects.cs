using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Security.Cryptography;
namespace IExtendFramework.Encryption
{

    public class SampleObjects
    {

        //*************************
        //** Create A Key
        //*************************

        public byte[] CreateRijndaelKeyWithSHA512(string strPassword)
        {
            //Convert strPassword to an array and store in chrData.
            char[] chrData = strPassword.ToCharArray();
            //Use intLength to get strPassword size.
            int intLength = chrData.GetUpperBound(0);
            //Declare bytDataToHash and make it the same size as chrData.
            byte[] bytDataToHash = new byte[intLength + 1];

            //Use For Next to convert and store chrData into bytDataToHash.
            for (int i = 0; i <= chrData.GetUpperBound(0); i++) {
                bytDataToHash[i] = Convert.ToByte(Strings.Asc(chrData[i]));
            }

            //Declare what hash to use.
            System.Security.Cryptography.SHA512Managed SHA512 = new System.Security.Cryptography.SHA512Managed();
            //Declare bytResult, Hash bytDataToHash and store it in bytResult.
            byte[] bytResult = SHA512.ComputeHash(bytDataToHash);
            //Declare bytKey(31).  It will hold 256 bits.
            byte[] bytKey = new byte[32];

            //Use For / Next to put a specific size (256 bits) of
            //bytResult into bytKey. The 0 To 31 will put the first 256 bits
            //of 512 bits into bytKey.
            for (int i = 0; i <= 31; i++) {
                bytKey[i] = bytResult[i];
            }

            return bytKey;
            //Return the key.
        }

        //This gets a key without SHA512 hashing
        public byte[] CreateRijndaelKeyWithoutSHA512(string strPassword)
        {
            byte[] bytKey = null;
            byte[] bytSalt = System.Text.Encoding.ASCII.GetBytes("saltsalt");
            Rfc2898DeriveBytes pdb2 = new Rfc2898DeriveBytes(strPassword, bytSalt);
            bytKey = pdb2.GetBytes(32);

            return bytKey;
            //Return the key.
        }

        //*************************
        //** Create An IV
        //*************************

        public byte[] CreateRijndaelIVWithSHA512(string strPassword)
        {
            //Convert strPassword to an array and store in chrData.
            char[] chrData = strPassword.ToCharArray();
            //Use intLength to get strPassword size.
            int intLength = chrData.GetUpperBound(0);
            //Declare bytDataToHash and make it the same size as chrData.
            byte[] bytDataToHash = new byte[intLength + 1];

            //Use For Next to convert and store chrData into bytDataToHash.
            for (int i = 0; i <= chrData.GetUpperBound(0); i++) {
                bytDataToHash[i] = Convert.ToByte(Strings.Asc(chrData[i]));
            }

            //Declare what hash to use.
            System.Security.Cryptography.SHA512Managed SHA512 = new System.Security.Cryptography.SHA512Managed();
            //Declare bytResult, Hash bytDataToHash and store it in bytResult.
            byte[] bytResult = SHA512.ComputeHash(bytDataToHash);
            //Declare bytIV(15).  It will hold 128 bits.
            byte[] bytIV = new byte[16];

            //Use For Next to put a specific size (128 bits) of bytResult into bytIV.
            //The 0 To 30 for bytKey used the first 256 bits of the hashed password.
            //The 32 To 47 will put the next 128 bits into bytIV.
            for (int i = 32; i <= 47; i++) {
                bytIV[i - 32] = bytResult[i];
            }

            return bytIV;
            //Return the IV.
        }

        //This gets an IV without SHA512 hashing
        public byte[] CreateRijndaelIVWithoutSHA512(string strPassword)
        {
            byte[] bytIV = null;
            byte[] bytSalt = System.Text.Encoding.ASCII.GetBytes("saltsalt");
            Rfc2898DeriveBytes pdb2 = new Rfc2898DeriveBytes(strPassword, bytSalt);
            bytIV = pdb2.GetBytes(16);

            return bytIV;
            //Return the IV.
        }

        public byte[] CreateDESKey()
        {
            byte[] R = {
                24,
                244,
                230,
                15,
                145,
                57,
                192,
                86
            };
            return R;
        }

        public byte[] CreateDESIV()
        {
            byte[] R = {
                158,
                133,
                174,
                222,
                231,
                182,
                216,
                64
            };
            return R;
        }

        public byte[] CreateTripleDESKey()
        {
            byte[] R = {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24
            };
            return R;
        }

        public byte[] CreateTripleDESIV()
        {
            byte[] R = {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            return R;
        }

        public byte[] CreateAESKey()
        {
            byte[] R = {
                168,
                34,
                103,
                160,
                7,
                33,
                183,
                125,
                192,
                218,
                32,
                187,
                63,
                166,
                174,
                234,
                156,
                207,
                144,
                59,
                212,
                234,
                196,
                244,
                79,
                140,
                91,
                7,
                160,
                115,
                95,
                116
            };
            return R;
        }

        public byte[] CreateAESIV()
        {
            byte[] R = {
                178,
                138,
                133,
                117,
                178,
                81,
                230,
                107,
                192,
                243,
                56,
                167,
                198,
                8,
                52,
                32
            };
            return R;
        }

        public byte[] CreateRC2Key()
        {
            byte[] R = {
                66,
                96,
                143,
                243,
                22,
                64,
                28,
                97,
                127,
                15,
                1,
                185,
                165,
                197,
                5,
                55
            };
            return R;
        }

        public byte[] CreateRC2IV()
        {
            byte[] R = {
                177,
                89,
                140,
                245,
                239,
                136,
                238,
                147
            };
            return R;
        }
    }
}
