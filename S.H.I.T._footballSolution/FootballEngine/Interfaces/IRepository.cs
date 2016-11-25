using System;
using System.Collections.Generic;

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
