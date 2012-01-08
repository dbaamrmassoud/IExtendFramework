/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/2/2011
 * Time: 3:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Mail;

namespace IExtendFramework.Net.Mail
{
    /// <summary>
    /// Description of HotmailMessage.
    /// </summary>
    public class HotmailMessage
    {
        private HotmailMessage()
        {
        }
        
        public static void Send(string[] to, string from, string subject, string body, string userEmail, string password, string[] attachments = null)
        {
            //mail message
            MailMessage mM = new MailMessage();
            //Mail Address
            mM.From = new MailAddress(from);
            //emailid to send
            foreach (string email in to)
                mM.To.Add(email);
            //your subject line of the message
            mM.Subject = subject;
            //now attached the file
            if (attachments != null)
            {
                foreach (string fn in attachments)
                    mM.Attachments.Add(new Attachment(fn));
            }
            //add the body of the email
            mM.Body = body;
            mM.IsBodyHtml = true;
            //port number to login hotmail server
            SmtpClient sC = new SmtpClient("smtp.live.com");
            sC.Credentials = new System.Net.NetworkCredential(userEmail, password);
            sC.EnableSsl = true;
            //port number for Hot mail
            sC.Port = 25;
            //Send the email
            sC.Send(mM);
        }//end of try block
    }
}
