using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Poule.Entities;
using Poule.ViewModel;

namespace Poule.Services
{
    public class SqlPredictionData : IPredictionData
    {
        private readonly PouleDbContext _context;

        public SqlPredictionData(PouleDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Prediction> GetAll()
        {
            return _context.Predictions.Include(p=> p.Game).Include(p => p.User).OrderBy(p => p.Game.Order).ThenBy(p => p.User.Order);
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
            _context.Attach(prediction).State = EntityState.Modified;
            _context.SaveChanges();
            return prediction;
        }

        public PredictionEditModel ToPredictionEditModel(Prediction prediction)
        {
            return new PredictionEditModel
            {
                Date = prediction.Game.Date,
                HomeTeam = prediction.Game.HomeTeam,
                AwayTeam = prediction.Game.AwayTeam,
                Username = prediction.User.Name,
                HalftimeScore = prediction.HalftimeScore,
                FulltimeScore = prediction.FulltimeScore
            };
        }

        public Prediction ToEntity(PredictionEditModel prediction, int id)
        {
            var entity = _context.Predictions.FirstOrDefault(p => p.Id == id) ?? new Prediction();

            entity.Id = id;
            entity.FulltimeScore = prediction.FulltimeScore;
            entity.HalftimeScore = prediction.HalftimeScore;
            return entity;
        }

        public IEnumerable<Prediction> GetForUser(int id)
        {
            return _context.Predictions.Where(p => p.User.Id == id);
        }

        public MyPredictionEditModel ToMyPredictionEditModel(Prediction prediction, DateTime currentTime)
        {   
            return new MyPredictionEditModel
            {
                Id = prediction.Id,
                GameId = prediction.Game.Id,
                Date = prediction.Game.Date,
                HomeTeam = prediction.Game.HomeTeam,
                AwayTeam = prediction.Game.AwayTeam,
                HalftimeScore = prediction.HalftimeScore,
                FulltimeScore = prediction.FulltimeScore,
                // only editable until 1h before start of game;
                Editable = currentTime.Add(TimeSpan.FromHours(1)).CompareTo(prediction.Game.Date) <=0
            };
        }

        public Prediction ToEntity(MyPredictionEditModel prediction, int id)
        {
            var entity = _context.Predictions.FirstOrDefault(p => p.Id == id) ?? new Prediction();

            entity.Id = prediction.Id;
            entity.FulltimeScore = prediction.FulltimeScore;
            entity.HalftimeScore = prediction.HalftimeScore;
            return entity;
        }
    }
}
