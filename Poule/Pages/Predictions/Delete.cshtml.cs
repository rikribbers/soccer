using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Entities;
using Poule.Services;

namespace Poule.Pages.Predictions
{
    public class DeleteModel : PageModel
    {
        private readonly IPredictionData _predictionData;
        private PouleDbContext _context;


        public DeleteModel(
            IPredictionData predictionData,
            PouleDbContext context)
        {
            _predictionData = predictionData;
            _context = context;
        }

        [BindProperty]
        public Prediction Prediction { get; set; }

        public IActionResult OnGet(int id)
        {
            Prediction = _predictionData.Get(id);

            if (Prediction == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Prediction = _predictionData.Get(id);

            if (Prediction == null)
                return NotFound();

            _predictionData.Remove(Prediction);

            return RedirectToPage("./Index");
        }
    }
}
