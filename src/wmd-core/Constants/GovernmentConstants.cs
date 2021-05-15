namespace WMD.Game.Constants
{
    /// <summary>
    /// Provides constants related to the government.
    /// </summary>
    public static class GovernmentConstants
    {
        /// <summary>
        /// The base amount of money the government steal back from a player.
        /// </summary>
        public const decimal BaseAmountOfMoneyTakenBack = 100m;

        /// <summary>
        /// The base amount of reputation a player loses when the government denounces them.
        /// </summary>
        public const int BaseAmountOfReputationLost = 5;

        /// <summary>
        /// The base chance that the government will issue an intervention.
        /// </summary>
        public const double BaseChanceOfGovernmentIntervention = 0.1;

        /// <summary>
        /// The initial percentage of the planet's population that starts in the government's army.
        /// </summary>
        public const double InitialPercentageOfPlanetPopulationInGovernmentArmy = 0.1;

        /// <summary>
        /// The minimum reputation percentage a player must have before the government will issue interventions against them.
        /// </summary>
        public const int MinimumNoticeableReputationPercentage = 10;
    }
}
