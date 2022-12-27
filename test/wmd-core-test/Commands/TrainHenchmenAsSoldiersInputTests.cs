using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;

namespace WMD.Game.Test.Commands;

[TestClass]
public class TrainHenchmenAsSoldiersInputTests
{
    [TestMethod]
    public void NumberToTrain_ShouldThrowIfProvidedValueIsLessThanOne()
    {
        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new TrainHenchmenAsSoldiersInput { NumberToTrain = 0 };
        });

        Assert.IsTrue(actual.Message.Contains("The number of henchmen to train must be greater than zero."));
    }
}