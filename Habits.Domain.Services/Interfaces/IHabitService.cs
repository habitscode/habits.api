using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Services
{
    public interface IHabitService : ICrudService<Habit>
    {
        Task<List<Habit>> GetItems(String teamId);
        Task<Habit> GetItem(String teamId, String habitId);
        Task DeleteAsync(String teamId, String habitId);
    }
}
