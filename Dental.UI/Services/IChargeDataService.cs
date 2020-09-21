using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IChargeDataService
    {
        Task<IEnumerable<Charge>> GetCharges();
        Task<Charge> GetCharge(int ID);
        Task<Charge> AddCharge(Charge charge);
        Task UpdateCharge(Charge charge);
        Task DeleteCharge(int ChargeId);
    }
}