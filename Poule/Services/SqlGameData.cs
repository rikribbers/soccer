using System;
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
    }
}
