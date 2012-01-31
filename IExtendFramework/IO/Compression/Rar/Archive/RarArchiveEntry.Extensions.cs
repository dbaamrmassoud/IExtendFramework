using System.IO;
using IExtendFramework.IO.Compression.Rar.Common;
#if THREEFIVE
using IExtendFramework.IO.Compression.Rar.Headers;
#endif

namespace IExtendFramework.IO.Compression.Rar.Archive
{

    public static class RarArchiveEntryExtensions
    {
#if !PORTABLE
        /// <summary>
        /// Extract to specific directory, retaining filename
        /// </summary>
        public static void WriteToDirectory(this RarArchiveEntry entry, string destinationDirectory,
            IRarExtractionListener listener,
            ExtractOptions options = ExtractOptions.Overwrite)
        {
            string destinationFileName = string.Empty;
            string file = Path.GetFileName(entry.FilePath);


            if (options.HasFlag(ExtractOptions.ExtractFullPath))
            {

                string folder = Path.GetDirectoryName(entry.FilePath);
                destinationDirectory = Path.Combine(destinationDirectory, folder);
                destinationFileName = Path.Combine(destinationDirectory, file);
            }
            else
            {
                destinationFileName = Path.Combine(destinationDirectory, file);
            }
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            entry.WriteToFile(destinationFileName, listener, options);
        }

        /// <summary>
        /// Extract to specific directory, retaining filename
        /// </summary>
        public static void WriteToDirectory(this RarArchiveEntry entry, string destinationPath,
            ExtractOptions options = ExtractOptions.Overwrite)
        {
            entry.WriteToDirectory(destinationPath, new NullRarExtractionListener(), options);
        }

        /// <summary>
        /// Extract to specific file
        /// </summary>
        public static void WriteToFile(this RarArchiveEntry entry, string destinationFileName,
                        IRarExtractionListener listener,
            ExtractOptions options = ExtractOptions.Overwrite)
        {
            FileMode fm = FileMode.Create;

            if (!options.HasFlag(ExtractOptions.Overwrite))
            {
                fm = FileMode.CreateNew;
            }
            using (FileStream fs = File.Open(destinationFileName, fm))
            {
                entry.WriteTo(fs, listener);
            }
        }

        /// <summary>
        /// Extract to specific file
        /// </summary>
        public static void WriteToFile(this RarArchiveEntry entry, string destinationFileName,
           ExtractOptions options = ExtractOptions.Overwrite)
        {
            entry.WriteToFile(destinationFileName, new NullRarExtractionListener(), options);
        }
#endif

        public static void WriteTo(this RarArchiveEntry entry, Stream stream)
        {
            entry.WriteTo(stream, new NullRarExtractionListener());
        }
    }
}
