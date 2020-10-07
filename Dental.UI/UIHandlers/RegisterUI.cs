using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
