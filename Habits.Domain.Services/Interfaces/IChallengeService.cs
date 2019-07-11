using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Services
{
    public interface IChallengeService : ICrudService<Challenge>
    {
        Task<List<Challenge>> GetItems(String teamId);
        Task<Challenge> GetItem(String teamId, String challengeId);
        Task DeleteAsync(String teamId, String challengeId);
    }
}
