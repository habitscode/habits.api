using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Services
{
    public interface ITeamMembersService
    {
        Task AddAsync(TeamMember item);
        Task DeleteAsync(TeamMember item);
    }
}
