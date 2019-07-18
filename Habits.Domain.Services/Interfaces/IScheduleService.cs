using System.Threading.Tasks;

namespace Habits.Domain.Services.Interfaces
{
    public interface IScheduleService
    {
        Task CreateSchedule(string teamId, string habitId);
    }
}
