namespace WMD.Game.Constants
{
    /// <summary>
    /// Provides constants related to henchmen.
    /// </summary>
    public static class HenchmenConstants
    {
        /// <summary>
        /// The minimum daily wage.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <see cref="State.Data.Henchmen.WorkforceState.DailyPayRate"/> property is initialized to this value by default.
        /// </para>
        /// <para>
        /// If a player chooses to pay less than this amount, then their henchmen will be much more likely to quit.
        /// </para>
        /// </remarks>
        public const decimal MinimumDailyWage = 7;
    }
}
