/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/29/2011
 * Time: 10:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;

namespace IExtendFramework.Drawing.XmlFormat
{
    /// <summary>
    /// Wrapper for an XImg item
    /// </summary>
    public class XImage
    {
        private List<IExtendFramework.Drawing.XPoint> Points = new List<IExtendFramework.Drawing.XPoint>();

        private XImage()
        {
        }
        

        public static XImage FromFile(string filename)
        {
            XImage x = new XImage();
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNodeList colorList = doc.SelectNodes("/Image/Point/Color");
            XmlNodeList xList = doc.SelectNodes("/Image/Point/X");
            XmlNodeList yList = doc.SelectNodes("/Image/Point/Y");
            
            for (int i = 0; i < xList.Count; i++)
            {
                IExtendFramework.Drawing.XPoint p = new IExtendFramework.Drawing.XPoint(
                    int.Parse(xList[i].InnerText),
                    int.Parse(yList[i].InnerText),
                    new Pen(Color.FromName(colorList[i].InnerText))
                   );
                x.Points.Add(p);
            }
            
            return x;
        }

        public static XImage FromPoints(List<IExtendFramework.Drawing.XPoint> points)
        {
            XImage i = new XImage();
            i.Points = points;
            return i;
        }

        public static XImage ConvertFromImage(System.Drawing.Image image)
        {
            return null;
        }

        public Control Render(Control inControl)
        {
            Graphics e = inControl.CreateGraphics();
            foreach (IExtendFramework.Drawing.XPoint p in Points)
                e.DrawLine(p.Pen, p.X, p.Y, p.X + 1, p.Y + 1); //draw lines
            
            inControl.Text = "Created " + Points.Count.ToString() + " points";
            
            return inControl;
        }
    }
}
