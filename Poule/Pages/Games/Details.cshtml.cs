using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private IGameData _gameData;

        public GameEditModel Game { get; set; }

        public DetailsModel(IGameData gameData)
        {
            _gameData = gameData;
        }

        public IActionResult OnGet(int id)
        {
            Game = _gameData.ToEditModel(_gameData.Get(id));
            if (Game == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}