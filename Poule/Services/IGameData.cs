using System.Collections.Generic;
using Poule.Models;
using Poule.ViewModel;

namespace Poule.Services
{
    public interface IGameData
    {
        IEnumerable<Game> GetAll();
        Game Get(int id);
        Game Add(Game newGame);
        Game Update(Game game);
        GameEditModel ToEditModel(Game game);
        Game ToEntity(GameEditModel game, int id = 0);
    }
}
