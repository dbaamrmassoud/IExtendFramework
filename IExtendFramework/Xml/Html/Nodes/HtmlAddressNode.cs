/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 3:44 PM
 */
using System;

namespace IExtendFramework.Xml.Html.Nodes
{
    /// <summary>
    /// An &lt;address&gt; node
    /// </summary>
    public class HtmlAddressNode : HtmlElement
    {
        public HtmlAddressNode(string prefix, string localName, 
            string namespaceUri,HtmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {
        }
    }
}
