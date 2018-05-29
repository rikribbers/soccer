using System;
using NUnit.Framework;
using Poule.Models;
using Poule.Services;

namespace Poule.Tests
{
    [TestFixture]
    public class ScoreValidatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("")]
        [TestCase("-")]
        [TestCase("0")]
        [TestCase("00")]
        [TestCase("00")]
        [TestCase("000")]
        [TestCase("0000")]
        [TestCase("-0")]
        [TestCase("-00")]
        [TestCase("-000")]
        [TestCase("-000")]
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
            var sut = new ScoreValidator();

            Assert.False(sut.IsValid(score));
            Assert.False(sut.IsValidFulltimeScore("0-0", score));
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
            var sut = new ScoreValidator();

            Assert.True(sut.IsValid(score));
            Assert.True(sut.IsValidFulltimeScore("0-0", score));
        }

        [Test]
        public void FullTimeScoreShouldBeInvalid()
        {
            var sut = new ScoreValidator();

            Assert.False(sut.IsValidFulltimeScore("1-0", "0-0"));
            Assert.False(sut.IsValidFulltimeScore("0-1", "0-0"));
        }

        [Test]
        public void FullTimeScoreShouldBeValid()
        {
            var sut = new ScoreValidator();

            Assert.True(sut.IsValidFulltimeScore("1-0", "1-0"));
            Assert.True(sut.IsValidFulltimeScore("0-1", "0-1"));

            Assert.True(sut.IsValidFulltimeScore("1-0", "2-0"));
            Assert.True(sut.IsValidFulltimeScore("0-1", "0-2"));
        }

        [Test]
        public void IsEditableOneSecondBeforeHour()
        {
            var sut = new ScoreValidator();
            Assert.True(sut.IsEditable(new DateTime(2018, 6, 4, 18, 59, 59), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0)
            }));
        }

        [Test]
        public void IsEditableAtExactOneHourBeforeGame()
        {
            var sut = new ScoreValidator();
            Assert.False(sut.IsEditable(new DateTime(2018, 6, 4, 19, 0, 0), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0)
            }));
        }

        [Test]
        public void IsEditableAtExactOneSecondAfterHour()
        {
            var sut = new ScoreValidator();
            Assert.False(sut.IsEditable(new DateTime(2018, 6, 4, 19, 0, 1), new Game
            {
                Date = new DateTime(2018, 6, 4, 20, 0, 0)
            }));
        }
    }
}