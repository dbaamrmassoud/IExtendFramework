/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/15/2011
 * Time: 12:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.Text
{
    /// <summary>
    /// Interface for managing documents
    /// </summary>
    public interface IDocument
    {
        string Filename
        {get; set;}
        
        void Open(string filename);
        
	ITextEditor Parent
	{get; }
    }
}
