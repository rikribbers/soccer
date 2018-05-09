using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Entities;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Users
{
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