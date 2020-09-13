using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface ICredentialDataService
    {
        Task<IEnumerable<Credential>> GetCredentials();
        Task<Credential> GetCredential(int ID);
        Task<Credential> AddCredential(Credential credential);
        Task UpdateCredential(Credential credential);
        Task DeleteCredential(int CredentialId);
    }
}