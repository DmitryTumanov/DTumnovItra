using System.Collections.Generic;
using System.Net;

namespace OnlinerTask.BLL.Services.Search.Request.Implementations
{
    public class OnlinerRequestFactory : IRequestFactory
    {
        public HttpWebRequest CreateRequest(string endpoint, IDictionary<string, string> parametersQuery)
        {
            if (endpoint == null)
            {
                return null;
            }
            var argumentsString = GetArgumentsString(parametersQuery);
            var webRequest = (HttpWebRequest)WebRequest.Create(endpoint + argumentsString);
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }

        private static string GetArgumentsString(IDictionary<string, string> parametersQuery)
        {
            if (parametersQuery == null)
            {
                return string.Empty;
            }
            return parametersQuery["searchString"] + parametersQuery["pageVariable"] + parametersQuery["pageNumber"];
        }
    }
}
