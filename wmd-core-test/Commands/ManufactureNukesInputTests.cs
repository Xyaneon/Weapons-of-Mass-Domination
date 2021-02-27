using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands
{
    [TestClass]
    public class ManufactureNukesInputTests
    {
        [TestMethod]
        public void NumberOfNukesToManufacture_ShouldThrowIfProvidedValueIsLessThanOne()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new ManufactureNukesInput() with { NumberOfNukesToManufacture = 0 };
            });

            Assert.IsTrue(actual.Message.Contains("The number of nukes to manufacture must be greater than zero."));
        }
    }
}
