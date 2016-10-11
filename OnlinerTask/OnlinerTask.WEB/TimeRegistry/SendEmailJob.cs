using FluentScheduler;
using OnlinerTask.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class SendEmailJob : IJob
    {
        private IRepository repository;

        public SendEmailJob()
        {
            repository = DependencyResolver.Current.GetService<IRepository>();
        }
        public SendEmailJob(IRepository repository)
        {
            this.repository = repository;
        }

        public async void Execute()
        {
            var user_list = repository.GetUsersAndProducts();
            foreach (var item in user_list)
            {
                if (item.Time - item.Time.Date < DateTime.Now - DateTime.Now.Date)
                {
                    await SendMail(item.UserEmail, item.ProductName);
                    repository.DeleteUserAndProduct(item.Id, item.UserEmail);
                }
            }
        }

        private Task SendMail(string username, string productname)
        {
            SmtpClient client = new SmtpClient("smtp.mail.ru", Convert.ToInt32(587));
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("tumanov.97.dima@mail.ru", "102938usugen");
            client.EnableSsl = true;
            var mail = new MailMessage("tumanov.97.dima@mail.ru", username);
            mail.Subject = productname;
            mail.Body = "Dear," + username + ", product " + productname + " has been changed.";
            return client.SendMailAsync(mail);
        }
    }
}