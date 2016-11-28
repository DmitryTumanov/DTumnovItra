namespace OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations
{
    public class AddProductRequest : INotifyRequest
    {
        public dynamic Message { get; set; }

        public string RedirectPath { get; set; }
    }
}
