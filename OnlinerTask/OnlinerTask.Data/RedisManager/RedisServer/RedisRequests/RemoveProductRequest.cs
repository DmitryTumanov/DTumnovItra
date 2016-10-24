namespace OnlinerTask.Data.RedisManager.RedisServer.RedisRequests
{
    public class RemoveProductRequest : INotifyRequest
    {
        public dynamic Message { get; set; }

        public string RedirectPath { get; set; }
    }
}
