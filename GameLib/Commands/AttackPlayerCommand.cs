using System;
using System.Diagnostics.CodeAnalysis;
using WMD.Game.State.Data;
using WMD.Game.State.Updates;

namespace WMD.Game.Commands
{
    /// <summary>
    /// The command for the current player attacking another player.
    /// </summary>
    public class AttackPlayerCommand : GameCommand<AttackPlayerInput, AttackPlayerResult>
    {
        private const string InvalidOperationException_playerAttackingThemselves = "A player cannot attack themselves.";
        private const string InvalidOperationException_targetPlayerIndexOutsideBounds = "The target player index is outside the player list bounds.";
        private const double BasePercentageOfHenchmenAttackerLost = 0.1;
        private const double MaxAdditionalPercentageOfHenchmenAttackerLost = 0.4;
        private const double BasePercentageOfHenchmenDefenderLost = 0.2;
        private const double MaxAdditionalPercentageOfHenchmenDefenderLost = 0.7;

        static AttackPlayerCommand()
        {
            _random = new Random();
        }

        private static readonly Random _random;

        public override bool CanExecuteForState([DisallowNull] GameState gameState)
        {
            return true;
        }

        public override bool CanExecuteForStateAndInput([DisallowNull] GameState gameState, [DisallowNull] AttackPlayerInput input)
        {
            return !CurrentPlayerIsAttackingThemselves(gameState, input) && TargetPlayerFound(gameState, input);
        }

        public override AttackPlayerResult Execute([DisallowNull] GameState gameState, [DisallowNull] AttackPlayerInput input)
        {
            if (CurrentPlayerIsAttackingThemselves(gameState, input))
            {
                throw new InvalidOperationException(InvalidOperationException_playerAttackingThemselves);
            }

            if (!TargetPlayerFound(gameState, input))
            {
                throw new InvalidOperationException(InvalidOperationException_targetPlayerIndexOutsideBounds);
            }

            double percentageOfAttackerHenchmenLost = CalculatePercentageOfHenchmenAttackerLost();
            double percentageOfDefenderHenchmenLost = CalculatePercentageOfHenchmenDefenderLost();
            int henchmenAttackerLost = CalculateNumberOfHenchmenAttackerLost(gameState, percentageOfAttackerHenchmenLost);
            int henchmenDefenderLost = CalculateNumberOfHenchmenDefenderLost(gameState, input, percentageOfDefenderHenchmenLost);

            GameState updatedGameState = GameStateUpdater.AdjustHenchmenForPlayer(gameState, gameState.CurrentPlayerIndex, -1 * henchmenAttackerLost);
            updatedGameState = GameStateUpdater.AdjustHenchmenForPlayer(updatedGameState, input.TargetPlayerIndex, -1 * henchmenDefenderLost);

            return new AttackPlayerResult(updatedGameState, gameState.CurrentPlayerIndex, input.TargetPlayerIndex, henchmenAttackerLost, henchmenDefenderLost);
        }

        private static int CalculateNumberOfHenchmenDefenderLost(GameState gameState, AttackPlayerInput input, double percentageOfDefenderHenchmenLost)
        {
            return (int)Math.Round(gameState.Players[input.TargetPlayerIndex].State.WorkforceState.NumberOfHenchmen * percentageOfDefenderHenchmenLost);
        }

        private static int CalculateNumberOfHenchmenAttackerLost(GameState gameState, double percentageOfAttackerHenchmenLost)
        {
            return (int)Math.Round(gameState.CurrentPlayer.State.WorkforceState.NumberOfHenchmen * percentageOfAttackerHenchmenLost);
        }

        private static double CalculatePercentageOfHenchmenDefenderLost()
        {
            return BasePercentageOfHenchmenDefenderLost + _random.NextDouble() * MaxAdditionalPercentageOfHenchmenDefenderLost;
        }

        private static double CalculatePercentageOfHenchmenAttackerLost()
        {
            return BasePercentageOfHenchmenAttackerLost + _random.NextDouble() * MaxAdditionalPercentageOfHenchmenAttackerLost;
        }

        private static bool CurrentPlayerIsAttackingThemselves(GameState gameState, AttackPlayerInput input)
        {
            return gameState.CurrentPlayerIndex == input.TargetPlayerIndex;
        }

        private static bool TargetPlayerFound(GameState gameState, AttackPlayerInput input)
        {
            return input.TargetPlayerIndex < gameState.Players.Count;
        }
    }
}
