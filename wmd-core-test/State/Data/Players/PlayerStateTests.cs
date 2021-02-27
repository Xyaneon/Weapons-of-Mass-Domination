using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Data.Research;

namespace WMD.Game.Test.State.Data.Players
{
    [TestClass]
    public class PlayerStateTests
    {
        [TestMethod]
        public void DefaultConstructor_ShouldSetPropertiesToExpectedValues()
        {
            var expectedResearchState = new ResearchState();
            var expectedWorkforceState = new WorkforceState();

            var actual = new PlayerState();

            Assert.IsFalse(actual.HasResigned);
            Assert.AreEqual(0, actual.Land);
            Assert.AreEqual(0, actual.Money);
            Assert.AreEqual(0, actual.Nukes);
            Assert.AreEqual(expectedResearchState, actual.ResearchState);
            Assert.IsNull(actual.SecretBase);
            Assert.AreEqual(expectedWorkforceState, actual.WorkforceState);
        }
    }
}
