using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Poule.Authorization;
using Poule.Data;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Predictions
{
    [Authorize(Roles = "PouleAdministrators")]
    public class EditModel : BasePageModel
    {
        private readonly IPredictionData _PredictionData;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public PredictionEditModel Prediction { get; set; }

        public EditModel(IPredictionData PredictionData,
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _PredictionData = PredictionData;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Prediction = _PredictionData.ToPredictionEditModel(_PredictionData.Get(id));
            Id = id;
            if (Prediction == null)
                return RedirectToAction("Index", "Home");

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Prediction,
                PouleOperations.Update);
            if (!isAuthorized.Succeeded)
                return new ChallengeResult();

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                    User, Prediction,
                    PouleOperations.Update);
                if (!isAuthorized.Succeeded)
                    return new ChallengeResult();

                _PredictionData.Update(_PredictionData.ToEntity(Prediction, Id));
                return RedirectToAction("Details", "Predictions", new {id = Id});
            }
            return Page();
        }
    }
}