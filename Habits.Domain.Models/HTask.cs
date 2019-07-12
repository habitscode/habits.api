using System;

namespace Habits.Domain.Models
{
    public class HTask
    {
        public string ChallengeId { get; set; }
        public string TaskId { get; set; }
        public string What { get; set; }
        public DateTime When { get; set; }
        public string Where { get; set; }
        public Status Status { get; set; }
        public string Notes { get; set; }

        public HTask() {
        }
    }
}
