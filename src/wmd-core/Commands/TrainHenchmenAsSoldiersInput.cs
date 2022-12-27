using System;

namespace WMD.Game.Commands;

/// <summary>
/// Additional data for the train henchmen as soldiers command.
/// </summary>
/// <value></value>
public record TrainHenchmenAsSoldiersInput : CommandInput
{
    private const string ArgumentOutOfRangeException_NumberToTrainLessThanOne = "The number of henchmen to train must be greater than zero.";

    /// <summary>
    /// Gets or initializes the number of henchmen to train.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is less than one.
    /// </exception>
    public long NumberToTrain
    {
        get => _numberToTrain;
        init
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_NumberToTrainLessThanOne);
            }
            _numberToTrain = value;
        }
    }

    private long _numberToTrain;
}