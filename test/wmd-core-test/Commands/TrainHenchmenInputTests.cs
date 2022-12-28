using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.Commands;
using WMD.Game.State.Data.Henchmen;

namespace WMD.Game.Test.Commands;

[TestClass]
public class TrainHenchmenInputTests
{
    [TestMethod]
    public void NumberToTrain_ShouldThrowIfProvidedValueIsLessThanOne()
    {
        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new TrainHenchmenInput { NumberToTrain = 0 };
        });

        Assert.IsTrue(actual.Message.Contains("The number of henchmen to train must be greater than zero."));
    }

    [TestMethod]
    public void Specialization_ShouldThrowIfProvidedValueIsAnInvalidEnumValue()
    {
        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new TrainHenchmenInput { Specialization = (Specialization)(-1) };
        });

        Assert.IsTrue(actual.Message.Contains("The provided value is not a valid specialization value."));
    }

    [TestMethod]
    public void Specialization_ShouldThrowIfProvidedValueIsUntrained()
    {
        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new TrainHenchmenInput { Specialization = Specialization.Untrained };
        });

        Assert.IsTrue(actual.Message.Contains("The provided value cannot be for the untrained specialization."));
    }
}
