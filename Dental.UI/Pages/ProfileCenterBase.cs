using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.Pages
{
    public class ProfileCenterBase : ComponentBase
    {
        [Parameter]
        public string UserName { get; set; }
        protected ProfileDialog profileDialog { get; set; }
        public void ProfileDialog_OnDialogClose()
        {
           
            StateHasChanged();
        }

        protected void EditProfile()
        {
            profileDialog.Show();
        }
    }
}
