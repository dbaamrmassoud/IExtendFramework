using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
using IExtendFramework.IO.Compression.Rar.Headers;
using IExtendFramework.IO;

namespace IExtendFramework.IO.Compression.Rar.Archive
{
    /// <summary>
    /// A rar part based on a FileInfo object
    /// </summary>
    internal class FileInfoRarArchiveVolume : RarArchiveVolume
    {
        internal FileInfoRarArchiveVolume(FileInfo fileInfo, RarOptions options)
            : base(StreamingMode.Seekable, FixOptions(options))
        {
            FileInfo = fileInfo;
            FileParts = base.GetVolumeFileParts().ToReadOnly();
        }

        private static RarOptions FixOptions(RarOptions options)
        {
            //make sure we're closing streams with fileinfo
            if (options.HasFlag(RarOptions.KeepStreamsOpen))
            {
                options = (RarOptions)FlagUtilities.SetFlag(options, RarOptions.KeepStreamsOpen, false);
            }
            return options;
        }

        internal IExtendFramework.Collections.Specialized.LazyReadOnlyCollection<RarFilePart> FileParts
        {
            get;
            private set;
        }

        internal FileInfo FileInfo
        {
            get;
            private set;
        }

        internal override RarFilePart CreateFilePart(FileHeader fileHeader, MarkHeader markHeader)
        {
            return new FileInfoRarFilePart(markHeader, fileHeader, FileInfo);
        }

        internal override Stream GetStream()
        {
            return FileInfo.OpenRead();
        }

        internal override IEnumerable<RarFilePart> ReadFileParts()
        {
            return FileParts;
        }

        public override FileInfo VolumeFile
        {
            get
            {
                return FileInfo;
            }
        }
    }
}
