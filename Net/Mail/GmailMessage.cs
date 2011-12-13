/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/2/2011
 * Time: 3:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Mail;

namespace IExtendFramework.Net.Mail
{
    /// <summary>
    /// Description of GmailMessage.
    /// </summary>
    public class GmailMessage
    {
        private GmailMessage()
        {
        }
        
        public static void Send(string[] to, string from, string subject, string body, string userEmail, string password, string[] attachments = null)
        {
            //Mail Message
            MailMessage mM = new MailMessage();
            //Mail Address
            mM.From = new MailAddress(from);
            //receiver email id
            foreach (string to_ in to)
                mM.To.Add(to_);
            //subject of the email
            mM.Subject = subject;
            //deciding for the attachment
            if (attachments != null)
            {
                foreach (string filename in attachments)
                    mM.Attachments.Add(new Attachment(filename));
            }
            //add the body of the email
            mM.Body = body;
            mM.IsBodyHtml = true;
            //SMTP client
            SmtpClient sC = new SmtpClient("smtp.gmail.com");
            //port number for Gmail mail
            sC.Port = 587;
            //credentials to login in to Gmail account
            sC.Credentials = new NetworkCredential(userEmail, password);
            //enabled SSL
            sC.EnableSsl = true;
            //Send an email
            sC.Send(mM);
        }//end of Email Method
    }
}
