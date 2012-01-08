/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/6/2012
 * Time: 2:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using IExtendFramework.Encryption;

namespace IExtendFramework
{
    /// <summary>
    /// Contains some extensions
    /// </summary>
    public static class TypeExtensions
    {
        #region String Extensions
        /// <summary>
        /// Formats a string according to the given FormatType
        /// </summary>
        /// <param name="s">this string</param>
        /// <param name="t">the format type</param>
        /// <returns>the new string</returns>
        public static string SpecialFormat(this string s, FormatType t)
        {
            if (t == FormatType.AllUppercase)
                return s.ToUpper();
            if (t == FormatType.AllLowercase)
                return s.ToLower();
            if (t == FormatType.SentenceCase)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return s;
                
                var len = s.Length;
                
                if (len == 1)
                    return s.ToUpper();
                
                if (len > 1)
                {
                    return char.ToUpper(s[0]) + s.Substring(1);
                }
                
                return s;
            }
            if (t == FormatType.TitleCase)
                return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s);
            if (t == FormatType.ToExclamation)
            {
                if (string.IsNullOrEmpty(s))
                    return s;
                
                if (s.EndsWith("!"))
                    return s;
                // other sentence types
                if (s.EndsWith(".") ||  s.EndsWith("?"))
                    return s.Substring(0, s.Length - 1) + "!"; //s.Substring(s.Length - 1, 1);
                
                // anything else
                return s + "!";
            }
            if (t == FormatType.ToQuestion)
            {
                if (string.IsNullOrEmpty(s))
                    return s;
                
                if (s.EndsWith("?"))
                    return s;
                // other sentence types
                if (s.EndsWith(".") ||  s.EndsWith("!"))
                    return s.Substring(0, s.Length - 1) + "?"; //s.Substring(s.Length - 1, 1);
                
                // anything else
                return s + "?";
            }
            if (t == FormatType.ToStatement)
            {
                if (string.IsNullOrEmpty(s))
                    return s;
                
                if (s.EndsWith("."))
                    return s;
                // other sentence types
                if (s.EndsWith("!") ||  s.EndsWith("?"))
                    return s.Substring(0, s.Length - 1) + "!"; //s.Substring(s.Length - 1, 1);
                
                // anything else
                return s + ".";
            }
            if (t == FormatType.AllWordsCapitalizedCase)
            {
                // Capitalize first character
                bool wasLastCharASpace = true;
                string n = "";
                foreach (char c in s)
                {
                    if (wasLastCharASpace)
                    {
                        n += c.ToString().ToUpper();
                        wasLastCharASpace = c == ' ';
                    }
                    else
                    {
                        n += c;
                        wasLastCharASpace = c == ' ';
                    }
                }
                return n;
            }
            if (t == FormatType.CreateAcronym)
            {
                if (s.Length == 0)
                    return "";
                
                // get words
                string[] s2 = s.Split(' ');
                for (int i = 0; i < s2.Length; i++)
                {
                    string tmp = s2[i];
                    while (!char.IsLetter(tmp[0]))
                        tmp = tmp.Substring(1);
                    s2[i] = tmp;
                }
                string result = "";
                foreach (string str in s2)
                {
                    result += str[0];
                }
                return result.SpecialFormat(FormatType.AllUppercase);
            }
            
            throw new NotImplementedException("type '" + t.ToString() + "' not yet implemented!");
        }
        
        /// <summary>
        /// Checks if a string is a palindrome (same read forward and back)
        /// </summary>
        /// <param name="s">this string</param>
        /// <returns>the result.</returns>
        public static bool IsPalindrome(this string s)
        {
            if (s.Length == 1 || s.Length == 0)
                return true;
            if (s.Substring(0, 1).ToLower() == s.Substring(s.Length -1, 1).ToLower())
            {
                if (s.Length == 2)
                    return true;
                else
                    return IsPalindrome(s.Substring(1, s.Length - 2));
            }
            // its not a palindrome
            return false;
        }
        #endregion
        
        #region Object Iterators
        public static IEnumerable<object> Iterate(this object o, IterateType t = IterateType.Object)
        {
            if (t == IterateType.Object)
                yield return o;
            if (t == IterateType.Events)
            {
                EventInfo[] eic = o.GetType().GetEvents();
                foreach (EventInfo ei in eic)
                    yield return ei;
            }
            if (t == IterateType.Methods)
            {
                MethodInfo[] mic = o.GetType().GetMethods();
                foreach (MethodInfo mi in mic)
                    yield return mi;
            }
            if (t == IterateType.Properties)
            {
                PropertyInfo[] pic =o.GetType().GetProperties();
                foreach (PropertyInfo pi in pic)
                    yield return pi;
            }
            if (t == IterateType.Constructors)
                foreach (ConstructorInfo ci in o.GetType().GetConstructors())
                    yield return ci;
            if (t == IterateType.ImplementedInterfaces)
                foreach (Type ty in o.GetType().GetInterfaces())
                    yield return t;
            if (t == IterateType.NestedTypes)
                foreach (Type ty in o.GetType().GetNestedTypes())
                    yield return t;
            if (t == IterateType.IterateValues)
                foreach (object o2 in (o as System.Collections.IEnumerable))
                    yield return o2;
        }
        #endregion
        
        #region Encryption/Decryption
        public static string Encrypt(this string s, EncryptionType e, params object[] args)
        {
            if (e == EncryptionType.ASCII)
                return ASCIIProvider.Encrypt(s, (int) args[0]);
            if (e == EncryptionType.AES)
            {
                if (args.Length >= 2)
                {
                    AESProvider.Key = (byte[]) args[0];
                    AESProvider.IV = (byte[]) args[1];
                }
                return AESProvider.Encrypt(s);
            }
            if (e == EncryptionType.DES)
            {
                if (args.Length >= 2)
                {
                    DESProvider.Key = (byte[]) args[0];
                    DESProvider.IV = (byte[]) args[1];
                }
                return DESProvider.Encrypt(s);
            }
            if (e == EncryptionType.L1F3)
            {
                return Utilities.ByteToString(L1F3Provider.Encrypt(s));
            }
            if (e == EncryptionType.RC2)
            {
                if (args.Length >= 2)
                {
                    RC2Provider.Key = (byte[]) args[0];
                    RC2Provider.IV = (byte[]) args[1];
                }
                return RC2Provider.Encrypt(s);
            }
            if (e == EncryptionType.Rijndael)
            {
                if (args.Length >= 2)
                {
                    RijndaelProvider.Key = (byte[]) args[0];
                    RijndaelProvider.IV = (byte[]) args[1];
                }
                return RijndaelProvider.Encrypt(s);
            }
            if (e == EncryptionType.RSA)
            {
                return RSAProvider.Encrypt(s);
            }
            if (e == EncryptionType.TripleDES)
            {
                if (args.Length >= 2)
                {
                    TripleDESProvider.Key = (byte[]) args[0];
                    TripleDESProvider.IV = (byte[]) args[1];
                }
                return TripleDESProvider.Encrypt(s);
            }
            if (e == EncryptionType.Xor)
                return XorProvider.Encrypt(s, (int) args[0]);
            return null;
        }
        
        public static string Decrypt(this string s, EncryptionType e, params object[] args)
        {
            if (e == EncryptionType.ASCII)
                return ASCIIProvider.Decrypt(s, (int) args[0]);
            if (e == EncryptionType.AES)
            {
                if (args.Length >= 2)
                {
                    AESProvider.Key = (byte[]) args[0];
                    AESProvider.IV = (byte[]) args[1];
                }
                return AESProvider.Decrypt(s);
            }
            if (e == EncryptionType.DES)
            {
                if (args.Length >= 2)
                {
                    DESProvider.Key = (byte[]) args[0];
                    DESProvider.IV = (byte[]) args[1];
                }
                return DESProvider.Decrypt(s);
            }
            if (e == EncryptionType.L1F3)
            {
                return L1F3Provider.Decrypt(Utilities.StringToByte(s));
            }
            if (e == EncryptionType.RC2)
            {
                if (args.Length >= 2)
                {
                    RC2Provider.Key = (byte[]) args[0];
                    RC2Provider.IV = (byte[]) args[1];
                }
                return RC2Provider.Decrypt(s);
            }
            if (e == EncryptionType.Rijndael)
            {
                if (args.Length >= 2)
                {
                    RijndaelProvider.Key = (byte[]) args[0];
                    RijndaelProvider.IV = (byte[]) args[1];
                }
                return RijndaelProvider.Decrypt(s);
            }
            if (e == EncryptionType.RSA)
            {
                return RSAProvider.Decrypt(s);
            }
            if (e == EncryptionType.TripleDES)
            {
                if (args.Length >= 2)
                {
                    TripleDESProvider.Key = (byte[]) args[0];
                    TripleDESProvider.IV = (byte[]) args[1];
                }
                return TripleDESProvider.Decrypt(s);
            }
            if (e == EncryptionType.Xor)
                return XorProvider.Decrypt(s, (int) args[0]);
            return null;
        }
        #endregion
        
        /// <summary>
        /// Fancier tostring
        /// </summary>
        /// <param name="obj">this object</param>
        /// <param name="indent">don't set this. its an internally set value.</param>
        /// <returns>the fancy Tostring output</returns>
        public static string PrettyToString(this object obj, string indent = " ")
        {
            string result = "";
            
            // Type name
            result += (indent == " " ? "" : indent) + "[Type '"+ obj.GetType().FullName + "']\n" + 
                (indent == " " ? "" : indent) +
                 "{\n";
            
            // Check if enumerator/iterator
            IEnumerable ienumberable = obj as IEnumerable;
            if (ienumberable != null)
            {
                result += indent + " IEnumerable Values\n";
                foreach (object o in ienumberable)
                    result += o.PrettyToString(indent + "  ");
            }
            
            // Properties
            result += indent + " Properties: \n";
            PropertyInfo[] pic = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pic)
                result += indent + "  " + pi.Name + " = " + pi.GetValue(obj, new object[] { pi.GetIndexParameters()[0] }) + "\n";
            
            result += indent + " ToString: '" + obj.ToString() + "'\n";
            result += indent + " Hashcode: '" + obj.GetHashCode() + "'\n";
            
            result += (indent == " " ? "" : indent) + "}\n";
            return result;
        }
    }
    
    public enum FormatType
    {
        // Sentence cases
        SentenceCase,
        TitleCase,
        AllWordsCapitalizedCase,
        // General string case
        AllUppercase,
        AllLowercase,
        // Change sentence type
        ToQuestion,
        ToStatement,
        ToExclamation,
        
        // Other random stuff
        CreateAcronym
    }
    
    public enum IterateType
    {
        // Weirdness
        Object,
        // Type infos
        Methods,
        Properties,
        Events,
        Constructors,
        ImplementedInterfaces,
        NestedTypes,
        // actual iteration
        IterateValues
    }
}
