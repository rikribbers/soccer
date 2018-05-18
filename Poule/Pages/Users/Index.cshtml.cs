using System.Collections.Generic;
using Poule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Entities;

namespace Poule.Pages.Users
{
    [Authorize]
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