using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.Data.EntityMappers.Interfaces
{
    public interface IPriceMapper<K, T> : IMapper<K, T>
    {
        K ConvertToModel(T model, int pricemaxid, int priceminid);
    }
}
