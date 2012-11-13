// AdvancedString - A fast, mutable string type
// Copyright 2011-2012 LoDC
// Currently faster than System.String, but slower than System.Text.StringBuilder
// History:
// January 24-26, 2012: creation
// May 18 2012 - documentation changes (spelling, etc.)
// May 19, 2012 - changed a lot of functions to void so as to lessen confusion 
//     about mutability, added ToList, fixed Split functions, changed some 
//     documentation, fixed comparison

using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using IExtendFramework.Text;
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
    /// An advanced, mutable, fast string type
    /// </summary>
    [Serializable]
    public class AdvancedString :
        IEnumerable,
    IEnumerable<char>,
    IComparable,
    ICloneable,
    IConvertible,
    IComparable<AdvancedString>,
    IEquatable<AdvancedString>,
    IDisposable
    {
        /// <summary>
        /// The internal "string"
        /// </summary>
        private List<char> internalString = new List<char>();

        /// <summary>
        /// An empty ("") AdvancedString
        /// </summary>
        public static readonly AdvancedString Empty = new AdvancedString("");
        /// <summary>
        /// Maximum string length
        /// </summary>
        public static readonly int MaxLength = int.MaxValue;
        /// <summary>
        /// Minimum string length (0)
        /// </summary>
        public static readonly int MinLength = 0;

        #region INIT

        /// <summary>
        /// Create an empty AdvancedString
        /// </summary>
        public AdvancedString()
        {
            //internalString = "";
        }

        /// <summary>
        /// Create an AdvancedString from the given normal string
        /// </summary>
        /// <param name="s"></param>
        public AdvancedString(string s)
        {
            //this.internalString = s;
            foreach (char c in s)
                internalString.Add(c);
        }

        /// <summary>
        /// Create an AdvancedString from the given char array
        /// </summary>
        /// <param name="chars"></param>
        public AdvancedString(List<char> chars)
        {
            internalString = chars;
        }

        /// <summary>
        /// Creates an AdvancedString from the given normal char
        /// </summary>
        /// <param name="c"></param>
        public AdvancedString(char c)
        {
            //this.internalString = c.ToString();
            internalString.Add(c);
        }

        /// <summary>
        /// Creates an AdvancedString by converting the byte into a string
        /// </summary>
        /// <param name="b"></param>
        public AdvancedString(byte b)
        {
            //this.internalString = Utilities.ByteToString(new byte[] { b});
            internalString.AddRange(Utilities.ByteToString(new byte[] { b }).ToCharArray());
        }


        /// <summary>
        /// Creates an AdvancedString by converting the byte array into a string
        /// </summary>
        /// <param name="b"></param>
        public AdvancedString(byte[] b)
        {
            this.internalString.AddRange(Utilities.ByteToString(b).ToCharArray());
        }

        /// <summary>
        /// Creates an AdvancedString from the given boolean
        /// </summary>
        /// <param name="b"></param>
        /// <param name="capitalizeFirstLetter">Whether the "T" or "F" of the bool should be capital</param>
        public AdvancedString(bool b, bool capitalizeFirstLetter = false)
        {
            if (capitalizeFirstLetter)
                this.internalString.AddRange(((b == true) ? "True" : "False").ToCharArray());
            else
                this.internalString.AddRange(((b == true) ? "true" : "false").ToCharArray());
        }

        /// <summary>
        /// Creates an AdvancedString from the given object
        /// </summary>
        /// <param name="o"></param>
        public AdvancedString(object o)
            : this(o.ToString())
        {
            //this.internalString = o.ToString();
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

        public AdvancedString(AdvancedString a)
        {
            this.internalString.AddRange(a.ToCharArray());
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

        // Implicit conversion from AdvancedString to String
        public static implicit operator String(AdvancedString a)
        {
            return a.ToString();
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
            List<char> tmp = new List<char>();
            tmp.AddRange(a1.internalString);
            tmp.AddRange(a2.internalString);
            return new AdvancedString(tmp);
        }

        public static AdvancedString operator +(string a1, AdvancedString a2)
        {
            List<char> tmp = new List<char>();
            tmp.AddRange(a1.ToCharArray());
            tmp.AddRange(a2.internalString);
            return new AdvancedString(tmp);
        }

        public static AdvancedString operator +(AdvancedString a1, string a2)
        {
            List<char> tmp = new List<char>();
            tmp.AddRange(a1.internalString);
            tmp.AddRange(a2.ToCharArray());
            return new AdvancedString(tmp);
        }

        public static AdvancedString operator *(AdvancedString a1, int count)
        {
            return AdvancedString.Repeat(a1, count);
        }

        public static AdvancedString operator *(int count, AdvancedString a1)
        {
            return AdvancedString.Repeat(a1, count);
        }

        public static AdvancedString operator >>(AdvancedString a1, int i)
        {
            AdvancedString a2 = new AdvancedString();
            foreach (char c in a1.internalString)
                a2.Append((char)(c >> i));
            return a2;
        }

        public static AdvancedString operator <<(AdvancedString a1, int i)
        {
            AdvancedString a2 = new AdvancedString();
            foreach (char c in a1.internalString)
                a2.Append((char)(c << i));
            return a2;
        }

        public static bool operator <(AdvancedString a1, object o)
        {
            return StringComparer.CurrentCulture.Compare(a1.ToString(), o.ToString()) == 1 ? true : false;

            AdvancedString b = o.ToAdvancedString();
            for (int i = 0; i < a1.Length; i++)
            {
                if (b.Length == i)
                    return false;

                if (a1.internalString[i] >= b.internalString[i])
                    return false;
            }


            return true;
        }

        public static bool operator >(AdvancedString a1, object o)
        {
            return StringComparer.CurrentCulture.Compare(a1.ToString(), o.ToString()) != 1 &&
                StringComparer.CurrentCulture.Compare(a1.ToString(), o.ToString()) != 0
            ? true : false;

            AdvancedString b = o.ToAdvancedString();
            for (int i = 0; i < a1.Length; i++)
            {
                if (b.Length == i)
                    return false;

                if (a1.internalString[i] <= b.internalString[i])
                    return false;
            }

            return true;
        }

        public static bool operator <=(AdvancedString a1, object o)
        {
            return StringComparer.CurrentCulture.Compare(a1.ToString(), o.ToString()) == 1 ||
                StringComparer.CurrentCulture.Compare(a1.ToString(), o.ToString()) == 0
            ? true : false;

            string b = o.ToString();
            for (int i = 0; i < a1.Length; i++)
            {
                if (b.Length == i)
                    return false;

                if (a1.internalString[i] > b[i])
                    return false;
            }

            return true;
        }

        public static bool operator >=(AdvancedString a1, object o)
        {
            return StringComparer.CurrentCulture.Compare(a1.ToString(), o.ToString()) != 1 ? true : false;

            string b = o.ToString();
            for (int i = 0; i < a1.Length; i++)
            {
                if (b.Length == i)
                    return false;

                if (a1.internalString[i] < b[i])
                    return false;
            }

            return true;
        }

        public static bool operator false(AdvancedString a1)
        {
            if (a1.Length == 0)
                return true;

            return false;
        }

        public static bool operator true(AdvancedString a1)
        {
            if (a1.Length != 0)
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
            // AdvancedStringBuilder causes overflow
            StringBuilder sb = new StringBuilder();
            foreach (char c in internalString)
                sb.Append(c);
            return sb.ToString();
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
                return internalString.Count;
            }
        }

        /// <summary>
        /// The Length of this string
        /// </summary>
        public int Count
        {
            get
            {
                return internalString.Count;
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

        #region INTERNAL METHODS

        private AdvancedString InternalSubstring(int low, int high = -1)
        {
            if (high == -1)
                high = this.Length;

            AdvancedStringBuilder asb = new AdvancedStringBuilder();
            for (int i = low; i < high; i++)
                asb.Append(internalString[i]);
            return asb.ToAdvancedString();
        }

        private AdvancedString[] InternalSplit(AdvancedString[] sep, int count, StringSplitOptions s)
        {
            List<AdvancedString> r = new List<AdvancedString>();
            List<char> old = new List<char>();
            StringBuilder sb;
            for (int i = 0; i < Length; i++)
            {
                char c = internalString[i];
                bool wasSep = false;
                foreach (AdvancedString sep2 in sep)
                {
                    if (c.ToString() == sep2.ToString())
                    {
                        sb = new StringBuilder();
                        foreach (char c2 in old)
                            sb.Append(c2);
                        r.Add(sb.ToString());
                        old.Clear();
                        wasSep = true;
                    }
                }
                if (!wasSep)
                    old.Add(c);
                if (r.Count >= count)
                    break;
            }
            // add any remaining chars
            sb = new StringBuilder();
            foreach (char c2 in old)
                sb.Append(c2);
            //if (sb.ToString().Length > 0)
            r.Add(sb.ToString());
            old.Clear();

            if (s == StringSplitOptions.RemoveEmptyEntries)
            {
                List<AdvancedString> r2 = new List<AdvancedString>();
                foreach (AdvancedString s3 in r)
                    if (!AdvancedString.IsNullOrEmpty(s3))
                        r2.Add(s3);
                return r2.ToArray();
            }
            else
                return r.ToArray();
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
                if (index < 0)
                    index = Length + index;

                return internalString[index];
            }
            set
            {
                if (index < 0)
                    index = Length + index;

                if (index > this.Length)
                    throw new Exception("Index cannot be greater then length!");

                AdvancedString a = InternalSubstring(0, index == 0 ? 0 : index - 1);
                AdvancedString b = "";
                b = InternalSubstring(index);
                internalString.Clear();
                // adds the AdvancedStrings together. Should be quite fast.
                this.internalString.AddRange((a + value.ToAdvancedString() + b).ToCharArray());
            }
        }

        /// <summary>
        /// Slices the string, also allows setting slice sections
        /// </summary>
        public AdvancedString this[int index1, int index2]
        {
            get
            {
                // convert negative to positive
                if (index1 < 0)
                    index1 = this.Length + index1;
                if (index2 < 0)
                    index2 = this.Length + index2;
                if (index2 < index1)
                {
                    AdvancedString o = InternalSubstring(index2, index1);
                    return o.Reverse().ToString();
                }
                else
                {
                    if (index2 > internalString.Count)
                        index2 = internalString.Count;

                    return InternalSubstring(index1, index2 - index1);
                }
            }
            set
            {
                // convert into positive numbers from end of string
                if (index1 < 0)
                    index1 = this.Length + index1;
                if (index2 < 0)
                    index2 = this.Length + index2;
                if (index2 < index1)
                    throw new Exception("Second slice index cannot be lower than the first!");
                if (index1 > index2)
                    throw new Exception("First slice index cannot be greater than the second!");
                if (index2 > Length)
                    index2 = Length;
                // trim value down to size
                if (value.Length > index2)
                    value = value.Substring(0, index2 - index1);

                char[] l = (InternalSubstring(0, index1) + value.ToString() + InternalSubstring(index2)).ToCharArray();
                internalString.Clear();
                internalString.AddRange(l);
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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                sb.Append(value[i]);
                sb.Append(separator.ToString());
            }
            // remove the last separator
            sb.Remove(sb.Length - separator.Length, separator.Length);
            return AdvancedString.From(sb.ToString());
        }

        /// <summary>
        /// Joins some AdvancedStrings together
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AdvancedString Join(AdvancedString separator, params object[] values)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                sb.Append(values[i].ToString());
                sb.Append(separator.ToString());
            }
            // remove the last separator
            sb.Remove(sb.Length - separator.Length, separator.Length);
            return AdvancedString.From(sb.ToString());
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
                start = this.Length + start;
            if (end < 0)
                end = this.Length + end;
            if (end > Length)
                end = Length;

            if (end < start)
            {
                AdvancedString o = InternalSubstring(start, end);
                return o.Reverse();
            }
            else
            {
                if (end > internalString.Count)
                    end = internalString.Count;

                return InternalSubstring(start, end - start);
            }
        }

        /// <summary>
        /// Adds the objects string representation into this string
        /// </summary>
        /// <param name="o"></param>
        public void Append(object o)
        {
            internalString.AddRange(o.ToString().ToCharArray());
        }

        #region SUBSTRING
        /// <summary>
        /// Gets the specified substring
        /// </summary>
        /// <param name="low"></param>
        /// <returns></returns>
        public AdvancedString Substring(int low)
        {
            return InternalSubstring(low);
        }

        /// <summary>
        /// Gets the specified substring
        /// </summary>
        /// <param name="l"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public AdvancedString Substring(int l, int h)
        {
            return new AdvancedString(InternalSubstring(l, h));
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
            internalString.InsertRange(index, s.internalString.ToArray());
            return this;
        }

        /// <summary>
        /// Inserts a string at the index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public AdvancedString Insert(int index, string s)
        {
            internalString.InsertRange(index, s.ToCharArray());
            return this;
        }

        /// <summary>
        /// Inserts a char at the index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public AdvancedString Insert(int index, char c)
        {
            internalString.Insert(index, c);
            return this;
        }
        #endregion

        #region NULL/WHITESPACE CHECKS
        /// <summary>
        /// Checks if the AdvancedString is null or empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(AdvancedString s)
        {
            if (s == null)
                return true;
            if (s.Length == 0)
                return true;

            return false;
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
            if (s == null || s.Length == 0)
                return true;
            foreach (char c in s)
                if (!char.IsWhiteSpace(c))
                    return false;

            return true;
        }
        #endregion

        #region SPLITTING
        /// <summary>
        /// splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(params char[] separators)
        {
            return Split(separators, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char[] separators, int count)
        {
            return Split(separators, count, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char[] separators, StringSplitOptions options)
        {
            return Split(separators, int.MaxValue, options);
        }

        /// <summary>
        /// Splits the AdvancedString
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char[] separators, int count, StringSplitOptions options)
        {
            List<AdvancedString> sep = new List<AdvancedString>();
            foreach (char c in separators)
                sep.Add(c);
            return InternalSplit(sep.ToArray(), count, options);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string[] separator, StringSplitOptions options)
        {
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s in separator)
                a.Add(s);
            return InternalSplit(a.ToArray(), int.MaxValue, options);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string[] separator, int count)
        {
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s in separator)
                a.Add(s);
            return InternalSplit(a.ToArray(), count, StringSplitOptions.None);
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
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s in separator)
                a.Add(s);
            return InternalSplit(a.ToArray(), count, options);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char separator)
        {
            return InternalSplit(new AdvancedString[] { new AdvancedString(separator) }, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char separator, int count)
        {
            AdvancedString a = separator;
            return InternalSplit(new AdvancedString[] { a }, count, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public AdvancedString[] Split(char separator, StringSplitOptions o)
        {
            AdvancedString a = separator;
            return InternalSplit(new AdvancedString[] { a }, int.MaxValue, o);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string separator)
        {
            AdvancedString a = separator;
            return InternalSplit(new AdvancedString[] { a }, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string separator, int count)
        {
            AdvancedString a = separator;
            return InternalSplit(new AdvancedString[] { a }, count, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public AdvancedString[] Split(string separator, StringSplitOptions o)
        {
            AdvancedString a = separator;
            return InternalSplit(new AdvancedString[] { a }, int.MaxValue, o);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public AdvancedString[] Split(AdvancedString separator)
        {
            return InternalSplit(new AdvancedString[] { separator }, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString[] Split(AdvancedString separator, int count)
        {
            return InternalSplit(new AdvancedString[] { separator }, count, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public AdvancedString[] Split(AdvancedString separator, StringSplitOptions o)
        {
            return InternalSplit(new AdvancedString[] { separator }, int.MaxValue, o);
        }
        #endregion

        #region TRIMMING
        /// <summary>
        /// Trims whitespace from this string, then returns itself
        /// </summary>
        /// <returns></returns>
        public AdvancedString Trim()
        {
            while (internalString[0] == ' ')
                internalString.RemoveAt(0);
            while (internalString[internalString.Count - 1] == ' ')
                internalString.RemoveAt(internalString.Count - 1);
            return this;
        }

        /// <summary>
        /// Trims whitespace from the beginning of this string, then returns itself
        /// </summary>
        /// <returns></returns>
        public AdvancedString TrimStart()
        {
            while (internalString[0] == ' ')
                internalString.RemoveAt(0);
            return this;
        }

        /// <summary>
        /// Trims whitespace from the end of this string, then returns itself
        /// </summary>
        /// <returns></returns>
        public AdvancedString TrimEnd()
        {
            while (internalString[internalString.Count - 1] == ' ')
                internalString.RemoveAt(internalString.Count - 1);
            return this;
        }
        #endregion

        #region IndexOf
        /// <summary>
        /// Gets the index of the char
        /// </summary>
        /// <param name="c">the char to find</param>
        /// <param name="index">The index to start at</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int IndexOf(char c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = Length;

            return internalString.IndexOf(c, index, count);
        }

        /// <summary>
        /// Gets the index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">The index to start at</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns>Where the char was found (-1 if not found)</returns>
        public int IndexOf(string c, int startIndex = 0, int count = -1, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (count == -1)
                count = Length;

            switch (comparisonType)
            {
                case StringComparison.CurrentCulture:
                    return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this.ToString(), c, startIndex, count, CompareOptions.None);

                case StringComparison.CurrentCultureIgnoreCase:
                    return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this.ToString(), c, startIndex, count, CompareOptions.IgnoreCase);

                case StringComparison.InvariantCulture:
                    return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this.ToString(), c, startIndex, count, CompareOptions.None);

                case StringComparison.InvariantCultureIgnoreCase:
                    return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this.ToString(), c, startIndex, count, CompareOptions.IgnoreCase);

                case StringComparison.Ordinal:
                    return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this.ToString(), c, startIndex, count, CompareOptions.Ordinal);

                case StringComparison.OrdinalIgnoreCase:
                    throw new Exception("Comparison not supported!");
                //return TextInfo.IndexOfStringOrdinalIgnoreCase(this, c, startIndex, count);

                default:
                    throw new ArgumentException("Unknown comparison type!");
            }
        }

        /// <summary>
        /// Gets the index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">The index to start at</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int IndexOf(AdvancedString c, int index = 0, int count = -1, StringComparison sc = StringComparison.CurrentCulture)
        {
            if (count == -1)
                count = Length;

            return this.IndexOf(c.ToString(), index, count, sc);
        }

        /// <summary>
        /// Gets the last index of the char
        /// </summary>
        /// <param name="c">the char to find</param>
        /// <param name="index">The index to start at</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int LastIndexOf(char c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = Length;

            return internalString.LastIndexOf(c, index, count);
        }

        /// <summary>
        /// Gets the last index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">The index to start at</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int LastIndexOf(string c, int index = 0, int count = -1, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (count == -1)
                count = Length;

            switch (comparisonType)
            {
                case StringComparison.CurrentCulture:
                    return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, c, index, count, CompareOptions.None);

                case StringComparison.CurrentCultureIgnoreCase:
                    return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, c, index, count, CompareOptions.IgnoreCase);

                case StringComparison.InvariantCulture:
                    return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, c, index, count, CompareOptions.None);

                case StringComparison.InvariantCultureIgnoreCase:
                    return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, c, index, count, CompareOptions.IgnoreCase);

                case StringComparison.Ordinal:
                    return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, c, index, count, CompareOptions.Ordinal);

                case StringComparison.OrdinalIgnoreCase:
                    throw new NotSupportedException("Comparison not supported!");
                //return TextInfo.LastIndexOfStringOrdinalIgnoreCase(this, c, startIndex, count);
                default:
                    throw new ArgumentException("Unknown comparison type!");
            }
        }

        /// <summary>
        /// Gets the index of the string
        /// </summary>
        /// <param name="c">the string to find</param>
        /// <param name="index">The index to start at</param>
        /// <param name="count">Maximum length to search</param>
        /// <returns></returns>
        public int LastIndexOf(AdvancedString c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = Length;

            return this.LastIndexOf(c.ToString(), index, count);
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
                if (i > Length)
                    break;

                int t = this.IndexOf(s.ToString(), i);
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
                if (i > Length)
                    break;

                int t = this.IndexOf(s, i);
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
                if (i > Length)
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
        /// Returns the first index of any of the given chars
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int IndexOfAny(char[] c)
        {
            int r = -1;
            int index = 0;
            foreach (char c1 in internalString)
            {
                foreach (char c2 in c)
                {
                    if (c1 == c2)
                    {
                        r = index;
                        break;
                    }
                }
                index++;
            }
            return r;
        }

        /// <summary>
        /// Returns the last index of any of the given characters
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int LastIndexOfAny(char[] c)
        {
            int r = -1;
            int index = Count - 1;
            for (int i = Length - 1; i > 0; i--)
            {
                char c2 = internalString[i];
                foreach (char c1 in c)
                {
                    if (c2 == c1)
                        r = index;
                }
                index--;
            }
            return r;
        }
        #endregion

        #region PADDING
        /// <summary>
        /// Pads this string to left with the specified length of the string
        /// </summary>
        /// <param name="count"></param>
        /// <param name="padChar"></param>
        /// <returns></returns>
        public void PadLeft(int count, char padChar = ' ')
        {
            while (this.Length < count)
                this.Insert(0, padChar);
        }

        /// <summary>
        /// Pads this right of the string until it hits the specified length
        /// </summary>
        /// <param name="count"></param>
        /// <param name="padChar"></param>
        /// <returns></returns>
        public void PadRight(int count, char padChar = ' ')
        {
            while (this.Length < count)
                this.Append(padChar);
        }
        #endregion

        #region Start/End With

        /// <summary>
        /// Checks if the string starts with s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase">whether to check for case or not</param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool StartsWith(string s, bool ignoreCase, CultureInfo c)
        {
            return c.CompareInfo.IsPrefix(this.ToString(), s, ignoreCase == true ? CompareOptions.IgnoreCase : CompareOptions.None);
        }

        /// <summary>
        /// Checks if the string starts with s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase">whether to check for case or not</param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool StartsWith(string s, bool ignoreCase = false)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this.ToString(), s, ignoreCase == true ? CompareOptions.IgnoreCase : CompareOptions.None);
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
            return c.CompareInfo.IsSuffix(this.ToString(), s, ignoreCase == true ? CompareOptions.IgnoreCase : CompareOptions.None);
        }

        /// <summary>
        /// Checks if the string ends with s
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool EndsWith(string s, bool ignoreCase = false)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this.ToString(), s, ignoreCase == true ? CompareOptions.IgnoreCase : CompareOptions.None);
        }
        #endregion

        /// <summary>
        /// Compares to an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return this.Equals(obj) == true ? 1 : 0;
        }

        #region ToCharArray/ToList
        /// <summary>
        /// Converts to a char array
        /// </summary>
        /// <returns></returns>
        public char[] ToCharArray()
        {
            return internalString.ToArray();
        }

        /// <summary>
        /// Converts to a char array
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public char[] ToCharArray(int startIndex, int length)
        {
            List<char> ret = new List<char>();
            for (int i = startIndex; i < length; i++)
                ret.Add(internalString[i]);
            return ret.ToArray();
        }

        public List<char> ToList()
        {
            return internalString;
        }
        #endregion

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
            return old.Clone() as AdvancedString;
        }

        /// <summary>
        /// Formats this string with the specified arguments, then returns itself
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public AdvancedString Format(params object[] args)
        {
            string t = this.ToString();
            this.internalString.Clear();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(t, args);
            this.internalString.AddRange(sb.ToString().ToCharArray());
            return this;
        }

        /// <summary>
        /// Formats the specified AdvancedString with the given arguments
        /// </summary>
        /// <param name="f">the AdvancedString format</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static AdvancedString FormatAdvancedString(AdvancedString f, params object[] args)
        {
            return f.Format(args);
        }

        #region Remove/Replace
        /// <summary>
        /// Removes a certain number of chars from the string. Not applied to the original string
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public AdvancedString Remove(int startIndex, int count)
        {
            AdvancedString s = Clone() as AdvancedString;
            s.internalString.RemoveRange(startIndex, count);
            return s;
        }

        /// <summary>
        /// Removes the string starting at startIndex. Not applied to the original string
        /// </summary>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public AdvancedString Remove(int startIndex)
        {
            AdvancedString s = (Clone() as AdvancedString).Substring(0, startIndex);//.InternalSubstring(startIndex, Length);
            //s.internalString.Clear();
            //s.internalString.AddRange(s.ToCharArray());
            return s;
        }

        /// <summary>
        /// Replaces strings. Not applied to the original string
        /// </summary>
        /// <param name="old">The string to replace</param>
        /// <param name="n">The string to replace with</param>
        /// <returns></returns>
        public AdvancedString Replace(string old, string n)
        {
            return Replace(old.ToAdvancedString(), n.ToAdvancedString());
        }

        /// <summary>
        /// Replaces chars
        /// </summary>
        /// <param name="old">The char to replace</param>
        /// <param name="n">The string to replace it with</param>
        /// <returns></returns>
        public AdvancedString Replace(char old, char n)
        {
            AdvancedString s = AdvancedString.Copy(this);
            for (int i = 0; i < s.internalString.Count; i++)
            {
                char c = s.internalString[i];
                if (c == old)
                    s.internalString[i] = n;
            }
            return s;
        }

        /// <summary>
        /// Replaces AdvancedStrings. Not applied to the original string
        /// </summary>
        /// <param name="old">The old AdvancedString to replace</param>
        /// <param name="n">The AdvancedString to replace it with</param>
        /// <returns></returns>
        public AdvancedString Replace(AdvancedString old, AdvancedString n)
        {
            AdvancedString s = AdvancedString.Copy(this);
            while (s.IndexOf(old) != -1)
                s = s.Substring(0, s.IndexOf(old)) + n + s.Substring(s.IndexOf(old) + old.Length);
            return s;
        }
        #endregion

        /// <summary>
        /// Converts the string to uppercase. Not applied to the original string
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToUpper()
        {
            AdvancedString s = "";
            foreach (char c in internalString)
                s.Append(char.ToUpper(c));
            return s;
        }

        /// <summary>
        /// Converts the string to lowercase. Not applied to the original string
        /// </summary>
        /// <returns></returns>
        public AdvancedString ToLower()
        {
            AdvancedString s = "";
            foreach (char c in internalString)
                s.Append(char.ToLower(c));
            return s;
        }

        /// <summary>
        /// Converts the string into sentence case string. Not applied to the original string
        /// Known Errors: If there is no space after the End-Of-Sentence punctuation char
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
                    r += char.ToUpper(c);
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
        /// Converts this string into an Exclamation
        /// </summary>
        /// <returns></returns>
        public void ToExclamation()
        {
            if (string.IsNullOrEmpty(ToString()))
                return;

            if (EndsWith("!"))
                return;
            // other sentence types
            if (EndsWith(".") || EndsWith("?"))
            {
                AdvancedString s = InternalSubstring(0, Length - 1);
                internalString.Clear();
                internalString.AddRange(s.ToCharArray());
            }

            // anything else
            this.Append("!");
        }

        /// <summary>
        /// Converts this string into a question
        /// </summary>
        /// <returns></returns>
        public void ToQuestion()
        {
            if (string.IsNullOrEmpty(ToString()))
                return;

            if (EndsWith("?"))
                return;
            // other sentence types
            if (EndsWith(".") || EndsWith("!"))
            {
                AdvancedString s = InternalSubstring(0, Length - 1);
                internalString.Clear();
                internalString.AddRange(s.ToCharArray());
            }

            // anything else
            this.Append("?");
        }

        /// <summary>
        /// Converts this string into a sentence
        /// </summary>
        /// <returns></returns>
        public void ToSentence()
        {
            if (AdvancedString.IsNullOrEmpty(this))
                return;

            if (this.EndsWith("."))
                return;
            // other sentence types
            if (EndsWith("?") || EndsWith("!"))
            {
                AdvancedString s = InternalSubstring(0, Length - 1);
                internalString.Clear();
                internalString.AddRange(s.ToCharArray());
            }

            // anything else
            this.Append(".");
        }

        /// <summary>
        /// Checks if the string is a palindrome (same forward and back)
        /// </summary>
        /// <returns></returns>
        public bool IsPalindrome()
        {
            if (Length == 1 || Length == 0)
                return true;
            return IsPalindrome(this);
        }

        /// <summary>
        /// The internal function to check for palindromes
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsPalindrome(AdvancedString s)
        {
            if (s.Length == 1 || s.Length == 0)
                return true;
            if (s.Substring(0, 1).ToLower() == s.Substring(s.Length - 1, 1).ToLower())
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
            return IndexOf(s) >= 0;
        }

        /// <summary>
        /// Checks if the AdvancedString contains the specified string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Contains(string s)
        {
            return IndexOf(s) >= 0;
        }

        /// <summary>
        /// Checks if the AdvancedString contains the specified string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Contains(char s)
        {
            return IndexOf(s) >= 0;
        }

        /// <summary>
        /// Returns a copy of this AdvancedString
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new AdvancedString() { internalString = this.internalString };
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
            return bool.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a char
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public char ToChar(IFormatProvider provider)
        {
            return char.Parse(this);
        }

        /// <summary>
        /// Tries to convert into an SByte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            return sbyte.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a byte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte ToByte(IFormatProvider provider)
        {
            return byte.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a short
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short ToInt16(IFormatProvider provider)
        {
            return Int16.Parse(this);
        }

        /// <summary>
        /// Tries to convert into an unsigned short
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            return UInt16.Parse(this);
        }

        /// <summary>
        /// Tries to convert into an int
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int ToInt32(IFormatProvider provider)
        {
            return Int32.Parse(this);
        }

        /// <summary>
        /// Tries to convert into an unsigned int
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            return UInt32.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a long
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long ToInt64(IFormatProvider provider)
        {
            return Int64.Parse(this);
        }

        /// <summary>
        /// Tries to convert into an unsigned long
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            return UInt64.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a single/float
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float ToSingle(IFormatProvider provider)
        {
            return float.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a double
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double ToDouble(IFormatProvider provider)
        {
            return double.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a decimal
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            return decimal.Parse(this);
        }

        /// <summary>
        /// Tries to convert into a DateTime
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            return DateTime.Parse(this);
        }

        /// <summary>
        /// Returns this AdvancedString as a string
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        /// <summary>
        /// Does not work. Period.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotSupportedException();
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
            for (int i = Length - 1; i >= 0; i--)
                ret += internalString[i];

            return ret;
        }

        #region TRUNCATE
        /// <summary>
        /// Truncates from the given index. Not applied to the original string
        /// </summary>
        /// <param name="lengthFromEnd"></param>
        /// <returns></returns>
        public AdvancedString Truncate(int lengthFromEnd)
        {
            string result = this.ToString();
            if ((lengthFromEnd > 0) && (Length > lengthFromEnd - 1))
                result = result.Remove(Length - lengthFromEnd, lengthFromEnd);
            return result;
        }

        /// <summary>
        /// Truncates from the index of the given string. Not applied to the original string
        /// </summary>
        /// <param name="TruncateDownTo"></param>
        /// <returns></returns>
        public AdvancedString Truncate(string TruncateDownTo)
        {
            int removeDownTo = this.ToString().LastIndexOf(TruncateDownTo);
            if (removeDownTo == -1)
                return AdvancedString.Empty;
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = Length - removeDownTo;

            if (Length > removeFromEnd - 1)
                return this.Remove(removeDownTo, removeFromEnd);

            return AdvancedString.Empty;
        }

        /// <summary>
        /// Truncates from the index of the given string. Not applied to the original string
        /// </summary>
        /// <param name="TruncateDownTo"></param>
        /// <returns></returns>
        public AdvancedString Truncate(AdvancedString TruncateDownTo)
        {
            int removeDownTo = this.LastIndexOf(TruncateDownTo.ToString());
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = Length - removeDownTo;

            string result = this.ToString();

            if (Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);

            return result;
        }

        /// <summary>
        /// Truncates from the index of the given char. Not applied to the original string
        /// </summary>
        /// <param name="TruncateDownTo"></param>
        /// <returns></returns>
        public AdvancedString Truncate(char TruncateDownTo)
        {
            int removeDownTo = internalString.LastIndexOf(TruncateDownTo);
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = Length - removeDownTo;

            string result = this.ToString();

            if (Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);

            return result;
        }
        #endregion

        /// <summary>
        /// Removes extra spaces
        /// </summary>
        /// <returns></returns>
        public void RemoveExtraSpaces()
        {
            AdvancedString[] lines = InternalSplit(new AdvancedString[] { ' ' }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (AdvancedString s in lines)
            {
                if (AdvancedString.IsNullOrEmpty(s.Trim()))
                    sb.Append(s + " ");
            }
            //remove the last space
            this.internalString = sb.ToAdvancedString().Truncate(1).internalString;
        }

        /// <summary>
        /// Finds all occurrences of the specified regex
        /// </summary>
        /// <param name="regexString"></param>
        /// <returns></returns>
        public List<AdvancedString> Find(AdvancedString regexString)
        {
            Regex reg = new Regex(regexString, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);

            List<AdvancedString> result = new List<AdvancedString>();
            foreach (Match m in reg.Matches(this.ToString()))
                result.Add(m.Value.ToAdvancedString());
            return result;
        }

        /// <summary>
        /// Returns an inverted casing of all chars in the . Not applied to the original string
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

        /// <summary>
        /// Creates a string by concatenating 's' n times
        /// </summary>
        /// <param name="s"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static AdvancedString Repeat(AdvancedString s, int n)
        {
            AdvancedString a = new AdvancedString();
            for (int i = 1; i <= n; i++)
                a.Append(s);

            return a;
        }

        public void Dispose()
        {
            internalString.Clear();
            internalString = null;
        }
    }
}
