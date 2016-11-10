namespace OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations
{
    public class RemoveProductRequest : INotifyRequest
    {
        public dynamic Message { get; set; }

        public string RedirectPath { get; set; }
    }
}
