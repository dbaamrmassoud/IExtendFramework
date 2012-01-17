/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/13/2012
 * Time: 8:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace IExtendFramework.Text
{
    /// <summary>
    /// A class that reads words from a string.
    /// It is a proof-of-concept class using the ParserBase
    /// </summary>
    public class WordReader : Parser.ParserBase<string>
    {
        public string Text
        {
            get
            {
                string s = "";
                for (int i = 0; i < Input.Length; i++)
                    s += Input.GetInputSymbol(i).ToString();
                return s;
            }
            set
            {
                Input = new Parser.TextInput(value);
                Position = 0;
                IsAtEndOfText = false;
            }
        }
        
        public bool IsAtEndOfText
        { get; set; }
        
        public string EndOfWordCharacters
        {
            get; set;
        }
        
        public WordReader()
            : base(new Parser.TextInput(""))
        {
            Position = 0;
            EndOfWordCharacters = ",.!?\"\' ;:\t\r\n[]{}<>/\\|+=_)(*&^%$#@`~";
            IsAtEndOfText = false;
        }
        
        public WordReader(string text)
            : base(new Parser.TextInput(text))
        {
            this.Text = text;
            Position = 0;
            EndOfWordCharacters = ",.!?\"\' ;:\t\r\n[]{}<>/\\|+=_)(*&^%$#@`~";
            IsAtEndOfText = false;
        }
        
        public override string Parse()
        {
            return ReadWord();
        }
        
        public string ReadWord()
        {
            // read until end of stream or a word.
            string s = ReadToSet(EndOfWordCharacters, false, true);
            while (s.Length == 0)
                if (Input.HasInput(Position))
                    s = ReadToSet(EndOfWordCharacters, false, true);
                else
                    break;
            // check if no input left
            if (!Input.HasInput(Position))
                IsAtEndOfText = true;
            ReadWhitespace();
            
            return s;
        }
        
        public List<string> ReadAllWords()
        {
            WordList wl = new WordList();
            while (true)
            {
                // read a word
                string s = ReadToSet(EndOfWordCharacters, false, true);
                if (!string.IsNullOrEmpty(s))
                    wl.Words.Add(s);
                ReadWhitespace();
                
                if (!Input.HasInput(Position))
                {
                    IsAtEndOfText = true;
                    break;
                }
            }
            // return the word list
            return wl.Words;
        }
        
        public void Reset()
        {
            IsAtEndOfText = false;
            Position = 0;
        }
        
        public int GetPosition()
        {
            return Position;
        }
        
        class WordList
        {
            public List<string> Words = new List<string>();
            
            public WordList()
            {
            }
            
            public override string ToString()
            {
                string r= string.Format("[Wordlist Words=");
                foreach (string s in Words)
                    r += "'" + s + "', ";
                if (Words.Count  == 0)
                    return r + "]";
                else
                    return r.Substring(0, r.Length - 2) + "]";
            }

        }
    }
}
