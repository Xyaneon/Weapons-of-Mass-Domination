using WMD.Game.State.Data;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player researching nukes.
    /// </summary>
    public record ResearchNukesResult : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellLandResult"/> class.
        /// </summary>
        /// <param name="updatedGameState">The updated <see cref="GameState"/> resulting from this action.</param>
        /// <param name="playerIndex">The <see cref="Player"/> whose action this is the result of.</param>
        /// <param name="newNukesResearchLevel">The player's new research level for nukes.</param>
        /// <param name="totalResearchPrice">The total price of the research.</param>
        public ResearchNukesResult(GameState updatedGameState, int playerIndex, int newNukesResearchLevel, decimal totalResearchPrice) : base(updatedGameState, playerIndex)
        {
            NewNukesResearchLevel = newNukesResearchLevel;
            TotalResearchPrice = totalResearchPrice;
        }

        /// <summary>
        /// Gets the new nukes research level.
        /// </summary>
        public int NewNukesResearchLevel { get; init; }

        /// <summary>
        /// Gets the total price of the research.
        /// </summary>
        public decimal TotalResearchPrice { get; init; }
    }
}
