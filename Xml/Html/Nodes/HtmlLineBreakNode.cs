using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IExtendFramework.Xml.Html.Nodes
{
    /// <summary>
    /// The Line Break element &lt;br /&gt;
    /// </summary>
    public class HtmlLineBreakNode : HtmlElement
    {
        public HtmlLineBreakNode(string prefix, string localName,
            string namespaceUri, XmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {

        }
    }
}
