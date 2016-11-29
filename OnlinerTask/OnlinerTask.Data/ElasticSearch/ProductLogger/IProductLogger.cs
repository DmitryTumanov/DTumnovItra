using System.Threading.Tasks;

namespace OnlinerTask.Data.ElasticSearch.ProductLogger
{
    public interface IProductLogger<in T>
    {
        Task LogObject(T productModel);
    }
}
