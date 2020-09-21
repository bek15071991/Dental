using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IScheduleDataService
    {
        Task<IEnumerable<Schedule>> GetSchedules();
        Task<Schedule> GetSchedule(int ID);
        Task<Schedule> AddSchedule(Schedule schedule);
        Task UpdateSchedule(Schedule schedule);
        Task DeleteSchedule(int ScheduleId);
    }
}