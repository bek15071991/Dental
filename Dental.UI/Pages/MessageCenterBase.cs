using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
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
        protected AddMessageDialog addMessageDialog { get; set; }

        [Inject]
        public IMessageDataService MessageDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            messages = (await MessageDataService.GetMessages()).Where(m => m.UserName == UserName).ToList();
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
            messages = (await MessageDataService.GetMessages()).Where(m => m.UserName == UserName).ToList();
            StateHasChanged();
        }

        protected void QuickAddMessage()
        {
            addMessageDialog.Show();
        }
    }
}
