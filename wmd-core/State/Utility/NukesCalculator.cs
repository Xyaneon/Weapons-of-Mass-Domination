using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.Constants;
using WMD.Game.State.Data;

namespace WMD.Game.State.Utility
{
    /// <summary>
    /// Provides methods for performing nukes-related calculations.
    /// </summary>
    public static class NukesCalculator
    {
        /// <summary>
        /// Calculates the total manufacturing price for the given quantity of nukes the current player will manufacture.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The total manufacturing price for the given quantity of nukes the current player will manufacture.</returns>
        /// <remarks>This method does not take the current status of nuke manufacturing prerequisites for the current player into account; it assumes they are already met.</remarks>
        public static decimal CalculateTotalManufacturingPrice([DisallowNull] GameState gameState, int quantity)
        {
            return NukeConstants.ManufacturingPrice * quantity;
        }

        /// <summary>
        /// Calculates the maximum amount of nukes the current player could manufacture with their funds.
        /// </summary>
        /// <param name="gameState">The current <see cref="GameState"/>.</param>
        /// <returns>The maximum amount of nukes the current player could manufacture with their funds.</returns>
        /// <remarks>This method does not take the current status of nuke manufacturing prerequisites for the current player into account; it assumes they are already met.</remarks>
        public static int CalculateMaximumNumberOfNukesCurrentPlayerCouldManufacture([DisallowNull] GameState gameState)
        {
            return (int)Math.Floor(gameState.CurrentPlayer.State.Money / NukeConstants.ManufacturingPrice);
        }
    }
}
