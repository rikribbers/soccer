using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Poule.Data;
using Poule.Models;
using Poule.ViewModel;

namespace Poule.Services
{
    public class SqlGameData : IGameData
    {
        private readonly ApplicationDbContext _context;

        public SqlGameData(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games.OrderBy(g => g.Order);
        }

        public Game Get(int id)
        {
            return _context.Games.FirstOrDefault(g => g.Id == id);
        }

        public Game Add(Game newGame)
        {
            _context.Add(newGame);
            _context.SaveChanges();
            return newGame;
        }

        public Game Update(Game game)
        {
            _context.Attach(game).State = EntityState.Modified;
            _context.SaveChanges();
            return game;
        }

        public GameEditModel ToEditModel(Game game)
        {
            return new GameEditModel
            {
                Order = game.Order,
                Date = game.Date,
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
                Date = game.Date,
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
