/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/14/2012
 * Time: 9:26 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace IExtendFramework.Parser
{
    /// <summary>
    /// Simple math parser using ParserBase. 
    /// If you actually want to parse math, use Mathematics.AdvancedMathProcessor
    /// </summary>
    public class MathParser : ParserBase<MathParser.MathFormula>
    {
        public bool EndOfStream = false;
        public MathParser(string m)
            : base(new Parser.TextInput(m))
        {
        }
        
        public override MathFormula Parse()
        {
            ReadWhitespace();
            if (!Input.HasInput(Position))
            {
                EndOfStream = true;
                return null;
            }
            char c = Input.GetInputSymbol(Position);
            if (c == '+')
            {
                Position++;
                return new MathFormula(Operation.Add, null);
            }
            if (c == '-')
            {
                Position++;
                return new MathFormula(Operation.Subtract, null);
            }
            if (c == '/')
            {
                Position++;
                return new MathFormula(Operation.Divide, null);
            }
            if (c == '*')
            {
                Position++;
                return new MathFormula(Operation.Multiply, null);
            }
            if (c == '^')
            {
                Position++;
                return new MathFormula(Operation.Pow, null);
            }
            if (c == ')')
            {
                Position++;
                return new MathFormula(Operation.ClosingBracket, null);
            }
            if (c == '(')
            {
                Position++;
                return new MathFormula(Operation.OpeningBracket, null);
            }
            ReadWhitespace();
            string s = ReadToSet("+-/* ^()", false, false);
            int r;
            if (int.TryParse(s, out r))
                return new MathFormula(Operation.IsNumber, s);
            else
                return new MathFormula(Operation.IsVariable, s);
        }
        
        public class MathFormula
        {
            public Operation Op;
            public object Value;
            
            public override string ToString()
            {
                return string.Format("[MathFormula Op={0}, Value={1}]", Op, Value);
            }
            
            public MathFormula(Operation op, object v)
            {
                this.Op = op;
                this.Value = v;
            }
        }
        
        public enum Operation
        {
            Pow,
            Add,
            Subtract,
            Divide,
            Multiply,
            
            OpeningBracket,
            ClosingBracket,
            
            IsNumber,
            IsVariable
        }
    }
}
