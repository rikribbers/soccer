using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly IGameData _gameData;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public GameEditModel Game { get; set; }

        public EditModel(IGameData gameData)
        {
            _gameData = gameData;
        }

        public IActionResult OnGet(int id)
        {
            Game = _gameData.ToEditModel(_gameData.Get(id));
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
                _gameData.Update(_gameData.ToEntity(Game, Id));
                return RedirectToAction("Details", "Games", new {id = Id});
            }
            return Page();
        }
    }
}