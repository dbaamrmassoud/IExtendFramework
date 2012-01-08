using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Security;
using System.Security.Cryptography;
namespace IExtendFramework.Encryption
{
    public partial class EncryptionUI
    {
        public EncryptionUI()
        {
            InitializeComponent();
        }
        private void encryptButton_Click(System.Object sender, System.EventArgs e)
        {
            string result = "";
            string result2 = "";
            if (asciiRadioButton.Checked) {
                result = ASCIIProvider.Encrypt(TextBox1.Text, Convert.ToInt32(codeTextBox.Text));
                result2 = ASCIIProvider.Decrypt(result, Convert.ToInt32(codeTextBox.Text));
            } else if (rijndaelRadioButton.Checked) {
                System.Text.Encoding myEnc = null;
                myEnc = System.Text.Encoding.GetEncoding("Windows-1252");
                byte[] t = myEnc.GetBytes(TextBox1.Text.ToCharArray());
                //convert string to bytes
                //Dim input() As Byte = m_utf8.GetBytes(TextBox1.Text.ToCharArray())
                byte[] R = RijndaelProvider.Encrypt(t);
                result = myEnc.GetString(R);
                result2 = myEnc.GetString(RijndaelProvider.Decrypt(R));
            } else if (desRadioButton.Checked) {
                result = DESProvider.Encrypt(TextBox1.Text);
                result2 = DESProvider.Decrypt(result);
            } else if (tripleDesRadioButton.Checked) {
                result = TripleDESProvider.Encrypt(TextBox1.Text);
                result2 = TripleDESProvider.Decrypt(result);
            } else if (xorRadioButton.Checked) {
                result = XorProvider.Encrypt(TextBox1.Text, Convert.ToInt32(codeTextBox.Text));
                result2 = XorProvider.Decrypt(result, Convert.ToInt32(codeTextBox.Text));
            } else if (aesRadioButton.Checked) {
                result = AESProvider.Encrypt(TextBox1.Text);
                result2 = AESProvider.Decrypt(result);
            } else if (rcTwoRadioButton.Checked) {
                result = RC2Provider.Encrypt(TextBox1.Text);
                result2 = RC2Provider.Decrypt(result);
            } else if (rsaRadioButton.Checked) {
                try {
                    result = RSAProvider.Encrypt(TextBox1.Text);
                    result2 = RSAProvider.Decrypt(result);
                } catch (Exception ex) {
                    Interaction.MsgBox(ex.ToString());
                    TextBox1.Text = ex.ToString();
                }
            } else {
                Interaction.MsgBox("Invalid Option!!!");
                return;
            }
            outputLabel.Text = "Encrypted Result: " + result + "  Decrypted Result: " + result2;
        }
    }
}
