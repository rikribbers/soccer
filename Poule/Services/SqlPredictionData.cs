using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Poule.Data;
using Poule.Models;
using Poule.ViewModel;
using Microsoft.Extensions.Logging;

namespace Poule.Services
{
    public class SqlPredictionData : IPredictionData
    {
        private readonly ApplicationDbContext _context;
        private readonly IScoreValidator _scoreValidator;
        private readonly ILogger _logger;
        
        public SqlPredictionData(ApplicationDbContext context, IScoreValidator scoreValidator, ILogger<SqlPredictionData> logger)
        {
            _context = context;
            _scoreValidator = scoreValidator;
            _logger = logger;
        }

        public IEnumerable<Prediction> GetAll()
        {
            return _context.Predictions.Include(p => p.Game).Include(p => p.User).OrderBy(p => p.Game.Order)
                .ThenBy(p => p.User.Order);
        }

        public Prediction Get(int id)
        {
            return _context.Predictions.Include(p => p.Game).Include(p => p.User).FirstOrDefault(p => p.Id == id);
        }

        public Prediction Add(Prediction newPrediction)
        {
            _context.Attach(newPrediction.Game).State = EntityState.Unchanged;
            _context.Add(newPrediction);
            _context.SaveChanges();
            return newPrediction;
        }

        public Prediction Update(Prediction prediction)
        {
            _context.Update(prediction);
            _context.SaveChanges();
            return prediction;
        }

        public Prediction GetForUser(int userId,int gameId)
        {
            var result = _context.Predictions.Where(p => p.User.Id == userId).FirstOrDefault( p => p.Game.Id == gameId);
            if (result == null)
            {
                _logger.LogInformation ("results == null");
                return result;
            }
            return result;
        }

        public void Remove(Prediction prediction)
        {
            _context.Remove(prediction);
            _context.SaveChanges();
        }
    }
}
