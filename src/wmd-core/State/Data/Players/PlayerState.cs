using WMD.Game.State.Data.Henchmen;
using WMD.Game.State.Data.Research;
using WMD.Game.State.Data.SecretBases;

namespace WMD.Game.State.Data.Players
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
        /// Gets how many nukes this player has.
        /// </summary>
        public int Nukes { get; init; } = 0;

        /// <summary>
        /// Gets this player's reputation percentage.
        /// </summary>
        public int ReputationPercentage { get; init; } = 0;

        /// <summary>
        /// Gets this player's current research state.
        /// </summary>
        public ResearchState ResearchState { get; init; } = new ResearchState();

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
