using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
using IExtendFramework.IO.Compression.Rar.Headers;

namespace IExtendFramework.IO.Compression.Rar.Archive
{
    internal class FileInfoRarFilePart : RarFilePart
    {
        internal FileInfoRarFilePart(MarkHeader mh, FileHeader fh, FileInfo fi)
            : base(mh, fh, true)
        {
            FileInfo = fi;
        }

        internal FileInfo FileInfo
        {
            get;
            private set;
        }

        internal override Stream GetStream()
        {
            Stream stream = FileInfo.OpenRead();
            stream.Position = FileHeader.DataStartPosition;
            return stream;
        }

        internal override string FilePartName
        {
            get
            {
                return "Rar File: " + FileInfo.FullName
                    + " File Entry: " + FileHeader.FileName;
            }
        }
    }
}
