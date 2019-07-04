using System;
using System.Collections.Generic;
using System.Text;

namespace Habits.Domain.Services
{
    interface ICrudService<T>
    {
        T Get();
        List<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
