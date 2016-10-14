using FluentScheduler;
using OnlinerTask.Data.Repository.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class SendEmailJob : IJob
    {
        private readonly ITimeServiceRepository repository;

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
            var userList = repository.GetUsersEmails();
            var date = DateTime.Now.TimeOfDay;
            foreach (var item in userList)
            {
                if (item.Time >= date) continue;
                await SendMail(item.UserEmail, item.ProductName);
                repository.DeleteUserAndProduct(item.Id, item.UserEmail);
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
            var client = new SmtpClient("smtp.mail.ru", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("tumanov.97.dima@mail.ru", "102938usugen"),
                EnableSsl = true
            };
            return client;
        }

        private MailMessage CreateMail(string username, string productname)
        {
            var mail = new MailMessage("tumanov.97.dima@mail.ru", username)
            {
                Subject = productname,
                Body = $"Dear, {username}, product {productname} has been changed."
            };
            return mail;
        }
    }
}