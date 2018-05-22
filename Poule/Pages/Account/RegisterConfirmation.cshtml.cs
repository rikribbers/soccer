using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmation : PageModel
    {
        public void OnGet()
        {

        }
    }
}
