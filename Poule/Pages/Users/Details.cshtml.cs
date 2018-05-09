using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private IUserData _UserData;

        public UserEditModel MyUser { get; set; }

        public DetailsModel(IUserData UserData)
        {
            _UserData = UserData;
        }

        public IActionResult OnGet(int id)
        {
            MyUser = _UserData.ToEditModel(_UserData.Get(id));
            if (MyUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}