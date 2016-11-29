using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll
{
    public interface IServiceGateway<T>
    {
        T Put(T t);
        T Get(int id);
        List<T> Get();
        T Post(T t);
        bool Delete(int id);
    }
}
