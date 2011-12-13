using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace IExtendFramework.Plugins
{
	public interface IPluginManager
	{
		void FindPlugins();
		void FindPlugins(string Path);
		void ClosePlugin(string pluginNameOrPath);
		void ClosePlugins();
		string[] GetPluginInfo(string FileName, string InterfaceToFind);
		void AddPlugin(string FileName);
		void GetControlFromString(string path, Control item);
		AvailablePlugins AvailablePlugins { get; set; }
		void Dispose();
	}
}
