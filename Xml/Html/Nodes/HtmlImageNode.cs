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
    /// Represents an &gtimg/&lt; html tag
    /// </summary>
    public class HtmlImageNode : HtmlElement
    {
        public HtmlImageNode(string prefix, string localName,
            string namespaceUri, XmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {

        }

        /// <summary>
        /// Height of the image
        /// </summary>
        public string Height
        {
            get
            {
                return this.GetAttribute("Height");
            }
            set
            {
                this.SetAttribute("Height", value);
                this.OnPropertyChanged("Height");
            }
        }

        /// <summary>
        /// The width of the image
        /// </summary>
        public string Width
        {
            get
            {
                return this.GetAttribute("Width");
            }
            set
            {
                this.SetAttribute("Width", value);
                this.OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// The border size of the image
        /// </summary>
        public string Border
        {
            get
            {
                return this.GetAttribute("Border");
            }
            set
            {
                this.SetAttribute("Border", value);
                this.OnPropertyChanged("Border");
            }
        }

        /// <summary>
        /// The url of the image
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return this.GetAttribute("src");
            }
            set
            {
                this.SetAttribute("src", value);
                this.OnPropertyChanged("ImageUrl");
            }
        }
    }
}
