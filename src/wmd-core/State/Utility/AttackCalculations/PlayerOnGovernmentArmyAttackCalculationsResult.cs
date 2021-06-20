namespace WMD.Game.State.Utility.AttackCalculations
{
    internal sealed record PlayerOnGovernmentArmyAttackCalculationsResult(
        int HenchmenAttackerLost,
        int SoldiersGovernmentArmyLost,
        int ReputationChangeForAttacker
    );
}
