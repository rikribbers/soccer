using System;
using System.Linq;
using Poule.Data;
using Poule.Models;
using Poule.ViewModel;
using SQLitePCL;

namespace Poule.Services
{
    public class PredictionConverter : IPredictionConverter
    {
        private readonly IScoreValidator _scoreValidator;
        //private readonly IApplicationDbContext _context;

        public PredictionConverter(IScoreValidator validator)//, ApplicationDbContext context)
        {
            _scoreValidator = validator;
          //  _context = context;
        }

        public MyPredictionEditModel ToMyPredictionEditModel(Prediction prediction, DateTime currentTime)
        {
            return new MyPredictionEditModel
            {
                Id = prediction.Id,
                GameId = prediction.Game.Id,
                Date = TimeZoneInfo.ConvertTimeFromUtc(prediction.Game.Date, TimeZoneInfo.Local),
                HomeTeam = prediction.Game.HomeTeam,
                AwayTeam = prediction.Game.AwayTeam,
                HalftimeScore = prediction.HalftimeScore,
                IsHalftimeScoreValid = _scoreValidator.IsValid(prediction.HalftimeScore),
                FulltimeScore = prediction.FulltimeScore,
                IsFulltimeScoreValid = _scoreValidator.IsValidFulltimeScore(prediction.HalftimeScore,prediction.FulltimeScore),
                Editable = _scoreValidator.IsEditable(currentTime, prediction.Game)
            };
        }

        public MyPredictionEditModel ToMyPredictionEditModel(Game game, DateTime currentTime)
        {
            return new MyPredictionEditModel
            {
                Date = TimeZoneInfo.ConvertTimeFromUtc(game.Date, TimeZoneInfo.Local),
                GameId = game.Id,
                HomeTeam = game.HomeTeam,
                AwayTeam = game.AwayTeam,
                IsHalftimeScoreValid = false,
                IsFulltimeScoreValid = false,
                Editable = _scoreValidator.IsEditable(currentTime, game)
            };
        }

        public Prediction ToEntity(MyPredictionEditModel prediction, IApplicationDbContext context, int id)
        {
            var entity = context.Predictions.FirstOrDefault(p => p.Id == id) ?? new Prediction();

            entity.Id = prediction.Id;
            entity.FulltimeScore = prediction.FulltimeScore;
            entity.HalftimeScore = prediction.HalftimeScore;
            return entity;
        }


        public Prediction ToEntity(PredictionEditModel prediction, IApplicationDbContext context, int id)
        {
            var entity = context.Predictions.FirstOrDefault(p => p.Id == id) ?? new Prediction();

            entity.Id = id;
            entity.FulltimeScore = prediction.FulltimeScore;
            entity.HalftimeScore = prediction.HalftimeScore;
            return entity;
        }
        public PredictionEditModel ToPredictionEditModel(Prediction prediction)
        {
            return new PredictionEditModel
            {
                Id = prediction.Id,
                Date = TimeZoneInfo.ConvertTimeFromUtc(prediction.Game.Date, TimeZoneInfo.Local),
                HomeTeam = prediction.Game.HomeTeam,
                AwayTeam = prediction.Game.AwayTeam,
                Username = prediction.User.Name,
                HalftimeScore = prediction.HalftimeScore,
                FulltimeScore = prediction.FulltimeScore,
                IsHalftimeScoreValid = _scoreValidator.IsValid(prediction.HalftimeScore),
                IsFulltimeScoreValid = _scoreValidator.IsValidFulltimeScore(prediction.HalftimeScore, prediction.FulltimeScore)
            };
        }
    }
}