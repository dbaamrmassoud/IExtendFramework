using System;
using System.Collections.Generic;
using System.Text;

namespace IExtendFramework.Parser
{
    /// <summary>
    /// The base parser class
    /// </summary>
    public abstract class ParserBase<T>
    {
        int position;

        protected int Position
        {
            get { return position; }
            set { position = value; }
        }

        protected IParserInput<char> Input;

        protected List<Tuple<int, string>> Errors = new List<Tuple<int, string>>();
        protected Stack<int> ErrorStack = new Stack<int>();
        
        /// <summary>
        /// Memories parsing results, key is (PositionStart, Noterminal), value is (SyntacticElement, success, PostionAfter).
        /// </summary>
        protected Dictionary<Tuple<int, string>, Tuple<object, bool, int>> ParsingResults = new Dictionary<Tuple<int, string>, Tuple<object, bool, int>>();
        
        protected ParserBase(IParserInput<char> input)
        {
            SetInput(input);
        }
        
        protected void SetInput(IParserInput<char> input)
        {
            Input = input;
            position = 0;
            ParsingResults.Clear();
        }

        protected bool Match(char terminal)
        {
            if (Input.HasInput(position))
            {
                char symbol = Input.GetInputSymbol(position);
                return terminal == symbol;
            }
            return false;
        }

        protected bool Match(char terminal, int pos)
        {
            if (Input.HasInput(pos))
            {
                char symbol = Input.GetInputSymbol(pos);
                return terminal == symbol;
            }
            return false;
        }

        protected char Match(char terminal, out bool success)
        {
            success = false;
            if (Input.HasInput(position))
            {
                char symbol = Input.GetInputSymbol(position);
                if (terminal == symbol)
                {
                    position++;
                    success = true;
                }
                return symbol;
            }
            return default(char);
        }

        protected char MatchRange(char start, char end, out bool success)
        {
            success = false;
            if (Input.HasInput(position))
            {
                char symbol = Input.GetInputSymbol(position);
                if (start <= symbol && symbol <= end)
                {
                    position++;
                    success = true;
                }
                return symbol;
            }
            return default(char);
        }

        protected char MatchSet(string terminalSet, bool isComplement, out bool success)
        {
            success = false;
            if (Input.HasInput(position))
            {
                char symbol = Input.GetInputSymbol(position);
                bool match = isComplement ? terminalSet.IndexOf(symbol) == -1 : terminalSet.IndexOf(symbol) > -1;
                if (match)
                {
                    position++;
                    success = true;
                }
                return symbol;
            }
            return default(char);
        }

        protected string MatchString(string terminalString, out bool success)
        {
            int currrent_position = position;
            foreach (char terminal in terminalString)
            {
                Match(terminal, out success);
                if (!success)
                {
                    position = currrent_position;
                    return null;
                }
            }
            success = true;
            return terminalString;
        }

        protected int AddError(string message)
        {
            Errors.Add(new Tuple<int, string>(position, message));
            return Errors.Count;
        }

        protected void ClearError(int count)
        {
            Errors.RemoveRange(count, Errors.Count - count);
        }

        protected string GetErrorMessages()
        {
            StringBuilder text = new StringBuilder();
            foreach (Tuple<int, string> msg in Errors)
            {
                text.Append(Input.FormErrorMessage(msg.Item1, msg.Item2));
                text.AppendLine();
            }
            return text.ToString();
        }
        
        protected void ReadWhitespace()
        {
            bool s = true;
            bool s2 = true;
            while (s || s2)
            { 
                Match(' ', out s);
                Match('\t', out s2);
            }
        }
        
        protected string ReadToChar(char c)
        {
            string r = "";
            bool s = true;
            while (s)
            {
                r += Match(c, out s).ToString();
            }
            return r;
        }
        
        protected string ReadToSet(string terminalSet, bool matchToSet, bool consumeLastChar)
        {
            int a = 0;
            string s = "";
            while (true)
            {
                Console.WriteLine(a.ToString() + " :" + s);
                a++;
                if (!Input.HasInput(position))
                    break;
                char symbol = Input.GetInputSymbol(position);
                bool match = matchToSet == false ?
                    terminalSet.IndexOf(symbol) == -1 :
                    terminalSet.IndexOf(symbol) > -1;
                
                if (match)
                {
                    position++;
                    s += symbol.ToString();
                }
                else
                {
                    if (consumeLastChar)
                        position++;
                    break;
                }
            }
            return s;
        }
        
        abstract public T Parse();
    }
}
