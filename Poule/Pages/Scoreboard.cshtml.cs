using System.Collections.Generic;
using System.Linq;
using Poule.Data;
using Poule.Models;
using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages
{
    public class ScoreboardModel : BasePageModel
    {
        private readonly IPredictionData _predictionData;
        private readonly IUserData _userData;
        private readonly IScoreCalculator _scoreCalculator;
        private IGameConverter _gameConverter;
        public IEnumerable<Prediction> Predictions { get; set; }
        public ICollection<GameEditModel> Games { get; set; }

        public IEnumerable<User> Users { get; set; }
        public Dictionary<int,int> TotalScores { get; set; }

        public ScoreboardModel(IPredictionData predictionData,IUserData userData, ApplicationDbContext context,
            IAuthorizationService authorizationService, IScoreCalculator scoreCalculator,
            IGameConverter gameConverter,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _predictionData = predictionData;
            _userData = userData;
            _scoreCalculator = scoreCalculator;
            _gameConverter = gameConverter;

        }

        public IActionResult OnGet()
        {
            Predictions = _predictionData.GetAll();
            var EntityGames = Predictions.Select(p => p.Game).Distinct().OrderBy(g => g.Order);
            Games = new List<GameEditModel>();
            foreach (var game in EntityGames)
            {
                if (Games.All(g => g.Id != game.Id))
                {
                    Games.Add(_gameConverter.ToEditModel(game));
                }
            }
            Users = _userData.GetAll();
            TotalScores = new Dictionary<int, int>();
            foreach (var user in Users)
            {
                var total = 0;
                foreach (var p in Predictions.Where(p => p.User.Id == user.Id))
                {
                    total += _scoreCalculator.Calculate(p.Game.HalftimeScore, p.Game.FulltimeScore, p.HalftimeScore,
                        p.FulltimeScore);
                }
                TotalScores.Add(user.Id,total);
            }          
            return Page();
        }

        public ScoreModel GetScoreForUser(GameEditModel game, int user)
        {
            // return ordered list of predictions for a game
            var prediction =  Predictions.Where(p => p.Game.Id == game.Id).FirstOrDefault( p => p.User.Id == user);
            var score = new ScoreModel();
            if (prediction != null)
            {
                score.HalftimeScore = prediction.HalftimeScore;
                score.FulltimeScore = prediction.FulltimeScore;
                score.Points  = _scoreCalculator.Calculate(game.HalftimeScore, game.FulltimeScore, prediction.HalftimeScore,prediction.FulltimeScore);
            };
            return score;
        }

    }
}