using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;
using Habits.Domain.Repositories;

namespace Habits.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository) {
            _taskRepository = taskRepository;
        }

        public async Task<List<HTask>> GetItems(String habitId)
        {
            var result = await _taskRepository.GetItems(habitId);
            return result;
        }

        public async Task<HTask> GetItem(string habitId, string taskId)
        {
            var result = await _taskRepository.GetItem(habitId, taskId);
            return result;
        }

        public async Task AddAsync(HTask item)
        {
            item.TaskId = Guid.NewGuid().ToString();
            await _taskRepository.AddAsync(item);
        }

        public async Task UpdateAsync(HTask item)
        {
            await _taskRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(String habitId, String taskId)
        {
            await _taskRepository.DeleteAsync(habitId, taskId);
        }
    }
}
