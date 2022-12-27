namespace WMD.Game.State.Utility.AttackCalculations;

internal sealed record PlayerOnGovernmentArmyAttackCalculationsResult(
    long HenchmenAttackerLost,
    long SoldiersGovernmentArmyLost,
    int ReputationChangeForAttacker
);
