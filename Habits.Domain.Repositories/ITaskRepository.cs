using System;
using System.Collections.Generic;
using System.Text;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public interface ITaskRepository : ICrudRepository<HTask>
    {
    }
}
