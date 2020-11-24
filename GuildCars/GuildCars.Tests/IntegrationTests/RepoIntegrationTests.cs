using GuildCars.Data;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class RepoIntegrationTests
    {
        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepository();

            var states = repo.GetAll();

            Assert.AreEqual(52, states.Count);

            Assert.AreEqual("AK", states[0].StateId);
            Assert.AreEqual("Alaska", states[0].StateName);
        }

        [Test]
        public void CanLoadColorsAndStyles()
        {
            var repo = new ColorsAndStylesRepository();

            var exteriorColors = repo.GetAllExteriorColors();
            var interiorColors = repo.GetAllInteriorColors();
            var bodyStyles = repo.GetAllBodyStyles();

            Assert.AreEqual(7, exteriorColors.Count);
            Assert.AreEqual(1, exteriorColors[0].ExteriorColorId);
            Assert.AreEqual("White", exteriorColors[0].ExteriorColorName);

            Assert.AreEqual(5, interiorColors.Count);
            Assert.AreEqual(1, interiorColors[0].InteriorColorId);
            Assert.AreEqual("Black", interiorColors[0].InteriorColorName);

            Assert.AreEqual(4, bodyStyles.Count);
            Assert.AreEqual(1, bodyStyles[0].BodyStyleId);
            Assert.AreEqual("Car", bodyStyles[0].BodyStyleName);
        }

        [Test]
        public void CanLoadMakesAndModels()
        {
            var repo = new TypesRepository();

            var makes = repo.GetAllMakeTypes();
            var audiModels = repo.GetAllModelTypesByMake(makes[0]);
            var fordModels = repo.GetAllModelTypesByMake(makes[3]);

            Assert.AreEqual(5, makes.Count);
            Assert.AreEqual(1, makes[0].MakeTypeId);
            Assert.AreEqual("Ford", makes[3].MakeTypeName);

            Assert.AreEqual(3, audiModels.Count);
            Assert.AreEqual(1, audiModels[0].ModelTypeId);
            Assert.AreEqual("A4", audiModels[1].ModelTypeName);
            Assert.AreEqual(2, fordModels.Count);
            Assert.AreEqual("Fusion", fordModels[0].ModelTypeName);

        }

        
    }
}
