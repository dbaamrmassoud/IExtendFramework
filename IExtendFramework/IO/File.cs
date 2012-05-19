using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IExtendFramework.IO
{
    /// <summary>
    /// A collection of useful file extension methods not found in the BCL. It includes new methods and asynchronous versions of existing methods
    /// </summary>
    public class FileEx
    {
        private FileEx() { }

        /// <summary>
        /// Copies a file asynchronously
        /// </summary>
        /// <param name="source">The source file</param>
        /// <param name="destination">The destination file</param>
        public static void AsyncCopy(string source, string destination)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                System.IO.File.Copy(source, destination);
            }));
            t.Start();
        }

        /// <summary>
        /// Copies a file asynchronously
        /// </summary>
        /// <param name="source">The source file</param>
        /// <param name="destination">The destination file</param>
        /// <param name="overwrite">Whether to overwrite existing files</param>
        public static void AsyncCopy(string source, string destination, bool overwrite)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                System.IO.File.Copy(source, destination, overwrite);
            }));
            t.Start();
        }

        /// <summary>
        /// Copies many files asynchronously
        /// </summary>
        /// <param name="sources">List of source files</param>
        /// <param name="destinations">List of destination files</param>
        public static void AsyncCopyBatch(string[] sources, string[] destinations)
        {
            for (int i = 0; i < sources.Length; i++)
                AsyncCopy(sources[i], destinations[i]);
        }

        /// <summary>
        /// Copies many files asynchronously
        /// </summary>
        /// <param name="sources">List of source files</param>
        /// <param name="destinations">List of destination files</param>
        /// <param name="overwrite">Whether to overwrite existing files</param>
        public static void AsyncCopyBatch(string[] sources, string[] destinations, bool overwrite)
        {
            for (int i = 0; i < sources.Length; i++)
                AsyncCopy(sources[i], destinations[i], overwrite);
        }

        /// <summary>
        /// Copies files to a specified directory asynchronously
        /// </summary>
        /// <param name="sources">The source files</param>
        /// <param name="destinationDir">The destination directory</param>
        public static void AsyncCopyBatch(string[] sources, string destinationDir)
        {
            for (int i = 0; i < sources.Length; i++)
                AsyncCopy(sources[i], System.IO.Path.Combine(destinationDir, System.IO.Path.GetFileName(sources[i])));
        }

        /// <summary>
        /// Copies many files asynchronously
        /// </summary>
        /// <param name="sources">The source files</param>
        /// <param name="destinationDir">The destination directory</param>
        /// <param name="overwrite">Whether to overwrite existing files</param>
        public static void AsyncCopyBatch(string[] sources, string destinationDir, bool overwrite)
        {
            for (int i = 0; i < sources.Length; i++)
                AsyncCopy(sources[i], System.IO.Path.Combine(destinationDir, System.IO.Path.GetFileName(sources[i])), overwrite);
        }

        /// <summary>
        /// Gets the largest from a list of files
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static FileInfo LargestFile(params FileInfo[] files)
        {
            FileInfo ret = null;
            foreach (FileInfo fi in files)
                if (ret == null || ret.Length < fi.Length)
                    ret = fi;
            return ret;
        }

        /// <summary>
        /// Gets the smallest file from a list of files
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static FileInfo SmallestFile(params FileInfo[] files)
        {
            FileInfo ret = null;
            foreach (FileInfo fi in files)
                if (ret == null || ret.Length > fi.Length)
                    ret = fi;
            return ret;
        }

        /// <summary>
        /// Returns the average size of a list of files
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static long AverageSize(params FileInfo[] files)
        {
            long total = 0;
            foreach (FileInfo fi in files)
                total += fi.Length;
            return total / files.Length;
        }

        /// <summary>
        /// Returns the average size of a list of files
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static string AverageSizeFormatted(params FileInfo[] files)
        {
            long total = 0;
            foreach (FileInfo fi in files)
                total += fi.Length;
            return Utilities.FormatByteToSize(total / files.Length);
        }
    }
}
