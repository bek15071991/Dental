using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IBillDataService
    {
        Task<IEnumerable<Bill>> GetBills();
        Task<Bill> GetBill(int ID);
        Task<Bill> AddBill(Bill bill);
        Task UpdateBill(Bill bill);
        Task DeleteBill(int BillId);
    }
}