using System;
using System.Collections.Generic;
using System.Text;
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

        public void Delete(HTask item)
        {
            throw new NotImplementedException();
        }

        public HTask Get()
        {
            throw new NotImplementedException();
        }

        public List<HTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(HTask item)
        {
            throw new NotImplementedException();
        }
    }
}
