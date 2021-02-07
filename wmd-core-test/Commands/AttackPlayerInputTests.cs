﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands
{
    [TestClass]
    public class AttackPlayerInputTests
    {
        [TestMethod]
        public void TargetPlayerIndex_ShouldThrowIfProvidedValueIsLessThanZero()
        {
            var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = new AttackPlayerInput() with { TargetPlayerIndex = -1 };
            });

            Assert.IsTrue(actual.Message.Contains("The target player index cannot be less than zero."));
        }
    }
}
