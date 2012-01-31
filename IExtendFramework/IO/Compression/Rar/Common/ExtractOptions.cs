using System;

namespace IExtendFramework.IO.Compression.Rar.Common
{
    [Flags]
    public enum ExtractOptions
    {
        None,
        /// <summary>
        /// overwrite target if it exists
        /// </summary>
        Overwrite,
        /// <summary>
        /// extract with internal directory structure
        /// </summary>
        ExtractFullPath,
    }
}
