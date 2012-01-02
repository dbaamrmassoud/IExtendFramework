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
        
        private ASCIIProvider asciiLib = new ASCIIProvider();
        private RijndaelProvider rijndaelLib;
        private XorProvider xorLib = new XorProvider();
        private DESProvider desLib;
        private TripleDESProvider tripleDesLib;
        private AESProvider aesLib;
        private RC2Provider rc2Lib;
        private RSAProvider rsaLib;
        
        private void encryptButton_Click(System.Object sender, System.EventArgs e)
        {
            string result = "";
            string result2 = "";
            tripleDesLib = new TripleDESProvider(SampleObjects.CreateTripleDESKey(), SampleObjects.CreateTripleDESIV());
            if (asciiRadioButton.Checked) {
                result = asciiLib.Encrypt(TextBox1.Text, Convert.ToInt32(codeTextBox.Text));
                result2 = asciiLib.Decrypt(result, Convert.ToInt32(codeTextBox.Text));
            } else if (rijndaelRadioButton.Checked) {
                System.Text.Encoding myEnc = null;
                myEnc = System.Text.Encoding.GetEncoding("Windows-1252");
                byte[] t = myEnc.GetBytes(TextBox1.Text.ToCharArray());
                //convert string to bytes
                //Dim input() As Byte = m_utf8.GetBytes(TextBox1.Text.ToCharArray())
                rijndaelLib = new RijndaelProvider(SampleObjects.CreateRijndaelKeyWithSHA512(Interaction.InputBox("Key: ")), SampleObjects.CreateRijndaelIVWithSHA512(Interaction.InputBox("IV: ")));
                byte[] R = rijndaelLib.Encrypt(t);
                result = myEnc.GetString(R);
                result2 = myEnc.GetString(rijndaelLib.Decrypt(R));
            } else if (desRadioButton.Checked) {
                desLib = new DESProvider(SampleObjects.CreateDESKey(), SampleObjects.CreateDESIV());
                result = desLib.Encrypt(TextBox1.Text);
                result2 = desLib.Decrypt(result);
            } else if (tripleDesRadioButton.Checked) {
                result = tripleDesLib.Encrypt(TextBox1.Text);
                result2 = tripleDesLib.Decrypt(result);
            } else if (xorRadioButton.Checked) {
                result = xorLib.Encrypt(TextBox1.Text, Convert.ToInt32(codeTextBox.Text));
                result2 = xorLib.Decrypt(result, Convert.ToInt32(codeTextBox.Text));
            } else if (aesRadioButton.Checked) {
                aesLib = new AESProvider(SampleObjects.CreateAESKey(), SampleObjects.CreateAESIV());
                result = aesLib.Encrypt(TextBox1.Text);
                result2 = aesLib.Decrypt(result);
            } else if (rcTwoRadioButton.Checked) {
                rc2Lib = new RC2Provider(SampleObjects.CreateRC2Key(), SampleObjects.CreateRC2IV());
                result = rc2Lib.Encrypt(TextBox1.Text);
                result2 = rc2Lib.Decrypt(result);
            } else if (rsaRadioButton.Checked) {
                rsaLib = new RSAProvider();
                try {
                    result = rsaLib.Encrypt(TextBox1.Text);
                    result2 = rsaLib.Decrypt(result);
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
