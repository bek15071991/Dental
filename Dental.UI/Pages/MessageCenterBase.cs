using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class MessageCenterBase : ComponentBase
    {
        [Parameter] public string UserName { get; set; }
        public int MessageCount { get; set; } = 0;
        public List<Message> messages { get; set; } = null;
        public bool DisplayMessages { get; set; } = false;
        public string buttonDisplay { get; set; } = "";
        public int messageID { get; set; }
        protected AddMessageDialog addMessageDialog { get; set; }

        [Inject]
        public IMessageDataService MessageDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public MessageUI messageUI { get; set; }
        protected override async Task OnInitializedAsync()
        {
            messageUI = new MessageUI(MessageDataService, mapper, UserName);
            messages = await messageUI.GetList();
            MessageCount = messages.Count();
        }

        public void DisplayReportHandler()
        {
            DisplayMessages = true;
            buttonDisplay = "btn-hide";
        }
        public void CloseMessageHandler()
        {
            DisplayMessages = false;
            buttonDisplay = "";
        }
        public async void AddMessageDialog_OnDialogClose()
        {
            messages = await messageUI.GetList();
            MessageCount = messages.Count();
            StateHasChanged();
        }

        protected void QuickAddMessage(int id)
        {
            messageID = id;
            InvokeAsync(StateHasChanged);
            addMessageDialog.Show(messageID);
        }
    }
}
