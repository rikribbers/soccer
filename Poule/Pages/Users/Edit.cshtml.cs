using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Users
{
    [Authorize(Roles = "PouleAdministrators")]
    public class EditModel : PageModel
    {
        private IUserData _UserData;
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public UserEditModel MyUser { get; set; }

        public EditModel(IUserData UserData)
        {
            _UserData = UserData;
        }
        
        public IActionResult OnGet(int id)
        {
            MyUser = _UserData.ToEditModel(_UserData.Get(id));
            Id = id;
            if (User == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _UserData.Update(_UserData.ToEntity(MyUser, Id));
                return RedirectToAction("Details", "Users", new {id = Id});
            }
            return Page();
        }
    }
}