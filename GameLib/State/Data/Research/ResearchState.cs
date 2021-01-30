namespace WMD.Game.State.Data.Research
{
    /// <summary>
    /// Holds current state for a player's research.
    /// </summary>
    public record ResearchState(int NukeResearchLevel = 0)
    {
        /// <summary>
        /// The maximum level for nukes research a player may attain.
        /// </summary>
        public const int MaxNukeResearchLevel = 10;

        /// <summary>
        /// The cost for gaining another level of nukes research.
        /// </summary>
        public const decimal NukeResearchLevelCost = 500;
    }
}
