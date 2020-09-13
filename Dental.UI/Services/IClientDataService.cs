using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IClientDataService
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client> GetClient(int ID);
        Task<Client> AddClient(Client account);
        Task UpdateClient(Client account);
        Task DeleteClient(int ClientId);
    }
}