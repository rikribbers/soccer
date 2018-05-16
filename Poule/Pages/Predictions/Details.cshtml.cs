using Microsoft.AspNetCore.Authorization;
using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Predictions
{
    [Authorize(Roles = "PouleAdministrators")]
    public class DetailsModel : PageModel
    {
        private IPredictionData _PredictionData;

        public PredictionEditModel Prediction { get; set; }

        public DetailsModel(IPredictionData PredictionData)
        {
            _PredictionData = PredictionData;
        }

        public IActionResult OnGet(int id)
        {
            Prediction = _PredictionData.ToPredictionEditModel(_PredictionData.Get(id));
            if (Prediction == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}