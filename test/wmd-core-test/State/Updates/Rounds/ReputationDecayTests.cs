using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Updates.Rounds;

namespace WMD.Game.Test.State.Updates.Rounds
{
    [TestClass]
    public class ReputationDecayTests
    {
        [TestMethod]
        public void Constructor_ShouldCreateObjectWithGivenValues()
        {
            int expectedPlayerIndex = 2;
            int expectedReputationPercentageLost = 3;

            var actualRecord = new ReputationDecay(expectedPlayerIndex, expectedReputationPercentageLost);

            Assert.AreEqual(expectedPlayerIndex, actualRecord.PlayerIndex);
            Assert.AreEqual(expectedReputationPercentageLost, actualRecord.ReputationPercentageLost);
        }
    }
}
