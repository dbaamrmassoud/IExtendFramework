/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 3:40 PM
 */
using System;

namespace IExtendFramework.Xml.Html.Nodes
{
    /// <summary>
    /// Description of HtmlAbbreviationNode.
    /// </summary>
    public class HtmlAbbreviationNode : HtmlElement
    {
        public HtmlAbbreviationNode(string prefix, string localName, 
            string namespaceUri,HtmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {
        }
        
        public string Title
        {
            get
            {
                return this.GetAttribute("title");
            }
            set
            {
                this.SetAttribute("title", value);
                this.OnPropertyChanged("Title");
            }
        }
        
        public string Abbreviation
        {
            get
            {
                return this.InnerText;
            }
            set
            {
                this.InnerText = value;
                this.OnPropertyChanged("Abbreviation");
            }
        }
    }
}
