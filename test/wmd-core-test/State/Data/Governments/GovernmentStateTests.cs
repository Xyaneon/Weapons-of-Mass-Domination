using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data.Governments;

namespace WMD.Game.Test.State.Data.Governments;

[TestClass]
public class GovernmentStateTests
{
    [TestMethod]
    public void DefaultConstructor_ShouldCreateInstanceWithExpectedDefaultValues()
    {
        const int expectedNumberOfSoldiers = 0;

        var subject = new GovernmentState();

        Assert.AreEqual(expectedNumberOfSoldiers, subject.NumberOfSoldiers);
    }

    [TestMethod]
    public void NumberOfSoldiers_ShouldInitializeWithProvidedValue()
    {
        const int expectedNumberOfSoldiers = 123;

        var subject = new GovernmentState() with { NumberOfSoldiers = expectedNumberOfSoldiers };

        Assert.AreEqual(expectedNumberOfSoldiers, subject.NumberOfSoldiers);
    }

    [TestMethod]
    public void NumberOfSoldiers_ShouldThrowIfInitializedWithNegativeValue()
    {
        const int numberOfSoldiers = -1;
        const string expectedMessage = "The number of soldiers cannot be less than zero.";

        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new GovernmentState() with { NumberOfSoldiers = numberOfSoldiers }
        );

        Assert.IsTrue(actual.Message.Contains(expectedMessage));
    }
}
