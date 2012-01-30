﻿using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
#if THREEFIVE
using IExtendFramework.IO.Compression.Rar.Headers;
#endif

namespace IExtendFramework.IO.Compression.Rar.Reader
{
    public static class RarReaderExtensions
    {
#if !PORTABLE
        public static void WriteEntryTo(this RarReader reader, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                reader.WriteEntryTo(stream);
            }
        }
        public static void WriteEntryTo(this RarReader reader, FileInfo filePath)
        {
            using (Stream stream = filePath.Open(FileMode.Create))
            {
                reader.WriteEntryTo(stream);
            }
        }

        /// <summary>
        /// Extract to specific directory, retaining filename
        /// </summary>
        public static void WriteEntryToDirectory(this RarReader reader, string destinationDirectory,
            ExtractOptions options = ExtractOptions.Overwrite)
        {
            string destinationFileName = string.Empty;
            string file = Path.GetFileName(reader.Entry.FilePath);


            if (options.HasFlag(ExtractOptions.ExtractFullPath))
            {
                string folder = Path.GetDirectoryName(reader.Entry.FilePath);
                string destdir = Path.Combine(destinationDirectory, folder);
                if (!Directory.Exists(destdir))
                {
                    Directory.CreateDirectory(destdir);
                }
                destinationFileName = Path.Combine(destdir, file);
            }
            else
            {
                destinationFileName = Path.Combine(destinationDirectory, file);
            }

            reader.WriteEntryToFile(destinationFileName, options);
        }

        /// <summary>
        /// Extract to specific file
        /// </summary>
        public static void WriteEntryToFile(this RarReader reader, string destinationFileName,
            ExtractOptions options = ExtractOptions.Overwrite)
        {
            FileMode fm = FileMode.Create;

            if (!options.HasFlag(ExtractOptions.Overwrite))
            {
                fm = FileMode.CreateNew;
            }
            using (FileStream fs = File.Open(destinationFileName, fm))
            {
                reader.WriteEntryTo(fs);
            }
        }
#endif
    }
}
