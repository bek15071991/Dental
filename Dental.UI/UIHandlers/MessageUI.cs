using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class MessageUI
    {
        private readonly string userName;

        public MessageUI(IMessageDataService messageDataService, IMapper mapper, string userName)
        {
            _messageDataService = messageDataService;
            _mapper = mapper;
            this.userName = userName;
        }

        public IMessageDataService _messageDataService { get; }
        public IMapper _mapper { get; }

        public async Task<List<Message>> GetList()
        {
            return (await _messageDataService.GetMessages()).Where(m => m.UserName == userName).ToList();
        }
        public async Task<MessageVM> Get(int ID)
        {
            Message Message = await _messageDataService.GetMessage(ID);
            return _mapper.Map<MessageVM>(Message);
        }
        public async Task Add(MessageVM messageVM, int ID)
        {
            Message Message = _mapper.Map<Message>(messageVM);
            if (ID == 0)
            {
                Message.CreateDate = DateTime.Now;
                Message.Read = false;
                Message.ReadDate = DateTime.Now;
                Message.UserName = userName;

                await _messageDataService.AddMessage(Message);
            }
            else
            {
                Message mof = await _messageDataService.GetMessage(ID);
                mof.MessageText = Message.MessageText;
                mof.Read = true;
                mof.ReadDate = DateTime.Now;
                // update message
                await _messageDataService.UpdateMessage(mof);
            }
        
        }
        public MessageVM New()
        {
            var MessageVM = new MessageVM
            {
                MessageText = "",
                Direction = "In",
                DoctorName = ""
            };
            return MessageVM;
        }
    }
}
