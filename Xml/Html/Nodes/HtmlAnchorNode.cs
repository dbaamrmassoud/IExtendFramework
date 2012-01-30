#region Fireball License
//    Copyright (C) 2005  Sebastian Faltoni sebastian{at}dotnetfireball{dot}net
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IExtendFramework.Xml.Html.Nodes
{
    /// <summary>
    /// Represent an &lt;a&gt; tag
    /// </summary>
    public class HtmlAnchorNode : HtmlElement
    {
        public HtmlAnchorNode(string prefix, string localName,
            string namespaceUri, XmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {

        }

        /// <summary>
        /// The target window or frame where to navigate the link
        /// </summary>
        public string Target
        {
            get
            {
                return this.GetAttribute("Target");
            }
            set
            {                
                this.SetAttribute("Target", value);
                this.OnPropertyChanged("Target");
            }
        }

        /// <summary>
        /// The url of the link
        /// </summary>
        public string LinkUrl
        {
            get
            {
                return this.GetAttribute("href");
            }
            set
            {
                this.SetAttribute("LinkUrl", value);
                this.OnPropertyChanged("LinkUrl");
            }
        }

        /// <summary>
        /// The text show on the link
        /// </summary>
        public string LinkText
        {
            get
            {
                return this.InnerText;
            }
            set
            {
                this.InnerText = value;
                this.OnPropertyChanged("LinkText");
            }
        }
    }
}
