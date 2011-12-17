/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/15/2011
 * Time: 12:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace IExtendFramework.Text
{
    /// <summary>
    /// Container for Text Editors that 1. Generate themselves on a file, and 2. Contain the proper extension
    /// </summary>
    public interface ITextEditor
    {
        DockContent DockingPanel
        {get; }
        
        IFileExtension Extension
        {get; }
        
        ITextEditor Create(string FileName);
        
        /// <summary>
        /// return CurrentDocument.Filename
        /// </summary>
        string Filename
        {get; set; }

        void Dispose();

        IDocument CurrentDocument
        {get; }

        string DocumentText
        {get; set; }


        #region TEXT EDITOR FUNCTIONS
        void Undo();
        void Redo();
        int UndoBuffer
        {get; }
        void Save();
        void SaveAs();
        void Print();
        void PrintPreview();
        void PrintSetup();
        void Cut();
        void Copy();
        void Paste();
        void Insert(int index, string text);
        void ChangeFont(Font newFont);
        void ChangeColor(Color newColor);
        void Open(string filename);
        void SelectAll();
        #endregion
    }
}
