/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/2/2011
 * Time: 1:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.Compiler
{
    /// <summary>
    /// A Parser.
    /// </summary>
    public interface IParser
    {
        object Parse();
        object Parse(string code);
        
    }
}
