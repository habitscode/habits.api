using System.Threading.Tasks;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public interface ITeamMembersRepository
    {
        Task AddAsync(TeamMember item);
        Task DeleteAsync(TeamMember item);
    }
}
