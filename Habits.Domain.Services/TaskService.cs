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

        public async Task AddAsync(HTask item)
        {
            item.TaskId = Guid.NewGuid().ToString();
            await _taskRepository.AddAsync(item);
        }

        public async Task<HTask> GetItem(String taskId)
        {
            var result = await _taskRepository.GetItem(taskId);
            return result;
        }

        public async Task<List<HTask>> GetItems(String challengeId)
        {
            var result = await _taskRepository.GetItems(challengeId);
            return result;
        }

        public async Task DeleteAsync(HTask item)
        {
            await _taskRepository.DeleteAsync(item);
        }

        Task ICrudService<HTask>.UpdateAsync(HTask item)
        {
            throw new NotImplementedException();
        }
    }
}
