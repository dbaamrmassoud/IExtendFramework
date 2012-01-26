/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/2/2011
 * Time: 1:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.CodeDom.Compiler;

namespace IExtendFramework.Compiler
{
    /// <summary>
    /// A Compiler.
    /// </summary>
    public interface ICompiler<T>
    {
        CompilerResults Compile(string code);
        CompilerResults Compile(T parsedCode);
    }
}
