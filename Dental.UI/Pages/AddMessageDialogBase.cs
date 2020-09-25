using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class AddMessageDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }

        [Parameter] 
        public int MessageID { get; set; }

        public Message Message { get; set; }
        public MessageVM MessageVM { get; set; }
        [Inject]
        public IMessageDataService MessageDataService { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public void Show()
        { 
            ResetDialog();
            ShowDialog = true;
        }
        private async void ResetDialog()
        {
            if (MessageID == 0)
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
            else
            {
                Message = await MessageDataService.GetMessage(MessageID);
            }

            MessageVM = new MessageVM
            {
                MessageText = Message.MessageText,
                Direction = Message.Direction
            };
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            ResetDialog();
        }

        public void Close()
        {
            ShowDialog = false;
        }

        protected async Task HandleValidSubmit()
        {
            if (MessageID == 0)
            {
                Message.UserName = UserName;
                Message.MessageText = MessageVM.MessageText;
                Message.Direction = MessageVM.Direction;
                await MessageDataService.AddMessage(Message);
            }

            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
