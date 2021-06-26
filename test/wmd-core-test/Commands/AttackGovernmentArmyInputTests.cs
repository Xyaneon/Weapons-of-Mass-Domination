using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands
{
    [TestClass]
    public class AttackGovernmentArmyInputTests
    {
        [TestMethod]
        public void NumberOfAttackingHenchmen_ShouldThrowIfProvidedValueIsLessThanOne()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new AttackGovernmentArmyInput() with { NumberOfAttackingHenchmen = 0 };
            });

            Assert.IsTrue(actual.Message.Contains("The number of attacking henchmen cannot be less than one."));
        }
    }
}
