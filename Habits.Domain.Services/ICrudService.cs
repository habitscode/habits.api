using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Domain.Services
{
    public interface ICrudService<T>
    {
        Task<T> GetItem(String id);
        Task<List<T>> GetItems(String id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
