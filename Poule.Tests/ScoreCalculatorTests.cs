using NUnit.Framework;
using Poule.Services;

namespace Poule.Tests
{
    [TestFixture]
    internal class ScoreCalculatorTests
    {
        // null and empty string corner cases
        [TestCase(null, null, null, null)]
        [TestCase("0-0", null, null, null)]
        [TestCase(null, "0-0", null, null)]
        [TestCase(null, null, "0-0", null)]
        [TestCase(null, null, null, "0-0")]
        [TestCase("", null, null, null)]
        [TestCase(null, "", null, null)]
        [TestCase(null, null, "", null)]
        [TestCase(null, null, null, "")]
        [TestCase("", "", null, null)]
        [TestCase(null, "", "", null)]
        [TestCase(null, null, "", "")]
        [TestCase(null, "", null, "")]
        [TestCase("", null, "", null)]
        [TestCase("", "", "", "")]
        [TestCase("0-0", "0-0", "", "")]
        [TestCase("", "", "0-0", "0-0")]
        [TestCase("0-0", "0-0", null, null)]
        [TestCase(null, null, "0-0", "0-0")]

        // valid scores no point
        [TestCase("0-0", "0-0", "1-0", "0-1")]
        [TestCase("1-0", "1-0", "0-0", "0-0")]
        public void ShouldHaveNoPoints(string gameHalftimeScore, string gameFulltimeScore,
            string predictionHalftimeScore,
            string predictionFulltimeScore)
        {
            var sut = new ScoreCalculator();
            Assert.That(
                sut.Calculate(gameHalftimeScore, gameFulltimeScore, predictionHalftimeScore, predictionFulltimeScore),
                Is.EqualTo(0));
        }

        [TestCase("0-0", "1-0", "0-0", "1-1")]
        [TestCase("0-0", "1-0", "1-0", "1-0")]
        public void ShouldHaveOnePoint(string gameHalftimeScore, string gameFulltimeScore,
            string predictionHalftimeScore,
            string predictionFulltimeScore)
        {
            var sut = new ScoreCalculator();
            Assert.That(
                sut.Calculate(gameHalftimeScore, gameFulltimeScore, predictionHalftimeScore, predictionFulltimeScore),
                Is.EqualTo(1));
        }

        [TestCase("0-0", "0-0", "0-0", "0-0")]
        [TestCase("1-0", "1-0", "1-0", "1-0")]
        public void ShouldHaveThreePoints(string gameHalftimeScore, string gameFulltimeScore,
            string predictionHalftimeScore,
            string predictionFulltimeScore)
        {
            var sut = new ScoreCalculator();
            Assert.That(
                sut.Calculate(gameHalftimeScore, gameFulltimeScore, predictionHalftimeScore, predictionFulltimeScore),
                Is.EqualTo(3));
        }
    }
}
