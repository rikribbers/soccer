using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Poule.Models;
using Poule.Services;

namespace Poule.Tests
{
    [TestFixture]
    public class ScoreValidatorTests
    {
        public ScoreValidator ScoreValidator { get; set; }

        [SetUp]
        public void Setup()
        {
            var dict = new Dictionary<string, string>
            {
                {"PredictionChangesAllowedUntil:First", "2018-06-12T00:00:00.000"}
            };

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(dict);
            ScoreValidator = new ScoreValidator(builder.Build());
        }

        [TestCase("")]
        [TestCase("-")]
        [TestCase("0")]
        [TestCase("00")]
        [TestCase("000")]
        [TestCase("0000")]
        [TestCase("-0")]
        [TestCase("-00")]
        [TestCase("-000")]
        [TestCase("-0000")]
        [TestCase("0-0000")]
        [TestCase("00-0000")]
        [TestCase("000-0000")]
        [TestCase("0-")]
        [TestCase("00-")]
        [TestCase("000-")]

        // characters
        [TestCase("0-a")]
        [TestCase("0-a0")]
        [TestCase("0-0a0")]
        [TestCase("0a-")]
        [TestCase("0a0-0")]
        [TestCase("a-0")]

        // multiple - characters
        [TestCase("0-0-")]
        [TestCase("-0-")]
        [TestCase("--0")]
        [TestCase("0--")]
        [TestCase("0-0--")]
        public void ShouldNotBeValid(string score)
        {
            
            Assert.False(ScoreValidator.IsValid(score));
            Assert.False(ScoreValidator.IsValidFulltimeScore("0-0", score));
        }

        [TestCase("0-0")]
        [TestCase("0-00")]
        [TestCase("0-000")]
        [TestCase("00-0")]
        [TestCase("000-0")]
        [TestCase("9-9")]
        [TestCase("9-99")]
        [TestCase("9-999")]
        [TestCase("99-9")]
        [TestCase("999-9")]
        public void ShouldBeValid(string score)
        {
            
            Assert.True(ScoreValidator.IsValid(score));
            Assert.True(ScoreValidator.IsValidFulltimeScore("0-0", score));
        }

        [Test]
        public void FullTimeScoreShouldBeInvalid()
        {
            Assert.False(ScoreValidator.IsValidFulltimeScore("1-0", "0-0"));
            Assert.False(ScoreValidator.IsValidFulltimeScore("0-1", "0-0"));
        }

        [Test]
        public void FullTimeScoreShouldBeValid()
        {
            Assert.True(ScoreValidator.IsValidFulltimeScore("1-0", "1-0"));
            Assert.True(ScoreValidator.IsValidFulltimeScore("0-1", "0-1"));

            Assert.True(ScoreValidator.IsValidFulltimeScore("1-0", "2-0"));
            Assert.True(ScoreValidator.IsValidFulltimeScore("0-1", "0-2"));
        }

        [Test]
        public void IsEditableOneSecondBeforeHourFinal()
        {
            Assert.True(ScoreValidator.IsEditable(new DateTime(2018, 6, 4, 16, 59, 59), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0),
                Round = RoundType.Finale
            }));
        }

        [Test]
        public void IsEditableAtExactOneHourBeforeGameFinal()
        {
            Assert.False(ScoreValidator.IsEditable(new DateTime(2018, 6, 4, 17, 0, 0), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0),
                Round = RoundType.Finale
            }));
        }

        [Test]
        public void IsEditableAtExactOneSecondAfterHourFinal()
        {
            Assert.False(ScoreValidator.IsEditable(new DateTime(2018, 6, 4, 17, 0, 1), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0),
                Round = RoundType.Finale
            }));
        }

        [Test]
        public void IsEditableOneSecondBeforeHourFirst()
        {
            Assert.True(ScoreValidator.IsEditable(new DateTime(2018, 6, 11, 23, 59, 59), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0),
                Round = RoundType.First
            }));
        }

        [Test]
        public void IsEditableAtExactOneHourBeforeGameFirst()
        {
            Assert.False(ScoreValidator.IsEditable(new DateTime(2018, 6, 12, 0, 0, 0), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0),
                Round = RoundType.First
            }));
        }

        [Test]
        public void IsEditableAtExactOneSecondAfterHourFirst()
        {
            Assert.False(ScoreValidator.IsEditable(new DateTime(2018, 6, 12, 0, 0, 1), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0),
                Round = RoundType.First
            }));
        }
    }
}