using ServiceStack.Messaging;

namespace OnlinerTask.Data.MqConstituents
{
    public interface IMqConstituentsFactory
    {
        void CreateAppHost();
        IMessageQueueClient CreateClient();
    }
}
