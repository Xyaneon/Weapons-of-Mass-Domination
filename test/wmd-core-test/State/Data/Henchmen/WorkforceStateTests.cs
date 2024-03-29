﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMD.Game.State.Data.Henchmen;

namespace WMD.Game.Test.State.Data.Henchmen;

[TestClass]
public class WorkforceStateTests
{
    [TestMethod]
    public void DefaultConstructor_ShouldCreateInstanceWithExpectedDefaultValues()
    {
        const int expectedGenericHenchmenCount = 0;
        const decimal expectedDailyPayRate = 7;
        const decimal expectedTotalDailyPay = expectedGenericHenchmenCount * expectedDailyPayRate;

        var subject = new WorkforceState {};

        Assert.AreEqual(expectedGenericHenchmenCount, subject.TotalHenchmenCount);
        Assert.AreEqual(expectedDailyPayRate, subject.DailyPayRate);
        Assert.AreEqual(expectedTotalDailyPay, subject.TotalDailyPay);
    }

    [TestMethod]
    public void Constructor_ShouldCreateInstanceWithExpectedValues()
    {
        const int expectedGenericHenchmenCount = 5;
        const decimal expectedDailyPayRate = 3;
        const decimal expectedTotalDailyPay = expectedGenericHenchmenCount * expectedDailyPayRate;

        var subject = new WorkforceState {
            DailyPayRate = expectedDailyPayRate,
            GenericHenchmenCount = expectedGenericHenchmenCount,
        };

        Assert.AreEqual(expectedGenericHenchmenCount, subject.TotalHenchmenCount);
        Assert.AreEqual(expectedDailyPayRate, subject.DailyPayRate);
        Assert.AreEqual(expectedTotalDailyPay, subject.TotalDailyPay);
    }

    [TestMethod]
    public void Constructor_ShouldThrowIfDailyPayRateIsNegative()
    {
        const int genericHenchmenCount = 5;
        const decimal dailyPayRate = -1;
        const string expectedMessage = "The daily pay rate cannot be less than zero.";

        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new WorkforceState { DailyPayRate = dailyPayRate, GenericHenchmenCount = genericHenchmenCount }
        );
        
        Assert.IsTrue(actual.Message.Contains(expectedMessage));
    }

    [TestMethod]
    public void Constructor_ShouldThrowIfNumberOfHenchmenIsNegative()
    {
        const int genericHenchmenCount = -1;
        const decimal dailyPayRate = 7;
        const string expectedMessage = "The number of generic henchmen cannot be less than zero.";

        var actual = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new WorkforceState { DailyPayRate = dailyPayRate, GenericHenchmenCount = genericHenchmenCount }
        );

        Assert.IsTrue(actual.Message.Contains(expectedMessage));
    }
}
