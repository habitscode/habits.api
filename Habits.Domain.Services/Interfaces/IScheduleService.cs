using System.Threading.Tasks;

namespace Habits.Domain.Services.Interfaces
{
    interface IScheduleService
    {
        Task CreateSchedule(string teamId, string habitId);
    }
}
