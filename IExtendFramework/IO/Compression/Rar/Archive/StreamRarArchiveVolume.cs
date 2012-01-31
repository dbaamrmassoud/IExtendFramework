
using System.Collections.Generic;
using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
using IExtendFramework.IO.Compression.Rar.Headers;
using IExtendFramework.IO;

namespace IExtendFramework.IO.Compression.Rar.Archive
{
    internal class StreamRarArchiveVolume : RarArchiveVolume
    {
        private bool streamOwner;

        internal StreamRarArchiveVolume(Stream stream, RarOptions options)
            : base(StreamingMode.Seekable, options)
        {
            Stream = stream;
            this.streamOwner = !options.HasFlag(RarOptions.KeepStreamsOpen);
        }

        internal Stream Stream
        {
            get;
            private set;
        }
#if !PORTABLE
        public override FileInfo VolumeFile
        {
            get { return null; }
        }
#endif

        internal override IEnumerable<RarFilePart> ReadFileParts()
        {
            return GetVolumeFileParts();
        }

        internal override Stream GetStream()
        {
            return Stream;
        }

        internal override RarFilePart CreateFilePart(FileHeader fileHeader, MarkHeader markHeader)
        {
            return new SeekableStreamFilePart(markHeader, fileHeader, Stream, streamOwner);
        }
    }
}
