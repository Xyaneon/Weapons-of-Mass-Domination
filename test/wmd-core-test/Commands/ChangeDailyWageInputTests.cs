using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands
{
    [TestClass]
    public class ChangeDailyWageInputTests
    {
        [TestMethod]
        public void NewDailyWage_ShouldThrowIfProvidedValueIsLessThanZero()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new ChangeDailyWageInput() with { NewDailyWage = -1 };
            });

            Assert.IsTrue(actual.Message.Contains("The new daily wage cannot be less than zero."));
        }
    }
}
