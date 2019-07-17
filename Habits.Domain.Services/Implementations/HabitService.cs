using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;
using Habits.Domain.Repositories;

namespace Habits.Domain.Services
{
    public class HabitService : IHabitService
    {
        private readonly IHabitRepository _habitRepository;

        public HabitService(IHabitRepository habitRepository)
        {
            _habitRepository = habitRepository;
        }

        public async Task<List<Habit>> GetItems(String teamId)
        {
            var result = await _habitRepository.GetItems(teamId);
            return result;
        }

        public async Task<Habit> GetItem(string teamId, string habitId)
        {
            var result = await _habitRepository.GetItem(teamId, habitId);
            return result;
        }

        public async Task AddAsync(Habit item)
        {
            item.HabitId = Guid.NewGuid().ToString();
            if (item.Notes == null) item.Notes = string.Empty;
            await _habitRepository.AddAsync(item);
        }

        public async Task UpdateAsync(Habit item)
        {
            await _habitRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(string teamId, string habitId)
        {
            await _habitRepository.DeleteAsync(teamId, habitId);
        }
    }
}
