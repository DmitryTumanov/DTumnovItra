using OnlinerTask.Data.MqConstituents;
using ServiceStack.Messaging;

namespace OnlinerTask.BLL.Services.Logger.Implementations
{
    public class Logger : ILogger
    {
        private readonly IMessageQueueClient queueClient;

        public Logger(IMqConstituentsFactory constituentsFactory)
        {
            queueClient = constituentsFactory.CreateClient();
        }

        public void LogObject(object logEntity)
        {
            queueClient.Publish(logEntity);
        }
    }
}
