/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/29/2011
 * Time: 10:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace IExtendFramework.Drawing
{
    /// <summary>
    /// A Location
    /// </summary>
    public class XPoint
    {
        public XPoint()
        {
        }
        
        public XPoint(int x, int y, Pen p)
        {
            this.X = x;
            this.Y = y;
            this.Pen = p;
        }
        
        public int X
        {
            get;
            set;
        }
        
        public int Y
        {
            get;
            set;
        }
        
        public Pen Pen
        {
            get;
            set;
        }
    }
}
