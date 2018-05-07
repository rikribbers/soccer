using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private IUserData _UserData;

        public UserEditModel User { get; set; }

        public DetailsModel(IUserData UserData)
        {
            _UserData = UserData;
        }

        public IActionResult OnGet(int id)
        {
            User = _UserData.ToEditModel(_UserData.Get(id));
            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}