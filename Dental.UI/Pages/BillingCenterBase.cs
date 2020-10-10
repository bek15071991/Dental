using AutoMapper;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
using Dental.UI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.Pages
{
    public class BillingCenterBase : ComponentBase
    {
        [Inject]
        public IBillDataService billDataService { get; set; }
        [Inject]
        public IClientDataService clientDataService { get; set; }
        [Inject]
        public ICredentialDataService credentialDataService { get; set; }
        [Inject]
        public IAppointmentDataService appointmentDataService { get; set; }
        [Inject]
        public IProcedureDataService procedureDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public BillVM billVM { get; set; }
        public AppointmentUI appointmentUI { get; set; }
        public RegisterUI registerUI { get; set; }
        public BillUI billUI { get; set; }
        public ProcedureUI procedureUI { get; set; }
        public bool DisplayBills { get; set; } = false;
        public List<BillVM> billVMs { get; set; } = new List<BillVM>();
        public EditContext editContext { get; set; }
        protected override async Task OnInitializedAsync()
        {
            billUI = new BillUI(billDataService, mapper, "");
            registerUI = new RegisterUI(clientDataService, credentialDataService, mapper);
            appointmentUI = new AppointmentUI(appointmentDataService, mapper, "");
            procedureUI = new ProcedureUI(procedureDataService, mapper);
            //await PostToAppointments();
        }
        public async Task PostToAppointments()
        {
            var appts = await appointmentUI.GetPage();
            if (appts.Count>0)
            {
                DisplayBills = true;
                billVMs = await billUI.GetListVM(appts, registerUI);
                StateHasChanged();
            }
        }
        public async Task SubmitHandler()
        {
            DisplayBills = false;
            await billUI.PostProcedures(billVMs, procedureUI);
            StateHasChanged();
        }
    }
}
