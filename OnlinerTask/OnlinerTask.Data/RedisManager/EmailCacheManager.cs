using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using OnlinerTask.Data.Resources;
using ServiceStack.Redis;

namespace OnlinerTask.Data.RedisManager
{
    public class EmailCacheManager: IEmailManager
    {
        private readonly IRedisClient redisClient;

        public EmailCacheManager(IRedisClient redisClient)
        {
            this.redisClient = redisClient;
        }

        public void Set<T>(T item)
        {
            redisClient.As<T>().Store(item);
        }

        public T Get<T>(int id)
        {
            return redisClient.As<T>().GetById(id);
        }

        public void Delete<T>(T item)
        {
            redisClient.As<T>().Delete(item);
        }

        public Task SendMail(string username, string productname)
        {
            var client = CreateClient();
            var mail = CreateMail(username, productname);
            return client.SendMailAsync(mail);
        }

        private SmtpClient CreateClient()
        {
            var client = new SmtpClient(ResourceSection.EmailSmtp, 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(ResourceSection.EmailName, ResourceSection.EmailPassword),
                EnableSsl = true
            };
            return client;
        }

        private MailMessage CreateMail(string username, string productname)
        {
            var mail = new MailMessage(ResourceSection.EmailName, username)
            {
                Subject = productname,
                Body = $"Dear, {username}, product {productname} has been changed."
            };
            return mail;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return redisClient.As<T>().GetAll();
        }
    }
}
