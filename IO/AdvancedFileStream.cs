/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/23/2012
 * Time: 3:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.IO
{
    /// <summary>
    /// Description of AdvancedFileStream.
    /// </summary>
    public class AdvancedFileStream : IFileStream
    {
        public AdvancedFileStream()
        {
        }
        
        public int Position {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
        
        public FileStreamMode FileMode {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
        
        public void Write(string s)
        {
            throw new NotImplementedException();
        }
        
        public void Write(int i)
        {
            throw new NotImplementedException();
        }
        
        public void Write(char c)
        {
            throw new NotImplementedException();
        }
        
        public void Write(byte b)
        {
            throw new NotImplementedException();
        }
        
        public void Write(bool b)
        {
            throw new NotImplementedException();
        }
        
        public void Write(object o)
        {
            throw new NotImplementedException();
        }
        
        public void WriteLine(string s)
        {
            throw new NotImplementedException();
        }
        
        public void WriteLine(int i)
        {
            throw new NotImplementedException();
        }
        
        public void WriteLine(char c)
        {
            throw new NotImplementedException();
        }
        
        public void WriteLine(byte b)
        {
            throw new NotImplementedException();
        }
        
        public void WriteLine(bool b)
        {
            throw new NotImplementedException();
        }
        
        public void WriteLine(object o)
        {
            throw new NotImplementedException();
        }
        
        public string ReadAllText()
        {
            throw new NotImplementedException();
        }
        
        public byte[] ReadAllBytes()
        {
            throw new NotImplementedException();
        }
        
        public string[] ReadLines()
        {
            throw new NotImplementedException();
        }
        
        public System.Collections.Generic.IEnumerable<string> IterateLines()
        {
            throw new NotImplementedException();
        }
        
        public System.Collections.Generic.IEnumerable<byte> IterateBytes()
        {
            throw new NotImplementedException();
        }
        
        public System.Collections.Generic.IEnumerable<char> IterateChars()
        {
            throw new NotImplementedException();
        }
        
        public char ReadChar()
        {
            throw new NotImplementedException();
        }
        
        public int ReadInt()
        {
            throw new NotImplementedException();
        }
        
        public byte ReadByte()
        {
            throw new NotImplementedException();
        }
        
        public string ReadString()
        {
            throw new NotImplementedException();
        }
        
        public string ReadWhiteSpace()
        {
            throw new NotImplementedException();
        }
        
        public bool ReadBool()
        {
            throw new NotImplementedException();
        }
        
        public operator <<(object o)
        {
            
        }
        
        public operator >>(object o)
        {
            
        }
    }
}
