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
using System.ComponentModel;

namespace IExtendFramework.Xml.Html.Nodes
{
    public abstract class HtmlElement : XmlElement, INotifyPropertyChanged
    {
        #region Ctor
        public HtmlElement(string prefix, string localName, 
            string namespaceUri,XmlDocument doc)
            : base(prefix, localName, namespaceUri, doc)
        {

        }
        #endregion

        #region Properties

        public string HtmlName
        {
            get
            {
                return this.GetAttribute("name");
            }
            set
            {
                this.SetAttribute("HtmlName", value);
                this.OnPropertyChanged("HtmlName");
            }
        }

        public string CssClass
        {
            get
            {
                return this.GetAttribute("class");
            }
            set
            {
                this.SetAttribute("class", value);
                this.OnPropertyChanged("class");
            }
        }

        public string Style
        {
            get
            {
                return this.GetAttribute("Style");
            }
            set
            {
                this.SetAttribute("Style", value);
                this.OnPropertyChanged("Style");
            }
        }


        public string ID
        {
            get
            {                
                return this.GetAttribute("ID");
            }
            set
            {
                this.Attributes["ID"].InnerText = value;
                this.OnPropertyChanged("ID");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, 
                    new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
