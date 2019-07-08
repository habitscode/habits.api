using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Domain.Repositories
{
    public interface ICrudRepository<T>
    {
        Task AddAsync(T item);
        Task DeleteAsync(T item);
        Task<T> GetItem(String id);
        Task<List<T>> GetItems(String id);
        Task UpdateAsync(T item);
    }
}
