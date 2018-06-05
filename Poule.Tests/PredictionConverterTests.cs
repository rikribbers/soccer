using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Poule.Data;
using Poule.Models;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Tests
{
    [TestFixture]
    internal class PredicitonConverterTests
    {
        private IPredictionConverter _sut;
        private Mock<IApplicationDbContext> _context;
        private Mock<IScoreValidator> _validator;
        private Mock<DbSet<Prediction>> _predictions;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IApplicationDbContext>();
            _predictions = new Mock<DbSet<Prediction>>();
//            _context.Setup(p => p.Predictions).Returns(_predictions);

            _sut = new PredictionConverter(_validator.Object);
        }

        [Test]
        public void ShouldConvertPredictionToPredcitionEditModel()
        {
            var utctime = new DateTime(2018, 5, 30, 9, 24, 37);

            var prediction = new Prediction
            {
                HalftimeScore = "1-2",
                FulltimeScore = "3-4",
                Id = 1,
                Game = new Game
                {
                    Id = 20,
                    HomeTeam = "HomeTeam",
                    AwayTeam = "AwayTeam",
                    Date = utctime,
                    FulltimeScore = "7-8",
                    HalftimeScore = "5-6",
                    Order = 10
                },
                User = new User
                {
                    Id = 24,
                    EmailAddress = "info@example.nl",
                    Name = "MeMyselfAndI",
                    Order = 40
                }
            };

            var predictionEditModel = _sut.ToPredictionEditModel(prediction);

            Assert.AreEqual("HomeTeam", predictionEditModel.HomeTeam);
            Assert.AreEqual("AwayTeam", predictionEditModel.AwayTeam);
            Assert.AreEqual("3-4", predictionEditModel.FulltimeScore);
            Assert.AreEqual("1-2", predictionEditModel.HalftimeScore);

            // Date should be converted from UTC to localtime
            // localtime is in CEST so +2 hours
            Assert.AreEqual(predictionEditModel.Date, new DateTime(2018, 5, 30, 11, 24, 37));
        }

        [Test]
        public void ShouldConvertPredictionAndToPredcitionEditModel()
        {
            //TODO WHICH TIME?
        }

        [Test]
        public void ShouldConvertGameAndTimeToPredcitionEditModel()
        {
        }

        [Test]
        public void ShouldConvertPredictionEditModelToEntity()
        {
            var localtime = new DateTime(2018, 5, 30, 9, 24, 37);
            var predictionEditModel = new PredictionEditModel
            {
                Date = localtime,
                AwayTeam = "AwayTeam",
                FulltimeScore = "3-4",
                HalftimeScore = "1-2",
                HomeTeam = "HomeTeam",
                Username = "Username"
            };
            //no index and with index

//            var entity = _sut.ToEntity(predictionEditModel,_cotext);

//            Assert.AreEqual("3-4", entity.FulltimeScore);
//            Assert.AreEqual("1-2", entity.HalftimeScore);
        }

        [Test]
        public void ShouldConvertMyPredictionEditModelToEntity()
        {
            //no index and with index
        }
    }
}
