/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/6/2012
 * Time: 2:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
        }
        #endregion
        
        #region Encryption
        
        public static string Encrypt(this string s, EncryptionType e, params object[] args)
        {
            if (e == EncryptionType.ASCII)
            {
                return ASCIIProvider.Encrypt(s, (int) args[0]);
            }
            return null;
        }
        #endregion
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
        ToExclamation
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
        NestedTypes
    }
}
