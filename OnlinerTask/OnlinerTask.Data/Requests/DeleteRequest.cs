namespace OnlinerTask.Data.Requests
{
    public class DeleteRequest
    {
        public DeleteRequest() { }

        public DeleteRequest(int itemId)
        {
            ItemId = itemId;
        }

        public int ItemId { get; set; }
    }
}
