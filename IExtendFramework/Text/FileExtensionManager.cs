﻿/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/15/2011
 * Time: 12:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace IExtendFramework.Text
{
    public delegate void FileOpened(string filename, ITextEditor editor);
    /// <summary>
    /// Class that contains methods for opening files
    /// </summary>
    public class FileExtensionManager
    {
        public static event FileOpened OpeningFile;
        
        public static readonly List<ITextEditor> Editors = new List<ITextEditor>();
        private static List<ITextEditor> openEditors = new List<ITextEditor>();
        
        private FileExtensionManager()
        {
        }
        
        public static ITextEditor OpenDocument(string filename)
        {
            string ext = System.IO.Path.GetExtension(filename);
            bool found = false;
            foreach (ITextEditor editor in Editors)
            {
                if (editor.Extension.Extension.ToLower() == ext)
                {
                    ITextEditor e = editor.Create(filename);
                    //e.Close += delegate() { openEditors.Remove(e); };
                    OpeningFile(filename, e);
                    openEditors.Add(e);
                    found = true;
                    return e;
                }
            }
            if (!found)
                throw new Exception("Cannot find extension '" + ext.ToLower() + "' in extension list!");
            return null;
        }
        
        public static void AddEditor(ITextEditor editor)
        {
            Editors.Add(editor);
        }
        
        public static void Reset()
        {
            Editors.Clear();
            foreach (ITextEditor i in openEditors)
                i.Dispose();
            openEditors.Clear();
        }
    }
}