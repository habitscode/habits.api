using System.Collections.Generic;

namespace Habits.Domain.Models
{
    public class Team
    {
        public string Name { get; set; }
        public List<Challenge> Challenges { get; set; }

        public Team()
        {
        }
    }
}
