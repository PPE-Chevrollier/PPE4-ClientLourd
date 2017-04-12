using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EASendMail;

namespace ChevLoc
{
    static class Mail
    {
        public static string site = "192.168.152.1";
        public static bool CreateMessage(string to, string sujet, string corps)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();

            // Set sender email address, please change it to yours
            oMail.From = "chevloc2@gmail.com";

            // Set recipient email address, please change it to yours
            oMail.To = to;

            // Set email subject
            oMail.Subject = sujet;

            // Set email body
            oMail.TextBody = corps;

            // Your SMTP server address
            SmtpServer oServer = new SmtpServer("smtp.gmail.com");
            //oServer.AuthType = SmtpAuthType.XOAUTH2;
            // Set 25 or 587 port.
            oServer.Port = 465;
            // detect TLS connection automatically
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            oServer.User = "chevloc2@gmail.com";
            oServer.Password = "Azerty123";

            try
            {
                oSmtp.SendMail(oServer, oMail);
                return true;
            }
            catch (Exception ep)
            {
                return false;
            }
        }
    }
}
