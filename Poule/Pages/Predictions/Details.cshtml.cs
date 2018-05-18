using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Predictions
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IPredictionData _PredictionData;

        public PredictionEditModel Prediction { get; set; }

        public DetailsModel(IPredictionData PredictionData)
        {
            _PredictionData = PredictionData;
        }

        public IActionResult OnGet(int id)
        {
            Prediction = _PredictionData.ToPredictionEditModel(_PredictionData.Get(id));
            if (Prediction == null)
                return NotFound();
            return Page();
        }
    }
}