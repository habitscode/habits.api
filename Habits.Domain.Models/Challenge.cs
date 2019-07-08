using System;
using System.Collections.Generic;

namespace Habits.Domain.Models
{
    public class Challenge
    {
        public string TeamId { get; set; }
        public string ChallengeId { get; set; }
        public string Name { get; set; }
        public List<HTask> Tasks { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Challenge()
        {
        }
    }
}
