using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
namespace IExtendFramework.IO.Compression.Packages
{
    /// <summary>
    /// Collection for creating Packages
    /// </summary>
    public class PackFileInfoCollection
    {
        public System.Collections.ArrayList FileList;

        public System.Collections.ArrayList FileNames;
        public PackFileInfoCollection()
        {
            FileList = new System.Collections.ArrayList();
            FileNames = new System.Collections.ArrayList();
        }
        ///<summary>
        /// Add a file to the collection
        /// </summary>
        /// <param name="Path">File path</param>
        /// <param name="ExtraPath">Extra Path for example \folder1, Can also be empty</param>
        public void AddFile(String Path, String ExtraPath)
        {
            if (!FileList.Contains(Path)) {
                FileList.Add(Path);
                FileNames.Add(ExtraPath + new System.IO.FileInfo(Path).Name);
            }
        }
        ///<summary>
        /// Add a file to the collection
        /// </summary>
        /// <param name="Path">File path</param>
        public void AddFile(String Path)
        {
            AddFile(Path, "");
        }
        /// <summary>
        /// Add all the files from a directory to the collection
        /// </summary>
        /// <param name="Directory">Directory path</param>
        /// <param name="ExtraPath">Extra Path for example \folder1, Can also be empty</param>
        public void AddDirectory(String Directory, String ExtraPath)
        {
            foreach (String File in System.IO.Directory.GetFiles(Directory)) {
                AddFile(File, ExtraPath);
            }
        }
        /// <summary>
        /// Add all the files from a directory to the collection
        /// </summary>
        /// <param name="Directory">Directory path</param>
        public void AddDirectory(String Directory)
        {
            AddDirectory(Directory, "");
        }
        /// <summary>
        /// Returns the file info string which will be used for the package
        /// </summary>
        public String CreateFileInfo()
        {
            String Info = "";
            foreach (String FilePath in FileList) {
                Info += Convert.ToString(FileNames[FileList.IndexOf(FilePath)]) + ";" + (new System.IO.FileInfo(FilePath).Length) + "|";
            }
            if (Info.EndsWith("|")) {
                Info = Info.Substring(0, Info.Length - 1);
            }
            return Info;
        }
    }

    public sealed class Packages
    {
        private Packages()
        {
        }
        /// <summary>
        /// Unpack the package file at the selected directory
        /// </summary>
        public static bool Unpack(string PackFilePath, string DestinationFolder)
        {
            if (!System.IO.Directory.Exists(DestinationFolder)) {
                System.IO.Directory.CreateDirectory(DestinationFolder);
            }

            System.IO.FileStream PackFile = new System.IO.FileStream(PackFilePath, System.IO.FileMode.Open);

            Byte[] Buffer = null;

            //Read File Info Size
            Buffer = new Byte[4];
            PackFile.Read(Buffer, 0, 4);

            Byte[] ByteInfoBytesSize = Buffer;

            //Fix original array size from size 4
            while (ByteInfoBytesSize[0] == 0) {
                Byte[] temp = ByteInfoBytesSize;
                ByteInfoBytesSize = new Byte[ByteInfoBytesSize.Length - 1];
                for (int i = 0; i <= ByteInfoBytesSize.Length - 1; i++) {
                    ByteInfoBytesSize[i] = temp[i + 1];
                }
            }
            int InfoSize = int.Parse(System.Text.Encoding.Default.GetString(ByteInfoBytesSize));

            //Read File Info
            Buffer = new Byte[InfoSize];
            PackFile.Read(Buffer, 0, InfoSize);
            String FileInfo = System.Text.Encoding.Default.GetString(Buffer);

            Console.WriteLine("Unpacking package file {0}", PackFilePath);

            String[] Files = FileInfo.Split('|');
            foreach (String File in Files) {
                String FileName = File.Split(';')[0];
                int FileByteSize = int.Parse(File.Split(';')[1]);

                String FilePath = DestinationFolder + "\\" + FileName;
                if (System.IO.File.Exists(FilePath)) {
                    System.IO.File.Delete(FilePath);
                }
                if (FileName.Contains("\\")) {
                    if (!System.IO.Directory.Exists(DestinationFolder + "\\" + FileName.Split('\\')[0])) {
                        System.IO.Directory.CreateDirectory(DestinationFolder + "\\" + FileName.Split('\\')[0]);
                    }
                }
                System.IO.FileStream newFile = new System.IO.FileStream(FilePath, System.IO.FileMode.Create);
                Console.WriteLine("- Unpacking file: {0}", FileName);

                Buffer = new Byte[FileByteSize];
                PackFile.Read(Buffer, 0, FileByteSize);

                newFile.Write(Buffer, 0, FileByteSize);
                newFile.Close();
            }
            PackFile.Close();
            return true;
        }
        /// <summary>
        /// Create a file pack, join all the files from PackFileInfoCollection into a single file
        /// </summary>
        public static bool Pack(PackFileInfoCollection Collection, string PackDestination)
        {
            
            String FileInfo = Collection.CreateFileInfo();
            
            Byte[] InfoBytes = System.Text.Encoding.Default.GetBytes(FileInfo);
            int InfoBytesSize = InfoBytes.Length;
            Byte[] ByteInfoBytesSize = System.Text.Encoding.Default.GetBytes(InfoBytesSize.ToString());
            Console.WriteLine("Packing package file " + PackDestination);
            //Fix Array Size to 4
            if (ByteInfoBytesSize.Length < 4) {
                int d = 4 - ByteInfoBytesSize.Length;
                Byte[] temp = ByteInfoBytesSize;
                ByteInfoBytesSize = new Byte[4];
                for (int i = 3; i >= 0; i += -1) {
                    if (i - d >= 0) {
                        ByteInfoBytesSize[i] = temp[i - d];
                    } else {
                        ByteInfoBytesSize[i] = 0;
                    }
                }
            }
            if (System.IO.File.Exists(PackDestination)) {
                System.IO.File.Delete(PackDestination);
            }
            System.IO.FileStream packFile = new System.IO.FileStream(PackDestination, System.IO.FileMode.CreateNew);

            //Write InfoByteSize as Bytes[]
            packFile.Write(ByteInfoBytesSize, 0, 4);

            //Write Info as Bytes[]
            packFile.Write(InfoBytes, 0, InfoBytes.Length);

            //Write Files as Bytes[]

            foreach (string File in Collection.FileList) {
                Console.WriteLine("- Packing: " + System.IO.Path.GetFileName(File));
                Byte[] FileBytes = System.IO.File.ReadAllBytes(File);
                packFile.Write(FileBytes, 0, FileBytes.Length);
            }
            packFile.Close();

            return true;
        }
        
        /// <summary>
        /// reads a pack file and returns a list of entry file names
        /// </summary>
        /// <param name="PackFilePath"></param>
        /// <returns></returns>
        public static string[] ReadEntries(string PackFilePath)
        {
            List<string> Filenames = new List<string>();
            
            System.IO.FileStream PackFile = new System.IO.FileStream(PackFilePath, System.IO.FileMode.Open);
            
            Byte[] Buffer = null;
            
            //Read File Info Size
            Buffer = new Byte[4];
            PackFile.Read(Buffer, 0, 4);
            
            Byte[] ByteInfoBytesSize = Buffer;
            
            //Fix original array size from size 4
            while (ByteInfoBytesSize[0] == 0) {
                Byte[] temp = ByteInfoBytesSize;
                ByteInfoBytesSize = new Byte[ByteInfoBytesSize.Length - 1];
                for (int i = 0; i <= ByteInfoBytesSize.Length - 1; i++) {
                    ByteInfoBytesSize[i] = temp[i + 1];
                }
            }
            int InfoSize = int.Parse(System.Text.Encoding.Default.GetString(ByteInfoBytesSize));
            
            //Read File Info
            Buffer = new Byte[InfoSize];
            PackFile.Read(Buffer, 0, InfoSize);
            String FileInfo = System.Text.Encoding.Default.GetString(Buffer);
                
            String[] Files = FileInfo.Split('|');
            foreach (String File in Files) {
                String FileName = File.Split(';')[0];
                int FileByteSize = int.Parse(File.Split(';')[1]);
                
                Filenames.Add(FileName);
                Buffer = new Byte[FileByteSize];
                PackFile.Read(Buffer, 0, FileByteSize);
            }
            PackFile.Close();
            return Filenames.ToArray();
        }
    }
}