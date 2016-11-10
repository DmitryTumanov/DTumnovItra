using System.Net;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.BLL.Services.Search.Request.Implementations
{
    public class OnlinerRequestFactory : IRequestFactory
    {
        public HttpWebRequest CreateRequest(string requestString, int page = 1)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(Configurations.OnlinerApiPath + requestString + 
                Configurations.OnlinerPageVariable + page);
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }
    }
}
