using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands;

[TestClass]
public class SellLandInputTests
{
    [TestMethod]
    public void AreaToSell_ShouldThrowIfProvidedValueIsLessThanZero()
    {
        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new SellLandInput() with { AreaToSell = -1 };
        });

        Assert.IsTrue(actual.Message.Contains("The amount of land area to sell cannot be less than zero."));
    }
}
