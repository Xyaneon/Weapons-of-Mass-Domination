namespace WMD.Game.Constants;

/// <summary>
/// Provides constants related to attacks.
/// </summary>
public static class AttackConstants
{
    /// <summary>
    /// The base percentage of henchmen the attacker will lose.
    /// </summary>
    public const double BasePercentageOfHenchmenAttackerLost = 0.1;

    /// <summary>
    /// The base percentage of henchmen the defender will lose.
    /// </summary>
    public const double BasePercentageOfHenchmenDefenderLost = 0.2;

    /// <summary>
    /// The base amount by which the attacker's reputation will change.
    /// </summary>
    public const int BaseReputationChangeAmountForAttacker = 5;
    
    /// <summary>
    /// The base amount by which the defender's reputation will change.
    /// </summary>
    public const int BaseReputationChangeAmountForDefender = 5;

    /// <summary>
    /// The maximum additional percentage of henchmen the attacker will lose.
    /// </summary>
    public const double MaxAdditionalPercentageOfHenchmenAttackerLost = 0.4;

    /// <summary>
    /// The maximum additional percentage of henchmen the defender will lose.
    /// </summary>
    public const double MaxAdditionalPercentageOfHenchmenDefenderLost = 0.7;

    /// <summary>
    /// The amount by which to multiply reputation changes when the defender loses all of their henchmen in an attack.
    /// </summary>
    public const int ReputationChangeFactorForDefeatedDefender = 2;
}
