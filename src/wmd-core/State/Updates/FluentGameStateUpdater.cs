using WMD.Game.State.Data;
using WMD.Game.State.Data.Planets;
using WMD.Game.State.Data.Players;
using WMD.Game.State.Utility;

namespace WMD.Game.State.Updates
{
    internal class FluentGameStateUpdater
    {
        public FluentGameStateUpdater(GameState initialGameState) => GameState = initialGameState;

        private GameState GameState { get; set; }

        public GameState AndReturnUpdatedGameState() => GameState;

        public FluentGameStateUpdater AdjustPlayerStatesAfterAttack(int attackerIndex, int defenderIndex, AttackCalculationsResult calculationsResult)
        {
            GameState = GameStateUpdater.AdjustPlayerStatesAfterAttack(GameState, attackerIndex, defenderIndex, calculationsResult);
            return this;
        }

        public FluentGameStateUpdater ConvertNeutralPopulationToPlayerHenchmen(int playerIndex, long numberOfHenchmen)
        {
            GameState = GameStateUpdater.ConvertNeutralPopulationToPlayerHenchmen(GameState, playerIndex, numberOfHenchmen);
            return this;
        }

        public FluentGameStateUpdater ConvertPlayerHenchmenToNeutralPopulation(int playerIndex, long numberOfHenchmen)
        {
            GameState = GameStateUpdater.ConvertPlayerHenchmenToNeutralPopulation(GameState, playerIndex, numberOfHenchmen);
            return this;
        }

        public FluentGameStateUpdater GiveUnclaimedLandToPlayer(int playerIndex, int area)
        {
            GameState = GameStateUpdater.GiveUnclaimedLandToPlayer(GameState, playerIndex, area);
            return this;
        }

        public FluentGameStateUpdater HavePlayerGiveUpLand(int playerIndex, int area)
        {
            GameState = GameStateUpdater.HavePlayerGiveUpLand(GameState, playerIndex, area);
            return this;
        }

        public FluentGameStateUpdater IncrementPlayerNukesResearchLevel(int playerIndex)
        {
            GameState = GameStateUpdater.IncrementPlayerNukesResearchLevel(GameState, playerIndex);
            return this;
        }

        public FluentGameStateUpdater AdjustMoneyForPlayer(int playerIndex, decimal adjustmentAmount)
        {
            GameState = GameStateUpdater.AdjustMoneyForPlayer(GameState, playerIndex, adjustmentAmount);
            return this;
        }

        public FluentGameStateUpdater AdjustNukesForPlayer(int playerIndex, int adjustmentAmount)
        {
            GameState = GameStateUpdater.AdjustNukesForPlayer(GameState, playerIndex, adjustmentAmount);
            return this;
        }

        public FluentGameStateUpdater AdjustHenchmenForPlayer(int playerIndex, long adjustmentAmount)
        {
            GameState = GameStateUpdater.AdjustHenchmenForPlayer(GameState, playerIndex, adjustmentAmount);
            return this;
        }

        public FluentGameStateUpdater AdjustReputationForPlayer(int playerIndex, int adjustmentPercentage)
        {
            GameState = GameStateUpdater.AdjustReputationForPlayer(GameState, playerIndex, adjustmentPercentage);
            return this;
        }

        public FluentGameStateUpdater AdjustUnclaimedLandArea(int adjustmentAmount)
        {
            GameState = GameStateUpdater.AdjustUnclaimedLandArea(GameState, adjustmentAmount);
            return this;
        }

        public FluentGameStateUpdater AdjustNeutralPopulation(long adjustmentAmount)
        {
            GameState = GameStateUpdater.AdjustNeutralPopulation(GameState, adjustmentAmount);
            return this;
        }

        public FluentGameStateUpdater IncrementSecretBaseLevel(int playerIndex)
        {
            GameState = GameStateUpdater.IncrementSecretBaseLevel(GameState, playerIndex);
            return this;
        }

        public FluentGameStateUpdater SetDailyWageForPlayer(int playerIndex, decimal dailyWage)
        {
            GameState = GameStateUpdater.SetDailyWageForPlayer(GameState, playerIndex, dailyWage);
            return this;
        }

        public FluentGameStateUpdater UpdatePlanetState(Planet planet)
        {
            GameState = GameStateUpdater.UpdatePlanetState(GameState, planet);
            return this;
        }

        public FluentGameStateUpdater UpdatePlayerState(int playerIndex, PlayerState playerState)
        {
            GameState = GameStateUpdater.UpdatePlayerState(GameState, playerIndex, playerState);
            return this;
        }
    }
}
