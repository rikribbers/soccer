using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Users
{
    public class CreateModel : PageModel
    {
        private IUserData _UserData;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public UserEditModel MyUser { get; set; }

        public CreateModel(IUserData UserData)
        {
            _UserData = UserData;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var u =_UserData.Add(_UserData.ToEntity(MyUser));
                return RedirectToAction("Details", "Users", new {id = u.Id});
            }
            return Page();
        }
    }
}