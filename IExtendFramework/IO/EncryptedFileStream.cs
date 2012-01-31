/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/9/2012
 * Time: 3:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using IExtendFramework.Encryption;

namespace IExtendFramework.IO
{
    /// <summary>
    /// EncryptedFileStream is a way to encrypt files using a stream...
    /// </summary>
    public class EncryptedFileStream  : Stream, IDisposable
    {
        public string Filename
        {get; set; }
        
        private List<byte> Text = new List<byte>();
        
        public EncryptionType Encryption
        {get; set; }
        
        public EncryptedFileStream(string filename)
        {
            this.Filename = filename;
        }
        
        /// <summary>
        /// Does the actual saving.
        /// </summary>
        public override void Close()
        {
            // save the file to the filestream
            FileStream fs = new FileStream(Filename, FileMode.Create);
            if (Encryption == EncryptionType.AES)
            {
                fs.Write(AESProvider.Encrypt(Text.ToArray()), 0, Text.Count);
            }
            if (Encryption == EncryptionType.ASCII)
            {
                fs.Write(Utilities.StringToByte(ASCIIProvider.Encrypt(Utilities.ByteToString(Text.ToArray()))), 0, Text.Count);
            }
            if (Encryption == EncryptionType.DES)
            {
                fs.Write(DESProvider.Encrypt(Text.ToArray()), 0, Text.Count);
            }
            if (Encryption == EncryptionType.L1F3)
            {
                fs.Write(L1F3Provider.Encrypt(Utilities.ByteToString(Text.ToArray())), 0, Text.Count);
            }
            if (Encryption == EncryptionType.RC2)
            {
                fs.Write(RC2Provider.Encrypt(Text.ToArray()), 0, Text.Count);
            }
            if (Encryption == EncryptionType.Rijndael)
            {
                fs.Write(RijndaelProvider.Encrypt(Text.ToArray()), 0, Text.Count);
            }
            if (Encryption == EncryptionType.RSA)
            {
                fs.Write(RSAProvider.Encrypt(Text.ToArray()), 0, Text.Count);
            }
            if (Encryption == EncryptionType.TripleDES)
            {
                fs.Write(TripleDESProvider.Encrypt(Text.ToArray()), 0, Text.Count);
            }
            if (Encryption == EncryptionType.Xor)
            {
                fs.Write(Utilities.StringToByte(XorProvider.Encrypt(Utilities.ByteToString(Text.ToArray()))), 0, Text.Count);
            }
            fs.Close();
            base.Close();
        }
        
        public override void Write(byte[] buffer, int offset, int count)
        {
            for (int i = offset; i < count; i++)
                Text.Add(buffer[i]);
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
            throw new NotImplementedException();
        }
        
        public override long Position {
            get; set; 
        }
        
        public override long Length {
            get {
                return Text.Count;
            }
        }
        
        public override void Flush()
        {
            
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
        
        public static string Open(string filename, EncryptionType et)
        {
            BinaryReader br = new BinaryReader(new FileStream(filename, FileMode.Open));
            List<byte> _in = new List<byte>();
            while (br.BaseStream.Position < br.BaseStream.Length)
                _in.Add(br.ReadByte());
            br.Close();
            if (et == EncryptionType.AES)
            {
                return Utilities.ByteToString(AESProvider.Decrypt(_in.ToArray()));
            }
            if (et == EncryptionType.ASCII)
            {
                return ASCIIProvider.Decrypt(Utilities.ByteToString(_in.ToArray()));
            }
            if (et == EncryptionType.DES)
            {
                return Utilities.ByteToString(DESProvider.Decrypt(_in.ToArray()));
            }
            if (et == EncryptionType.L1F3)
            {
                return L1F3Provider.Decrypt(_in.ToArray());
            }
            if (et == EncryptionType.RC2)
            {
                return Utilities.ByteToString(RC2Provider.Decrypt(_in.ToArray()));
            }
            if (et == EncryptionType.Rijndael)
            {
                return Utilities.ByteToString(RijndaelProvider.Decrypt(_in.ToArray()));
            }
            if (et == EncryptionType.RSA)
            {
                return Utilities.ByteToString(RSAProvider.Decrypt(_in.ToArray()));
            }
            if (et == EncryptionType.TripleDES)
            {
                return Utilities.ByteToString(TripleDESProvider.Decrypt(_in.ToArray()));
            }
            if (et == EncryptionType.Xor)
            {
                return AESProvider.Decrypt(Utilities.ByteToString(_in.ToArray()));
            }
            throw new Exception("Invalid decryption type!");
        }
        
        public void Write(string text)
        {
            Text.AddRange(Utilities.StringToByte(text));
        }
    }
}
