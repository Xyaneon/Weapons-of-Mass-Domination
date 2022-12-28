using System;
using WMD.Game.Commands;
using WMD.Game.State.Data;

namespace WMD.AI.Default;

internal sealed class AttackGovernmentArmyInputRetriever : ICommandInputRetriever
{
    public CommandInput? GetCommandInput(GameState gameState)
    {
        if (!HasHenchmenToAttackWith(gameState))
        {
            return null;
        }

        long henchmenToAttackWith = CalculateHenchmenToAttackWith(gameState);

        return new AttackGovernmentArmyInput() { NumberOfAttackingHenchmen = henchmenToAttackWith };
    }

    private static long CalculateHenchmenToAttackWith(GameState gameState) =>
        Math.Max(gameState.CurrentPlayer.State.WorkforceState.TotalHenchmenCount / 2, 1);

    private static bool HasHenchmenToAttackWith(GameState gameState) => gameState.CurrentPlayer.State.WorkforceState.TotalHenchmenCount > 0;
}
