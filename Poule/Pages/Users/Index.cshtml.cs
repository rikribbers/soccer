using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Entities;
using Poule.Services;

namespace Poule.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserData _UserData;

        public IEnumerable<User> Users { get; set; }

        public IndexModel(IUserData UserData)
        {
            _UserData = UserData;
        }

        public IActionResult OnGet()
        {
            Users = _UserData.GetAll();
            return Page();
        }
    }
}