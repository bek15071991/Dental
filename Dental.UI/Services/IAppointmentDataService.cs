using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IAppointmentDataService
    {
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<Appointment> GetAppointment(int ID);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task UpdateAppointment(Appointment appointment);
        Task DeleteAppointment(int AppointmentId);
    }
}