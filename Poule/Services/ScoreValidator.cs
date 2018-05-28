using System;
using System.Text.RegularExpressions;
using Poule.Models;

namespace Poule.Services
{
    public interface IScoreValidator
    {
        bool IsValid(string score);
        bool IsEditable(DateTime time, Game game);
    }

    public class ScoreValidator : IScoreValidator
    {
        private Regex _scoreRegex { get; }

        public ScoreValidator()
        {
            var pattern = @"^[0-9]{1,3}-[0-9]{1,3}$";
            _scoreRegex = new Regex(pattern);
        }

        public bool IsValid(string score)
        {
            if (score != null) return _scoreRegex.IsMatch(score);
            return false;
        }
 
        public bool IsEditable(DateTime time, Game game)
        {
            // only editable until 1h before start of game;
            return time.Add(TimeSpan.FromHours(1)).CompareTo(game.Date) <= 0;
        }
    }
}
