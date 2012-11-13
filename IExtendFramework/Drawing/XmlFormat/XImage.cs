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
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace IExtendFramework.Drawing.XmlFormat
{
    /// <summary>
    /// Wrapper for an XImg item
    /// </summary>
    public class XImage
    {
        //public List<IExtendFramework.Drawing.XPoint> Points = new List<IExtendFramework.Drawing.XPoint>();
        public IExtendFramework.Collections.Generic.LightCollection<XPoint> Points = new IExtendFramework.Collections.Generic.LightCollection<XPoint>();

        public event EventHandler OnChanged = null;

        public XImage()
        {
            Points.ItemAdd += raiseChanged;
            Points.ItemRemove += new Collections.Generic.LightCollection<XPoint>.LightCollectionRemoveHandler(Points_ItemRemove);
            Points.ItemMove += new Collections.Generic.LightCollection<XPoint>.LightCollectionMoveHandler(Points_ItemMove);
            Points.ItemAddRange += new Collections.Generic.LightCollection<XPoint>.LightCollectionAddRangeHandler(Points_ItemAddRange);
        }

        void Points_ItemAddRange(object sender, Collections.Generic.LightCollection<XPoint>.LightCollectionAddRangeEventArgs e)
        {
            raiseChanged(null, null);
        }

        void Points_ItemMove(object sender, Collections.Generic.LightCollection<XPoint>.LightCollectionMoveEventArgs e)
        {
            raiseChanged(null, null);
        }

        void Points_ItemRemove(object sender, Collections.Generic.LightCollection<XPoint>.LightCollectionRemoveEventArgs e)
        {
            raiseChanged(null, null);
        }


        void raiseChanged(object sender, Collections.Generic.LightCollection<XPoint>.LightCollectionAddEventArgs e)
        {
            if (OnChanged != null)
                OnChanged(this, new EventArgs());
        }

        public static XImage FromFile(string filename)
        {
            XImage x = new XImage();
            try
            {
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
                    //MessageBox.Show(p.ToString());
                    x.Points.Add(p);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return x;
        }

        public static XImage FromPoints(List<IExtendFramework.Drawing.XPoint> points)
        {
            XImage i = new XImage();
            i.Points = new IExtendFramework.Collections.Generic.LightCollection<XPoint>(points.ToArray());
            return i;
        }

        public static XImage ConvertFromImage(System.Drawing.Image image)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Saves the image into a nicely formatted XML file, using 4 spaces instead of tabs.
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine("<Image>");
            // write and format XML string of the image
            foreach (XPoint xp in Points)
                sw.Write("    <Point>\r\n        <X>" + xp.X.ToString() + "</X>\r\n        <Y>" + xp.Y.ToString() + "</Y>\r\n        <Color>" + xp.Pen.Color.Name + "</Color>\r\n    </Point>\r\n");
            sw.WriteLine("</Image>");
            sw.Close();
        }
    }
}
