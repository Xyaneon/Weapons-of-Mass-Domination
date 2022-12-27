using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Game.Test.State.Updates.Rounds;

[TestClass]
public class ReputationChangeTests
{
    [TestMethod]
    public void Constructor_ShouldCreateObjectWithGivenValues()
    {
        int expectedPlayerIndex = 2;
        int expectedReputationPercentageChange = 3;

        var actualRecord = new ReputationChange(expectedPlayerIndex, expectedReputationPercentageChange);

        Assert.AreEqual(expectedPlayerIndex, actualRecord.PlayerIndex);
        Assert.AreEqual(expectedReputationPercentageChange, actualRecord.ReputationPercentageChanged);
    }
}
