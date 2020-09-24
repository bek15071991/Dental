using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IProcedureDataService
    {
        Task<IEnumerable<Procedure>> GetProcedures();
        Task<Procedure> GetProcedure(int ID);
        Task<Procedure> AddProcedure(Procedure procedure);
        Task UpdateProcedure(Procedure procedure);
        Task DeleteProcedure(int ProcedureId);
    }
}