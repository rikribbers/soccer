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
        private IPredictionData _predictionData;
        private IPredictionConverter _converter;

        public PredictionEditModel Prediction { get; set; }

        public DetailsModel(IPredictionData PredictionData,IPredictionConverter  converter)
        {
            _predictionData = PredictionData;
            _converter = converter;
        }

        public IActionResult OnGet(int id)
        {
            Prediction = _converter.ToPredictionEditModel(_predictionData.Get(id));
            if (Prediction == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}