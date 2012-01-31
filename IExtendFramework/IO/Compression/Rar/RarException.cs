using System;

namespace IExtendFramework.IO.Compression.Rar
{
    public class RarException : ApplicationException
    {
        public RarException(string message)
            : base(message)
        {
        }

        public RarException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
