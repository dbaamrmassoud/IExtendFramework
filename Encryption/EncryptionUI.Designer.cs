using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
namespace IExtendFramework.Encryption
{
    partial class EncryptionUI : System.Windows.Forms.Form
    {

        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try {
                if (disposing && components != null) {
                    components.Dispose();
                }
            } finally {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.
        //Do not modify it using the code editor.
        private void InitializeComponent()
        {
            this.asciiRadioButton = new System.Windows.Forms.RadioButton();
            this.rijndaelRadioButton = new System.Windows.Forms.RadioButton();
            this.tripleDesRadioButton = new System.Windows.Forms.RadioButton();
            this.xorRadioButton = new System.Windows.Forms.RadioButton();
            this.desRadioButton = new System.Windows.Forms.RadioButton();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.rsaRadioButton = new System.Windows.Forms.RadioButton();
            this.rcTwoRadioButton = new System.Windows.Forms.RadioButton();
            this.aesRadioButton = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.encryptButton = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            //asciiRadioButton
            //
            this.asciiRadioButton.AutoSize = true;
            this.asciiRadioButton.Checked = true;
            this.asciiRadioButton.Location = new System.Drawing.Point(16, 21);
            this.asciiRadioButton.Name = "asciiRadioButton";
            this.asciiRadioButton.Size = new System.Drawing.Size(52, 17);
            this.asciiRadioButton.TabIndex = 0;
            this.asciiRadioButton.TabStop = true;
            this.asciiRadioButton.Text = "ASCII";
            this.asciiRadioButton.UseVisualStyleBackColor = true;
            //
            //rijndaelRadioButton
            //
            this.rijndaelRadioButton.AutoSize = true;
            this.rijndaelRadioButton.Location = new System.Drawing.Point(16, 44);
            this.rijndaelRadioButton.Name = "rijndaelRadioButton";
            this.rijndaelRadioButton.Size = new System.Drawing.Size(63, 17);
            this.rijndaelRadioButton.TabIndex = 1;
            this.rijndaelRadioButton.Text = "Rijndael";
            this.rijndaelRadioButton.UseVisualStyleBackColor = true;
            //
            //tripleDesRadioButton
            //
            this.tripleDesRadioButton.AutoSize = true;
            this.tripleDesRadioButton.Location = new System.Drawing.Point(16, 90);
            this.tripleDesRadioButton.Name = "tripleDesRadioButton";
            this.tripleDesRadioButton.Size = new System.Drawing.Size(76, 17);
            this.tripleDesRadioButton.TabIndex = 2;
            this.tripleDesRadioButton.Text = "Triple DES";
            this.tripleDesRadioButton.UseVisualStyleBackColor = true;
            //
            //xorRadioButton
            //
            this.xorRadioButton.AutoSize = true;
            this.xorRadioButton.Location = new System.Drawing.Point(16, 113);
            this.xorRadioButton.Name = "xorRadioButton";
            this.xorRadioButton.Size = new System.Drawing.Size(41, 17);
            this.xorRadioButton.TabIndex = 3;
            this.xorRadioButton.Text = "Xor";
            this.xorRadioButton.UseVisualStyleBackColor = true;
            //
            //desRadioButton
            //
            this.desRadioButton.AutoSize = true;
            this.desRadioButton.Location = new System.Drawing.Point(16, 67);
            this.desRadioButton.Name = "desRadioButton";
            this.desRadioButton.Size = new System.Drawing.Size(47, 17);
            this.desRadioButton.TabIndex = 4;
            this.desRadioButton.Text = "DES";
            this.desRadioButton.UseVisualStyleBackColor = true;
            //
            //TextBox1
            //
            this.TextBox1.Location = new System.Drawing.Point(150, 85);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox1.Size = new System.Drawing.Size(228, 253);
            this.TextBox1.TabIndex = 5;
            this.TextBox1.Text = "Hello";
            this.TextBox1.WordWrap = false;
            //
            //GroupBox1
            //
            this.GroupBox1.Controls.Add(this.rsaRadioButton);
            this.GroupBox1.Controls.Add(this.rcTwoRadioButton);
            this.GroupBox1.Controls.Add(this.aesRadioButton);
            this.GroupBox1.Controls.Add(this.desRadioButton);
            this.GroupBox1.Controls.Add(this.asciiRadioButton);
            this.GroupBox1.Controls.Add(this.rijndaelRadioButton);
            this.GroupBox1.Controls.Add(this.xorRadioButton);
            this.GroupBox1.Controls.Add(this.tripleDesRadioButton);
            this.GroupBox1.Location = new System.Drawing.Point(12, 57);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(129, 281);
            this.GroupBox1.TabIndex = 6;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Encryption Type";
            //
            //rsaRadioButton
            //
            this.rsaRadioButton.AutoSize = true;
            this.rsaRadioButton.Location = new System.Drawing.Point(16, 185);
            this.rsaRadioButton.Name = "rsaRadioButton";
            this.rsaRadioButton.Size = new System.Drawing.Size(47, 17);
            this.rsaRadioButton.TabIndex = 7;
            this.rsaRadioButton.TabStop = true;
            this.rsaRadioButton.Text = "RSA";
            this.rsaRadioButton.UseVisualStyleBackColor = true;
            //
            //rcTwoRadioButton
            //
            this.rcTwoRadioButton.AutoSize = true;
            this.rcTwoRadioButton.Location = new System.Drawing.Point(16, 161);
            this.rcTwoRadioButton.Name = "rcTwoRadioButton";
            this.rcTwoRadioButton.Size = new System.Drawing.Size(46, 17);
            this.rcTwoRadioButton.TabIndex = 6;
            this.rcTwoRadioButton.TabStop = true;
            this.rcTwoRadioButton.Text = "RC2";
            this.rcTwoRadioButton.UseVisualStyleBackColor = true;
            //
            //aesRadioButton
            //
            this.aesRadioButton.AutoSize = true;
            this.aesRadioButton.Location = new System.Drawing.Point(16, 137);
            this.aesRadioButton.Name = "aesRadioButton";
            this.aesRadioButton.Size = new System.Drawing.Size(46, 17);
            this.aesRadioButton.TabIndex = 5;
            this.aesRadioButton.TabStop = true;
            this.aesRadioButton.Text = "AES";
            this.aesRadioButton.UseVisualStyleBackColor = true;
            //
            //Label1
            //
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(147, 69);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 13);
            this.Label1.TabIndex = 7;
            this.Label1.Text = "Text To Encrypt:";
            //
            //outputLabel
            //
            this.outputLabel.Location = new System.Drawing.Point(12, 387);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(366, 195);
            this.outputLabel.TabIndex = 8;
            this.outputLabel.Text = "Output:";
            //
            //encryptButton
            //
            this.encryptButton.Location = new System.Drawing.Point(159, 344);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 9;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            //
            //Label3
            //
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(12, 353);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(35, 13);
            this.Label3.TabIndex = 10;
            this.Label3.Text = "Code:";
            //
            //codeTextBox
            //
            this.codeTextBox.Location = new System.Drawing.Point(53, 346);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(100, 20);
            this.codeTextBox.TabIndex = 11;
            //
            //Encryption_UI
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 591);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.TextBox1);
            this.Name = "Encryption_UI";
            this.Text = "Encryption_UI";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.RadioButton asciiRadioButton;
        internal System.Windows.Forms.RadioButton rijndaelRadioButton;
        internal System.Windows.Forms.RadioButton tripleDesRadioButton;
        internal System.Windows.Forms.RadioButton xorRadioButton;
        internal System.Windows.Forms.RadioButton desRadioButton;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Button withEventsField_encryptButton;
        internal System.Windows.Forms.Button encryptButton {
            get { return withEventsField_encryptButton; }
            set {
                if (withEventsField_encryptButton != null) {
                    withEventsField_encryptButton.Click -= encryptButton_Click;
                }
                withEventsField_encryptButton = value;
                if (withEventsField_encryptButton != null) {
                    withEventsField_encryptButton.Click += encryptButton_Click;
                }
            }
        }
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox codeTextBox;
        internal System.Windows.Forms.RadioButton rsaRadioButton;
        internal System.Windows.Forms.RadioButton rcTwoRadioButton;
        internal System.Windows.Forms.RadioButton aesRadioButton;
    }
}
