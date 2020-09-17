using System.Collections.Generic;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public interface IMessageDataService
    {
        Task<IEnumerable<Message>> GetMessages();
        Task<Message> GetMessage(int ID);
        Task<Message> AddMessage(Message credential);
        Task UpdateMessage(Message credential);
        Task DeleteMessage(int MessageId);
    }
}