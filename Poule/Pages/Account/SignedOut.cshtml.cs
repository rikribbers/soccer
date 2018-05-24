using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Account
{
    public class SignedOutModel : PageModel
    {
        [AllowAnonymous]
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}