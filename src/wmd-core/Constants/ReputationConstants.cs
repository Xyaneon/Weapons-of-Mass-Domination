namespace WMD.Game.Constants
{
    /// <summary>
    /// Provides constants related to reputation.
    /// </summary>
    public static class ReputationConstants
    {
        /// <summary>
        /// The maximum effective amount of money a player can spend on a propaganda distribution action
        /// and still expect it to potentially increase the amount of reputation they can get from doing so.
        /// </summary>
        public const decimal MaxEffectiveSpendingAmountOnPropagandaDistribution = 200.0M;

        /// <summary>
        /// The maximum amount of reputation a player can obtain from a single propaganda distribution command.
        /// </summary>
        public const int MaxGainableReputationFromPropagandaDistribution = 20;

        /// <summary>
        /// The maximum reputation percentage a player can attain.
        /// </summary>
        public const int MaxReputationPercentage = 100;

        /// <summary>
        /// The rate at which players' reputation changes naturally between rounds.
        /// </summary>
        public const int ReputationChangeRate = 1;
    }
}
