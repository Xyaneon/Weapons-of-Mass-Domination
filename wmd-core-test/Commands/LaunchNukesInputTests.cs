using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands
{
    [TestClass]
    public class LaunchNukesInputTests
    {
        [TestMethod]
        public void NumberOfNukes_ShouldThrowIfProvidedValueIsLessThanOne()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new LaunchNukesInput() with { NumberOfNukesLaunched = 0 };
            });

            Assert.IsTrue(actual.Message.Contains("The number of nukes to launch cannot be less than one."));
        }
        
        [TestMethod]
        public void TargetPlayerIndex_ShouldThrowIfProvidedValueIsLessThanZero()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new LaunchNukesInput() with { TargetPlayerIndex = -1 };
            });

            Assert.IsTrue(actual.Message.Contains("The target player index cannot be less than zero."));
        }
    }
}
