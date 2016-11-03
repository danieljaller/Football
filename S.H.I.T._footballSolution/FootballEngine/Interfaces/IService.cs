using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Interfaces
{
    interface IService<T>
    {
        void Add(T entity);
        void Delete(Guid id);
        IEnumerable<T> GetAll();
        T GetBy(Guid id);
        T GetBy(string name);
    }
}
