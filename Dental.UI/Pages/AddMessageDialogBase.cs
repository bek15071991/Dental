using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class AddMessageDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }
        public Message Message { get; set; } = new Message
        {
            MessageText = "",
            Direction = "In",
            CreateDate = DateTime.Now,
            Read = false,
            ReadDate = new DateTime(),
            UserName=""
        };
        [Inject]
        public IMessageDataService MessageDataService { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
        }

        private void ResetDialog()
        {
            Message = new Message
            {
                MessageText = "",
                Direction = "In",
                CreateDate = DateTime.Now,
                Read = false,
                ReadDate = new DateTime(),
                UserName = UserName
            };
        }

        public void Close()
        {
            ShowDialog = false;
        }

        protected async Task HandleValidSubmit()
        {
            Message.UserName = UserName;
            await MessageDataService.AddMessage(Message);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
