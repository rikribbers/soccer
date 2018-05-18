using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SignedOutModel : PageModel
{
    [AllowAnonymous]
    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToPage("/Index");

        return Page();
    }
}