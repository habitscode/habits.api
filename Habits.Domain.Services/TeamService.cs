using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;
using Habits.Domain.Repositories;

namespace Habits.Domain.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }


        public async Task AddAsync(Team item)
        {
            item.TeamId = Guid.NewGuid().ToString();
            await _teamRepository.AddAsync(item);
        }

        public Task DeleteAsync(Team item)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> GetItem(String teamId)
        {
            var result = await _teamRepository.GetItem(teamId);
            return result;
        }

        public async Task<List<Team>> GetItems(String teamId)
        {
            var result = await _teamRepository.GetItems(teamId);
            return result;
        }

        public Task UpdateAsync(Team item)
        {
            throw new NotImplementedException();
        }
    }
}
