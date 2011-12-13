/*
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
    /// <summary>
    /// Class that contains methods for opening files
    /// </summary>
    public class FileExtensionManager
    {
        public static readonly List<ITextEditor> Editors = new List<ITextEditor>();
        private static List<ITextEditor> openEditors = new List<ITextEditor>();
        
        private FileExtensionManager()
        {
            
        }
        
        public static ITextEditor OpenDocument(string filename)
        {
            string ext = System.IO.Path.GetExtension(filename);
            foreach (ITextEditor editor in Editors)
            {
                if (editor.Extension.Extension.ToLower() == ext)
                {
                    ITextEditor e = editor.Create(filename);
                    //e.Close += 
                    openEditors.Add(e);
                    return e;
                }
            }
            
            throw new IExtendFrameworkException("Cannot find extension '" + ext.ToLower() + "' in extension list!");
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
