/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/24/2012
 * Time: 3:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace IExtendFramework
{
    /// <summary>
    /// An AdvancedString.
    /// It is basically the System.String type, but more advanced.
    /// It actually uses a string internally, but according to all my tests, it is faster.
    /// </summary>
    [Serializable]
    public class AdvancedString :
        IEnumerable, IEnumerable<char>,
    IComparable, ICloneable, IConvertible,
    IComparable<AdvancedString>, IEquatable<AdvancedString>
    {
        /// <summary>
        /// The string that this AdvancedString wraps
        /// </summary>
        private string internalString;
        
        /// <summary>
        /// An empty ("") AdvancedString
        /// </summary>
        public static readonly AdvancedString Empty = new AdvancedString("");
        public static readonly int MaxLength = int.MaxValue;
        public static readonly int MinLength = 0;
        
        #region Init
        
        /// <summary>
        /// Create an empty AdvancedString
        /// </summary>
        public AdvancedString()
        {
            internalString = "";
        }
        
        /// <summary>
        /// Create an AdvancedString from the given normal string
        /// </summary>
        /// <param name="s"></param>
        public AdvancedString(string s)
        {
            this.internalString = s;
        }
        
        /// <summary>
        /// Creates an AdvancedString from the given normal char
        /// </summary>
        /// <param name="c"></param>
        public AdvancedString(char c)
        {
            this.internalString = c.ToString();
        }
        
        /// <summary>
        /// Creates an AdvancedString by converting the byte into a string
        /// </summary>
        /// <param name="b"></param>
        public AdvancedString(byte b)
        {
            this.internalString = Utilities.ByteToString(new byte[] { b});
        }
        
        /// <summary>
        /// Creates an AdvancedString by converting the byte array into a string
        /// </summary>
        /// <param name="b"></param>
        public AdvancedString(byte[] b)
        {
            this.internalString = Utilities.ByteToString(b);
        }
        
        /// <summary>
        /// Creates an AdvancedString from the given boolean
        /// </summary>
        /// <param name="b"></param>
        /// <param name="capitalizeFirstLetter">Whether the "T" or "F" of the bool should be capital</param>
        public AdvancedString(bool b, bool capitalizeFirstLetter = false)
        {
            if (capitalizeFirstLetter)
                this.internalString = (b == true) ? "True" : "False";
            else
                this.internalString = (b == true) ? "true" : "false";
        }
        
        /// <summary>
        /// Creates an AdvancedString from the given object
        /// </summary>
        /// <param name="o"></param>
        public AdvancedString(object o)
        {
            this.internalString = o.ToString();
        }
        
        /// <summary>
        /// Creates an AdvancedString from the given normal string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static AdvancedString From(string s)
        {
            return new AdvancedString(s);
        }
        
        /// <summary>
        /// Creates an AdvancedString from the given char
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static AdvancedString From(char c)
        {
            return new AdvancedString(c);
        }
        
        /// <summary>
        /// Creates an AdvancedString from the given byte
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static AdvancedString From(byte b)
        {
            return new AdvancedString(b);
        }
        
        /// <summary>
        /// Creates an AdvancedString from the given byte array
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static AdvancedString From(byte[] b)
        {
            return new AdvancedString(b);
        }
        #endregion
        
        #region Operator Overloaders
        
        // These do not have xml documentation, because none is needed.
        // They are also quite self-explanatory if you want to look at them.
        
        #region Convert From
        
        public static implicit operator AdvancedString(string o)
        {
            return AdvancedString.From(o);
        }
        
        public static implicit operator AdvancedString(char o)
        {
            return AdvancedString.From(o);
        }
        #endregion
        
        public static bool operator ==(AdvancedString lhs, AdvancedString rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;
            return lhs.Equals(rhs);
        }
        
        public static bool operator !=(AdvancedString lhs, AdvancedString rhs)
        {
            return !(lhs == rhs);
        }
        
        public static AdvancedString operator +(AdvancedString a1, AdvancedString a2)
        {
            return new AdvancedString(a1.ToString() + a2.ToString());
        }
        
        public static AdvancedString operator +(string a1, AdvancedString a2)
        {
            return new AdvancedString(a1.ToString() + a2.ToString());
        }
        
        public static AdvancedString operator +(AdvancedString a1, string a2)
        {
            return new AdvancedString(a1.ToString() + a2.ToString());
        }
        
        public static AdvancedString operator *(AdvancedString a1, int count)
        {
            AdvancedString a = new AdvancedString();
            for (int i = 0; i < count; i++)
                a += a1;
            return a;
        }
        
        public static AdvancedString operator >>(AdvancedString a1, int i)
        {
            AdvancedString a2 = new AdvancedString();
            foreach (char c in a1.internalString)
                a2 += new AdvancedString((char)(c >> i));
            return a2;
        }
        
        public static AdvancedString operator <<(AdvancedString a1, int i)
        {
            AdvancedString a2 = new AdvancedString();
            foreach (char c in a1.internalString)
                a2 += new AdvancedString((char)(c << i));
            return a2;
        }
        
        public static bool operator <(AdvancedString a1, object o)
        {
            string a = a1.internalString, b = o.ToString();
            for (int i = 0; i < a.Length; i++)
            {
                if (b.Length == i)
                    return false;
                
                if (a[i] > b[i])
                    return false;
            }
            
            return true;
        }
        
        public static bool operator >(AdvancedString a1, object o)
        {
            string a = a1.internalString, b = o.ToString();
            for (int i = 0; i < a.Length; i++)
            {
                if (b.Length == i)
                    return false;
                
                if (a[i] < b[i])
                    return false;
            }
            
            return true;
        }
        
        public static bool operator <=(AdvancedString a1, object o)
        {
            string a = a1.internalString, b = o.ToString();
            for (int i = 0; i < a.Length; i++)
            {
                if (b.Length == i)
                    return false;
                
                if (a[i] > b[i])
                    return false;
            }
            
            return true;
        }
        
        public static bool operator >=(AdvancedString a1, object o)
        {
            string a = a1.internalString, b = o.ToString();
            for (int i = 0; i < a.Length; i++)
            {
                if (b.Length == i)
                    return false;
                
                if (a[i] < b[i])
                    return false;
            }
            
            return true;
        }
        
        public static bool operator false(AdvancedString a1)
        {
            if (a1.internalString.Length == 0)
                return true;
            
            return false;
        }
        
        public static bool operator true(AdvancedString a1)
        {
            if (a1.internalString.Length != 0)
                return true;
            
            return false;
        }
        #endregion
        
        /// <summary>
        /// Converts the AdvancedString into a normal string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return internalString;
        }
        
        #region ENUMERATION
        
        /// <summary>
        /// Creates an enumerator for this
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalString.GetEnumerator();
        }
        
        /// <summary>
        /// Creates an enumerator for each char
        /// </summary>
        /// <returns></returns>
        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            foreach (char c in internalString)
                yield return c;
        }
        #endregion
        
        #region LENGTH
        /// <summary>
        /// The Length of this string
        /// </summary>
        public int Length
        {
            get
            {
                return internalString.Length;
            }
        }
        
        /// <summary>
        /// The Length of this string
        /// </summary>
        public int Count
        {
            get
            {
                return internalString.Length;
            }
        }
        #endregion
        
        #region Equals and GetHashCode implementation
        /// <summary>
        /// Checks whether this equals the other AdvancedString value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            AdvancedString other = obj as AdvancedString;
            if (other == null)
                return false;
            return this.internalString == other.internalString;
        }
        
        /// <summary>
        /// The HashCode for this string
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return internalString.GetHashCode();
        }
        #endregion
        
        #region INDEXING
        /// <summary>
        /// Gets or sets char at the specified index
        /// </summary>
        public char this[int index]
        {
            get
            {
                return internalString[index];
            }
            set
            {
                if (index > this.internalString.Length)
                    throw new Exception("Index cannot be greater then length!");
                
                string a = internalString.Substring(0, index == 0 ? 0 : index - 1);
                Console.WriteLine(a);
                string b = "";
                b = internalString.Substring(index);
                this.internalString = a + value.ToString() + b;
            }
        }
        
        /// <summary>
        /// Slices the string, also allows setting slice sections
        /// </summary>
        public string this[int index1, int index2]
        {
            get
            {
                if (index1 < 0)
                    index1 = this.internalString.Length + index1;
                if (index2 < 0)
                    index2 = this.internalString.Length + index2;
                if (index2 < index1)
                    throw new Exception("Second slice index cannot be lower than the first!");
                if (index1 > index2)
                    throw new Exception("First slice index cannot be greater than the second!");
                if (index2 > internalString.Length)
                    index2 = internalString.Length;
                
                return internalString.Substring(index1, index2 - index1);
            }
            set
            {
                // convert into positive numbers from end of string
                if (index1 < 0)
                    index1 = this.internalString.Length + index1;
                if (index2 < 0)
                    index2 = this.internalString.Length + index2;
                if (index2 < index1)
                    throw new Exception("Second slice index cannot be lower than the first!");
                if (index1 > index2)
                    throw new Exception("First slice index cannot be greater than the second!");
                if (index2 > internalString.Length)
                    index2 = internalString.Length;
                // trim value down to size
                if (value.Length > index2)
                    value = value.Substring(0, index2 - index1);
                
                internalString = internalString.Substring(0, index1) + value.ToString() + internalString.Substring(index2);
            }
        }
        #endregion
        
        #region JOIN
        /// <summary>
        /// Joins some AdvancedStrings together
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AdvancedString Join(AdvancedString separator, params string[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return AdvancedString.Join(separator, value, 0, value.Length);
        }
        
        /// <summary>
        /// Joins some AdvancedStrings together
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AdvancedString Join(AdvancedString separator, params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length == 0 || values[0] == null)
            {
                return AdvancedString.Empty;
            }
            if (separator == null)
            {
                separator = AdvancedString.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            string text = values[0].ToString();
            if (text != null)
            {
                stringBuilder.Append(text);
            }
            for (int i = 1; i < values.Length; i++)
            {
                stringBuilder.Append(separator);
                if (values[i] != null)
                {
                    text = values[i].ToString();
                    if (text != null)
                    {
                        stringBuilder.Append(text);
                    }
                }
            }
            return new AdvancedString(stringBuilder.ToString());
        }
        
        /// <summary>
        /// Joins AdvancedStrings together
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AdvancedString Join<T>(AdvancedString separator, IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (separator == null)
            {
                separator = AdvancedString.Empty;
            }
            string result;
            using (IEnumerator<T> enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    result = string.Empty;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (enumerator.Current != null)
                    {
                        T current = enumerator.Current;
                        string text = current.ToString();
                        if (text != null)
                        {
                            stringBuilder.Append(text);
                        }
                    }
                    while (enumerator.MoveNext())
                    {
                        stringBuilder.Append(separator);
                        if (enumerator.Current != null)
                        {
                            T current2 = enumerator.Current;
                            string text2 = current2.ToString();
                            if (text2 != null)
                            {
                                stringBuilder.Append(text2);
                            }
                        }
                    }
                    result = stringBuilder.ToString();
                }
            }
            return new AdvancedString(result);
        }
        
        /// <summary>
        /// Joins AdvancedStrings together
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AdvancedString Join(AdvancedString separator, IEnumerable<AdvancedString> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (separator == null)
            {
                separator = AdvancedString.Empty;
            }
            string result;
            using (IEnumerator<AdvancedString> enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    result = string.Empty;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (enumerator.Current != null)
                    {
                        stringBuilder.Append(enumerator.Current);
                    }
                    while (enumerator.MoveNext())
                    {
                        stringBuilder.Append(separator);
                        if (enumerator.Current != null)
                        {
                            stringBuilder.Append(enumerator.Current);
                        }
                    }
                    result = stringBuilder.ToString();
                }
            }
            return new AdvancedString(result);
        }
        
        /// <summary>
        /// Joins strings into an AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AdvancedString Join(string separator, params string[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return AdvancedString.Join(separator, value, 0, value.Length);
        }
        
        /// <summary>
        /// Joins strings into an AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AdvancedString Join(string separator, params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length == 0 || values[0] == null)
            {
                return AdvancedString.Empty;
            }
            if (separator == null)
            {
                separator = string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            string text = values[0].ToString();
            if (text != null)
            {
                stringBuilder.Append(text);
            }
            for (int i = 1; i < values.Length; i++)
            {
                stringBuilder.Append(separator);
                if (values[i] != null)
                {
                    text = values[i].ToString();
                    if (text != null)
                    {
                        stringBuilder.Append(text);
                    }
                }
            }
            return new AdvancedString(stringBuilder.ToString());
        }
        
        /// <summary>
        /// Joins strings into an AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AdvancedString Join<T>(string separator, IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (separator == null)
            {
                separator = string.Empty;
            }
            string result;
            using (IEnumerator<T> enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    result = string.Empty;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (enumerator.Current != null)
                    {
                        T current = enumerator.Current;
                        string text = current.ToString();
                        if (text != null)
                        {
                            stringBuilder.Append(text);
                        }
                    }
                    while (enumerator.MoveNext())
                    {
                        stringBuilder.Append(separator);
                        if (enumerator.Current != null)
                        {
                            T current2 = enumerator.Current;
                            string text2 = current2.ToString();
                            if (text2 != null)
                            {
                                stringBuilder.Append(text2);
                            }
                        }
                    }
                    result = stringBuilder.ToString();
                }
            }
            return new AdvancedString(result);
        }
        
        /// <summary>
        /// Joins strings together into an AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AdvancedString Join(string separator, IEnumerable<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (separator == null)
            {
                separator = string.Empty;
            }
            string result;
            using (IEnumerator<string> enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    result = string.Empty;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (enumerator.Current != null)
                    {
                        stringBuilder.Append(enumerator.Current);
                    }
                    while (enumerator.MoveNext())
                    {
                        stringBuilder.Append(separator);
                        if (enumerator.Current != null)
                        {
                            stringBuilder.Append(enumerator.Current);
                        }
                    }
                    result = stringBuilder.ToString();
                }
            }
            return new AdvancedString(result);
        }
        #endregion
        
        /// <summary>
        /// Slices the string
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public AdvancedString Slice(int start, int end)
        {
            if (start < 0)
                start = this.internalString.Length + start;
            if (end < 0)
                end = this.internalString.Length + end;
            if (end < start)
                throw new Exception("Second slice index cannot be lower than the first!");
            if (start > end)
                throw new Exception("First slice index cannot be greater than the second!");
            if (end > internalString.Length)
                end = internalString.Length;
            
            return internalString.Substring(start, end - start);
        }
        
        #region SUBSTRING
        /// <summary>
        /// Gets the specified substring
        /// </summary>
        /// <param name="low"></param>
        /// <returns></returns>
        public AdvancedString Substring(int low)
        {
            return AdvancedString.From(internalString.Substring(low));
        }
        
        /// <summary>
        /// Gets the specified substring
        /// </summary>
        /// <param name="l"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public AdvancedString Substring(int l, int h)
        {
            return new AdvancedString(internalString.Substring(l, h));
        }
        #endregion
        
        #region INSERT
        /// <summary>
        /// Inserts an AdvancedString at the index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public AdvancedString Insert(int index, AdvancedString s)
        {
            return new AdvancedString(internalString.Insert(index, s.ToString()));
        }
        
        /// <summary>
        /// Inserts a string at the index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public AdvancedString Insert(int index, string s)
        {
            return new AdvancedString(internalString.Insert(index, s));
        }
        #endregion
        
        /// <summary>
        /// Checks if the AdvancedString is null or empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(AdvancedString s)
        {
            return string.IsNullOrEmpty(s.internalString);
        }
        
        /// <summary>
        /// Checks if the AdvancedString is whitespace
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(AdvancedString s)
        {
            foreach (char c in s)
                if (!char.IsWhiteSpace(c))
                    return false;
            return true;
        }
        
        /// <summary>
        /// Checks if the AdvancedString is null or whitespace
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(AdvancedString s)
        {
            return string.IsNullOrWhiteSpace(s.internalString);
        }
        
        #region Splitting
        /// <summary>
        /// splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(params char[] separator)
        {
            string[] s = internalString.Split(separator);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char[] separator, int count)
        {
            string[] s = internalString.Split(separator, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char[] separator, StringSplitOptions options)
        {
            string[] s = internalString.Split(separator, 2147483647, options);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char[] separator, int count, StringSplitOptions options)
        {
            string[] s = internalString.Split(separator, count, options);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string[] separator, StringSplitOptions options)
        {
            return this.Split(separator, 2147483647, options);
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string[] separator, int count)
        {
            string[] s = internalString.Split(separator, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string[] separator, int count, StringSplitOptions options)
        {
            string[] s = internalString.Split(separator, count, options);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char separator)
        {
            string[] s = internalString.Split(new char[] { separator}, 2147483647, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char separator, int count)
        {
            string[] s = internalString.Split(new char[] {separator}, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char separator, StringSplitOptions o)
        {
            string[] s = internalString.Split(new char[] {separator}, 2147483647, o);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string separator)
        {
            string[] s = internalString.Split(new string[] { separator}, 2147483647, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string separator, int count)
        {
            string[] s = internalString.Split(new string[] {separator}, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string separator, StringSplitOptions o)
        {
            string[] s = internalString.Split(new string[] {separator}, 2147483647, o);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(AdvancedString separator)
        {
            string[] s = internalString.Split(new string[] { separator.ToString()}, 2147483647, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(AdvancedString separator, int count)
        {
            string[] s = internalString.Split(new string[] {separator.ToString()}, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public AdvancedString[] Split(AdvancedString separator, StringSplitOptions o)
        {
            string[] s = internalString.Split(new string[] {separator.ToString()}, 2147483647, o);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        #endregion
        
        #region TRIMMING
        /// <summary>
        /// Trims whitespace from the string
        /// </summary>
        /// <returns></returns>
        public AdvancedString Trim()
        {
            return new AdvancedString(internalString.Trim());
        }
        
        /// <summary>
        /// Trims whitespace from the string
        /// </summary>
        /// <param name="tchars"></param>
        /// <returns></returns>
        public AdvancedString Trim(params char[] tchars)
        {
            return new AdvancedString(internalString.Trim(tchars));
        }
        #endregion
        
        #region IndexOf
        /// <summary>
        /// Gets the index of the char
        /// </summary>
        /// <param name="c">the char to find</param>
        /// <param name="index">Where the char was found (-1 if not found)</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int IndexOf(char c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = internalString.Length;
            
            return internalString.IndexOf(c, index, count);
        }
        
        /// <summary>
        /// Gets the index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">Where the char was found (-1 if not found)</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int IndexOf(string c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = internalString.Length;
            
            return internalString.IndexOf(c, index, count);
        }
        
        /// <summary>
        /// Gets the index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">Where the char was found (-1 if not found)</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int IndexOf(AdvancedString c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = internalString.Length;
            
            return internalString.IndexOf(c.internalString, index, count);
        }
        
        /// <summary>
        /// Gets the last index of the char
        /// </summary>
        /// <param name="c">the char to find</param>
        /// <param name="index">Where the char was found (-1 if not found)</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int LastIndexOf(char c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = internalString.Length;
            
            return internalString.LastIndexOf(c, index, count);
        }
        
        /// <summary>
        /// Gets the last index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">Where the char was found (-1 if not found)</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int LastIndexOf(string c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = internalString.Length;
            
            return internalString.LastIndexOf(c, index, count);
        }
        
        /// <summary>
        /// Gets the index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">Where the char was found (-1 if not found)</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int LastIndexOf(AdvancedString c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = internalString.Length;
            
            return internalString.LastIndexOf(c.ToString(), index, count);
        }
        
        /// <summary>
        /// Gets the last index of any char
        /// </summary>
        /// <param name="anyOf">the chars to search for</param>
        /// <returns></returns>
        public int LastIndexOfAny(char[] anyOf)
        {
            return internalString.LastIndexOfAny(anyOf);
        }
        
        /// <summary>
        /// Gets the last index of any specified char
        /// </summary>
        /// <param name="anyOf">the chars</param>
        /// <param name="startIndex">the start index</param>
        /// <returns></returns>
        public int LastIndexOfAny(char[] anyOf, int startIndex)
        {
            return internalString.LastIndexOfAny(anyOf, startIndex);
        }
        
        /// <summary>
        /// Gets the last index of any specified char
        /// </summary>
        /// <param name="anyOf">thes chars</param>
        /// <param name="startIndex">the start index</param>
        /// <param name="count">The length to search</param>
        /// <returns></returns>
        public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
        {
            return internalString.LastIndexOfAny(anyOf, startIndex, count);
        }
        
        /// <summary>
        /// Gets the index of any specified char
        /// </summary>
        /// <param name="anyOf"></param>
        /// <returns></returns>
        public int IndexOfAny(char[] anyOf)
        {
            return internalString.IndexOfAny(anyOf);
        }
        
        /// <summary>
        /// Gets the index of any specified char
        /// </summary>
        /// <param name="anyOf"></param>
        /// <param name="startIndex">The index to start at</param>
        /// <returns></returns>
        public int IndexOfAny(char[] anyOf, int startIndex)
        {
            return internalString.IndexOfAny(anyOf, startIndex);
        }
        
        /// <summary>
        /// Gets the index of any specified char
        /// </summary>
        /// <param name="anyOf">the chars to find</param>
        /// <param name="startIndex">The index to start at</param>
        /// <param name="count">The length to search</param>
        /// <returns></returns>
        public int IndexOfAny(char[] anyOf, int startIndex, int count)
        {
            return internalString.IndexOfAny(anyOf, startIndex, count);
        }
        
        /// <summary>
        /// Returns all the indexes of the specified string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<int> IndexOfAll(AdvancedString s)
        {
            List<int> r = new List<int>();
            int i = 0;
            
            while (true)
            {
                if (i > internalString.Length)
                    break;
                
                int t = internalString.IndexOf(s.ToString(), i);
                if (t == -1)
                    break;
                
                r.Add(t);
                
                // make sure its actually counting up
                i += (t == 0 ? 1 : t);
            }
            
            return r;
        }
        
        /// <summary>
        /// Returns all indexes of the specified string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<int> IndexOfAll(string s)
        {
            List<int> r = new List<int>();
            int i = 0;
            
            while (true)
            {
                if (i > internalString.Length)
                    break;
                
                int t = internalString.IndexOf(s, i);
                if (t == -1)
                    break;
                
                r.Add(t);
                
                // make sure its actually counting up
                i += (t == 0 ? 1 : t);
            }
            
            return r;
        }
        
        /// <summary>
        /// Returns all the indexes of the specified char
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<int> IndexOfAll(char s)
        {
            List<int> r = new List<int>();
            int i = 0;
            
            while (true)
            {
                if (i > internalString.Length)
                    break;
                
                int t = internalString.IndexOf(s, i);
                if (t == -1)
                    break;
                
                r.Add(t);
                
                // make sure its actually counting up
                i += (t == 0 ? 1 : t);
            }
            
            return r;
        }
        #endregion
        
        /// <summary>
        /// Pads the string to left with the specified length of the string
        /// </summary>
        /// <param name="count"></param>
        /// <param name="padChar"></param>
        /// <returns></returns>
        public AdvancedString PadLeft(int count, char padChar = ' ')
        {
            return new AdvancedString(internalString.PadLeft(count, padChar));
        }
        
        /// <summary>
        /// Pads the right of the string until it hits the specified length
        /// </summary>
        /// <param name="count"></param>
        /// <param name="padChar"></param>
        /// <returns></returns>
        public AdvancedString PadRight(int count, char padChar = ' ')
        {
            return new AdvancedString(internalString.PadRight(count, padChar));
        }
        
        #region Start/End With
        /// <summary>
        /// Checks if the string starts with s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="sc"></param>
        /// <returns></returns>
        public bool StartsWith(string s, StringComparison sc = StringComparison.CurrentCulture)
        {
            return internalString.StartsWith(s, sc);
        }
        
        /// <summary>
        /// Checks if the string starts with s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase">whether to check for case or not</param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool StartsWith(string s, bool ignoreCase, CultureInfo c)
        {
            return internalString.StartsWith(s, ignoreCase, c);
        }
        
        /// <summary>
        /// Checks if the string ends with s
        /// </summary>
        /// <param name="s">The string to search for</param>
        /// <param name="sc"></param>
        /// <returns></returns>
        public bool EndsWith(string s, StringComparison sc = StringComparison.CurrentCulture)
        {
            return internalString.EndsWith(s, sc);
        }
        
        /// <summary>
        /// Checks if the string ends with s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool EndsWith(string s, bool ignoreCase, CultureInfo c)
        {
            return internalString.EndsWith(s, ignoreCase, c);
        }
        #endregion
        
        /// <summary>
        /// Compares to an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return internalString.CompareTo(obj);
        }
        
        /// <summary>
        /// Converts to a char array
        /// </summary>
        /// <returns></returns>
        public char[] ToCharArray()
        {
            return internalString.ToCharArray();
        }
        
        /// <summary>
        /// Converts to a char array
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public char[] ToCharArray(int startIndex, int length)
        {
            return internalString.ToCharArray(startIndex, length);
        }
        
        #region CONCAT
        /// <summary>
        /// Sticks a bunch of AdvancedStrings together
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static AdvancedString Concat(params AdvancedString[] args)
        {
            AdvancedString a = "";
            foreach (AdvancedString a2 in args)
                a += a2;
            return a;
        }
        
        /// <summary>
        /// Sticks a bunch of AdvancedStrings together
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static AdvancedString Concat(IEnumerable<AdvancedString> args)
        {
            AdvancedString a = "";
            foreach (AdvancedString a2 in args)
                a += a2;
            return a;
        }
        
        /// <summary>
        /// Sticks a bunch of AdvancedStrings together
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static AdvancedString Concat(IEnumerable args)
        {
            AdvancedString a = "";
            foreach (object a2 in args)
                a += a2.ToString();
            return a;
        }
        
        /// <summary>
        /// Sticks a bunch of AdvancedStrings together
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static AdvancedString Concat(params object[] args)
        {
            AdvancedString a = "";
            foreach (object a2 in args)
                a += a2.ToString();
            return a;
        }
        #endregion
        
        /// <summary>
        /// Returns a copy of the specified AdvancedString
        /// </summary>
        /// <param name="old">The string to copy</param>
        /// <returns>The copy</returns>
        public static AdvancedString Copy(AdvancedString old)
        {
            return new AdvancedString(old.internalString);
        }
        
        /// <summary>
        /// Formats this string with the specified arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public AdvancedString Format(params object[] args)
        {
            this.internalString = string.Format(this.internalString, args);
            return this;
        }
        
        /// <summary>
        /// Formats the specified AdvancedString with the given arguments
        /// </summary>
        /// <param name="f">the AdvancedString format</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static AdvancedString Format(AdvancedString f, params object[] args)
        {
            return f.Format(args);
        }
        
        /// <summary>
        /// Removes a certain number of chars from the string
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString Remove(int startIndex, int count)
        {
            return internalString.Remove(startIndex, count);
        }
        
        /// <summary>
        /// Removes the string starting at startIndex
        /// </summary>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public AdvancedString Remove(int startIndex)
        {
            return internalString.Substring(0, startIndex);
        }
        
        /// <summary>
        /// Replaces strings
        /// </summary>
        /// <param name="old">The string to replace</param>
        /// <param name="n">The string to replace with</param>
        /// <returns></returns>
        public AdvancedString Replace(string old, string n)
        {
            return internalString.Replace(old, n);
        }
        
        /// <summary>
        /// Replaces chars
        /// </summary>
        /// <param name="old">The char to replace</param>
        /// <param name="n">The string to replace it with</param>
        /// <returns></returns>
        public AdvancedString Replace(char old, char n)
        {
            return internalString.Replace(old, n);
        }
        
        /// <summary>
        /// Replaces AdvancedStrings
        /// </summary>
        /// <param name="old">The old AdvancedString to replace</param>
        /// <param name="n">The AdvancedString to replace it with</param>
        /// <returns></returns>
        public AdvancedString Replace(AdvancedString old, AdvancedString n)
        {
            return internalString.Replace(old.ToString(), n.ToString());
        }
        
        /// <summary>
        /// Converts the string to uppercase
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToUpper()
        {
            return AdvancedString.From(internalString.ToUpper());
        }
        
        /// <summary>
        /// Converts the string to lowercase
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToLower()
        {
            return AdvancedString.From(internalString.ToLower());
        }
        
        /// <summary>
        /// Converts the string into sentence case string.
        /// Known Errors: If there is no space after the EoS punctuation char
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToSentenceCase()
        {
            AdvancedString r = "";
            
            bool wasLastCharEndOfSentence = true;
            bool willNextCharCapital = false;
            char lastChar = '\0';
            foreach (char c in internalString)
            {
                if (wasLastCharEndOfSentence)
                {
                    r += c.ToString().ToUpper();
                    wasLastCharEndOfSentence = false;
                }
                else
                {
                    r += c;
                }
                
                if ((willNextCharCapital))
                {
                    wasLastCharEndOfSentence = true;
                    willNextCharCapital = false;
                }
                
                if ((c == '!') || (c == '?') || (c == '.'))
                {
                    // its an EoS
                    wasLastCharEndOfSentence = true;
                    willNextCharCapital = true;
                }
                lastChar = c;
            }
            return r;
        }
        
        /// <summary>
        /// Converts the string into an Exclamation
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToExclamation()
        {
            if (string.IsNullOrEmpty(internalString))
                return internalString;
            
            if (internalString.EndsWith("!"))
                return internalString;
            // other sentence types
            if (internalString.EndsWith(".") ||  internalString.EndsWith("?"))
                return internalString.Substring(0, internalString.Length - 1) + "!"; //s.Substring(s.Length - 1, 1);
            
            // anything else
            return internalString + "!";
        }
        
        /// <summary>
        /// Converts the string into a question
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToQuestion()
        {
            if (string.IsNullOrEmpty(internalString))
                return internalString;
            
            if (internalString.EndsWith("?"))
                return internalString;
            // other sentence types
            if (internalString.EndsWith(".") ||  internalString.EndsWith("!"))
                return internalString.Substring(0, internalString.Length - 1) + "?"; //s.Substring(s.Length - 1, 1);
            
            // anything else
            return internalString + "?";
        }
        
        /// <summary>
        /// Converts the string into a sentence
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToSentence()
        {
            if (string.IsNullOrEmpty(internalString))
                return internalString;
            
            if (internalString.EndsWith("."))
                return internalString;
            // other sentence types
            if (internalString.EndsWith("!") ||  internalString.EndsWith("?"))
                return internalString.Substring(0, internalString.Length - 1) + "."; //s.Substring(s.Length - 1, 1);
            
            // anything else
            return internalString + ".";
        }
        
        /// <summary>
        /// Checks if the string is a palindrome (same forward and back)
        /// </summary>
        /// <returns></returns>
        public bool IsPalindrome()
        {
            if (internalString.Length == 1 || internalString.Length == 0)
                return true;
            return IsPalindrome(internalString);
        }
        
        /// <summary>
        /// The internal function to check for palindromes
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsPalindrome(string s)
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
        /// Checks if the AdvancedString contains the specified string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Contains(AdvancedString s)
        {
            return internalString.Contains(s.ToString());
        }
        
        /// <summary>
        /// Checks if the AdvancedString contains the specified string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Contains(string s)
        {
            return internalString.Contains(s.ToString());
        }
        
        /// <summary>
        /// Checks if the AdvancedString contains the specified string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Contains(char s)
        {
            return internalString.Contains(s.ToString());
        }
        
        /// <summary>
        /// Returns a copy of this AdvancedString
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new AdvancedString(this.internalString);
        }
        
        /// <summary>
        /// Returns the TypeCode of this object (String)
        /// </summary>
        /// <returns></returns>
        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }
        
        #region CONVERSIONS
        
        /// <summary>
        /// Tries to convert into a boolean
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public bool ToBoolean(IFormatProvider provider)
        {
            return bool.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a char
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public char ToChar(IFormatProvider provider)
        {
            return char.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into an SByte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            return sbyte.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a byte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte ToByte(IFormatProvider provider)
        {
            return byte.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a short
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short ToInt16(IFormatProvider provider)
        {
            return Int16.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into an unsigned short
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            return UInt16.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into an int
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int ToInt32(IFormatProvider provider)
        {
            return Int32.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into an unsigned int
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            return UInt32.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a long
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long ToInt64(IFormatProvider provider)
        {
            return Int64.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into an unsigned long
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            return UInt64.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a single/float
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float ToSingle(IFormatProvider provider)
        {
            return float.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a double
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double ToDouble(IFormatProvider provider)
        {
            return double.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a decimal
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            return decimal.Parse(internalString);
        }
        
        /// <summary>
        /// Tries to convert into a DateTime
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            return DateTime.Parse(internalString);
        }
        
        /// <summary>
        /// Returns this AdvancedString as a string
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider provider)
        {
            return internalString;
        }
        
        /// <summary>
        /// Does not work. Period.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new Exception();
        }
        #endregion
        
        /// <summary>
        /// Compares to the other AdvancedString
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(AdvancedString other)
        {
            AdvancedString a = other as AdvancedString;
            if (other == null || a == null)
                return 0;
            
            return (this == other ? 1 : 0);
        }
        
        /// <summary>
        /// Checks if this string equals the other string
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AdvancedString other)
        {
            return this.Equals(other as object);
        }
        
        /// <summary>
        /// Returns a reverse copy of this AdvancedString
        /// </summary>
        /// <returns></returns>
        public AdvancedString Reverse()
        {
            AdvancedString ret = "";
            // go through the string backwards
            for (int i = internalString.Length - 1; i >= 0; i--)
                ret += internalString[i];
            
            return ret;
        }
        
        /// <summary>
        /// Truncates from the given index
        /// </summary>
        /// <param name="lengthFromEnd"></param>
        /// <returns></returns>
        public AdvancedString Truncate(int lengthFromEnd)
        {
            string result = internalString;
            if ((lengthFromEnd > 0) && (internalString.Length > lengthFromEnd - 1))
                result = result.Remove(internalString.Length - lengthFromEnd, lengthFromEnd);
            return result;
        }
        
        /// <summary>
        /// Truncates from the index of the given string
        /// </summary>
        /// <param name="TruncateDownTo"></param>
        /// <returns></returns>
        public AdvancedString Truncate(string TruncateDownTo)
        {
            int removeDownTo = internalString.LastIndexOf(TruncateDownTo);
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = internalString.Length - removeDownTo;
            
            string result = internalString;
            
            if (internalString.Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);
            
            return result;
        }
        
        /// <summary>
        /// Truncates from the index of the given string
        /// </summary>
        /// <param name="TruncateDownTo"></param>
        /// <returns></returns>
        public AdvancedString Truncate(AdvancedString TruncateDownTo)
        {
            int removeDownTo = internalString.LastIndexOf(TruncateDownTo.ToString());
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = internalString.Length - removeDownTo;
            
            string result = internalString;
            
            if (internalString.Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);
            
            return result;
        }
        
        /// <summary>
        /// Truncates from the index of the given char
        /// </summary>
        /// <param name="TruncateDownTo"></param>
        /// <returns></returns>
        public AdvancedString Truncate(char TruncateDownTo)
        {
            int removeDownTo = internalString.LastIndexOf(TruncateDownTo);
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = internalString.Length - removeDownTo;
            
            string result = internalString;
            
            if (internalString.Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);
            
            return result;
        }
        
        /// <summary>
        /// Removes extra spaces
        /// </summary>
        /// <returns></returns>
        public AdvancedString RemoveExtraSpaces()
        {
            char[] delim = { ' ' };
            string[] lines = internalString.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string s in lines) {
                if (!String.IsNullOrEmpty(s.Trim()))
                    sb.Append(s + " ");
            }
            //remove the last pipe
            AdvancedString result = sb.ToAdvancedString().Truncate(1);
            return result.Trim();
        }
        
        /// <summary>
        /// Finds all occurrences of the specified regex
        /// </summary>
        /// <param name="regexString"></param>
        /// <returns></returns>
        public List<AdvancedString> Find(AdvancedString regexString)
        {
            Regex reg = new Regex(regexString.ToString(), RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);

            List<AdvancedString> result = new List<AdvancedString>();
            foreach (Match m in reg.Matches(internalString.ToString()))
                result.Add(m.Value.ToAdvancedString());
            return result;
        }
        
        /// <summary>
        /// Returns an inverted casing of all chars in the string
        /// e.g. Abc = aBC
        /// </summary>
        /// <returns></returns>
        public AdvancedString InvertCases()
        {
            AdvancedString r = "";
            foreach (char c in internalString)
            {
                if (char.IsLower(c))
                    r += char.ToUpper(c);
                else
                    r += char.ToLower(c);
            }
            return r;
        }
    }
}
