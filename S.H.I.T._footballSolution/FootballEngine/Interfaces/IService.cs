using System;
using System.Collections.Generic;

namespace FootballEngine.Interfaces
{
    interface IService<T>
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entitis);
        void Delete(Guid id);
        IEnumerable<T> GetAll();
        T GetBy(Guid id);
        T GetBy(string name);

        void Save();
    }
}
