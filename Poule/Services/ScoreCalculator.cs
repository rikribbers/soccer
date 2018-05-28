using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Poule.Services
{
    public interface IScoreCalculator
    {
        int Calculate(string gameHalftimeScore, string gameFulltimeScore, string predictionHalftimeScore,
            string predictionFulltimeScore);
    }
    public class ScoreCalculator : IScoreCalculator
    {      
        public int Calculate(string gameHalftimeScore, string gameFulltimeScore, string predictionHalftimeScore, string predictionFulltimeScore)
        {
            int points = 0;
            if (gameHalftimeScore != null && gameHalftimeScore.Equals(predictionHalftimeScore)) points++;
            if (gameFulltimeScore != null && gameFulltimeScore.Equals(predictionFulltimeScore)) points++;
            if (points == 2) points++;
            return points;
        }
    }
}
