﻿namespace IExtendFramework.Text.RTF
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Base Class for RtfBuilder and GDF Builder
    /// Handles format (font , fontstyle, colour) delegating string appending to derived classes. 
    /// </summary>
    public abstract class RTFBuilderBase : IDisposable
    {
 
        #region Fields
        private readonly Color _defaultbackcolor;
        private readonly float _defaultFontSize;
        private readonly Color _defaultforecolor;
        protected Color _backcolor;
        protected List <Color> _colortbl;
        protected int _firstLineIndent;
        protected int _font;
        protected  StringCollection _rawFonts;
        protected float _fontSize;
        protected FontStyle _fontStyle;
        /// <summary>
        /// 
        /// </summary>
        protected List <string> _fontTable;
        protected Color _forecolor;
        protected int _lineIndent;
        protected StringFormat _sf;
        protected bool _unwrapped;
        #endregion
        #region Constructor
        protected RTFBuilderBase(RTFFont  defaultFont, float defaultFontSize)
        {

            _rawFonts = new StringCollection();
            this._font = IndexOfFont(defaultFont);
            this._defaultFontSize = defaultFontSize;
            this._fontSize = this._defaultFontSize;
            this._defaultforecolor = SystemColors.WindowText;
            this._defaultbackcolor = SystemColors.Window;
            this._colortbl = new List <Color>();
            this._colortbl.Add(this._defaultforecolor);
            this._colortbl.Add(this._defaultbackcolor);
            this._fontStyle = System.Drawing.FontStyle.Regular;
            this._backcolor = this._defaultbackcolor;
            this._forecolor = this._defaultforecolor;
            this._sf = (StringFormat) StringFormat.GenericDefault.Clone();
            this._sf.FormatFlags = StringFormatFlags.NoWrap;
            this._sf.Trimming = StringTrimming.Word;
        }
        #endregion
        #region Public Properties
        public int Length
        {
            get { return this.LengthInternal(); }
        }
        public float DefaultFontSize
        {
            [DebuggerStepThrough]
            get { return this._defaultFontSize; }
        }
        public Color Defaultforecolor
        {
            [DebuggerStepThrough]
            get { return this._defaultforecolor; }
        }
        public List <RTFFont> Fonts
        {
            [DebuggerStepThrough]
            get { return null; }
        }
        public Color DefaultBackColor
        {
            [DebuggerStepThrough]
            get { return this._defaultbackcolor; }
        }
        #endregion
        #region Virtual Methods

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion
        #region Override Methods

        ~RTFBuilderBase()
        {
            this.Dispose(false);
        }

        #endregion
        #region Abstract Methods

        protected abstract void AppendInternal(string value);
        protected abstract void AppendLevelInternal(int level);
        protected abstract void AppendLineInternal(string value);
        protected abstract void AppendLineInternal();
        protected abstract void AppendPageInternal();
        protected abstract void AppendParaInternal();
        protected abstract void AppendRTFInternal(string rtf);
        protected abstract IEnumerable <RTFBuilderBase> EnumerateCellsInternal(RTFRowDefinition rowDefinition, RTFCellDefinition[] cellDefinitions);
        public abstract IDisposable FormatLock();
        protected abstract void InsertImageInternal(Image image);
        protected abstract int LengthInternal();
        protected abstract void ResetInternal();

        #endregion
        #region Public Methods
        public RTFBuilderBase Alignment(StringAlignment alignment)
        {
            this._sf.Alignment = alignment;
            return this;
        }
        public RTFBuilderBase Append(string value)
        {
            this.AppendInternal(value);
            return this;
        }
        public RTFBuilderBase Append(char[] value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value);
            return Append(sb.ToString());
        }
        public RTFBuilderBase Append(short value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(double value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(float value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(int value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(string value, int startIndex, int count)
        {
            return Append(value.Substring(startIndex, count));
        }
        public RTFBuilderBase Append(char value, int repeatCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value, repeatCount);
            return Append(sb.ToString());
        }
        public RTFBuilderBase Append(long value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(object value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(bool value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(byte value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(decimal value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase Append(char value)
        {
            return Append(value.ToString());
        }
        public RTFBuilderBase AppendFormat(string format, object arg0)
        {
            string formated = string.Format(format, arg0);
            return Append(formated);
        }
        public RTFBuilderBase AppendFormat(string format, params object[] args)
        {
            string formated = string.Format(format, args);
            return Append(formated);
        }
        public RTFBuilderBase AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            string formated = string.Format(provider, format, args);
            return Append(formated);
        }
        public RTFBuilderBase AppendFormat(string format, object arg0, object arg1)
        {
            string formated = string.Format(format, arg0, arg1);
            return Append(formated);
        }
        public RTFBuilderBase AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            string formated = string.Format(format, arg0, arg1, arg2);
            return Append(formated);
        }
        public RTFBuilderBase AppendLevel(int level)
        {
            this.AppendLevelInternal(level);
            return this;
        }
        public RTFBuilderBase AppendLine(string value)
        {
            this.AppendLineInternal(value);
            return this;
        }
        public RTFBuilderBase AppendLine()
        {
            this.AppendLineInternal();
            return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase AppendLineFormat(string format, object arg0)
        {
            string formated = string.Format(format, arg0);
            return this.AppendLine(formated);
        }
        [DebuggerStepThrough]
        public RTFBuilderBase AppendLineFormat(string format, params object[] args)
        {
            string formated = string.Format(format, args);
            return this.AppendLine(formated);
        }
        [DebuggerStepThrough]
        public RTFBuilderBase AppendLineFormat(string format, object arg0, object arg1, object arg2)
        {
            string formated = string.Format(format, arg0, arg1, arg2);
            return this.AppendLine(formated);
        }
        public RTFBuilderBase AppendPage()
        {
            this.AppendPageInternal();
            return this;
        }
        public RTFBuilderBase AppendPara()
        {
            this.AppendParaInternal();
            return this;
        }
        public RTFBuilderBase AppendRTF(string rtf)
        {
            this.AppendRTFInternal(rtf);
            return this;
        }
        /// <summary>
        /// Appends the RTF document.
        /// </summary>
        /// <param name="rtf">The RTF.</param>
        /// <returns></returns>
        public RTFBuilderBase AppendRTFDocument(string rtf)
        {
            if (!string.IsNullOrEmpty(rtf))
            {
                if (rtf.IndexOf("viewkind4") > 0)
                {
                    try
                    {
                        string rtfc = this.GetColoursFromRtf(rtf);
                        string rtff = GetFontsFromRtf(rtfc);
                        string texttoadd = GetConcentratedText(rtff);
                        this.AppendRTF(texttoadd);
                    }
                    catch (ArgumentException ex)
                    {
                        this.AppendLine("RTF Document ERROR:");
                        this.AppendLine(ex.Message);
                    }
                }
            }
            return this;
        }
        private static string GetConcentratedText(string rtf)
        {
            string find = @"\viewkind4";
            int start = rtf.IndexOf(find);
            start = rtf.IndexOf("\\pard", start);
            int end = rtf.LastIndexOf("}");
            int end2 = rtf.LastIndexOf("\\par");
            if (end2 > 0 && ((end - end2) < 8))
            {
                end = end2;
            }
            return rtf.Substring(start + 5, end - start - 5);
        }

        private string GetColoursFromRtf(string rtf)
        {
            Regex RegexObj = new Regex("\\{\\\\colortbl ;(?<colour>\\\\red(?<red>\\d{0,3})\\\\green(?<green>\\d{0,3})\\\\blue(?<blue>\\d{0,3});)*\\}");
            Match MatchResults = RegexObj.Match(rtf);
            Dictionary <int, int> replaces = new Dictionary <int, int>();
            while (MatchResults.Success)
            {
                Group GroupObj = MatchResults.Groups["red"];
                if (GroupObj.Success)
                {
                    for (int i = 0; i < GroupObj.Captures.Count; i++)
                    {
                        int redval, greenval, blueval;
                        if (int.TryParse(MatchResults.Groups["red"].Captures[i].Value, out redval) &&
                            int.TryParse(MatchResults.Groups["green"].Captures[i].Value, out greenval) &&
                            int.TryParse(MatchResults.Groups["blue"].Captures[i].Value, out blueval))
                        {
                            Color c = Color.FromArgb(redval, greenval, blueval);
                            int count = this._colortbl.Count;
                            int index = this.IndexOf(c);
                            if ((i + 1) == index)
                            {
                                continue;
                            }
                            replaces.Add(i + 1, index);
                        }
                    }
                }
                MatchResults = MatchResults.NextMatch();
            }
            if (replaces.Count > 0)
            {

                    //delegate string MatchEvaluator(System.Text.RegularExpressions.Match match)
                    rtf = Regex.Replace(rtf, "(?:\\\\cf)([0-9]{1,2})", delegate(Match match)
                                                                           {
                                                                               int val;
                                                                               if (int.TryParse(match.Groups[1].Value, out val) && val > 0)
                                                                               {
                                                                                       if (replaces.ContainsKey(val))
                                                                                       {
                                                                                           return string.Format(@"\cf{0}", replaces[val]);
                                                                                       }
                                                                               }
                                                                               return match.Value;
                                                                           });

                    //delegate string MatchEvaluator(System.Text.RegularExpressions.Match match)
                    rtf = Regex.Replace(rtf, "(?:\\\\highlight)([0-9]{1,2})", delegate(Match match)
                                                                           {
                                                                               int val;
                                                                               if (int.TryParse(match.Groups[1].Value, out val) && val > 0)
                                                                               {
                                                                                       if (replaces.ContainsKey(val))
                                                                                       {
                                                                                           return string.Format(@"\highlight{0}", replaces[val]);
                                                                                       }
                                                                               }
                                                                               return match.Value;
                                                                           });

            }
            return rtf;
        }
     
        private string GetFontsFromRtf(string rtf)
        {
            // Regex that almost works for MS Word
            // \{\\fonttbl(?<raw>\{\\f(?<findex>\d{1,3})(\\fbidi )?\\(?<fstyle>fswiss|fnil|froman|fmodern|fscript|fdecor)\\fcharset(?<fcharset>\d{1,3}) ?(?<fprq>\\fprq\d{1,3} ?)(\{\\\*\\panose [\dabcdef]{20}\}\d?)?(?<FONT>[@\w -]+)(\\'\w\w)?(\{\\\*\\falt \\'\w\w\\'\w\w\\'\w\w\\'\w\w\})?;\}(\r\n)?)+
            Regex RegexObj = new Regex(@"\{\\fonttbl(?<raw>\{\\f(?<findex>\d{1,3}) ?\\(?<fstyle>fswiss|fnil|froman|fmodern|fscript|fdecor|ftech|fbidi) ?(?<fprq>\\fprq\d{1,2} ?)?(?<fcharset>\\fcharset\d{1,3})? (?<FONT>[\w -]+);\})+\}\r?\n", RegexOptions.Multiline);
            Match MatchResults = RegexObj.Match(rtf);
            Dictionary<int, int> replaces = new Dictionary<int, int>();
            Group GroupObj = MatchResults.Groups["raw"];
            for (int i = 0; i <GroupObj.Captures.Count;i++)
            {
                string raw = GroupObj.Captures[i].Value;
                //string font = MatchResults.Groups["FONT"].Captures[newdocindex].Value;
                Capture cap =MatchResults.Groups["findex"].Captures[i];
     
                //have to replace findex with {0}
                raw =string.Concat( "{", Regex.Replace(raw, "\\{\\\\f\\d{1,3}", "{\\f{0}"),"}");
 
                string curdocstringindex = MatchResults.Groups["findex"].Captures[i].Value;
                

                int sourceindex;
                if (int.TryParse(curdocstringindex, out sourceindex))
                {

                    int destinationindex = IndexOfRawFont(raw);
                    if (destinationindex != sourceindex)
                    {
                        replaces.Add(sourceindex, destinationindex);
                    }
                }
            }
            if (replaces.Count > 0)
            {


                        rtf = Regex.Replace(rtf, "(?<!\\{)(\\\\f)(\\d{1,3})( ?|\\\\)", match => 
                        {
                            int sourceindex2;
                            if (int.TryParse(match.Groups[2].Value, out sourceindex2))
                            {
                                if (replaces.ContainsKey(sourceindex2))
                                {
                                    string rep=  string.Format("\\f{0}{1}", replaces[sourceindex2], match.Groups[3].Value);
                                   return rep;
                                }
                            } 
                            return match.Value;
                        });

            }
            return rtf;
        }
        /// <summary>
        /// Indexes the of font.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <returns></returns>
        public int IndexOfFont(RTFFont  font)
        {
           return IndexOfRawFont(RTFBuilder.RawFonts.GetKnownFontstring(font));
        }
        private int IndexOfRawFont(string font)
        {
            if (!string.IsNullOrEmpty(font))
            {
                int index = _rawFonts.IndexOf(font);
                if (index < 0)
                {
                    return this._rawFonts.Add(font);
                }
                return index;
            }
            return 0;
        }

        [DebuggerStepThrough]
        public RTFBuilderBase BackColor(KnownColor color)
        {
            return this.BackColor(Color.FromKnownColor(color));
        }
        [DebuggerStepThrough]
        public RTFBuilderBase BackColor(Color color)
        {
            this._backcolor = color;
            return this;
        }
        public void Clear()
        {
        }
        public String ComputeReplacement(Match m)
        {
            // You can vary the replacement text for each match on-the-fly
            return "";
        }
        public IEnumerable <RTFBuilderBase> EnumerateCells(RTFRowDefinition rowDefinition, RTFCellDefinition[] cellDefinitions)
        {
            return this.EnumerateCellsInternal(rowDefinition, cellDefinitions);
        }
        [DebuggerStepThrough]
        public RTFBuilderBase Font(RTFFont font)
        {
           this._font = IndexOfFont(font);
           return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase Font(int index)
        {
            if (index >= 0 && index < this._rawFonts.Count)
            {
                this._font = index;
            }
            return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase FontSize(float fontSize)
        {
            this._fontSize = fontSize;
            return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase ForeColor(KnownColor color)
        {
            this._forecolor = Color.FromKnownColor(color);
            return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase ForeColor(Color color)
        {
            this._forecolor = color;
            return this;
        }
        public RTFBuilderBase ForeColor(int index)
        {
            index--;
            if (index >= 0 && index < this._colortbl.Count)
            {
                this._forecolor = this._colortbl[index];
            }
            else if (index < 0)
            {
                this._forecolor = this._colortbl[0];
            }
            return this;
        }
        /// <summary>
        /// Changes the Font Style.
        /// </summary>
        /// <param name="fontStyle">The font style.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public RTFBuilderBase FontStyle(FontStyle fontStyle)
        {
            this._fontStyle = fontStyle;
            return this;
        }
        [DebuggerStepThrough]

        public RTFBuilderBase FontStyleNOT(FontStyle fontStyle)
        {
            if ((this._fontStyle & fontStyle) == fontStyle)
            {
                this._fontStyle &= ~fontStyle;
            }
            return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase FontStyleOR(FontStyle fontStyle)
        {
            this._fontStyle |= fontStyle;
            return this;
        }
        /// <summary>
        /// Gets the index of the Color.
        /// Important for merging ColorTables
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public int IndexOf(Color color)
        {
            if (!this._colortbl.Contains(color))
            {
                this._colortbl.Add(color);
            }
            int index = this._colortbl.IndexOf(color) + 1;
            return index;
        }
        /// <summary>
        /// Inserts the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public RTFBuilderBase InsertImage(Image image)
        {
            this.InsertImageInternal(image);
            return this;
        }
        public RTFBuilderBase LineIndent(int indent)
        {
            this._lineIndent = indent;
            return this;
        }
        public RTFBuilderBase LineIndentFirst(int indent)
        {
            this._firstLineIndent = indent;
            return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase PrependLineFormatIf(string format, ref bool appended, object arg0)
        {
            string formated = string.Format(format, arg0);
            return this.PrependLineIf(formated, ref appended);
        }
        [DebuggerStepThrough]
        public RTFBuilderBase PrependLineFormatIf(string format, ref bool appended, params object[] args)
        {
            string formated = string.Format(format, args);
            return this.PrependLineIf(formated, ref appended);
        }
        [DebuggerStepThrough]
        public RTFBuilderBase PrependLineFormatIf(string format, ref bool appended, object arg0, object arg1, object arg2)
        {
            string formated = string.Format(format, arg0, arg1, arg2);
            return this.PrependLineIf(formated, ref appended);
        }
        [DebuggerStepThrough]
        public RTFBuilderBase PrependLineIf(ref bool appended)
        {
            if (appended)
            {
                this.AppendLine();
            }
            appended = true;
            return this;
        }
        [DebuggerStepThrough]
        public RTFBuilderBase PrependLineIf(string value, ref bool appended)
        {
            if (appended)
            {
                this.AppendLine();
                Append(value);
            }
            else
            {
                Append(value);
            }
            appended = true;
            return this;
        }
        public RTFBuilderBase Reset()
        {
            this.ResetInternal();
            return this;
        }
        #endregion
        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion
    }
}