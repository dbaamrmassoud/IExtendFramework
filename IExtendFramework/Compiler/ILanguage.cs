/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/2/2011
 * Time: 1:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.Compiler
{
    /// <summary>
    /// A Language
    /// </summary>
    public interface ILanguage<T>
    {
        string LanguageName
        {get; }
        
        ICompiler<T> Compiler
        {get; }
        
        IParser<T> Parser
        {get; }
    }
}
