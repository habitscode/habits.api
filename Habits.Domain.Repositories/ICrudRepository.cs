using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Domain.Repositories
{
    public interface ICrudRepository<T>
    {
        Task AddAsync(T item);
        Task UpdateAsync(T item);
    }
}
