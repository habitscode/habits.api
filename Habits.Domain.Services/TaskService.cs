using System;
using System.Collections.Generic;
using System.Text;
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

        public void Add(HTask item)
        {
            _taskRepository.AddAsync(item);
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
