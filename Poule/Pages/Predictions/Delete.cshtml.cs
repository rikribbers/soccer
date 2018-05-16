using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Poule.Authorization;
using Poule.Data;
using Poule.Models;
using Poule.Services;

namespace Poule.Pages.Predictions
{
    [Authorize(Roles = "PouleAdministrators")]
    public class DeleteModel : BasePageModel
    {
        private readonly IPredictionData _predictionData;


        public DeleteModel(
            IPredictionData predictionData,
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _predictionData = predictionData;
        }

        [BindProperty]
        public Prediction Prediction { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Prediction = _predictionData.Get(id);

            if (Prediction == null)
                return NotFound();

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Prediction,
                PouleOperations.Delete);
            if (!isAuthorized.Succeeded)
                return new ChallengeResult();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Prediction = _predictionData.Get(id);

            if (Prediction == null)
                return NotFound();

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Prediction,
                PouleOperations.Delete);
            if (!isAuthorized.Succeeded)
                return new ChallengeResult();

            _predictionData.Remove(Prediction);

            return RedirectToPage("./Index");
        }
    }
}
