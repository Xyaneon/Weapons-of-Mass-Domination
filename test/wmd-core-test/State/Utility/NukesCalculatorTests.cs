using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Utility;

namespace WMD.Game.Test.State.Utility;

[TestClass]
public class NukesCalculatorTests
{
    [DataTestMethod]
    [DynamicData(nameof(CalculateMaximumNumberOfNukesCurrentPlayerCouldManufactureData))]
    public void CalculateMaximumNumberOfNukesCurrentPlayerCouldManufacture_ShouldReturnExpectedAmountForGameState(
        decimal playerMoney,
        int initialNukes,
        int expectedNukesPlayerCouldPurchase
    )
    {
        var playerState = new PlayerState() with {
            Money = playerMoney,
            Nukes = initialNukes
        };
        var player = new Player(
            new PlayerIdentification("Test player", PlayerColor.Red, true)
        ) with { State = playerState };
        IList<Player> players = new Player[] { player };
        var gameState = new GameState(players, new Earth());

        int actual = NukesCalculator.CalculateMaximumNumberOfNukesCurrentPlayerCouldManufacture(gameState);

        Assert.AreEqual(
            expectedNukesPlayerCouldPurchase,
            actual,
            "Money: <{0}>. Initial nukes: <{1}>.",
            playerMoney,
            initialNukes
        );
    }

    public static IEnumerable<Object[]> CalculateMaximumNumberOfNukesCurrentPlayerCouldManufactureData =>
        new [] {
            new Object[] { 0m, 0, 0 },
            new Object[] { 1000m, 0, 1 },
            new Object[] { 2000m, 0, 2 },
            new Object[] { 2338310951653.58m, 0, Int32.MaxValue },
            new Object[] { 2338310951653.58m, 2, Int32.MaxValue - 2 },
        };
}
