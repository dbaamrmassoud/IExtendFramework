/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 4:21 PM
 */
using System;

namespace IExtendFramework.Xml.Html.Nodes
{
    /// <summary>
    /// H1 to H6
    /// </summary>
    public class HtmlHeaderNode : HtmlElement
    {
        public HtmlHeaderNode(string prefix, string localName,
            string namespaceUri, HtmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {
        }
    }
}
