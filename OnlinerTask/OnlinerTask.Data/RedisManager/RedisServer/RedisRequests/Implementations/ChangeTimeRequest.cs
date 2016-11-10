namespace OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations
{
    public class ChangeTimeRequest : INotifyRequest
    {
        public dynamic Message { get; set; }
    }
}
