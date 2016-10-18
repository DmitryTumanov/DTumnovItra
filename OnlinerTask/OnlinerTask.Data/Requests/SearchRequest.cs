namespace OnlinerTask.Data.Requests
{
    public class SearchRequest
    {
        public string SearchString { get; set; }

        public SearchRequest(string searchString)
        {
            SearchString = searchString;
        }
    }
}
