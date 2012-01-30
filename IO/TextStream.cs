/*
 * User: elijah
 * Date: 1/29/2012
 * Time: 5:44 PM
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace IExtendFramework.IO
{
    /// <summary>
    /// A Text Stream
    /// </summary>
    public class TextStream : Stream
    {
        public List<byte> Bytes = new List<byte>();
        
        public string Text
        {
            get
            {
                return Utilities.ByteToString(Bytes.ToArray());
            }
        }
        
        public TextStream()
        {
        }
        
        public override void Write(byte[] buffer, int offset, int count)
        {
            Bytes.AddRange(buffer.Slice(offset, count));
        }
        
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
        
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
        
        public override int Read(byte[] buffer, int offset, int count)
        {
            int pos = 0;
            for (int i = offset; i < count; i++)
                buffer[pos++] = Bytes[i];
            return pos;
        }
        
        public override long Position {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
        
        public override long Length {
            get {
                return Bytes.Count;
            }
        }
        
        public override void Flush()
        {
            throw new NotImplementedException();
        }
        
        public override bool CanWrite {
            get {
                return true;
            }
        }
        
        public override bool CanSeek {
            get {
                return false;
            }
        }
        
        public override bool CanRead {
            get {
                return true;
            }
        }
    }
}
