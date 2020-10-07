using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
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
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public MessageVM MessageVM { get; set; }
        [Inject]
        public IMessageDataService MessageDataService { get; set; }
        [Inject]
        public IDoctorDataService DoctorDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public DoctorUI doctorUI { get; set; }
        public MessageUI messageUI { get; set; }
        public void Show()
        { 
            ResetDialog();
            ShowDialog = true;
        }
        private async void ResetDialog()
        {
            if (MessageID == 0)
            {
                MessageVM = messageUI.New();
            }
            else
            {
                MessageVM = await messageUI.Get(MessageID);
            }

            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            doctorUI = new DoctorUI(DoctorDataService);
            Doctors = await doctorUI.GetList();
            messageUI = new MessageUI(MessageDataService, mapper, UserName);
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
                await messageUI.Add(MessageVM, MessageID);
            }

            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
