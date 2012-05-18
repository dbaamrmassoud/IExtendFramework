using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IExtendFramework.IO.Compression.Packages
{
    public class PackEntry
    {
        public string FullPath { get; set; }
        public string Filename { get; set; }
        public string ArchiveDirectory { get; set; }
        public byte[] Data { get; set; }

        public PackEntry()
        {
            FullPath = "";
            Filename = "";
            ArchiveDirectory = "";
            Data = new byte[0];
        }

        public PackEntry(byte[] data)
        {
            Data = data;
        }

        public static PackEntry FromFile(string filename, string archiveDir = "")
        {
            return new PackEntry() { FullPath = filename, Filename = System.IO.Path.GetFileName(filename), Data = System.IO.File.ReadAllBytes(filename), ArchiveDirectory = archiveDir };
        }

        public string GetArchivePath()
        {
            if (string.IsNullOrEmpty(this.ArchiveDirectory))
                return Filename;
            else
                return System.IO.Path.Combine(ArchiveDirectory, Filename);
        }
    }
}
