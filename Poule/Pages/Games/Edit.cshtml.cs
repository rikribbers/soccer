using Microsoft.AspNetCore.Authorization;
using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Games
{
    [Authorize(Roles = "PouleAdministrators")]
    public class EditModel : PageModel
    {
        private readonly IGameData _gameData;
        private readonly IGameConverter _gameConverter;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public GameEditModel Game { get; set; }

        public EditModel(IGameData gameData, IGameConverter gameConverter)
        {
            _gameData = gameData;
            _gameConverter = gameConverter;
        }

        public IActionResult OnGet(int id)
        {
            Game = _gameConverter.ToEditModel(_gameData.Get(id));
            Id = id;
            if (Game == null)
                return RedirectToAction("Index", "Home");
            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _gameData.Update(_gameConverter.ToEntity(Game, Id));
                return RedirectToAction("Details", "Games", new {id = Id});
            }
            return Page();
        }
    }
}