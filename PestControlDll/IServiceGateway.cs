using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestControlDll
{
    public interface IServiceGateway<T>
    {
        /// <summary>
        /// Creates a new object of provided type in API
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        T Post(T t);
        /// <summary>
        /// Gets one object from api who has the exact id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Gets a list of objects
        /// </summary>
        /// <returns></returns>
        List<T> Get();
        /// <summary>
        /// Updates provided object
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        T Put(T t);
        /// <summary>
        /// Deletes provided object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
