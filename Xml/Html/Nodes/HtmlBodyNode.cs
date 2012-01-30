using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IExtendFramework.Xml.Html.Nodes
{
    public class HtmlBodyNode : HtmlElement
    {
        public HtmlBodyNode(string prefix, string localName,
            string namespaceUri, XmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {

        }
    }
}
