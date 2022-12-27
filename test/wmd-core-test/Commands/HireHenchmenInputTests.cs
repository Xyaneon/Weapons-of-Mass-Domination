using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands;

[TestClass]
public class HireHenchmenInputTests
{
    [TestMethod]
    public void OpenPositionsOffered_ShouldThrowIfProvidedValueIsLessThanOne()
    {
        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new HireHenchmenInput() with { OpenPositionsOffered = 0 };
        });

        Assert.IsTrue(actual.Message.Contains("The number of open positions offered must be greater than zero."));
    }
}
