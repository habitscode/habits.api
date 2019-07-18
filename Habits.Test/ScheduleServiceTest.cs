using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Habits.Domain.Repositories;
using Habits.Domain.Services.Implementations;
using Xunit;

namespace Habits.Test
{
    public class ScheduleServiceTest
    {
        [Fact]
        public async void CreateScheduleTest() {
            var taskRepository = new TaskRepository();
            var habitRepository = new HabitRepository();
            var service = new ScheduleService(taskRepository, habitRepository);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            await service.CreateSchedule("eda1df41-a346-48dc-81ea-1f3288fcd58a", "d6a79a07-8f98-40fd-b1c2-d6269b3d6918");

            Assert.True(true);
        }
    }
}
