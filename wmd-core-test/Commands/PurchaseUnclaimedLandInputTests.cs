using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands
{
    [TestClass]
    public class PurchaseUnclaimedLandInputTests
    {
        [TestMethod]
        public void AreaToPurchase_ShouldThrowIfProvidedValueIsLessThanZero()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new PurchaseUnclaimedLandInput() with { AreaToPurchase = -1 };
            });

            Assert.IsTrue(actual.Message.Contains("The area of land to purchase cannot be negative."));
        }
    }
}
