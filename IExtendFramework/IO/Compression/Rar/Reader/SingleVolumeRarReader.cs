using System.Collections.Generic;
using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;

namespace IExtendFramework.IO.Compression.Rar.Reader
{
    internal class SingleVolumeRarReader : RarReader
    {
        private readonly Stream stream;

        internal SingleVolumeRarReader(Stream stream, RarOptions options, IRarExtractionListener listener)
            : base(options, listener)
        {
            this.stream = stream;
        }

        internal override void ValidateArchive(RarVolume archive)
        {
            if (archive.IsMultiVolume)
            {
                throw new RarExtractionException("Streamed archive is a Multi-volume archive.  Use different RarReader method to extract.");
            }
        }

        internal override Stream RequestInitialStream()
        {
            return stream;
        }

        internal override IEnumerable<RarFilePart> CreateFilePartEnumerable()
        {
            return Entry.Part.AsEnumerable();
        }
    }
}
