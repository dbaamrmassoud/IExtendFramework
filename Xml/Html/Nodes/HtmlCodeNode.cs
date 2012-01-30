/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 4:45 PM
 */
using System;

namespace IExtendFramework.Xml.Html.Nodes
{
    /// <summary>
    /// A &lt;code&gt; tag
    /// </summary>
    public class HtmlCodeNode : HtmlElement
    {
        public HtmlCodeNode(string prefix, string localName, 
            string namespaceUri,HtmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {
        }
    }
}
