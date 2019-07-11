using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Services
{
    public interface ITaskService : ICrudService<HTask>
    {
        Task<List<HTask>> GetItems(String challengeId);
        Task<HTask> GetItem(String challengeId, String taskId);
        Task DeleteAsync(String challengeId, String taskId);
    }
}
