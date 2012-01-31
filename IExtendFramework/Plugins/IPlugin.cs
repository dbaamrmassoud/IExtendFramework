using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
namespace IExtendFramework.Plugins
{
    public interface IPlugin
    {
        /// <summary>
        /// The Plugin Version
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        string Version { get; }
        /// <summary>
        /// The Plugin Name
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        string Name { get; }
        /// <summary>
        /// The Plugin Description
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        string Description { get; }
        /// <summary>
        /// The Plugin Creator/Author
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        string Author { get; }
        /// <summary>
        /// The http(s):// file location of the DLL for updates.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        string DownloadURL { get; }
        
        /// <summary>
        /// The Path for the ToolStripMenuItem
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        string MenuItemPath { get; }

        /// <summary>
        /// Creates the plugin.
        /// </summary>
        /// <remarks></remarks>
        void Initialize();

        /// <summary>
        /// Destroy the Plugin
        /// </summary>
        /// <remarks></remarks>
        void Dispose();
        
        Control Control
        {get; }
    }
}