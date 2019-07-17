using System;

namespace Habits.Domain.Models
{
    public class HTask : IComparable<HTask>
    {
        public string HabitId { get; set; }
        public string TaskId { get; set; }
        public string What { get; set; }
        public DayOfWeek When { get; set; }
        public TimeSpan TimeTable { get; set; }
        public string Notes { get; set; }

        public int CompareTo(HTask other)
        {
            if (other == null) {
                return 1;
            }

            if (this.When == other.When) {
                return this.TimeTable.CompareTo(other.TimeTable);
            }

            return this.When.CompareTo(other);
        }
    }
}
