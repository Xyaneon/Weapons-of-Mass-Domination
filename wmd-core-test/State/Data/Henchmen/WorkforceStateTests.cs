using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data.Henchmen;

namespace WMD.Game.Test.State.Data.Henchmen
{
    [TestClass]
    public class WorkforceStateTests
    {
        [TestMethod]
        public void DefaultConstructor_ShouldCreateInstanceWithExpectedDefaultValues()
        {
            var subject = new WorkforceState();

            Assert.AreEqual(0, subject.NumberOfHenchmen);
            Assert.AreEqual(7, subject.DailyPayRate);
        }
    }
}
