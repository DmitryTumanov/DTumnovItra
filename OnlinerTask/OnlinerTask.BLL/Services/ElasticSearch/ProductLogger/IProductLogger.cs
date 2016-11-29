using System.Threading.Tasks;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger
{
    public interface IProductLogger<in T>
    {
        Task LogObject(T productModel);
    }
}
