using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.Data.EntityMappers.Interfaces
{
    public interface IProductMapper<K, T> : IMapper<K, T>
    {
        K ConvertToModel(T model, string useremail, int pricemaxid, int priceminid);
    }
}
