using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IExtendFramework.IO.Compression.Packages
{
    /// <summary>
    /// A pack archive
    /// </summary>
    public class PackArchive
    {
        /// <summary>
        /// A list of entries in the pack archive
        /// </summary>
        public List<PackEntry> Entries { get; set; }
        /// <summary>
        /// The filename of the pack archive
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Creates a new empty archive
        /// </summary>
        public PackArchive()
        {
            Entries = new List<PackEntry>();
            Filename = "";
        }

        /// <summary>
        /// Loads a pack archive from a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static PackArchive FromFile(string filename)
        {
            PackArchive file = new PackArchive();
            file.Filename = filename;

            System.IO.FileStream PackFile = new System.IO.FileStream(filename, System.IO.FileMode.Open);

            Byte[] Buffer = null;

            //Read File Info Size
            Buffer = new Byte[4];
            PackFile.Read(Buffer, 0, 4);

            Byte[] ByteInfoBytesSize = Buffer;

            //Fix original array size from size 4
            while (ByteInfoBytesSize[0] == 0)
            {
                Byte[] temp = ByteInfoBytesSize;
                ByteInfoBytesSize = new Byte[ByteInfoBytesSize.Length - 1];
                for (int i = 0; i <= ByteInfoBytesSize.Length - 1; i++)
                {
                    ByteInfoBytesSize[i] = temp[i + 1];
                }
            }
            int InfoSize = int.Parse(System.Text.Encoding.Default.GetString(ByteInfoBytesSize));

            //Read File Info
            Buffer = new Byte[InfoSize];
            PackFile.Read(Buffer, 0, InfoSize);
            String FileInfo = System.Text.Encoding.Default.GetString(Buffer);

            String[] Files = FileInfo.Split('|');
            foreach (String File in Files)
            {
                PackEntry pe = new PackEntry();
                String FileName = File.Split(';')[0];
                int FileByteSize = int.Parse(File.Split(';')[1]);

                Buffer = new Byte[FileByteSize];
                PackFile.Read(Buffer, 0, FileByteSize);

                pe.Filename = FileName;
                pe.Data = Buffer;
                file.Entries.Add(pe);
            }
            PackFile.Close();
            return file;
        }

        /// <summary>
        /// Extracts the archive to the specified directory
        /// </summary>
        /// <param name="dir">The directory to write to</param>
        /// <param name="overwrite">whether to overwrite existing files</param>
        public void ExtractToDirectory(string dir, bool overwrite = true)
        {
            foreach (PackEntry e in Entries)
            {
                if (System.IO.File.Exists(dir + "\\" + e.GetArchivePath()) && overwrite == false)
                    continue;
                else
                    System.IO.File.WriteAllBytes(dir + "\\" + e.GetArchivePath(), e.Data);
            }
        }

        /// <summary>
        /// Saves the archive to Filename
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (string.IsNullOrEmpty(Filename))// || System.IO.File.Exists(Filename) == true)
                return false;
            AdvancedString Info = "";
            foreach (PackEntry entry in Entries)
            {
                Info += Convert.ToString(entry.GetArchivePath()) + ";" + (entry.Data.Length) + "|";
            }
            if (Info.EndsWith("|"))
            {
                Info = Info.Substring(0, Info.Length - 1);
            }

            Byte[] InfoBytes = System.Text.Encoding.Default.GetBytes(Info);
            int InfoBytesSize = InfoBytes.Length;
            Byte[] ByteInfoBytesSize = System.Text.Encoding.Default.GetBytes(InfoBytesSize.ToString());
            //Fix Array Size to 4
            if (ByteInfoBytesSize.Length < 4)
            {
                int d = 4 - ByteInfoBytesSize.Length;
                Byte[] temp = ByteInfoBytesSize;
                ByteInfoBytesSize = new Byte[4];
                for (int i = 3; i >= 0; i += -1)
                {
                    if (i - d >= 0)
                    {
                        ByteInfoBytesSize[i] = temp[i - d];
                    }
                    else
                    {
                        ByteInfoBytesSize[i] = 0;
                    }
                }
            }
            if (System.IO.File.Exists(Filename))
            {
                System.IO.File.Delete(Filename);
            }
            System.IO.FileStream packFile = new System.IO.FileStream(Filename, System.IO.FileMode.CreateNew);

            //Write InfoByteSize as Bytes[]
            packFile.Write(ByteInfoBytesSize, 0, 4);

            //Write Info as Bytes[]
            packFile.Write(InfoBytes, 0, InfoBytes.Length);

            //Write Files as Bytes[]

            foreach (PackEntry e in Entries)
            {
                packFile.Write(e.Data, 0, e.Data.Length);
            }
            packFile.Close();

            return true;
        }
    }
}
