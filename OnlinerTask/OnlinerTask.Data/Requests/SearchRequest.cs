namespace OnlinerTask.Data.Requests
{
    public class SearchRequest
    {
        public string SearchString { get; set; }
        public int PageNumber { get; set; }

        public SearchRequest(string searchString, int pageNumber = 1)
        {
            SearchString = searchString;
            PageNumber = pageNumber;
        }
    }
}
