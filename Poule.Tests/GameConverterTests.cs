using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Poule.Models;
using Poule.Services;
using Poule.ViewModel;

namespace Poule.Tests
{
    [TestFixture]
    class GameConverterTests
    {
    
        [Test]
        public void ShouldConvertToEntity()
        {
            var localtime = new DateTime(2018, 5, 30, 9, 24, 37);
            var viewModelObject = new GameEditModel
            {
                AwayTeam = "Awayteam",
                HomeTeam = "Hometeam",
                Date = localtime,
                Round = RoundType.First,
                FulltimeScore = "3-4",
                HalftimeScore = "1-2"
            };

            var convertor = new GameConverter();
            var entity = convertor.ToEntity(viewModelObject,2);

            Assert.AreEqual(viewModelObject.HomeTeam, entity.HomeTeam);
            Assert.AreEqual(viewModelObject.AwayTeam, entity.AwayTeam);
            Assert.AreEqual(viewModelObject.Round, entity.Round);
            Assert.AreEqual(viewModelObject.HalftimeScore, entity.HalftimeScore);
            Assert.AreEqual(viewModelObject.FulltimeScore, entity.FulltimeScore);
           
            // Date should be converted from local time to UTC
            // localtime is in CEST so -2 hours
            Assert.AreEqual(entity.Date, new DateTime(2018, 5, 30, 7, 24, 37));
        }
        [Test]
        public void ShouldConvertToViewmodel()
        {
            var utctime = new DateTime(2018, 5, 30, 9, 24, 37);
            var entity = new Game
            {
                Id = 77,
                AwayTeam = "Awayteam",
                HomeTeam = "Hometeam",
                Date = utctime,
                Round = RoundType.First,
                FulltimeScore = "3-4",
                HalftimeScore = "1-2"
            };

            var convertor = new GameConverter();
            var viewModelObject = convertor.ToEditModel(entity);

            Assert.AreEqual(viewModelObject.Id, entity.Id);
            Assert.AreEqual(viewModelObject.HomeTeam, entity.HomeTeam);
            Assert.AreEqual(viewModelObject.AwayTeam, entity.AwayTeam);
            Assert.AreEqual(viewModelObject.Round, entity.Round);
            Assert.AreEqual(viewModelObject.HalftimeScore, entity.HalftimeScore);
            Assert.AreEqual(viewModelObject.FulltimeScore, entity.FulltimeScore);

            // Date should be converted from UTC to localtime
            // localtime is in CEST so +2 hours
            Assert.AreEqual(viewModelObject.Date, new DateTime(2018, 5, 30, 11, 24, 37));

        }
    }
}
