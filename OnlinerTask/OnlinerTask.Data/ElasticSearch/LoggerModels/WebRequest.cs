using System;

namespace OnlinerTask.Data.ElasticSearch.LoggerModels
{
    public class WebRequest
    {
        public string RequestUrl { get; set; }
        public string HttpMethod { get; set; }
        public DateTime RequestTime { get; set; }
        public string UserName { get; set; }

        public WebRequest(string httpMethod, string requestUrl, DateTime requestTime, string userName)
        {
            RequestUrl = requestUrl;
            HttpMethod = httpMethod;
            RequestTime = requestTime;
            UserName = userName;
        }
    }
}
