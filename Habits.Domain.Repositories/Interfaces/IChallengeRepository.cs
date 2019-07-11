using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public interface IChallengeRepository : ICrudRepository<Challenge>
    {
        Task<List<Challenge>> GetItems(String teamId);
        Task<Challenge> GetItem(String teamId, String challengeId);
        Task DeleteAsync(String teamId, String challengeId);
    }
}
