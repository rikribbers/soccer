using Microsoft.AspNetCore.Authorization;
using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Games
{
    [Authorize(Roles = "PouleAdministrators")]
    public class CreateModel : PageModel
    {
        private readonly IGameData _gameData;
        private readonly IGameConverter _gameConverter;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public GameEditModel Game { get; set; }

        public CreateModel(IGameData gameData, IGameConverter gameConvertor)
        {
            _gameData = gameData;
            _gameConverter = gameConvertor;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var g  = _gameData.Add(_gameConverter.ToEntity(Game));
                return RedirectToAction("Details", "Games", new {id = g.Id});
            }
            return Page();
        }
    }
}