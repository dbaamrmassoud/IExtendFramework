/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/26/2012
 * Time: 2:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace IExtendFramework.Text
{
    public enum InsertType
    {
        Beginning,
        End
    }
    
    /// <summary>
    /// An advanced stringbuiler, theoretically faster then the normal one.
    /// </summary>
    public class AdvancedStringBuilder
    {
        private LinkedList<AdvancedString> strings;
        
        /// <summary>
        /// The strings this AdvancedStringBuilder is composed of.
        /// </summary>
        private LinkedList<AdvancedString> Strings
        {
            get
            {
                return this.strings;
            }
            set
            {
                this.strings = value;
            }
        }
        
        /// <summary>
        /// Returns the collected string
        /// </summary>
        public string Value
        {
            get
            {
                string str;
                if (this.Strings.Count == 0)
                {
                    str = null;
                }
                else
                {
                    StringBuilder builder = this.ToStringBuilder();
                    str = builder.ToString();
                    this.Strings.Clear();
                    this.Strings.AddLast(new AdvancedString(str));
                }
                return str;
            }
        }
        
        /// <summary>
        /// The length of the collected string
        /// </summary>
        public int Length
        {
            get
            {
                return ToString().Length;
            }
        }
        
        /// <summary>
        /// Creates a AdvancedStringBuilder that contains an empty string.
        /// </summary>
        public AdvancedStringBuilder()
            : this(string.Empty)
        {
        }
        
        /// <summary>
        /// Creates a AdvancedStringBuilder that contains the specified string.
        /// </summary>
        /// <param name="str">The initial string.</param>
        public AdvancedStringBuilder(string str)
        {
            this.Strings = new LinkedList<AdvancedString>();
            if (str != null)
                this.Append(str);
        }
        
        /// <summary>
        /// Adds the specified AdvancedStringBuilder to the beginning of this AdvancedStringBuilder.
        /// </summary>
        /// <param name="builder">The AdvancedStringBuilder to prefix.</param>
        public void Prepend(AdvancedStringBuilder builder)
        {
            this.Insert(builder.ToString(), InsertType.Beginning);
        }
        
        /// <summary>
        /// Adds the specified AdvancedStringBuilder to the end of this AdvancedStringBuilder.
        /// </summary>
        /// <param name="builder">The AdvancedStringBuilder to suffix.</param>
        public void Append(AdvancedStringBuilder builder)
        {
            this.Insert(builder.ToString(), InsertType.End);
        }
        
        /// <summary>
        /// Adds the specified string to the beginning of this AdvancedStringBuilder.
        /// </summary>
        /// <param name="str">The string to prefix.</param>
        public void Prepend(string str)
        {
            this.Insert(str, InsertType.Beginning);
        }
        
        /// <summary>
        /// Adds the specified string to the end of this builder=.
        /// </summary>
        /// <param name="str">The string to suffix.</param>
        public void Append(string str)
        {
            this.Insert(str, InsertType.End);
        }
        
        /// <summary>
        /// Adds the string + a newline to the collected string
        /// </summary>
        /// <param name="s"></param>
        public void AppendLine(string s)
        {
            this.Insert(s + Environment.NewLine, InsertType.End);
        }
        
        /// <summary>
        /// Adds the specified string to the end of this builder=.
        /// </summary>
        /// <param name="str">The string to suffix.</param>
        public void Append(AdvancedString str)
        {
            this.Insert(str.ToString(), InsertType.End);
        }
        
        /// <summary>
        /// Adds the string + a newline to the collected string
        /// </summary>
        /// <param name="s"></param>
        public void AppendLine(AdvancedString s)
        {
            this.Insert(s.ToString() + Environment.NewLine, InsertType.End);
        }
        
        public void Insert(string s, InsertType t)
        {
            if (t == InsertType.Beginning)
                strings.AddFirst(new AdvancedString(s));
            else if (t == InsertType.End)
                strings.AddLast(new AdvancedString(s));
        }
        
        /// <summary>
        /// Appends all the strings this AdvancedStringBuilder contains to a string builder.
        /// </summary>
        /// <param name="builder">The string builder to append to.</param>
        public void AppendTo(StringBuilder builder)
        {
            foreach (AdvancedString str in this.Strings)
            {
                builder.Append(str.ToString());
            }
        }
        
        /// <summary>
        /// Creates a string builder from this AdvancedStringBuilder.
        /// </summary>
        /// <returns>The string builder.</returns>
        public StringBuilder ToStringBuilder()
        {
            StringBuilder builder = new StringBuilder();
            this.AppendTo(builder);
            return builder;
        }
        
        /// <summary>
        /// Returns the string stored by this AdvancedStringBuilder.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return this.Value;
        }
    }
}