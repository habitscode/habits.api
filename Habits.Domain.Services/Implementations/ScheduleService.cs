using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Habits.Domain.Models;
using Habits.Domain.Repositories;
using Habits.Domain.Services.Interfaces;

namespace Habits.Domain.Services.Implementations
{
    class ScheduleService : IScheduleService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IHabitRepository _habitRepository;

        public ScheduleService(ITaskRepository taskRepository, IHabitRepository habitRepository) {
            _taskRepository = taskRepository;
            _habitRepository = habitRepository;
        }

        public async Task CreateSchedule(string teamId, string habitId)
        {
            var tasks = await _taskRepository.GetItems(habitId);
            var habit = await _habitRepository.GetItem(teamId, habitId);
            var dates = GetDates(habit.StartDate, habit.EndDate);
            tasks.Sort();
            var scheduledTasks = new List<ScheduledTask>();

            // cyclomatic complexity = tasks * days
            foreach (var dat in dates) {
                foreach(var task in tasks) {
                    if (task.When == dat.DayOfWeek)
                    {
                        scheduledTasks.Add(new ScheduledTask()
                        {
                            HabitId = habitId,
                            TaskId = task.TaskId,
                            What = task.What,
                            Notes = task.Notes,
                            When = dat.Add(task.TimeTable)
                        });
                    }
                }
            }

            await _taskRepository.SaveAsync(scheduledTasks);
        }

        private IEnumerable<DateTime> GetDates(DateTime startDate, DateTime endDate)
        {
            for(var day = startDate; day <= endDate; day = day.AddDays(1)) {
                yield return day;
            }
        }
    }
}
