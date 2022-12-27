using System;
using WMD.Game.State.Data.Henchmen;

namespace WMD.Game.Commands;

/// <summary>
/// Additional data for the train henchmen command.
/// </summary>
public record TrainHenchmenInput : CommandInput
{
    private const string ArgumentOutOfRangeException_InvalidSpecializationValue = "The provided value is not a valid specialization value.";
    private const string ArgumentOutOfRangeException_NumberToTrainLessThanOne = "The number of henchmen to train must be greater than zero.";
    private const string ArgumentOutOfRangeException_UntrainedSpecializationValue = "The provided value cannot be for the untrained specialization.";

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

    /// <summary>
    /// Gets or initializes the specialization to train the henchmen in.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The provided value is not a valid <see cref="HenchmenSpecialization"/> value.
    /// -or-
    /// The provided value is <see cref="HenchmenSpecialization.Untrained"/>.
    /// </exception>
    public HenchmenSpecialization Specialization
    {
        get => _specialization;
        init
        {
            if (!Enum.IsDefined<HenchmenSpecialization>(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_InvalidSpecializationValue);
            }

            if (value == HenchmenSpecialization.Untrained)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ArgumentOutOfRangeException_UntrainedSpecializationValue);
            }

            _specialization = value;
        }
    }

    private long _numberToTrain;
    private HenchmenSpecialization _specialization;
}
