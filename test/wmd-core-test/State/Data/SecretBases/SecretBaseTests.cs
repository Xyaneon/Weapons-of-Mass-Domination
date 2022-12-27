using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data.SecretBases;

namespace WMD.Game.Test.State.Data.SecretBases;

[TestClass]
public class SecretBaseTests
{
    [TestMethod]
    public void DefaultConstructor_ShouldSetExpectedPropertyValues()
    {
        var actual = new SecretBase();

        Assert.AreEqual(1, actual.Level);
    }

    [TestMethod]
    public void Level_Init_ShouldThrowIfValueIsLessThanOne()
    {
        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new SecretBase() with { Level = 0 };
        });

        Assert.IsTrue(actual.Message.Contains("A secret base's level cannot be less than one."));
    }

    [TestMethod]
    public void CalculateUpgradePrice_ShouldReturnSecretBaseBuildPriceIfSecretBaseIsNull()
    {
        Assert.AreEqual(SecretBase.SecretBaseBuildPrice, SecretBase.CalculateUpgradePrice(null));
    }

    [TestMethod]
    public void CalculateUpgradePrice_ShouldReturnLevelTimesSecretBaseUpgradeFactorIfSecretBaseIsNotNull()
    {
        const int level = 5;
        var secretBase = new SecretBase() with { Level = level };
        const decimal expectedPrice = level * SecretBase.SecretBaseUpgradeFactor;

        Assert.AreEqual(expectedPrice, SecretBase.CalculateUpgradePrice(secretBase));
    }
}
