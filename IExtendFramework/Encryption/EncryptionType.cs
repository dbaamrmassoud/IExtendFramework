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
        [Enums.ReadableEnumAttribute("AES Encryption")]
        AES,
        [Enums.ReadableEnumAttribute("ASCII Encryption")]
        ASCII,
        [Enums.ReadableEnumAttribute("DES Encryption")]
        DES,
        [Enums.ReadableEnumAttribute("L1F3 Encryption")]
        L1F3,
        [Enums.ReadableEnumAttribute("RC2 Encryption")]
        RC2,
        [Enums.ReadableEnumAttribute("Rijndael Encryption")]
        Rijndael,
        [Enums.ReadableEnumAttribute("RSA Encryption")]
        RSA,
        [Enums.ReadableEnumAttribute("TripleDES Encryption")]
        TripleDES,
        [Enums.ReadableEnumAttribute("Xor Encryption")]
        Xor
    }
}
