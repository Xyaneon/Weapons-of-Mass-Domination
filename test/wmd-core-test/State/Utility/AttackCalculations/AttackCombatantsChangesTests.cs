using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Utility.AttackCalculations;

namespace WMD.Game.Test.State.Utility.AttackCalculations;

[TestClass]
public class AttackCombatantsChangesTests
{
    [TestMethod]
    public void Constructor_ShouldThrowArgumentOutOfRangeExceptionIfStartingCombatantsOnAttackingSideIsLessThanZero()
    {
        long expectedStartingCombatantsOnAttackingSide = -1;
        long expectedStartingCombatantsOnDefendingSide = 10;
        long expectedCombatantsLostByAttacker = -1;
        long expectedCombatantsLostByDefender = 10;

        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new AttackCombatantsChanges(
                expectedStartingCombatantsOnAttackingSide,
                expectedStartingCombatantsOnDefendingSide,
                expectedCombatantsLostByAttacker,
                expectedCombatantsLostByDefender
            );
        });

        Assert.IsTrue(actual.Message.Contains("The number of starting combatants on the attacker's side cannot be less than zero."));
    }
    
    [TestMethod]
    public void Constructor_ShouldThrowArgumentOutOfRangeExceptionIfStartingCombatantsOnDefendingSideIsLessThanZero()
    {
        long expectedStartingCombatantsOnAttackingSide = 10;
        long expectedStartingCombatantsOnDefendingSide = -1;
        long expectedCombatantsLostByAttacker = 10;
        long expectedCombatantsLostByDefender = -1;

        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new AttackCombatantsChanges(
                expectedStartingCombatantsOnAttackingSide,
                expectedStartingCombatantsOnDefendingSide,
                expectedCombatantsLostByAttacker,
                expectedCombatantsLostByDefender
            );
        });

        Assert.IsTrue(actual.Message.Contains("The number of starting combatants on the defender's side cannot be less than zero."));
    }

    [TestMethod]
    public void Constructor_ShouldThrowArgumentOutOfRangeExceptionIfCombatantsLostByAttackerIsLessThanZero()
    {
        long expectedStartingCombatantsOnAttackingSide = 10;
        long expectedStartingCombatantsOnDefendingSide = 10;
        long expectedCombatantsLostByAttacker = -1;
        long expectedCombatantsLostByDefender = 10;

        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new AttackCombatantsChanges(
                expectedStartingCombatantsOnAttackingSide,
                expectedStartingCombatantsOnDefendingSide,
                expectedCombatantsLostByAttacker,
                expectedCombatantsLostByDefender
            );
        });

        Assert.IsTrue(actual.Message.Contains("The number of combatants lost on the attacker's side cannot be less than zero."));
    }

    [TestMethod]
    public void Constructor_ShouldThrowArgumentOutOfRangeExceptionIfCombatantsLostByDefenderIsLessThanZero()
    {
        long expectedStartingCombatantsOnAttackingSide = 10;
        long expectedStartingCombatantsOnDefendingSide = 10;
        long expectedCombatantsLostByAttacker = 10;
        long expectedCombatantsLostByDefender = -1;

        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new AttackCombatantsChanges(
                expectedStartingCombatantsOnAttackingSide,
                expectedStartingCombatantsOnDefendingSide,
                expectedCombatantsLostByAttacker,
                expectedCombatantsLostByDefender
            );
        });

        Assert.IsTrue(actual.Message.Contains("The number of combatants lost on the defender's side cannot be less than zero."));
    }

    [TestMethod]
    public void Constructor_ShouldThrowArgumentExceptionIfStartingCombatantsOnAttackingSideIsLessThanCombatantsLostByAttacker()
    {
        long expectedStartingCombatantsOnAttackingSide = 10;
        long expectedStartingCombatantsOnDefendingSide = 10;
        long expectedCombatantsLostByAttacker = 11;
        long expectedCombatantsLostByDefender = 10;

        var actual = Assert.ThrowsException<ArgumentException>(() =>
        {
            _ = new AttackCombatantsChanges(
                expectedStartingCombatantsOnAttackingSide,
                expectedStartingCombatantsOnDefendingSide,
                expectedCombatantsLostByAttacker,
                expectedCombatantsLostByDefender
            );
        });

        Assert.IsTrue(actual.Message.Contains("The number of combatants lost by the attacker cannot be greater than the number they started with."));
    }

    [TestMethod]
    public void Constructor_ShouldThrowArgumentExceptionIfStartingCombatantsOnDefendingSideIsLessThanCombatantsLostByDefender()
    {
        long expectedStartingCombatantsOnAttackingSide = 10;
        long expectedStartingCombatantsOnDefendingSide = 10;
        long expectedCombatantsLostByAttacker = 10;
        long expectedCombatantsLostByDefender = 11;

        var actual = Assert.ThrowsException<ArgumentException>(() =>
        {
            _ = new AttackCombatantsChanges(
                expectedStartingCombatantsOnAttackingSide,
                expectedStartingCombatantsOnDefendingSide,
                expectedCombatantsLostByAttacker,
                expectedCombatantsLostByDefender
            );
        });

        Assert.IsTrue(actual.Message.Contains("The number of combatants lost by the defender cannot be greater than the number they started with."));
    }

    [TestMethod]
    public void Constructor_ShouldInitializeInstanceWithExpectedValues()
    {
        long expectedStartingCombatantsOnAttackingSide = 10;
        long expectedStartingCombatantsOnDefendingSide = 20;
        long expectedCombatantsLostByAttacker = 1;
        long expectedCombatantsLostByDefender = 2;

        var actual = new AttackCombatantsChanges(
            expectedStartingCombatantsOnAttackingSide,
            expectedStartingCombatantsOnDefendingSide,
            expectedCombatantsLostByAttacker,
            expectedCombatantsLostByDefender
        );

        Assert.AreEqual(expectedStartingCombatantsOnAttackingSide, actual.StartingCombatantsOnAttackingSide);
        Assert.AreEqual(expectedStartingCombatantsOnDefendingSide, actual.StartingCombatantsOnDefendingSide);
        Assert.AreEqual(expectedCombatantsLostByAttacker, actual.CombatantsLostByAttacker);
        Assert.AreEqual(expectedCombatantsLostByDefender, actual.CombatantsLostByDefender);
    }
}
