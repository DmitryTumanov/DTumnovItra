using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace OnlinerTask.WEB.Module
{
    public class TimerModule : IHttpModule
    {
        static Timer timer;
        static object locker = new object();

        public void Init(HttpApplication context)
        {
            timer = new Timer(new TimerCallback(SendEmail), null, 0, 30000);
        }

        private void SendEmail(object o)
        {
            lock (locker)
            {
                var from = "tumanov.97.dima@mail.ru";
                var password = "102938usugen";
                SmtpClient client = new SmtpClient("smtp.mail.ru", Convert.ToInt32(587));
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(from, password);
                client.EnableSsl = true;
                var mail = new MailMessage(from, "tumanov.97.dima@gmail.com");
                mail.Subject = "TEST";
                mail.Body = "TESTBODY";
                client.Send(mail);
            }
        }

        public void Dispose() { }
    }
}