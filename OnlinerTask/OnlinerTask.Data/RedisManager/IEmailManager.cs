using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlinerTask.Data.RedisManager
{
    public interface IEmailManager
    {
        void Set<T>(T item);
        T Get<T>(int id);
        IEnumerable<T> GetAll<T>();
        void Delete<T>(T item);
        Task SendMail(string username, string productname);
    }
}