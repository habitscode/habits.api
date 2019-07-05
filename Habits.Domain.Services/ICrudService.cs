using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Domain.Services
{
    public interface ICrudService<T>
    {
        T Get();
        List<T> GetAll();
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
    }
}
