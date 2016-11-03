using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(Guid id);
        IEnumerable<T> GetAll();
        T GetBy(Guid id);
        T GetBy(string name);
        void Load();
        void Save();
    }
}
