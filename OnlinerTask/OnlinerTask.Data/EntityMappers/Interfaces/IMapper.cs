using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.Data.EntityMappers.Interfaces
{
    public interface IMapper<K,T>
    {
        T ConvertToModel(K dbmodel);
    }
}
