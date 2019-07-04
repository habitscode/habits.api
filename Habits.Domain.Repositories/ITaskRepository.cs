using System;
using System.Collections.Generic;
using System.Text;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    interface ITaskRepository : ICrudRepository<HTask>
    {
    }
}
