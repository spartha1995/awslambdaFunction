using AWSLambda1.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;

namespace AWSLambda1.Util
{
    class MailOperator
    {
        public void sendMail(EmailSettingsAC emailsettingsAc,List<string>operatorlist)
        {
            try
            {
                #region variables

                //From Address 
                string FromAddress = emailsettingsAc.From;

                //To Address 
                List<string> ToAddress = new List<string>() { "kinjal@promactinfo.com" , "rajdeep@promactinfo.com", "hetulpatel@promactinfo.com" };

                string Subject = emailsettingsAc.Subject;
                string BodyContent = "Test";

                //Smtp Server 
                string SmtpServer = emailsettingsAc.SMTPHost;

                //Smtp Port Number 
                int SmtpPortNumber = Convert.ToInt32(emailsettingsAc.SMTPPortNo);

                #endregion

                #region Mail Template

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAddress));
                foreach (var emailAddress in ToAddress)
                {
                    mimeMessage.To.Add(new MailboxAddress(emailAddress));
                }

                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent

                };

                #endregion

                #region Http client authentication and send mail function

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    // Note: only needed if the SMTP server requires authentication 
                    // Error 5.5.1 Authentication  
                    client.Authenticate(emailsettingsAc.SMTPUserName, emailsettingsAc.SMTPPassword);
                    client.Send(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully");
                    client.Disconnect(true);

                }

                #endregion

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
