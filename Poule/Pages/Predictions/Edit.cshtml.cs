using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Predictions
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IPredictionData _PredictionData;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public PredictionEditModel Prediction { get; set; }

        public EditModel(IPredictionData PredictionData)
        {
            _PredictionData = PredictionData;
        }

        public IActionResult OnGet(int id)
        {
            Prediction = _PredictionData.ToPredictionEditModel(_PredictionData.Get(id));
            Id = id;
            if (Prediction == null)
                return RedirectToAction("Index", "Home");
            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _PredictionData.Update(_PredictionData.ToEntity(Prediction, Id));
                return RedirectToAction("Details", "Predictions", new {id = Id});
            }
            return Page();
        }
    }
}