using System.Net;

namespace OnlinerTask.BLL.Services.Search.Request
{
    public interface IRequestFactory
    {
        HttpWebRequest CreateRequest(string requestString, int page = 1);
    }
}
