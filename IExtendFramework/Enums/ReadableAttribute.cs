/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/26/2012
 * Time: 2:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IExtendFramework.Enums
{
    /// <summary>
    /// Specifies an easy way to get readable enum string values
    /// </summary>
    public class ReadableEnumAttribute : Attribute
    {
        public AdvancedString Value
        { get; set; }
        
        public ReadableEnumAttribute(string v)
        {
            this.Value = v;
        }
        
        public ReadableEnumAttribute(AdvancedString v)
        {
            this.Value = v;
        }
    }
}
