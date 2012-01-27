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
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using IExtendFramework.Encryption;

namespace IExtendFramework
{
    /// <summary>
    /// Contains some extensions
    /// </summary>
    public static class TypeExtensions
    {
        public static AdvancedString ToAdvancedString(this object o)
        {
            if (o is AdvancedString)
                return o as AdvancedString;
            
            return AdvancedString.From(o.ToString());
        }
        
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
                    return s.Substring(0, s.Length - 1) + "."; //s.Substring(s.Length - 1, 1);
                
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
            
            throw new NotImplementedException("type '" + t.ToString() + "' not implemented or exitant!");
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
        
        
        /// <summary>
        /// Remove leading strings with zeros and adjust for singular/plural
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="previousStr">The previous string.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static string FormatString(this string str, string previousStr, int t) {
            if ((t == 0) && (previousStr.Length == 0))
                return String.Empty;

            string suffix = (t == 1) ? String.Empty : "s";
            return String.Concat(t, " ", str, suffix, " ");
        }
        
        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromEnd">The remove from end.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString, int removeFromEnd) {
            string result = sourceString;
            if ((removeFromEnd > 0) && (sourceString.Length > removeFromEnd - 1))
                result = result.Remove(sourceString.Length - removeFromEnd, removeFromEnd);
            return result;
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="backDownTo">The back down to.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString, string backDownTo) {
            int removeDownTo = sourceString.LastIndexOf(backDownTo);
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = sourceString.Length - removeDownTo;

            string result = sourceString;

            if (sourceString.Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);

            return result;
        }

        /// <summary>
        /// Plurals to singular.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string PluralToSingular(this string sourceString) {
            return sourceString.MakeSingular();
        }

        /// <summary>
        /// Singulars to plural.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string SingularToPlural(this string sourceString) {
            return sourceString.MakePlural();
        }

        /// <summary>
        /// Make plural when count is not one
        /// </summary>
        /// <param name="number">The number of things</param>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Pluralize(this int number, string sourceString) {
            if (number == 1)
                return String.Concat(number, " ", sourceString.MakeSingular());
            return String.Concat(number, " ", sourceString.MakePlural());
        }

        /// <summary>
        /// Removes the specified chars from the beginning of a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromBeginning">The remove from beginning.</param>
        /// <returns></returns>
        public static string Clip(this string sourceString, int removeFromBeginning) {
            string result = sourceString;
            if (sourceString.Length > removeFromBeginning)
                result = result.Remove(0, removeFromBeginning);
            return result;
        }

        /// <summary>
        /// Removes chars from the beginning of a string, up to the specified string
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeUpTo">The remove up to.</param>
        /// <returns></returns>
        public static string Clip(this string sourceString, string removeUpTo) {
            int removeFromBeginning = sourceString.IndexOf(removeUpTo);
            string result = sourceString;

            if (sourceString.Length > removeFromBeginning && removeFromBeginning > 0)
                result = result.Remove(0, removeFromBeginning);

            return result;
        }
        
        /// <summary>
        /// Returns text that is located between the startText and endText tags.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="startText">The text from which to start the crop</param>
        /// <param name="endText">The endpoint of the crop</param>
        /// <returns></returns>
        public static string Crop(this string sourceString, string startText, string endText) {
            int startIndex = sourceString.IndexOf(startText, StringComparison.CurrentCultureIgnoreCase);
            if (startIndex == -1)
                return String.Empty;

            startIndex += startText.Length;
            int endIndex = sourceString.IndexOf(endText, startIndex, StringComparison.CurrentCultureIgnoreCase);
            if (endIndex == -1)
                return String.Empty;

            return sourceString.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Removes excess white space in a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string RemoveExcessWhiteSpace(this string sourceString) {
            char[] delim = { ' ' };
            string[] lines = sourceString.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string s in lines) {
                if (!String.IsNullOrEmpty(s.Trim()))
                    sb.Append(s + " ");
            }
            //remove the last pipe
            string result = Chop(sb.ToString(), 1);
            return result.Trim();
        }

        /// <summary>
        /// Removes all non-alpha numeric characters in a string
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string ToAlphaNumericOnly(this string sourceString) {
            return Regex.Replace(sourceString, @"\W*", "");
        }
        
        /// <summary>
        /// Strips all HTML tags from a string
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <returns></returns>
        public static string StripHTML(this string htmlString) {
            return StripHTML(htmlString, String.Empty);
        }

        /// <summary>
        /// Strips all HTML tags from a string and replaces the tags with the specified replacement
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <param name="htmlPlaceHolder">The HTML place holder.</param>
        /// <returns></returns>
        public static string StripHTML(this string htmlString, string htmlPlaceHolder) {
            const string pattern = @"<(.|\n)*?>";
            string sOut = Regex.Replace(htmlString, pattern, htmlPlaceHolder);
            sOut = sOut.Replace("&nbsp;", String.Empty);
            sOut = sOut.Replace("&amp;", "&");
            sOut = sOut.Replace("&gt;", ">");
            sOut = sOut.Replace("&lt;", "<");
            return sOut;
        }

        public static List<string> FindMatches(this string source, string find) {
            Regex reg = new Regex(find, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);

            List<string> result = new List<string>();
            foreach (Match m in reg.Matches(source))
                result.Add(m.Value);
            return result;
        }

        /// <summary>
        /// Converts a generic List collection to a single comma-delimitted string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static string ToDelimitedList(this IEnumerable<string> list) {
            return ToDelimitedList(list, ",");
        }

        /// <summary>
        /// Converts a generic List collection to a single string using the specified delimitter.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ToDelimitedList(this IEnumerable<string> list, string delimiter) {
            StringBuilder sb = new StringBuilder();
            foreach (string s in list)
                sb.Append(String.Concat(s, delimiter));
            string result = sb.ToString();
            result = Chop(result, 1);
            return result;
        }

        /// <summary>
        /// Strips the specified input.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="stripValue">The strip value.</param>
        /// <returns></returns>
        public static string Strip(this string sourceString, string stripValue) {
            if (!String.IsNullOrEmpty(stripValue)) {
                string[] replace = stripValue.Split(new[] { ',' });
                for (int i = 0; i < replace.Length; i++) {
                    if (!String.IsNullOrEmpty(sourceString))
                        sourceString = Regex.Replace(sourceString, replace[i], String.Empty);
                }
            }
            return sourceString;
        }

        /// <summary>
        /// Converts ASCII encoding to Unicode
        /// </summary>
        /// <param name="asciiCode">The ASCII code.</param>
        /// <returns></returns>
        public static string AsciiToUnicode(this int asciiCode) {
            Encoding utf = Encoding.UTF32;
            char c = (char)asciiCode;
            Byte[] b = utf.GetBytes(c.ToString());
            return utf.GetString((b));
        }



        /// <summary>
        /// Formats the args using String.Format with the target string as a format string.
        /// </summary>
        /// <param name="fmt">The format string passed to String.Format</param>
        /// <param name="args">The args passed to String.Format</param>
        /// <returns></returns>
        public static string ToFormattedString(this string format, params object[] args) {
            return String.Format(format, args);
        }

        /// <summary>
        /// Strings to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string Value) {
            T oOut = default(T);
            Type t = typeof(T);
            foreach (FieldInfo fi in t.GetFields()) {
                if (fi.Name.Matches(Value))
                    oOut = (T)fi.GetValue(null);
            }

            return oOut;
        }
        
        public static bool Matches(this string source, string compare) {
            return String.Equals(source, compare, StringComparison.OrdinalIgnoreCase);
        }

        public static bool MatchesTrimmed(this string source, string compare) {
            return String.Equals(source.Trim(), compare.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool MatchesRegex(this string inputString, string matchPattern) {
            return Regex.IsMatch(inputString, matchPattern,
                                 RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }
        
        public static bool IsUpperCase(this string s)
        {
            return (s.ToUpper() == s) ? true : false;
        }
        
        public static bool IsLowerCase(this string s)
        {
            return (s.ToLower() == s) ? true : false;
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
        /*
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
         */
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
            if (obj.GetType().GetProperties().Length > 0)
            {
                result += indent + " Properties: \n";
                PropertyInfo[] pic = obj.GetType().GetProperties();
                foreach (PropertyInfo pi in pic)
                    result += indent + "  " + pi.Name + "\n"; //TODO: " = " + pi.GetValue(obj, new object[] { }) + "\n";
            }
            result += indent + " ToString: '" + obj.ToString() + "'\n";
            result += indent + " Hashcode: '" + obj.GetHashCode() + "'\n";
            
            result += (indent == " " ? "" : indent) + "}\n";
            return result;
        }
        
        #region ENUM
        public static bool Has<T>(this Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static T Add<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format(
                    "Could not append value from enumerated type '{0}'.", typeof(T).Name), ex);
            }
        }

        public static T Remove<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format(
                    "Could not remove value from enumerated type '{0}'.", typeof(T).Name), ex);
            }
        }
        
        public static AdvancedString ToReadableString(this Enum val)
        {
            // Gets the field info for the value
            FieldInfo fi = val.GetType().GetField(val.ToString());
            Enums.EnumStringAttribute[] attribs = (Enums.EnumStringAttribute[]) fi.GetCustomAttributes(typeof(Enums.EnumStringAttribute), false);
            if (attribs.Length == 0)
                return val.ToAdvancedString();
            else
                return attribs[0].Value;
            
        }
        #endregion
        
        #region SLICING
        /// <summary>
        /// Get the string slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// Similar to Pythons slice
        /// </summary>
        public static string Slice(this string s, int start, int end)
        {
            // Handles negative ends (like Python)
            if (end < 0)
            {
                end = s.Length + end;
            }

            int length = end - start;
            
            return s.Substring(start, length);
        }
        
        /// <summary>
        /// Get the array slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// Similar to Python slice
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends, like in Python
            if (end < 0)
            {
                end = source.Length + end;
            }

            int length = end - start;

            var result = new T[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = source[i + start];
            }

            return result;
        }
        #endregion
        
        #region MATH
        public static double Log10(this double d) { return Math.Log10(d); }
        public static double Exp10(this double d) { return Math.Pow(10d, d); }
        public static double LogB(this double d, double @base) { return Math.Log(d, @base); }
        public static double Log(this double d) { return Math.Log(d); }
        public static double Exp(this double d) { return Math.Exp(d); }
        public static double ExpB(this double d, double @base) { return Math.Pow(@base, d); }
        public static double Pow(this double d, double exponent) { return Math.Pow(d, exponent); }
        public static long Floor(this double d) { return ((long)(Math.Floor(d))); }
        public static long Ceiling(this double d) { return ((long)(Math.Ceiling(d))); }
        public static double DFloor(this double d) { return ((long)(Math.Floor(d))); }
        public static double DCeiling(this double d) { return ((long)(Math.Ceiling(d))); }
        public static double Round(this double d, int digits = 4) { return Math.Round(d, digits); }

        public static DoubleComponents Decompose(this double d)
        {
            // See http://msdn.microsoft.com/en-us/library/aa691146(VS.71).aspx
            // and Steve Hollasch's http://steve.hollasch.net/cgindex/coding/ieeefloat.html
            // and PremK's http://blogs.msdn.com/b/premk/archive/2006/02/25/539198.aspx

            var result = new DoubleComponents { Datum = d };

            long bits = BitConverter.DoubleToInt64Bits(d);
            bool fNegative = (bits < 0);
            int exponent = (int)((bits >> 52) & 0x7ffL);
            long mantissa = (bits & 0xfffffffffffffL);

            result.Negative = fNegative;
            result.RawExponentInt = exponent;
            result.RawMantissaLong = mantissa;

            if (exponent == 0x7ffL && mantissa != 0)
            {
                // The number is an NaN. Client must interpret.
                result.IsNaN = true;
                return result;
            }

            // The first bit of the mathematical mantissaBits is always 1, and it is not
            // represented in the stored mantissaBits bits. The following logic accounts for
            // this and restores the mantissaBits to its mathematical value.

            if (exponent == 0)
            {
                if (mantissa == 0)
                {
                    // Returning either +0 or -0.
                    return result;
                }
                // Denormalized: A fool-proof detector for denormals is a zero exponentBits.
                // Mantissae for denormals do not have an assumed leading 1-bit. Bump the
                // exponentBits by one so that when we re-bias it by -1023, we have actually
                // brought it back down by -1022, the way it should be. This increment merges
                // the logic for normals and denormals.
                exponent++;
            }
            else
            {
                // Normalized: radix point (the binary point) is after the first non-zero digit (bit).
                // Or-in the *assumed* leading 1 bit to restore the mathematical mantissaBits.
                mantissa = mantissa | (1L << 52);
            }

            // Re-bias the exponentBits by the IEEE 1023, which treats the mathematical mantissaBits
            // as a pure fraction, minus another 52, because we treat the mathematical mantissaBits
            // as a pure integer.
            exponent -= 1075;

            // Produce form with lowest possible whole-number mantissaBits.
            while ((mantissa & 1) == 0)
            {
                mantissa >>= 1;
                exponent++;
            }

            result.MathematicalBase2Exponent = exponent;
            result.MathematicalBase2Mantissa = mantissa;
            return result;
        }

        /// <summary>
        /// Compares two doubles for nearness in the absolute value within a given delta, also a double. Not realiable for infinities, epsilon, max_value, min_value, and NaNs.
        /// </summary>
        /// <param name="d1">The first double.</param>
        /// <param name="d2">The second double.</param>
        /// <param name="delta">The allowable absolute difference.</param>
        /// <returns></returns>
        public static bool DoublesWithinAbsoluteDifference(double d1, double d2, double delta)
        {
            return (Math.Abs(d1 - d2) <= Math.Abs(delta));
        }

        /// <summary>
        /// Return the bits of a double as a twos-complement long integer. IEEE 745 doubles are
        /// lexicographically ordered in this representation.
        /// </summary>
        /// <param name="d">The double to convert.</param>
        /// <returns>A long containing the bits of the double in twos-complement.</returns>
        private static long TwosComplementBits(double d)
        {
            long bits = BitConverter.DoubleToInt64Bits(d);
            // Convert to 2-s complement, which are lexicographically ordered.
            if (bits < 0)
                bits = unchecked((long)(0x8000000000000000 - (ulong)bits));
            return bits;
        }

        /// <summary>
        /// Returns the number of IEEE 754 double-precision values separating a pair of doubles. Adjacent doubles
        /// will have a quanta-difference of 1. Returns -1L if either number is NaN. Reports that double.MaxValue
        /// is adjacent to double.PositiveInfinity and likewise for double.MinValue and double.NegativeInfinity.
        /// </summary>
        /// <param name="d1">The first double to compare.</param>
        /// <param name="d2">The second double to compare.</param>
        /// <returns>The number of IEEE 745 doubles separating the pair.</returns>
        public static long LexicographicQuantaDifference(double d1, double d2)
        {
            if (double.IsNaN(d1) || double.IsNaN(d2))
                return -1L;

            // See Bruce Dawson's http://www.cygnus-software.com/papers/comparingfloats/comparingfloats.htm
            // The major limitation of this technique is that double.MaxValue and double.PostiveInfinity will
            // report almost equal. See the paper for mitigations.

            // Extract bits from the doubles.
            long bits1 = TwosComplementBits(d1);
            long bits2 = TwosComplementBits(d2);

            long diff = bits1 - bits2;
            if (diff < 0)
                diff = -diff;

            return diff;
        }

        /// <summary>
        /// Compare doubles for equality within a given number of possible discrete double-precision values.
        /// Produces false if either number is NaN. Reports that double.MaxValue is almost equal to
        /// double.PositiveInfinity; likewise for double.MinValue and double.NegativeInfinity.
        /// </summary>
        /// <param name="d1">The first double.</param>
        /// <param name="d2">The second double.</param>
        /// <param name="nQuanta">The number of discrete double-precision values permitted between d1 and d2. Must be >= 0.</param>
        /// <returns></returns>
        public static bool DoublesNearlyEqual(double d1, double d2, int nQuanta)
        {
            if (double.IsNaN(d1) || double.IsNaN(d2))
                return false;
            
            return LexicographicQuantaDifference(d1, d2) <= nQuanta;
        }
        #endregion
        
        #region FILES
        /// <summary>
        /// Read a text file and obtain it's contents.
        /// </summary>
        /// <param name="absolutePath">The complete file path to write to.</param>
        /// <returns>String containing the content of the file.</returns>
        public static string GetFileText(this string absolutePath) {
            using (StreamReader sr = new StreamReader(absolutePath))
                return sr.ReadToEnd();
        }

        /// <summary>
        /// Creates or opens a file for writing and writes text to it.
        /// </summary>
        /// <param name="absolutePath">The complete file path to write to.</param>
        /// <param name="fileText">A String containing text to be written to the file.</param>
        public static void CreateToFile(this string fileText, string absolutePath) {
            using (StreamWriter sw = File.CreateText(absolutePath))
                sw.Write(fileText);
        }

        /// <summary>
        /// Update text within a file by replacing a substring within the file.
        /// </summary>
        /// <param name="absolutePath">The complete file path to write to.</param>
        /// <param name="lookFor">A String to be replaced.</param>
        /// <param name="replaceWith">A String to replace all occurrences of lookFor.</param>
        public static void UpdateFileText(this string absolutePath, string lookFor, string replaceWith) {
            string newText = GetFileText(absolutePath).Replace(lookFor, replaceWith);
            WriteToFile(absolutePath, newText);
        }

        /// <summary>
        /// Writes out a string to a file.
        /// </summary>
        /// <param name="absolutePath">The complete file path to write to.</param>
        /// <param name="fileText">A String containing text to be written to the file.</param>
        public static void WriteToFile(this string absolutePath, string fileText) {
            using (StreamWriter sw = new StreamWriter(absolutePath, false))
                sw.Write(fileText);
        }

        /// <summary>
        /// Fetches a web page
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string ReadWebPage(this string url) {
            return ReadWebResponse(GetWebResponse(url));
        }

        public static string ReadWebResponse(WebResponse response)
        {
            string result = "";

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;

        }

        /// <summary>
        /// Fetches a web page
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static HttpWebResponse GetWebResponse(this string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            return (HttpWebResponse)request.GetResponse();

        }
        #endregion
        
        #region STRING MATCHING
        /// <summary>
        /// Determines whether the specified eval string contains only alpha characters.
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified eval string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlpha(this string evalString) {
            return !Regex.IsMatch(evalString, RegexPattern.ALPHA);
        }

        /// <summary>
        /// Determines whether the specified eval string contains only alphanumeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric(this string evalString) {
            return !Regex.IsMatch(evalString, RegexPattern.ALPHA_NUMERIC);
        }

        /// <summary>
        /// Determines whether the specified eval string contains only alphanumeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <param name="allowSpaces">if set to <c>true</c> [allow spaces].</param>
        /// <returns>
        /// 	<c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric(this string evalString, bool allowSpaces) {
            if (allowSpaces)
                return !Regex.IsMatch(evalString, RegexPattern.ALPHA_NUMERIC_SPACE);
            return IsAlphaNumeric(evalString);
        }

        /// <summary>
        /// Determines whether the specified eval string contains only numeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the string is numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumeric(this string evalString) {
            return !Regex.IsMatch(evalString, RegexPattern.NUMERIC);
        }

        /// <summary>
        /// Determines whether the specified email address string is valid based on regular expression evaluation.
        /// </summary>
        /// <param name="emailAddressString">The email address string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified email address is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmail(this string emailAddressString) {
            return Regex.IsMatch(emailAddressString, RegexPattern.EMAIL);
        }
        
        /// <summary>
        /// Determines whether the specified string is a valid GUID.
        /// </summary>
        /// <param name="guid">The GUID.</param>
        /// <returns>
        /// 	<c>true</c> if the specified string is a valid GUID; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGuid(this string guid) {
            return Regex.IsMatch(guid, RegexPattern.GUID);
        }

        /// <summary>
        /// Determines whether the specified string is a valid US Zip Code, using either 5 or 5+4 format.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid zip code; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZipCodeAny(this string zipCode) {
            return Regex.IsMatch(zipCode, RegexPattern.US_ZIPCODE_PLUS_FOUR_OPTIONAL);
        }

        /// <summary>
        /// Determines whether the specified string is a valid US Zip Code, using the 5 digit format.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid zip code; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZipCodeFive(this string zipCode) {
            return Regex.IsMatch(zipCode, RegexPattern.US_ZIPCODE);
        }

        /// <summary>
        /// Determines whether the specified string is a valid US Zip Code, using the 5+4 format.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid zip code; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZipCodeFivePlusFour(this string zipCode) {
            return Regex.IsMatch(zipCode, RegexPattern.US_ZIPCODE_PLUS_FOUR);
        }

        /// <summary>
        /// Determines whether the specified string is a valid Social Security number. Dashes are optional.
        /// </summary>
        /// <param name="socialSecurityNumber">The Social Security Number</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid Social Security number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSocialSecurityNumber(this string socialSecurityNumber) {
            return Regex.IsMatch(socialSecurityNumber, RegexPattern.SOCIAL_SECURITY);
        }

        /// <summary>
        /// Determines whether the specified string is a valid IP address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIPAddress(this string ipAddress) {
            return Regex.IsMatch(ipAddress, RegexPattern.IP_ADDRESS);
        }

        /// <summary>
        /// Determines whether the specified string is a valid US phone number using the referenced regex string.
        /// </summary>
        /// <param name="telephoneNumber">The telephone number.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUSTelephoneNumber(this string telephoneNumber) {
            return Regex.IsMatch(telephoneNumber, RegexPattern.US_TELEPHONE);
        }

        /// <summary>
        /// Determines whether the specified string is a valid currency string using the referenced regex string.
        /// </summary>
        /// <param name="currency">The currency string.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUSCurrency(this string currency) {
            return Regex.IsMatch(currency, RegexPattern.US_CURRENCY);
        }
        
        /// <summary>
        /// Determines whether the specified string is consider a strong password based on the supplied string.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>
        /// 	<c>true</c> if strong; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStrongPassword(this string password) {
            return Regex.IsMatch(password, RegexPattern.STRONG_PASSWORD);
        }

        #endregion
        
        #region DATES
        
        #region Date Math

        /// <summary>
        /// Returns a date in the past by days.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        public static DateTime DaysAgo(this int days) {
            TimeSpan t = new TimeSpan(days, 0, 0, 0);
            return DateTime.Now.Subtract(t);
        }

        /// <summary>
        ///  Returns a date in the future by days.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        public static DateTime DaysFromNow(this int days) {
            TimeSpan t = new TimeSpan(days, 0, 0, 0);
            return DateTime.Now.Add(t);
        }

        /// <summary>
        /// Returns a date in the past by hours.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <returns></returns>
        public static DateTime HoursAgo(this int hours) {
            TimeSpan t = new TimeSpan(hours, 0, 0);
            return DateTime.Now.Subtract(t);
        }

        /// <summary>
        /// Returns a date in the future by hours.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <returns></returns>
        public static DateTime HoursFromNow(this int hours) {
            TimeSpan t = new TimeSpan(hours, 0, 0);
            return DateTime.Now.Add(t);
        }

        /// <summary>
        /// Returns a date in the past by minutes
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public static DateTime MinutesAgo(this int minutes) {
            TimeSpan t = new TimeSpan(0, minutes, 0);
            return DateTime.Now.Subtract(t);
        }

        /// <summary>
        /// Returns a date in the future by minutes.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public static DateTime MinutesFromNow(this int minutes) {
            TimeSpan t = new TimeSpan(0, minutes, 0);
            return DateTime.Now.Add(t);
        }

        /// <summary>
        /// Gets a date in the past according to seconds
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static DateTime SecondsAgo(this int seconds) {
            TimeSpan t = new TimeSpan(0, 0, seconds);
            return DateTime.Now.Subtract(t);
        }

        /// <summary>
        /// Gets a date in the future by seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static DateTime SecondsFromNow(this int seconds) {
            TimeSpan t = new TimeSpan(0, 0, seconds);
            return DateTime.Now.Add(t);
        }

        #endregion
        
        #region Diffs

        /// <summary>
        /// Diffs the specified date.
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static TimeSpan Diff(this DateTime dateOne, DateTime dateTwo) {
            TimeSpan t = dateOne.Subtract(dateTwo);
            return t;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffDays(this string dateOne, string dateTwo) {
            DateTime dtOne;
            DateTime dtTwo;
            if (DateTime.TryParse(dateOne, out dtOne) && DateTime.TryParse(dateTwo, out dtTwo))
                return Diff(dtOne, dtTwo).TotalDays;
            return 0;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffDays(this DateTime dateOne, DateTime dateTwo) {
            return Diff(dateOne, dateTwo).TotalDays;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffHours(this string dateOne, string dateTwo) {
            DateTime dtOne;
            DateTime dtTwo;
            if (DateTime.TryParse(dateOne, out dtOne) && DateTime.TryParse(dateTwo, out dtTwo))
                return Diff(dtOne, dtTwo).TotalHours;
            return 0;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffHours(this DateTime dateOne, DateTime dateTwo) {
            return Diff(dateOne, dateTwo).TotalHours;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffMinutes(this string dateOne, string dateTwo) {
            DateTime dtOne;
            DateTime dtTwo;
            if (DateTime.TryParse(dateOne, out dtOne) && DateTime.TryParse(dateTwo, out dtTwo))
                return Diff(dtOne, dtTwo).TotalMinutes;
            return 0;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffMinutes(this DateTime dateOne, DateTime dateTwo) {
            return Diff(dateOne, dateTwo).TotalMinutes;
        }

        /// <summary>
        /// Displays the difference in time between the two dates. Return example is "12 years 4 months 24 days 8 hours 33 minutes 5 seconds"
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static string ReadableDiff(this DateTime startTime, DateTime endTime) {
            string result;

            int seconds = endTime.Second - startTime.Second;
            int minutes = endTime.Minute - startTime.Minute;
            int hours = endTime.Hour - startTime.Hour;
            int days = endTime.Day - startTime.Day;
            int months = endTime.Month - startTime.Month;
            int years = endTime.Year - startTime.Year;

            if (seconds < 0) {
                minutes--;
                seconds += 60;
            }
            if (minutes < 0) {
                hours--;
                minutes += 60;
            }
            if (hours < 0) {
                days--;
                hours += 24;
            }

            if (days < 0) {
                months--;
                int previousMonth = (endTime.Month == 1) ? 12 : endTime.Month - 1;
                int year = (previousMonth == 12) ? endTime.Year - 1 : endTime.Year;
                days += DateTime.DaysInMonth(year, previousMonth);
            }
            if (months < 0) {
                years--;
                months += 12;
            }

            //put this in a readable format
            if (years > 0) {
                result = years.Pluralize("year");
                if (months != 0)
                    result += ", " + months.Pluralize("month");
                result += " ago";
            } else if (months > 0) {
                result = months.Pluralize("month");
                if (days != 0)
                    result += ", " + days.Pluralize("day");
                result += " ago";
            } else if (days > 0) {
                result = days.Pluralize("day");
                if (hours != 0)
                    result += ", " + hours.Pluralize("hour");
                result += " ago";
            } else if (hours > 0) {
                result = hours.Pluralize("hour");
                if (minutes != 0)
                    result += ", " + minutes.Pluralize("minute");
                result += " ago";
            } else if (minutes > 0)
                result = minutes.Pluralize("minute") + " ago";
            else
                result = seconds.Pluralize("second") + " ago";

            return result;
        }

        #endregion


        // many thanks to ASP Alliance for the code below
        // http://authors.aspalliance.com/olson/methods/

        /// <summary>
        /// Counts the number of weekdays between two dates.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static int CountWeekdays(this DateTime startTime, DateTime endTime) {
            TimeSpan ts = endTime - startTime;
            Console.WriteLine(ts.Days);
            int cnt = 0;
            for (int i = 0; i < ts.Days; i++) {
                DateTime dt = startTime.AddDays(i);
                if (IsWeekDay(dt))
                    cnt++;
            }
            return cnt;
        }

        /// <summary>
        /// Counts the number of weekends between two dates.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static int CountWeekends(this DateTime startTime, DateTime endTime) {
            TimeSpan ts = endTime - startTime;
            Console.WriteLine(ts.Days);
            int cnt = 0;
            for (int i = 0; i < ts.Days; i++) {
                DateTime dt = startTime.AddDays(i);
                if (IsWeekEnd(dt))
                    cnt++;
            }
            return cnt;
        }

        /// <summary>
        /// Verifies if the object is a date
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        /// 	<c>true</c> if the specified dt is date; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDate(this object dt) {
            DateTime newDate;
            return DateTime.TryParse(dt.ToString(), out newDate);
        }

        /// <summary>
        /// Checks to see if the date is a week day (Mon - Fri)
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        /// 	<c>true</c> if [is week day] [the specified dt]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWeekDay(this DateTime dt) {
            return (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday);
        }

        /// <summary>
        /// Checks to see if the date is Saturday or Sunday
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        /// 	<c>true</c> if [is week end] [the specified dt]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWeekEnd(this DateTime dt) {
            return (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday);
        }

        /// <summary>
        /// Displays the difference in time between the two dates. Return example is "12 years 4 months 24 days 8 hours 33 minutes 5 seconds"
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static string TimeDiff(this DateTime startTime, DateTime endTime) {
            int seconds = endTime.Second - startTime.Second;
            int minutes = endTime.Minute - startTime.Minute;
            int hours = endTime.Hour - startTime.Hour;
            int days = endTime.Day - startTime.Day;
            int months = endTime.Month - startTime.Month;
            int years = endTime.Year - startTime.Year;
            if (seconds < 0) {
                minutes--;
                seconds += 60;
            }
            if (minutes < 0) {
                hours--;
                minutes += 60;
            }
            if (hours < 0) {
                days--;
                hours += 24;
            }
            if (days < 0) {
                months--;
                int previousMonth = (endTime.Month == 1) ? 12 : endTime.Month - 1;
                int year = (previousMonth == 12) ? endTime.Year - 1 : endTime.Year;
                days += DateTime.DaysInMonth(year, previousMonth);
            }
            if (months < 0) {
                years--;
                months += 12;
            }

            string sYears = FormatString("year", String.Empty, years);
            string sMonths = FormatString("month", sYears, months);
            string sDays = FormatString("day", sMonths, days);
            string sHours = FormatString("hour", sDays, hours);
            string sMinutes = FormatString("minute", sHours, minutes);
            string sSeconds = FormatString("second", sMinutes, seconds);

            return String.Concat(sYears, sMonths, sDays, sHours, sMinutes, sSeconds);
        }

        /// <summary>
        /// Given a datetime object, returns the formatted month and day, i.e. "April 15th"
        /// </summary>
        /// <param name="date">The date to extract the string from</param>
        /// <returns></returns>
        public static string GetFormattedMonthAndDay(this DateTime date) {
            return String.Concat(String.Format("{0:MMMM}", date), " ", GetDateDayWithSuffix(date));
        }

        /// <summary>
        /// Given a datetime object, returns the formatted day, "15th"
        /// </summary>
        /// <param name="date">The date to extract the string from</param>
        /// <returns></returns>
        public static string GetDateDayWithSuffix(this DateTime date) {
            int dayNumber = date.Day;
            string suffix = "th";

            if (dayNumber == 1 || dayNumber == 21 || dayNumber == 31)
                suffix = "st";
            else if (dayNumber == 2 || dayNumber == 22)
                suffix = "nd";
            else if (dayNumber == 3 || dayNumber == 23)
                suffix = "rd";

            return String.Concat(dayNumber, suffix);
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

    public struct DoubleComponents
    {
        public double Datum { get; set; }
        public bool IsNaN { get; set; }
        public bool Negative { get; set; }
        public int MathematicalBase2Exponent { get; set; }
        public long MathematicalBase2Mantissa { get; set; }
        public int RawExponentInt { get; set; }
        public long RawMantissaLong { get; set; }
        // TODO: Memoize these
        public long FloorLog10 { get { return Log10.Floor(); } }
        public double FracLog10 { get { return Log10 - FloorLog10; } }
        public double Log10 { get { return Datum.Log10(); } }
        public byte[] ExponentBits { get { return BitConverter.GetBytes(RawExponentInt); } }
        public byte[] MantissaBits { get { return BitConverter.GetBytes(RawMantissaLong); } }
    }
}
