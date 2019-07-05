using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Domain.Repositories
{
    public interface ICrudRepository<T>
    {
        T Get(int id);
        Task AddAsync(T item);
        void Delete(T item);
        void Update(T item);
    }
}
