using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
using IExtendFramework.IO;

namespace IExtendFramework.IO.Compression.Rar.Archive
{
    public abstract class RarArchiveVolume : RarVolume
    {
        internal RarArchiveVolume(StreamingMode mode, RarOptions options)
            : base(mode, options)
        {
        }

#if !PORTABLE
        /// <summary>
        /// File that backs this volume, if it not stream based
        /// </summary>
        public abstract FileInfo VolumeFile
        {
            get;
        }
#endif
    }
}
