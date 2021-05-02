using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data.Planets;

namespace WMD.Game.Test.State.Data.Planets
{
    [TestClass]
    public class PlanetTests
    {
        private record TestPlanet : Planet
        {
            public TestPlanet(string name, int totalLandArea, int totalSurfaceArea, int totalWaterArea, long neutralPopulation)
                : base(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation) { }
        }

        [TestMethod]
        public void Constructor_ShouldThrowIfNameIsEmpty()
        {
            string name = "";
            int totalLandArea = 1;
            int totalSurfaceArea = 3;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var actual = Assert.ThrowsException<ArgumentException>(() =>
            {
                _ = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);
            });

            Assert.IsTrue(actual.Message.Contains("The name of the planet cannot be empty or all whitespace."));
        }

        [TestMethod]
        public void Constructor_ShouldThrowIfNameIsOnlyWhitespace()
        {
            string name = "   ";
            int totalLandArea = 1;
            int totalSurfaceArea = 3;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var actual = Assert.ThrowsException<ArgumentException>(() =>
            {
                _ = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);
            });

            Assert.IsTrue(actual.Message.Contains("The name of the planet cannot be empty or all whitespace."));
        }

        [TestMethod]
        public void Constructor_ShouldThrowIfTotalLandAreaIsNegative()
        {
            string name = "MyPlanet";
            int totalLandArea = -1;
            int totalSurfaceArea = 1;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);
            });

            Assert.IsTrue(actual.Message.Contains("The total land area cannot be negative."));
        }

        [TestMethod]
        public void Constructor_ShouldThrowIfTotalSurfaceAreaIsNegative()
        {
            string name = "MyPlanet";
            int totalLandArea = 1;
            int totalSurfaceArea = -1;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);
            });

            Assert.IsTrue(actual.Message.Contains("The total surface area cannot be negative."));
        }

        [TestMethod]
        public void Constructor_ShouldThrowIfTotalWaterAreaIsNegative()
        {
            string name = "MyPlanet";
            int totalLandArea = 1;
            int totalSurfaceArea = 3;
            int totalWaterArea = -2;
            long neutralPopulation = 1;

            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);
            });

            Assert.IsTrue(actual.Message.Contains("The total water area cannot be negative."));
        }

        [TestMethod]
        public void Constructor_ShouldThrowIfLandAndWaterAreasDoNotAddUpToTotalSurfaceArea()
        {
            string name = "MyPlanet";
            int totalLandArea = 1;
            int totalSurfaceArea = 5;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var actual = Assert.ThrowsException<ArgumentException>(() =>
            {
                _ = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);
            });

            Assert.IsTrue(actual.Message.Contains("The land and water areas do not add up to the total surface area."));
        }

        [TestMethod]
        public void Constructor_ShouldStoreExpectedValues()
        {
            string expectedName = "MyPlanet";
            int expectedTotalLandArea = 1;
            int expectedTotalSurfaceArea = 3;
            int expectedTotalWaterArea = 2;
            long expectedNeutralPopulation = 1;
            int expectedUnclaimedLandArea = 1;
            double expectedPercentageOfLandStillUnclaimed = 1.0;

            var actual = new TestPlanet(expectedName, expectedTotalLandArea, expectedTotalSurfaceArea, expectedTotalWaterArea, expectedNeutralPopulation);

            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedTotalLandArea, actual.TotalLandArea);
            Assert.AreEqual(expectedTotalSurfaceArea, actual.TotalSurfaceArea);
            Assert.AreEqual(expectedTotalWaterArea, actual.TotalWaterArea);
            Assert.AreEqual(expectedNeutralPopulation, actual.NeutralPopulation);
            Assert.AreEqual(expectedUnclaimedLandArea, actual.UnclaimedLandArea);
            Assert.AreEqual(expectedPercentageOfLandStillUnclaimed, actual.PercentageOfLandStillUnclaimed);
        }

        [TestMethod]
        public void Constructor_ShouldTrimPlanetNameWhenStored()
        {
            string name = "     MyPlanet    ";
            string expectedName = "MyPlanet";
            int totalLandArea = 1;
            int totalSurfaceArea = 3;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var actual = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);

            Assert.AreEqual(expectedName, actual.Name);
        }

        [TestMethod]
        public void NeutralPopulation_Init_ShouldThrowIfValueIsNegative()
        {
            string name = "MyPlanet";
            int totalLandArea = 1;
            int totalSurfaceArea = 3;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var initialPlanet = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);

            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = initialPlanet with { NeutralPopulation = -1 };
            });

            Assert.IsTrue(actual.Message.Contains("The neutral population cannot be negative."));
        }

        [TestMethod]
        public void UnclaimedLandArea_Init_ShouldThrowIfValueIsNegative()
        {
            string name = "MyPlanet";
            int totalLandArea = 1;
            int totalSurfaceArea = 3;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var initialPlanet = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);

            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = initialPlanet with { UnclaimedLandArea = -1 };
            });

            Assert.IsTrue(actual.Message.Contains("The amount of unclaimed land cannot be negative."));
        }

        [TestMethod]
        public void UnclaimedLandArea_Init_ShouldThrowIfValueExceedsTotalLandArea()
        {
            string name = "MyPlanet";
            int totalLandArea = 1;
            int totalSurfaceArea = 3;
            int totalWaterArea = 2;
            long neutralPopulation = 1;

            var initialPlanet = new TestPlanet(name, totalLandArea, totalSurfaceArea, totalWaterArea, neutralPopulation);

            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = initialPlanet with { UnclaimedLandArea = totalLandArea + 1 };
            });

            Assert.IsTrue(actual.Message.Contains("The amount of unclaimed land cannot exceed the total land area."));
        }
    }
}
