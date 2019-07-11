using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Habits.Domain.Models;
using Habits.Domain.Repositories;

namespace Habits.Domain.Services
{
    public class TeamMembersService : ITeamMembersService
    {
        private readonly ITeamMembersRepository _teamMembersRepository;

        public TeamMembersService(ITeamMembersRepository teamMembersRepository)
        {
            _teamMembersRepository = teamMembersRepository;
        }

        public async Task AddAsync(TeamMember item)
        {
            await _teamMembersRepository.AddAsync(item);
        }

        public async Task DeleteAsync(TeamMember item)
        {
            await _teamMembersRepository.DeleteAsync(item);
        }
    }
}
