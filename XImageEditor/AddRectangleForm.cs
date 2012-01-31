/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 1/21/2012
 * Time: 6:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace XImageEditor
{
    /// <summary>
    /// Description of AddRectangleForm.
    /// </summary>
    public partial class AddRectangleForm : Form
    {
        public int Width;
        public int Height;
        public Point Result;
        
        public AddRectangleForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            this.Width = int.Parse(textBox1.Text);
            this.Height = int.Parse(textBox2.Text);
            this.DialogResult = DialogResult.OK;
            Result = new Point(int.Parse(textBox3.Text), int.Parse(textBox4.Text));
            Close();
        }
        
        void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
