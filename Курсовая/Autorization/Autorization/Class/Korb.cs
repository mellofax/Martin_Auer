using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Autorization.Class
{
    class Korb
    {
        public static double sum = 0;
        public static List<Zakaz> list = new List<Zakaz>();
        public static void SendMessage(MailAddress fromMailAddress, MailAddress toAddress, MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("barmansuperman4@gmail.com", "nikita22");
            smtpClient.Send(mailMessage);
        }
    }
}
