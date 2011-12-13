/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/2/2011
 * Time: 3:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Mail;

namespace IExtendFramework.Net.Mail
{
    /// <summary>
    /// Description of YahooMessage.
    /// </summary>
    public class YahooMessage
    {
        private YahooMessage()
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
            //SMTP
            SmtpClient SmtpServer = new SmtpClient();
            //your credential will go here
            SmtpServer.Credentials = new System.Net.NetworkCredential(userEmail, password);
            //port number to login yahoo server
            SmtpServer.Port = 587;
            //yahoo host name
            SmtpServer.Host = "smtp.mail.yahoo.com";
            //Send the email
            SmtpServer.Send(mM);
        }
    }
}
