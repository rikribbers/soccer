using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Predictions
{
    [Authorize(Roles = "PouleAdministrators")]
    public class IndexModel : PageModel
    {
        private readonly IPredictionData _predictiondata;
        private readonly IPredictionConverter _converter;

        public List<PredictionEditModel> Predictions { get; set; }

        public IndexModel(IPredictionData predictiondata, IPredictionConverter converter)
        {
            _predictiondata = predictiondata;
            _converter = converter;
        }

        public IActionResult OnGet()
        {
            var predictions = _predictiondata.GetAll();
            Predictions = new List<PredictionEditModel>();

            foreach (var p in predictions)
            {
                var b = _converter.ToPredictionEditModel(p);
                Predictions.Add(b);
            }

            return Page();
        }
    }
}