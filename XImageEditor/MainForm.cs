/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/21/2012
 * Time: 5:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IExtendFramework.Drawing;
using IExtendFramework.Drawing.XmlFormat;

namespace XImageEditor
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        XImage Image = new XImage();
        string filename = "";
        
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            xImageControl1.Click += new EventHandler(xImageControl1_Click);
        }

        void xImageControl1_Click(object sender, EventArgs e)
        {
            // add a point (using the location clicked on form, not screen) 
            // the extra pixels removed increase accuracy to the end of the mouse (if regular sized)
            Image.Points.Add(new IExtendFramework.Drawing.XPoint(MousePosition.X - this.Location.X - 10, MousePosition.Y - this.Location.Y - 55, Pens.Black));
            this.xImageControl1.LoadImage(Image);
            this.Invalidate();
            xImageControl1.Invalidate();
        }
        
        void NEwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image = new XImage();
            filename = "";
            xImageControl1.LoadImage(Image);
        }
        
        void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XImages|*.ximg|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image = XImage.FromFile(ofd.FileName);
                // repaint
                this.Invalidate();
                this.xImageControl1.Invalidate();
            }
        }
        
        void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XImage|*.ximg|All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filename = sfd.FileName;
                }
            }
            if (filename != "")
                Image.Save(filename);
        }
        
        void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filename = "";
            SaveToolStripMenuItem_Click(sender, e);
        }
        
        void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        void Timer1_Tick(object sender, EventArgs e)
        {
            if (filename == "")
                this.Text = "XImage Editor - [Untitled] - Points: " + Image.Points.Count.ToString();
            else
                this.Text = "XImage Editor - [" + Path.GetFileName(filename) + "] - Points: " + Image.Points.Count.ToString();
            // update UI
            this.xImageControl1.Invalidate();
            this.Invalidate();
        }
        
        void RectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRectangleForm arf = new AddRectangleForm();
            if (arf.ShowDialog() == DialogResult.OK)
            {
                int a = arf.Result.X;
                int b = arf.Result.Y;
                
                while (a < arf.Width)
                {
                    Image.Points.Add(new XPoint(a, b, Pens.Black));
                    a++;
                }
                while (b < arf.Height)
                {
                    Image.Points.Add(new XPoint(a, b, Pens.Black));
                    b++;
                }
                a = arf.Result.X;
                b = arf.Result.Y;
                while (b < arf.Height)
                {
                    Image.Points.Add(new XPoint(a, b, Pens.Black));
                    b++;
                }
                while (a < arf.Width)
                {
                    Image.Points.Add(new XPoint(a, b, Pens.Black));
                    a++;
                }
            }
            xImageControl1.LoadImage(Image);
        }
        
        void AddCircle(object sender, EventArgs e)
        {
            
        }
    }
}
