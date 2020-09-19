using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IPaySetupDataService
    {
        Task<IEnumerable<PaySetup>> GetPaySetups();
        Task<PaySetup> GetPaySetup(int ID);
        Task<PaySetup> AddPaySetup(PaySetup paysetup);
        Task UpdatePaySetup(PaySetup paysetup);
        Task DeletePaySetup(int PaySetupId);
    }
}