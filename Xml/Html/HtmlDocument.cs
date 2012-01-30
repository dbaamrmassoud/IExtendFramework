using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using IExtendFramework.Xml.Sgml;
using System.IO;
using System.Net;
using IExtendFramework.Xml.Html.Nodes;

namespace IExtendFramework.Xml.Html
{
    /// <summary>
    /// Implementation of an HtmlDocument
    /// </summary>
    public class HtmlDocument : XmlDocument
    {
        /// <summary>
        /// Load an html file from the specified path
        /// </summary>
        /// <param name="filename">The path of the html file</param>
        public override void Load(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string html = sr.ReadToEnd();
                sr.Close();
                base.Load(new StringReader(html));
            }
        }

        /// <summary>
        /// Load an html from a stream
        /// </summary>
        /// <param name="inStream"></param>
        public override void Load(System.IO.Stream inStream)
        {
            string s = null;
            using (StreamReader sr = new StreamReader(inStream))
            {
                s = sr.ReadToEnd();

                sr.Close();
            }
            StringReader str = new StringReader(s);
            base.Load(new StringReader(s));
        }

        /// <summary>
        /// Load from the html from a TextReader
        /// </summary>
        /// <param name="txtReader"></param>
        public override void Load(System.IO.TextReader txtReader)
        {
            SgmlReader reader = new SgmlReader();

            reader.InputStream = txtReader;

            this.Load(reader);
        }
        
        /// <summary>
        /// Load an html file from a remote URL
        /// </summary>
        /// <param name="uri"></param>
        public void Load(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream strm = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(strm);

                string html = sr.ReadToEnd();

                sr.Close();

                this.LoadXml(html);
            }
        }
        
        /// <summary>
        /// Return the node that inherit from HtmlElement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xpath">XPath Query</param>
        /// <returns></returns>
        public T SelectSingleNode<T>(string xpath) where T: HtmlElement
        {
            return (T)this.SelectSingleNode(xpath);
        }
        
        public new IList<HtmlElement> SelectNodes(string xpath)
        {
            XmlNodeList xlist = base.SelectNodes(xpath);
            
            List<HtmlElement> lstNodes = new List<HtmlElement>((IEnumerable<HtmlElement>) xlist);
            
            
            return lstNodes;
        }
        
        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            if (Regex.Match(localName, "h[^1-6]").Captures.Count > 0)
            {
                return new HtmlHeaderNode(prefix, localName, namespaceURI, this);
            }
            switch (localName.ToLower())
            {
                case "a":
                    {
                        return new HtmlAnchorNode(prefix, localName, namespaceURI, this);
                    }
                case "br":
                    {
                        return new HtmlLineBreakNode(prefix, localName, namespaceURI, this);
                    }
                case "img":
                    {
                        return new HtmlImageNode(prefix, localName, namespaceURI, this);
                    }
                case "html":
                    {
                        return new HtmlNode(prefix, localName, namespaceURI, this);
                    }
                case "title":
                    {
                        return new HtmlTitleNode(prefix, localName, namespaceURI, this);
                    }
                case "body":
                    {
                        return new HtmlBodyNode(prefix, localName, namespaceURI, this);
                    }
                case "head":
                    {
                        return new HtmlHeadNode(prefix, localName, namespaceURI, this);
                    }
                case "b":
                    return new HtmlBoldTextNode(prefix, localName, namespaceURI, this);
                case "i":
                    return new HtmlItalicTextNode(prefix, localName, namespaceURI, this);
                case "u":
                    return new HtmlUnderlinedTextNode(prefix, localName, namespaceURI, this);
                case "s":
                    return new HtmlStrikeThroughTextNode(prefix, localName, namespaceURI, this);
            }

            return base.CreateElement(prefix, localName, namespaceURI);
        }
        
        /// <summary>
        /// Return all HtmlAnchor of this document
        /// </summary>
        /// <returns></returns>
        public IList<HtmlAnchorNode> GetAllHtmlAnchors()
        {
            XmlNodeList xlist = base.SelectNodes("//a");

            HtmlAnchorNode[] anchors = new HtmlAnchorNode[xlist.Count];

            for (int i = 0; i < anchors.Length; i++)
            {
                anchors[i] = (HtmlAnchorNode)xlist[i];
            }

            return anchors;
        }
        
        /// <summary>
        /// Load an html string
        /// </summary>
        /// <param name="xml"></param>
        public override void LoadXml(string html)
        {
            base.LoadXml(html);
        }
    }
}
