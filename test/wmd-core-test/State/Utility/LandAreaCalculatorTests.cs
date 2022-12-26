using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Utility;

namespace WMD.Game.Test.State.Utility
{
    [TestClass]
    public class LandAreaCalculatorTests
    {
        [TestMethod]
        public void CalculateMaximumLandAreaCurrentPlayerCouldPurchase_ShouldReturnExpectedAmountForGameState()
        {
            int expectedSquareKilometersPlayerCouldPurchase = 6;
            var playerState = new PlayerState() with { Money = 1000 };
            var player = new Player(
                new PlayerIdentification("Test player", PlayerColor.Red, true)
            ) with { State = playerState };
            IList<Player> players = new Player[] { player };
            var gameState = new GameState(players, new Earth());

            int actual = LandAreaCalculator.CalculateMaximumLandAreaCurrentPlayerCouldPurchase(gameState);

            Assert.AreEqual(expectedSquareKilometersPlayerCouldPurchase, actual);
        }
    }
}
