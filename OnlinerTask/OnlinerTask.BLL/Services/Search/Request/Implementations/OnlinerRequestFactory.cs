using System.Linq;
using System.Net;

namespace OnlinerTask.BLL.Services.Search.Request.Implementations
{
    public class OnlinerRequestFactory : IRequestFactory
    {
        public HttpWebRequest CreateRequest(string endpoint, params string[] arguments)
        {
            var argumentsString = GetArgumentsString(arguments);
            var webRequest = (HttpWebRequest)WebRequest.Create(endpoint + argumentsString);
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }

        private static string GetArgumentsString(params string[] arguments)
        {
            return arguments.Aggregate(string.Empty, (current, element) => current + element);
        }
    }
}
