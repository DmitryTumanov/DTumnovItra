using System.Net;

namespace OnlinerTask.BLL.Services.Search.Request
{
    public interface IRequestCreator
    {
        HttpWebRequest CreateRequest(string requestString);
    }
}
