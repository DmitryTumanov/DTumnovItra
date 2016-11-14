using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OnlinerTask.BLL.Services.Search.Request.Implementations
{
    public class OnlinerRequestFactory : IRequestFactory
    {
        public HttpWebRequest CreateRequest(string endpoint, IDictionary<string, object> parametersQuery)
        {
            if (endpoint == null)
            {
                return null;
            }
            var argumentsString = GetArgumentsString(parametersQuery);
            var webRequest = (HttpWebRequest) WebRequest.Create($"{endpoint}?{argumentsString}");
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }

        private static string GetArgumentsString(IDictionary<string, object> parametersQuery)
        {
            if (parametersQuery == null)
            {
                return string.Empty;
            }
            return string.Join("&", parametersQuery.Select(k => $"{k.Key}={k.Value}"));
        }
    }
}
