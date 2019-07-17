using System;

namespace Habits.Domain.Models
{
    public class ScheduledTask
    {
        public string HabitId { get; set; }
        public string TaskId { get; set; }
        public string What { get; set; }
        public DateTime When { get; set; }
        public string Notes { get; set; }
    }
}