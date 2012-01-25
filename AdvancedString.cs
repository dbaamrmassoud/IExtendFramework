/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/24/2012
 * Time: 3:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
namespace IExtendFramework
{
    [ComVisible(true)]
    [Serializable]
    public class AdvancedString : IEnumerable, IEnumerable<char>// : IComparable, ICloneable, IConvertible, IComparable<AdvancedString>, IEnumerable<char>, IEnumerable, IEquatable<AdvancedString>
    {
        private string str;
        
        public static AdvancedString Empty = new AdvancedString("");
        
        #region Init
        
        public AdvancedString()
        {
            str = "";
        }
        
        public AdvancedString(string s)
        {
            this.str = s;
        }
        
        public AdvancedString(char c)
        {
            this.str = c.ToString();
        }
        
        public AdvancedString(byte b)
        {
            this.str = Utilities.ByteToString(new byte[] { b});
        }
        
        public AdvancedString(byte[] b)
        {
            this.str = Utilities.ByteToString(b);
        }
        
        public AdvancedString(bool b, bool capitalizeFirstLetter = false)
        {
            if (capitalizeFirstLetter)
                this.str = (b == true) ? "True" : "False";
            else
                this.str = (b == true) ? "true" : "false";
        }
        
        public AdvancedString(object o)
        {
            this.str = o.ToString();
        }
        
        public static AdvancedString From(string s)
        {
            return new AdvancedString(s);
        }
        
        public static AdvancedString From(char c)
        {
            return new AdvancedString(c);
        }
        
        public static AdvancedString From(byte b)
        {
            return new AdvancedString(b);
        }
        
        public static AdvancedString From(byte[] b)
        {
            return new AdvancedString(b);
        }
        #endregion
        
        public override string ToString()
        {
            return str;
        }
        
        #region Enumeration
        IEnumerator IEnumerable.GetEnumerator()
        {
            return str.GetEnumerator();
        }
        
        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            return str.GetEnumerator();
        }
        #endregion
        
        public int Length
        {
            get
            {
                return str.Length;
            }
        }
        
        #region Equals and GetHashCode implementation
        public override bool Equals(object obj)
        {
            AdvancedString other = obj as AdvancedString;
            if (other == null)
                return false;
            return this.str == other.str;
        }
        
        public override int GetHashCode()
        {
            return str.GetHashCode();
        }
        
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
        #endregion
        
        public char this[int index]
        {
            get
            {
                return str[index];
            }
            set
            {
                if (index > this.str.Length)
                    throw new Exception("Index cannot be greater then length!");
                
                string a = str.Substring(0, index == 0 ? 0 : index - 1);
                Console.WriteLine(a);
                string b = "";
                b = str.Substring(index);
                this.str = a + value.ToString() + b;
            }
        }
        
        public string this[int index1, int index2]
        {
            get
            {
                if (index2 < 0)
                    index2 = this.str.Length + index2;
                
                int l = index1 - index2;
                return str.Substring(l);
            }
        }
        
        #region Join
        public static AdvancedString Join(AdvancedString separator, params string[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return AdvancedString.Join(separator, value, 0, value.Length);
        }
        
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
        
        public static AdvancedString Join(AdvancedString separator, IEnumerable<string> values)
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
        
        public static AdvancedString Join(string separator, params string[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return AdvancedString.Join(separator, value, 0, value.Length);
        }
        
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
        
        public AdvancedString Slice(int start, int end)
        {
            // Handles negative ends (like Python)
            if (end < 0)
            {
                end = str.Length + end;
            }

            int length = end - start;
            
            return new AdvancedString(str.Substring(start, length));
        }
        
        public AdvancedString SubString(int low)
        {
            return AdvancedString.From(str.Substring(low));
        }
        
        public AdvancedString SubString(int l, int h)
        {
            return new AdvancedString(str.Substring(l, h));
        }
        
        public AdvancedString Insert(int index, AdvancedString s)
        {
            return new AdvancedString(str.Insert(index, s.ToString()));
        }
        
        public AdvancedString Insert(int index, string s)
        {
            return new AdvancedString(str.Insert(index, s));
        }
        
        public static bool IsNullOrEmpty(AdvancedString s)
        {
            return string.IsNullOrEmpty(s.str);
        }
        
        public static bool IsWhiteSpace(AdvancedString s)
        {
            foreach (char c in s)
                if (!char.IsWhiteSpace(c))
                    return false;
            return true;
        }
        
        public static bool IsNullOrWhiteSpace(AdvancedString s)
        {
            return string.IsNullOrWhiteSpace(s.str);
        }
        
        #region Splitting
        public AdvancedString[] Split(params char[] separator)
        {
            string[] s = str.Split(separator);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(char[] separator, int count)
        {
            string[] s = str.Split(separator, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(char[] separator, StringSplitOptions options)
        {
            string[] s = str.Split(separator, 2147483647, options);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(char[] separator, int count, StringSplitOptions options)
        {
            string[] s = str.Split(separator, count, options);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(string[] separator, StringSplitOptions options)
        {
            return this.Split(separator, 2147483647, options);
        }
        
        public AdvancedString[] Split(string[] separator, int count)
        {
            string[] s = str.Split(separator, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(string[] separator, int count, StringSplitOptions options)
        {
           string[] s = str.Split(separator, count, options);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(char separator)
        {
            string[] s = str.Split(new char[] { separator}, 2147483647, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(char separator, int count)
        {
            string[] s = str.Split(new char[] {separator}, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(char separator, StringSplitOptions o)
        {
            string[] s = str.Split(new char[] {separator}, 2147483647, o);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(string separator)
        {
            string[] s = str.Split(new string[] { separator}, 2147483647, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(string separator, int count)
        {
            string[] s = str.Split(new string[] {separator}, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(string separator, StringSplitOptions o)
        {
            string[] s = str.Split(new string[] {separator}, 2147483647, o);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(AdvancedString separator)
        {
            string[] s = str.Split(new string[] { separator.ToString()}, 2147483647, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(AdvancedString separator, int count)
        {
            string[] s = str.Split(new string[] {separator.ToString()}, count, StringSplitOptions.None);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        
        public AdvancedString[] Split(AdvancedString separator, StringSplitOptions o)
        {
            string[] s = str.Split(new string[] {separator.ToString()}, 2147483647, o);
            List<AdvancedString> a = new List<AdvancedString>();
            foreach (string s2 in s)
                a.Add(new AdvancedString(s2));
            return a.ToArray();
        }
        #endregion
        
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
            foreach (char c in a1.str)
                a2 += new AdvancedString((char)(c >> i));
            return a2;
        }
        
        public static AdvancedString operator <<(AdvancedString a1, int i)
        {
            AdvancedString a2 = new AdvancedString();
            foreach (char c in a1.str)
                a2 += new AdvancedString((char)(c << i));
            return a2;
        }
        
        public AdvancedString Trim()
        {
            return new AdvancedString(str.Trim());
        }
        
        public AdvancedString Trim(params char[] tchars)
        {
            return new AdvancedString(str.Trim(tchars));
        }
        
        public int IndexOf(char c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = str.Length;
            
            return str.IndexOf(c, index, count);
        }
        
        public int IndexOf(string c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = str.Length;
            
            return str.IndexOf(c, index, count);
        }
        
        public int IndexOf(AdvancedString c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = str.Length;
            
            return str.IndexOf(c.str, index, count);
        }
        
        public int LastIndexOf(char c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = str.Length;
            
            return str.LastIndexOf(c, index, count);
        }
        
        public int LastIndexOf(string c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = str.Length;
            
            return str.LastIndexOf(c, index, count);
        }
        
        public int LastIndexOf(AdvancedString c, int index = 0, int count = -1)
        {
            if (count == -1)
                count = str.Length;
            
            return str.LastIndexOf(c.ToString(), index, count);
        }
        
        public AdvancedString PadLeft(int count, char padChar = ' ')
        {
            return new AdvancedString(str.PadLeft(count, padChar));
        }
        
        public AdvancedString PadRight(int count, char padChar = ' ')
        {
            return new AdvancedString(str.PadRight(count, padChar));
        }
        
        public bool StartsWith(string s)
        {
            return str.StartsWith(s);
        }
    }
}
