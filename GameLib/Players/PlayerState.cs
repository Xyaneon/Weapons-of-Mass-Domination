using WMD.Game.Henchmen;

namespace WMD.Game.Players
{
    /// <summary>
    /// Holds current state for an individual <see cref="Player"/>.
    /// </summary>
    public record PlayerState
    {
        /// <summary>
        /// Gets whether this player has resigned.
        /// </summary>
        public bool HasResigned { get; init; } = false;

        /// <summary>
        /// The amount of land this player controls, in square kilometers.
        /// </summary>
        public int Land { get; init; } = 0;

        /// <summary>
        /// Gets how much money this player has.
        /// </summary>
        public decimal Money { get; init; } = 0;

        /// <summary>
        /// Gets this player's secret base.
        /// </summary>
        /// <remarks>
        /// The value of this property is <see langword="null"/> if the player
        /// does not have a base.
        /// </remarks>
        public SecretBase? SecretBase { get; init; } = null;

        /// <summary>
        /// Gets this player's current workforce state.
        /// </summary>
        public WorkforceState WorkforceState { get; init; } = new WorkforceState();
    }
}
