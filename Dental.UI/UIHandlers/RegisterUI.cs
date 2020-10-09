using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class RegisterUI
    {
        public RegisterUI(IClientDataService clientDataService,
            ICredentialDataService credentialDataService,
            IMapper mapper)
        {
            _clientDataService = clientDataService;
            _credentialDataService = credentialDataService;
            _mapper = mapper;
        }

        public IClientDataService _clientDataService { get; }
        public ICredentialDataService _credentialDataService { get; }
        public IMapper _mapper { get; }
        public RegisterVM New(string userName)
        {
            var registerVM = new RegisterVM();
            registerVM.UserName = userName;
            return registerVM;
        }
        public async Task AddClient(RegisterVM registerVM)
        {
            var client = _mapper.Map<Client>(registerVM);
            await _clientDataService.AddClient(client);
        }
        public async Task<RegisterVM> GetClient(string userName)
        {
            var client = (await _clientDataService.GetClients())
                .FirstOrDefault(c => c.UserName == userName);
            var credentials=(await _credentialDataService.GetCredentials())
                .FirstOrDefault(c => c.UserName == userName);
            var registerVM = _mapper.Map<RegisterVM>(client);
            registerVM.Password = credentials.Password;
            registerVM.Password2 = credentials.Password;
            return registerVM;
        }
        public async Task UpdateClient(RegisterVM registerVM, string userName)
        {
            var client = (await _clientDataService.GetClients())
                            .FirstOrDefault(c => c.UserName == userName);
            client.FirstName = registerVM.FirstName;
            client.LastName = registerVM.LastName;
            client.Street = registerVM.Street;
            client.City = registerVM.City;
            client.State = registerVM.State;
            client.Zipcode = registerVM.Zipcode;
            client.Email = registerVM.Email;
            client.HomePhone = registerVM.HomePhone;
            client.MobilePhone = registerVM.MobilePhone;
            await _clientDataService.UpdateClient(client);

        }
        public async Task AddCredentials(RegisterVM registerVM)
        {
            var credentials = _mapper.Map<Credential>(registerVM);
            await _credentialDataService.AddCredential(credentials);
        }
        public async Task Register(RegisterVM registerVM)
        {
            await AddClient(registerVM);
            await AddCredentials(registerVM);
        }
    }
}
