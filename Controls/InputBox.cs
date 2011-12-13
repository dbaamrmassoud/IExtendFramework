/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/14/2011
 * Time: 11:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IExtendFramework.Controls
{
    /// <summary>
    /// InputBox form for getting string input
    /// </summary>
    public partial class InputBox : Form
    {
        private string Result;
        private bool closable = false;
        private InputBox()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            Result = textBox1.Text;
            closable = true;
            Close();
        }
        
        void InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closable)
                e.Cancel = true;
        }
        
        public static string Show(string title, string text)
        {
            InputBox ib = new InputBox();
            ib.Text = title;
            ib.label1.Text = text;
            ib.ShowDialog();
            return ib.Result;
        }
    }
}
