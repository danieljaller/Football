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
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Load();
        void Save();
    }
}
