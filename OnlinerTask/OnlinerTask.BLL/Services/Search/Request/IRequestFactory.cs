using System.Collections.Generic;
using System.Net;

namespace OnlinerTask.BLL.Services.Search.Request
{
    public interface IRequestFactory
    {
        HttpWebRequest CreateRequest(string endpoint, IDictionary<string, string> parametersQuery);
    }
}
