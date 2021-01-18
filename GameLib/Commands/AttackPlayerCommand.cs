using System;
using WMD.Game.Henchmen;
using WMD.Game.Players;

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

        public override bool CanExecuteForState(GameState gameState)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            return true;
        }

        public override bool CanExecuteForStateAndInput(GameState gameState, AttackPlayerInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return !CurrentPlayerIsAttackingThemselves(gameState, input) && TargetPlayerFound(gameState, input);
        }

        public override AttackPlayerResult Execute(GameState gameState, AttackPlayerInput input)
        {
            if (gameState == null)
            {
                throw new ArgumentNullException(nameof(gameState));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

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

            PlayerState attackerPlayerState = gameState.CurrentPlayer.State;
            WorkforceState attackerWorkforceState = attackerPlayerState.WorkforceState;

            PlayerState defenderPlayerState = gameState.Players[input.TargetPlayerIndex].State;
            WorkforceState defenderWorkforceState = defenderPlayerState.WorkforceState;

            gameState.CurrentPlayer.State = attackerPlayerState with
            {
                WorkforceState = attackerWorkforceState with
                {
                    NumberOfHenchmen = attackerWorkforceState.NumberOfHenchmen - henchmenAttackerLost
                }
            };

            gameState.Players[input.TargetPlayerIndex].State = defenderPlayerState with
            {
                WorkforceState = defenderWorkforceState with
                {
                    NumberOfHenchmen = defenderWorkforceState.NumberOfHenchmen - henchmenDefenderLost
                }
            };

            return new AttackPlayerResult(gameState.CurrentPlayer, gameState, input.TargetPlayerIndex, henchmenAttackerLost, henchmenDefenderLost);
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
