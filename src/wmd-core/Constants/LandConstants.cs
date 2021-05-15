namespace WMD.Game.Constants
{
    /// <summary>
    /// Provides constants related to land.
    /// </summary>
    public static class LandConstants
    {
        /// <summary>
        /// Base price for 1 km² of land.
        /// </summary>
        public const decimal LandBasePrice = 150m;

        /// <summary>
        /// Maximum increase in price for 1 km² of land due to scarcity.
        /// </summary>
        public const decimal MaxLandPriceIncreaseFromScarcity = 1000m;
    }
}
