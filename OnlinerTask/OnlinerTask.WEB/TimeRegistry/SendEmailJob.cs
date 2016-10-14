using FluentScheduler;
using OnlinerTask.Data.Repository.Interfaces;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class SendEmailJob : IJob
    {
        private ITimeServiceRepository repository;

        public SendEmailJob()
        {
            repository = DependencyResolver.Current.GetService<ITimeServiceRepository>();
        }
        public SendEmailJob(ITimeServiceRepository repository)
        {
            this.repository = repository;
        }

        public async void Execute()
        {
            var user_list = repository.GetUsersEmails();
            var date = DateTime.Now.TimeOfDay;
            foreach (var item in user_list)
            {
                if (item.Time < date)
                {
                    await SendMail(item.UserEmail, item.ProductName);
                    repository.DeleteUserAndProduct(item.Id, item.UserEmail);
                }
            }
        }

        private Task SendMail(string username, string productname)
        {
            var client = CreateClient();
            var mail = CreateMail(username, productname);
            return client.SendMailAsync(mail);
        }

        private SmtpClient CreateClient()
        {
            SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("tumanov.97.dima@mail.ru", "102938usugen");
            client.EnableSsl = true;
            return client;
        }

        private MailMessage CreateMail(string username, string productname)
        {
            var mail = new MailMessage("tumanov.97.dima@mail.ru", username);
            mail.Subject = productname;
            mail.Body = string.Format("Dear, {0}, product {1} has been changed.", username, productname);
            return mail;
        }
    }
}