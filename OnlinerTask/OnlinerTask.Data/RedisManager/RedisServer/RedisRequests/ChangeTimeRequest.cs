namespace OnlinerTask.Data.RedisManager.RedisServer.RedisRequests
{
    public class ChangeTimeRequest : INotifyRequest
    {
        public dynamic Message { get; set; }
    }
}
