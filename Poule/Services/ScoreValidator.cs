using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Poule.Models;
using static System.Int32;

namespace Poule.Services
{
    public interface IScoreValidator
    {
        bool IsValid(string score);
        bool IsEditable(DateTime time, Game game);
    }

    public class ScoreValidator : IScoreValidator
    {
        private Regex ScoreRegex { get; }
        
        private readonly DateTime _endOfFirstRoundEditing;

        public ScoreValidator(IConfiguration configuration)
        {
            var pattern = @"^[0-9]{1,3}-[0-9]{1,3}$";
            ScoreRegex = new Regex(pattern);
            _endOfFirstRoundEditing = DateTime.Parse(configuration["PredictionChangesAllowedUntil:First"]);
        }

        public bool IsValid(string score)
        {
            if (string.IsNullOrEmpty(score))
                return false;
            return ScoreRegex.IsMatch(score);
        }

        public bool IsEditable(DateTime time, Game game)
        {
            if (RoundType.First.Equals(game.Round))
            {
                return time.CompareTo(_endOfFirstRoundEditing) < 0;
            }
            return time.Add(TimeSpan.FromHours(3)).CompareTo(game.Date) < 0;
        }

        public bool IsValidFulltimeScore(string halftimeScore, string fulltimeScore)
        {
            if (string.IsNullOrEmpty(halftimeScore) || string.IsNullOrEmpty(fulltimeScore) ||
                !ScoreRegex.IsMatch(fulltimeScore))
                return false;

            // fulltime score is valid and halftime score is not null
            var splittedFulltime = fulltimeScore.Split("-");
            var splittedHalftime = halftimeScore.Split("-");


            if (splittedHalftime.Length > 0)
            {
                if (string.IsNullOrEmpty(splittedHalftime[0]) ||
                    string.IsNullOrEmpty(splittedFulltime[0]))
                    return false;
                int score = -9999;
                if (TryParse(splittedHalftime[0], out score) && Parse(splittedFulltime[0]) < score )
                    return false;
            }

            if (splittedHalftime.Length > 1)
            {
                if (string.IsNullOrEmpty(splittedHalftime[1]) ||
                    string.IsNullOrEmpty(splittedFulltime[1]))
                    return false;
                int score = -9999;
                if (TryParse(splittedHalftime[1], out score) && Parse(splittedFulltime[1]) < score)
                {
                    return false;
                }
                else
                {
                    // only if all checks succeed return true
                    return true;
                }
            }

            if (splittedHalftime.Length > 2)
            {
                return false;
            }
            // splitting did something strange here,
            // just ignore and return false; 
            return false;
        }
    }
}
