﻿using System.Collections.Generic;
using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
using IExtendFramework.IO.Compression.Rar.Headers;
using IExtendFramework.IO;

namespace IExtendFramework.IO.Compression.Rar.Reader
{
    public class RarReaderVolume : RarVolume
    {
        private bool streamOwner;

        internal RarReaderVolume(Stream stream, RarOptions options)
            : base(StreamingMode.Streaming, options)
        {
            Stream = stream;
            this.streamOwner = !options.HasFlag(RarOptions.KeepStreamsOpen);
        }

        internal Stream Stream
        {
            get;
            private set;
        }

        internal override Stream GetStream()
        {
            return Stream;
        }

        internal override RarFilePart CreateFilePart(FileHeader fileHeader, MarkHeader markHeader)
        {
            return new NonSeekableStreamFilePart(markHeader, fileHeader, streamOwner);
        }

        internal override IEnumerable<RarFilePart> ReadFileParts()
        {
            return GetVolumeFileParts();
        }
    }
}
