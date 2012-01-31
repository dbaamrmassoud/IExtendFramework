/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/15/2011
 * Time: 12:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.Plugins
{
    /// <summary>
    /// Contains an IPluginManager and contains methods for using it
    /// </summary>
    public class GlobalPluginManager : IDisposable
    {
        public IPluginManager Manager;
        
        public GlobalPluginManager()
        {
        }
        
        public GlobalPluginManager(IPluginManager ipm)
        {
            this.Manager = ipm;
        }
        
        public void Scan(string dir)
        {
            Manager.FindPlugins(dir);
        }
        
        public void Dispose()
        {
            Manager.Dispose();
            Manager = null;
        }
        
        public void SetPluginManager(IPluginManager manager)
        {
            this.Manager.Dispose();
            this.Manager = manager;
        }
        
        public void AddPlugin(IPlugin plugin, string filename)
        {
            this.Manager.AvailablePlugins.Add(new AvailablePlugin() { Instance = plugin, AssemblyPath = filename });
        }
        
    }
}
