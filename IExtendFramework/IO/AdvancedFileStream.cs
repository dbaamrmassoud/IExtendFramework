/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/23/2012
 * Time: 3:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace IExtendFramework.IO
{
    /// <summary>
    /// An advanced filestream for writing files
    /// </summary>
    public class AdvancedFileStream : IFileStream, IDisposable
    {
        private FileStream _file = null;
        
        public AdvancedFileStream(string filename, FileStreamMode mode)
        {
            this.mode = mode;
            switch (mode)
            {
                case FileStreamMode.Overwrite:
                    _file = new FileStream(filename, System.IO.FileMode.Create);
                    break;
                case FileStreamMode.Read:
                    _file = new FileStream(filename, System.IO.FileMode.Open);
                    break;
                case FileStreamMode.Write:
                    _file = new FileStream(filename, System.IO.FileMode.Append);
                    break;
                default:
                    throw new Exception("Invalid value for FileStreamMode");
            }
            Position = 0;
        }
        
        /// <summary>
        /// The position in the current stream
        /// </summary>
        public long Position {
            get
            {
                return _file.Position;
            }
            set
            {
                _file.Position = value;
            }
        }
        
        /// <summary>
        /// Whether the stream can be written to
        /// </summary>
        public bool Writable
        {
            get
            {
                // if its open for read, return false
                return FileMode == FileStreamMode.Read ? false : true;
            }
        }
        
        private FileStreamMode mode;
        public FileStreamMode FileMode {
            get
            {
                return mode;
            }
            set
            {
                throw new InvalidOperationException("Cannot set the FileMode!");
            }
        }
        
        public long Length
        {
            get
            {
                return _file.Length;
            }
        }
        
        public void Write(string s)
        {
            if (Writable)
            {
                byte[] b = Utilities.StringToByte(s);
                _file.Write(b, 0, b.Length);
            }
            else
                throw new InvalidOperationException("Cannot write to a readonly stream!");
        }
        
        public void Write(int i)
        {
            if (Writable)
            {
                byte[] b = Utilities.StringToByte(i.ToString());
                _file.Write(b, 0, b.Length);
            }
            else
                throw new InvalidOperationException("Cannot write to a readonly stream!");
        }
        
        public void Write(char c)
        {
            if (Writable)
            {
                byte[] b = Utilities.StringToByte(c.ToString());
                _file.Write(b, 0, b.Length);
            }
            else
                throw new InvalidOperationException("Cannot write to a readonly stream!");
        }
        
        public void Write(byte b)
        {
            if (Writable)
            {
                _file.WriteByte(b);
            }
            else
                throw new InvalidOperationException("Cannot write to a readonly stream!");
        }
        
        public void Write(bool b)
        {
            if (Writable)
            {
                byte[] b2 = Utilities.StringToByte((b == true ? "true" : "false"));
                _file.Write(b2, 0, b2.Length);
            }
            else
                throw new InvalidOperationException("Cannot write to a readonly stream!");
        }
        
        public void Write(object o)
        {
            if (Writable)
            {
                byte[] b = Utilities.StringToByte(o.ToString());
                _file.Write(b, 0, b.Length);
            }
            else
                throw new InvalidOperationException("Cannot write to a readonly stream!");
        }
        
        public void WriteLine(string s)
        {
            Write(s + "\n");
        }
        
        public void WriteLine(int i)
        {
            Write(i.ToString() + "\n");
        }
        
        public void WriteLine(char c)
        {
            Write(c.ToString() + "\n");
        }
        
        public void WriteLine(byte b)
        {
            Write(b.ToString() + "\n");
        }
        
        public void WriteLine(bool b)
        {
            Write((b == true ? "true" : "false") + "\n");
        }
        
        public void WriteLine(object o)
        {
            Write(o.ToString() + "\n");
        }
        
        public string ReadAllText()
        {
            if (!Writable)
            {
                List<byte> bytes = new List<byte>();
                while (_file.Position <= _file.Length)
                    bytes.Add((byte)_file.ReadByte());
                return Utilities.ByteToString(bytes.ToArray());
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public byte[] ReadAllBytes()
        {
            if (!Writable)
            {
                List<byte> bytes = new List<byte>();
                while (_file.Position <= _file.Length)
                    bytes.Add((byte)_file.ReadByte());
                return bytes.ToArray();
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public string[] ReadLines()
        {
            if (!Writable)
            {
                List<byte> bytes = new List<byte>();
                while (_file.Position <= _file.Length)
                    bytes.Add((byte)_file.ReadByte());
                
                string s =  Utilities.ByteToString(bytes.ToArray());
                return s.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public System.Collections.Generic.IEnumerable<string> IterateLines()
        {
            if (!Writable)
            {
                List<byte> bytes = new List<byte>();
                while (_file.Position <= _file.Length)
                    bytes.Add((byte)_file.ReadByte());
                
                string s =  Utilities.ByteToString(bytes.ToArray());
                foreach (string s2 in s.Split(new string[] { "\r\n"}, StringSplitOptions.None))
                    yield return s2;
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public System.Collections.Generic.IEnumerable<byte> IterateBytes()
        {
            if (!Writable)
            {
                while (_file.Position <= _file.Length)
                    yield return (byte)_file.ReadByte();
                
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public System.Collections.Generic.IEnumerable<char> IterateChars()
        {
            if (!Writable)
            {
                while (_file.Position <= _file.Length)
                    yield return char.Parse(char.ConvertFromUtf32(_file.ReadByte()));
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public char ReadChar()
        {
            if (!Writable)
            {
               return char.Parse(char.ConvertFromUtf32(_file.ReadByte()));
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public int ReadInt()
        {
            if (!Writable)
            {
                return _file.ReadByte();
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public byte ReadByte()
        {
            if (!Writable)
            {
                return (byte)_file.ReadByte();
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public string ReadString(int length)
        {
            if (!Writable)
            {
                List<byte> b = new List<byte>();
                for(int i = 0; i < length; i++)
                    b.Add((byte)_file.ReadByte());
                return Utilities.ByteToString(b.ToArray());
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public string ReadWhiteSpace()
        {
            if (!Writable)
            {
                List<byte> b = new List<byte>();
                while (true)
                {
                    // get char
                    int i = _file.ReadByte();
                    char c = (char) i;
                    _file.Position--;
                    if (char.IsWhiteSpace(c))
                    {
                        b.Add((byte)i);
                        _file.Position++;
                    }
                    else
                        break;
                }
                return Utilities.ByteToString(b.ToArray());
            }
            else
                throw new InvalidOperationException("Cannot read from a write stream!");
        }
        
        public void Dispose()
        {
            if (_file != null)
                _file.Close();
        }
        
        public void Close()
        {
            _file.Close();
            _file = null;
        }
    }
}
