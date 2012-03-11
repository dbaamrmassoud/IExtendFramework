/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/23/2012
 * Time: 3:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace IExtendFramework.IO
{
    /// <summary>
    /// Filestream interface
    /// </summary>
    public interface IFileStream
    {
        void Close();
        
        void Write(string s);
        void Write(int i);
        void Write(char c);
        void Write(byte b);
        void Write(bool b);
        void Write(object o);
        
        void WriteLine(string s);
        void WriteLine(int i);
        void WriteLine(char c);
        void WriteLine(byte b);
        void WriteLine(bool b);
        void WriteLine(object o);
        
        string ReadAllText();
        byte[] ReadAllBytes();
        string[] ReadLines();
        
        IEnumerable<string> IterateLines();
        IEnumerable<byte> IterateBytes();
        IEnumerable<char> IterateChars();
        
        char ReadChar();
        int ReadInt();
        byte ReadByte();
        string ReadString(int length);
        string ReadWhiteSpace();
        
        long Position
        {get; set; }
        
        long Length
        {
            get;
        }
        
        FileStreamMode FileMode
        {get; set; }
    }

    public enum FileStreamMode
    {
        Overwrite,
        Read,
        Write
    }
}
