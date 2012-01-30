/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 3:24 PM
 */
using System;
using System.Windows.Forms;
using System.Xml;

namespace IExtendFramework.Xml.Html.Nodes
{
    /// <summary>
    /// &lt;s&gt; code
    /// </summary>
    public class HtmlStrikeThroughTextNode : HtmlElement
    {
        public HtmlStrikeThroughTextNode(string prefix, string localName, 
            string namespaceUri,XmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {
        }
    }
}
