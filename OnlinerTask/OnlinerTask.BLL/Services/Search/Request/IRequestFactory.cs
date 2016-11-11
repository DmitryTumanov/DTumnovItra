using System.Net;

namespace OnlinerTask.BLL.Services.Search.Request
{
    public interface IRequestFactory
    {
        HttpWebRequest CreateRequest(string endpoint, params string[] arguments);
    }
}
