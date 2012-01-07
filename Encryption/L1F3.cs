/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/1/2012
 * Time: 4:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;

namespace IExtendFramework.Encryption
{
    /// <summary>
    /// L1F3 is an encryption system that uses multiple other types in its system.
    /// </summary>
    public sealed class L1F3Provider
    {
        /// <summary>
        /// All methods are static, no need to create an instance.
        /// </summary>
        private L1F3Provider()
        {
            
        }
        
        public static byte[] Encrypt(string _in)
        {
            _in = new ASCIIProvider().Encrypt(_in, 10);
            byte[] _out = new AESProvider(SampleObjects.CreateAESKey(), SampleObjects.CreateAESIV()).Encrypt(new UTF8Encoding().GetBytes(_in));
            _out  = new TripleDESProvider(SampleObjects.CreateTripleDESKey(), SampleObjects.CreateTripleDESIV()).Encrypt(_out);
            
            return _out;
        }
        
        public static string Decrypt(byte[] _in)
        {
            _in = new TripleDESProvider(SampleObjects.CreateTripleDESKey(), SampleObjects.CreateTripleDESIV()).Decrypt(_in);
            _in = new AESProvider(SampleObjects.CreateAESKey(), SampleObjects.CreateAESIV()).Decrypt(_in);
            string _out = new UTF8Encoding().GetString(_in);
            return new ASCIIProvider().Decrypt(_out, 10);
        }
    }
}
