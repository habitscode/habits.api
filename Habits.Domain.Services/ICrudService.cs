using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Domain.Services
{
    public interface ICrudService<T>
    {
        Task AddAsync(T item);
        Task UpdateAsync(T item);
    }
}
