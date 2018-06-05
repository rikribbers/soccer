using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poule.Models;
using Poule.ViewModel;

namespace Poule.Services
{
    public class GameConverter : IGameConverter
    {
        public GameEditModel ToEditModel(Game game)
        {
            return new GameEditModel
            {
                Id = game.Id,
                Order = game.Order,
                Date = TimeZoneInfo.ConvertTimeFromUtc(game.Date, TimeZoneInfo.Local),
                Round = game.Round,
                HomeTeam = game.HomeTeam,
                AwayTeam = game.AwayTeam,
                HalftimeScore = game.HalftimeScore,
                FulltimeScore = game.FulltimeScore
            };
        }

        public Game ToEntity(GameEditModel game, int id)
        {
            var g = new Game
            {
                Order = game.Order,
                Date = TimeZoneInfo.ConvertTimeToUtc(game.Date, TimeZoneInfo.Local),
                Round = game.Round,
                HomeTeam = game.HomeTeam,
                AwayTeam = game.AwayTeam,
                HalftimeScore = game.HalftimeScore,
                FulltimeScore = game.FulltimeScore
            };
            if (id > 0)
                g.Id = id;
            return g;
        }
    }

}  
