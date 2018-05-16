using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Data;

namespace Poule.Pages
{
    public class BasePageModel : PageModel
    {
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<ApplicationUser> UserManager { get; }
        protected ApplicationDbContext Context { get; set; }

        public BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base()
        {
            UserManager = userManager;
            AuthorizationService = authorizationService;
            Context = context;
        }

    }
}
