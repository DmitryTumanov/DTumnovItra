using System.Configuration;

namespace OnlinerTask.Data.Resources
{
    public class Configurations : ConfigurationSection
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

        public static string NotifyTechnology
        {
            get { return ConfigurationManager.AppSettings["NotifyTechnology"]; }
            set
            {
                ConfigurationManager.AppSettings["NotifyTechnology"] = value == SignalRTechnology ? SignalRTechnology : NetMqTechnology;
            }
        }

        public static string SignalRTechnology => ConfigurationManager.AppSettings["SignalRTechnology"];

        public static string NetMqTechnology => ConfigurationManager.AppSettings["NetMqTechnology"];

        public static string TcpConnectionPath => ConfigurationManager.AppSettings["TcpConnectionPath"];

        public static string WebSocketConnectionPath => ConfigurationManager.AppSettings["WebSocketConnectionPath"];

        public static string SearchAddChatType => ConfigurationManager.AppSettings["SearchAddChatType"];

        public static string SearchRemoveChatType => ConfigurationManager.AppSettings["SearchRemoveChatType"];

        public static string AddChatType => ConfigurationManager.AppSettings["AddChatType"];

        public static string RemoveChatType => ConfigurationManager.AppSettings["RemoveChatType"];

        public static string InfoChatType => ConfigurationManager.AppSettings["InfoChatType"];
    }
}
