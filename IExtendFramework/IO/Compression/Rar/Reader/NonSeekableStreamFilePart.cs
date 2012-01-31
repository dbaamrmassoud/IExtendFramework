using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
using IExtendFramework.IO.Compression.Rar.Headers;

namespace IExtendFramework.IO.Compression.Rar.Reader
{
    internal class NonSeekableStreamFilePart : RarFilePart
    {
        internal NonSeekableStreamFilePart(MarkHeader mh, FileHeader fh, bool streamOwner)
            : base(mh, fh, streamOwner)
        {
        }

        internal override Stream GetStream()
        {
            return FileHeader.PackedStream;
        }

        internal override string FilePartName
        {
            get
            {
                return "Unknown Stream - File Entry: " + base.FileHeader.FileName;
            }
        }
    }
}
