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
    /// A file extension
    /// </summary>
    public interface IFileExtension
    {
        string Extension
        {get;}
        
        string Description
        {get;}
        
        string Category
        {get; }
    }
}
