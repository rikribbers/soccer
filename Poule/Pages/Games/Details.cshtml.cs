using Microsoft.AspNetCore.Authorization;
using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Games
{
    [Authorize(Roles = "PouleAdministrators")]
    public class DetailsModel : PageModel
    {
        private readonly IGameData _gameData;
        private readonly IGameConverter _gameConverter;

        public GameEditModel Game { get; set; }

        public DetailsModel(IGameData gameData,IGameConverter gameConverter)
        {
            _gameData = gameData;
            _gameConverter = gameConverter;
        }

        public IActionResult OnGet(int id)
        {
            Game = _gameConverter.ToEditModel(_gameData.Get(id));
            if (Game == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}