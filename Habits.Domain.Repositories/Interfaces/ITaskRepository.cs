using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public interface ITaskRepository : ICrudRepository<HTask>
    {
        Task<List<HTask>> GetItems(String habitId);
        Task<HTask> GetItem(String habitId, String taskId);
        Task DeleteAsync(String habitId, String taskId);
    }
}
