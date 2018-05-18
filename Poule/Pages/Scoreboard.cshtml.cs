using System.Collections.Generic;
using System.Linq;
using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poule.Entities;

namespace Poule.Pages
{
    public class ScoreboardModel : PageModel
    {
        private readonly IPredictionData _predictionData;
        private readonly IUserData _userData;
        private PouleDbContext _context;
        public IEnumerable<Prediction> Predictions { get; set; }
        public IEnumerable<Game> Games { get; set; }

        public IEnumerable<User> Users { get; set; }
        public Dictionary<int,int> TotalScores { get; set; }

        public ScoreboardModel(IPredictionData predictionData,IUserData userData, PouleDbContext context)
        {
            _predictionData = predictionData;
            _userData = userData;
            _context = context;
        }

        public IActionResult OnGet()
        {
            Predictions = _predictionData.GetAll();
            Games = Predictions.Select(p => p.Game).Distinct().OrderBy(g => g.Order);
            Users = _userData.GetAll();
            TotalScores = new Dictionary<int, int>();
            foreach (var user in Users)
            {
                var total = 0;
                foreach (var p in Predictions.Where(p => p.User.Id == user.Id))
                {
                    total += CalculateScore(p.Game.HalftimeScore, p.Game.FulltimeScore, p.HalftimeScore,
                        p.FulltimeScore);
                }
                TotalScores.Add(user.Id,total);
            }          
            return Page();
        }

        public ScoreModel GetScoreForUser(Game game, int user)
        {
            // return ordered list of predictions for a game
            var prediction =  Predictions.Where(p => p.Game.Id == game.Id).FirstOrDefault( p => p.User.Id == user);
            var score = new ScoreModel();
            if (prediction != null)
            {
                score.HalftimeScore = prediction.HalftimeScore;
                score.FulltimeScore = prediction.FulltimeScore;
                score.Points  = CalculateScore(game.HalftimeScore, game.FulltimeScore, prediction.HalftimeScore,prediction.FulltimeScore);
            };
            return score;
        }

        private int CalculateScore(string gameHalftimeScore, string gameFulltimeScore, string predictionHalftimeScore, string predictionFulltimeScore)
        {
            int points = 0;
            if (gameHalftimeScore!= null && gameHalftimeScore.Equals(predictionHalftimeScore)) points++;
            if (gameFulltimeScore != null && gameFulltimeScore.Equals(predictionFulltimeScore)) points++;
            if (points == 2) points++;
            return points;
        }
    }
}