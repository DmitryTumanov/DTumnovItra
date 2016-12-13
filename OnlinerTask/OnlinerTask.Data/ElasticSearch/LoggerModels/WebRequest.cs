namespace OnlinerTask.Data.ElasticSearch.LoggerModels
{
    public class WebRequest
    {
        public string RequestUrl { get; set; }
        public string HttpMethod { get; set; }

        public WebRequest(string httpMethod, string requestUrl)
        {
            RequestUrl = requestUrl;
            HttpMethod = httpMethod;
        }
    }
}
