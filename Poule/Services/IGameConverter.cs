using Poule.Models;
using Poule.ViewModel;

namespace Poule.Services
{
    public interface IGameConverter
    {
        GameEditModel ToEditModel(Game game);
        Game ToEntity(GameEditModel game, int id = 0);
    }
}