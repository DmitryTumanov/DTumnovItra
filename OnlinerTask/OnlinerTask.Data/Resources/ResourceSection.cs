using System.Configuration;

namespace OnlinerTask.Data.Resources
{
    public class ResourceSection: ConfigurationSection
    {
        public static string OnlinerApiPath => ConfigurationManager.AppSettings["OnlinerApiPath"];

        public static string RedisAppHost => ConfigurationManager.AppSettings["RedisAppHost"];

        public static string RedisClient => ConfigurationManager.AppSettings["RedisClient"];

        public static string RedisRoute => ConfigurationManager.AppSettings["RedisRoute"];

        public static string RedisFullRoute => ConfigurationManager.AppSettings["RedisFullRoute"];

        public static string RedisServerName => ConfigurationManager.AppSettings["RedisServerName"];

        public static string EmailSmtp => ConfigurationManager.AppSettings["EmailSMTP"];

        public static string EmailName => ConfigurationManager.AppSettings["EmailName"];

        public static string EmailPassword => ConfigurationManager.AppSettings["EmailPassword"];
    }
}
