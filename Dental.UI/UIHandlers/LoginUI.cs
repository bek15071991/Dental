using AutoMapper;
using Dental.UI.Services;
using Dental.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class LoginUI
    {
        public LoginUI(ICredentialDataService credentialDataService, IMapper mapper)
        {
            _credentialDataService = credentialDataService;
            _mapper = mapper;
        }

        public ICredentialDataService _credentialDataService { get; }
        public IMapper _mapper { get; }
        public async Task<bool> Login(LoginVM loginVM)
        {
            var credFound = (await _credentialDataService.GetCredentials())
                .FirstOrDefault(c => c.UserName == loginVM.UserName && c.Password == loginVM.Password);
            if (credFound==null)
            {
                return false;
            }
            return true;
        }
        public async Task Logout()
        {

        }
        public LoginVM New()
        {
            var loginVM = new LoginVM
            {
                UserName = "",
                Password = ""
            };
            return loginVM;
        }
    }
}
