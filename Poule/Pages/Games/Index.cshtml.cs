using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Poule.Models;
using Poule.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages.Games
{
    [Authorize(Roles = "PouleAdministrators")]
    public class IndexModel : PageModel
    {
        private readonly IGameData _gameData;

        public IEnumerable<Game> Games { get; set; }

        public IndexModel(IGameData gameData)
        {
            _gameData = gameData;
        }

        public IActionResult OnGet()
        {
            Games = _gameData.GetAll();
            return Page();
        }
    }
}