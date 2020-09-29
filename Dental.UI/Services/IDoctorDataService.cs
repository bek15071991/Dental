using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IDoctorDataService
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor> GetDoctor(int ID);
        Task<Doctor> AddDoctor(Doctor doctor);
        Task UpdateDoctor(Doctor doctor);
        Task DeleteDoctor(int DoctorId);
    }
}