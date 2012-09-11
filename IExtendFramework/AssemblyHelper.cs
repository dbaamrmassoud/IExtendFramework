/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/14/2011
 * Time: 11:20 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Reflection;

namespace IExtendFramework
{
    /// <summary>
    /// Some random utilities involving assemblies
    /// </summary>
    public class AssemblyHelper
    {
        private AssemblyHelper()
        {
        }

        /// <summary>
        /// Assembly defaults to the current IExtendFramework assembly
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static Stream GetEmbeddedResource(string resourceName, Assembly asm = null)
        {
            if (asm == null)
                asm = Assembly.GetExecutingAssembly();
            //Assembly a1 = Assembly.GetExecutingAssembly();
            //Stream s = a1.GetManifestResourceStream(resourceName);
            //return s;
            Stream s = asm.GetManifestResourceStream(resourceName);
            if (s == null)
                throw new Exception("Could not load '" + resourceName + "' from '" + asm.ToString() + "'!");

            return s;
        }
    }
}
