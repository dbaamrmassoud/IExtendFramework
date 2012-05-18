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
        /// <param name="ArchiveDirectory">Extra Path for example \folder1, Can also be empty</param>
        public void AddFile(String Path, String ExtraPath)
        {
            if (!FileList.Contains(Path))
            {
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
        /// <param name="ArchiveDirectory">Extra Path for example \folder1, Can also be empty</param>
        public void AddDirectory(String Directory, String ExtraPath)
        {
            foreach (String File in System.IO.Directory.GetFiles(Directory))
            {
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
            foreach (String FilePath in FileList)
            {
                Info += Convert.ToString(FileNames[FileList.IndexOf(FilePath)]) + ";" + (new System.IO.FileInfo(FilePath).Length) + "|";
            }
            if (Info.EndsWith("|"))
            {
                Info = Info.Substring(0, Info.Length - 1);
            }
            return Info;
        }
    }
}
