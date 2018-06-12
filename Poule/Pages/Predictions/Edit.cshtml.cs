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
        private readonly IPredictionData _predictionData;
        private readonly IPredictionConverter _predictionConverter;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public PredictionEditModel Prediction { get; set; }

        public EditModel(IPredictionData PredictionData,
            IPredictionConverter predictionConverter,
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _predictionData = PredictionData;
            _predictionConverter = predictionConverter;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Prediction = _predictionConverter.ToPredictionEditModel(_predictionData.Get(id));
            Id = id;
            if (Prediction == null)
                return RedirectToAction("Index", "Home");

//            var isAuthorized = await AuthorizationService.AuthorizeAsync(
//                User, Prediction,
//                PouleOperations.Update);
//            if (!isAuthorized.Succeeded)
//                return new ChallengeResult();

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
//                var isAuthorized = await AuthorizationService.AuthorizeAsync(
//                   User, Prediction,
//                    PouleOperations.Update);
//                if (!isAuthorized.Succeeded)
//                    return new ChallengeResult();

                _predictionData.Update(_predictionConverter.ToEntity(Prediction,Context, Id));
                return RedirectToAction("Details", "Predictions", new {id = Id});
            }
            return Page();
        }
    }
}