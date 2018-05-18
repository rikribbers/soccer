using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Poule.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Entities;

namespace Poule.Pages.Predictions
{
    public class IndexModel : PageModel
    {
        private readonly IPredictionData _PredictionData;

        public IEnumerable<Prediction> Predictions { get; set; }

        public IndexModel(IPredictionData PredictionData)
        {
            _PredictionData = PredictionData;
        }

        public IActionResult OnGet()
        {
            Predictions = _PredictionData.GetAll();
            return Page();
        }
    }
}