using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands
{
    [TestClass]
    public class DistributePropagandaInputTests
    {
        [TestMethod]
        public void DefaultConstructor_ShouldCreateWithExpectedDefaultPropertyValues()
        {
            var actual = new DistributePropagandaInput();

            Assert.AreEqual(0.0M, actual.MoneyToSpend);
        }

        [TestMethod]
        public void Constructor_ShouldThrowWhenMoneyToSpendIsLessThanZero()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new DistributePropagandaInput() with { MoneyToSpend = -1.0M };
            });

            Assert.IsTrue(actual.Message.Contains("The amount of money to spend on distributing propaganda cannot be less than zero."));
        }
    }
}
