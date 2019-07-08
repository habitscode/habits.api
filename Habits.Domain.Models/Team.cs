using System.Collections.Generic;

namespace Habits.Domain.Models
{
    public class Team
    {
        public string TeamId { get; set; }
        public string Name { get; set; }
        public List<Challenge> Challenges { get; set; }
    }
}
