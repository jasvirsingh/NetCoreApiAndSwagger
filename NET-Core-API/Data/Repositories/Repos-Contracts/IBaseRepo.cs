using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_Core_API.Data.Repositories.Repos_Contracts
{
    public interface IBaseRepo<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(int id);

        Task<int> Save(T item);

        Task<int> Delete(T item);
    }
}
