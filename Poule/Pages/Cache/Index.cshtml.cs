using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;

namespace Poule.Pages.Cache
{
    [Authorize(Roles = "PouleAdministrators")]
    public class IndexModel : PageModel
    {
        private readonly IPredictionData _predictionData;

        public IndexModel(IPredictionData predictionData)
        {
            _predictionData = predictionData;
        }


        public void OnGet()
        {
            _predictionData.InitCache();
        }
    }
}