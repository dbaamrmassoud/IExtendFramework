/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/6/2012
 * Time: 5:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.Encryption
{
    /// <summary>
    /// An enum of all provided encryption types
    /// </summary>
    public enum EncryptionType
    {
        [Enums.EnumString("AES Encryption")]
        AES,
        [Enums.EnumString("ASCII Encryption")]
        ASCII,
        [Enums.EnumString("DES Encryption")]
        DES,
        [Enums.EnumString("L1F3 Encryption")]
        L1F3,
        [Enums.EnumString("RC2 Encryption")]
        RC2,
        [Enums.EnumString("Rijndael Encryption")]
        Rijndael,
        [Enums.EnumString("RSA Encryption")]
        RSA,
        [Enums.EnumString("TripleDES Encryption")]
        TripleDES,
        [Enums.EnumString("Xor Encryption")]
        Xor
    }
}
