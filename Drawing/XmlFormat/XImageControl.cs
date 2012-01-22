/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/21/2012
 * Time: 2:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IExtendFramework.Drawing.XmlFormat
{
    /// <summary>
    /// An Ximage renderer control
    /// </summary>
    public class XImageControl : UserControl
    {
        /// <summary>
        /// The number of points created in the last redraw
        /// </summary>
        public int PointsCreated
        {get; private set; }
        
        private XImage ximg = null;
        /// <summary>
        /// The current XImage being rendered
        /// </summary>
        public XImage Image
        {
            get
            {
                return ximg;
            }
            set
            {
                //ximg = value;
                LoadImage(value);
            }
        }
        
        public XImageControl()
        {
            this.BackColor = Color.White;
            PointsCreated = 0;
            this.Paint += new PaintEventHandler(XImageControl_Paint);
        }

        void XImageControl_Paint(object sender, PaintEventArgs e)
        {
            if (this.Image != null)
            {
                foreach (IExtendFramework.Drawing.XPoint p in Image.Points)
                {
                    e.Graphics.DrawLine(p.Pen, p.X, p.Y, p.X + 1, p.Y + 1); //draw lines
                    //MessageBox.Show(string.Format("{0},{1},{2},{3}", p.X, p.Y, 1, 1));
                }
                PointsCreated = this.Image.Points.Count;
            }
        }
        
        /// <summary>
        /// Loads a new XImage to be rendered
        /// </summary>
        /// <param name="image"></param>
        public void LoadImage(XImage image)
        {
            this.ximg = image;
        }
    }
}
