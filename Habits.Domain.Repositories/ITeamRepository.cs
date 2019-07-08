using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public interface ITeamRepository : ICrudRepository<Team>
    {
        Task<List<Team>> GetAll();
        Task<Team> GetItem(String teamId);
        Task DeleteAsync(String teamId);
    }
}
