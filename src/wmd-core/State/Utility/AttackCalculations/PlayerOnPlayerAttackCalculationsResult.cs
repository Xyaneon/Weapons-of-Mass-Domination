namespace WMD.Game.State.Utility.AttackCalculations
{
    internal sealed record PlayerOnPlayerAttackCalculationsResult(
        int HenchmenAttackerLost,
        int HenchmenDefenderLost,
        int ReputationChangeForAttacker,
        int ReputationChangeForDefender,
        int LandAreaChangeForDefender
    );
}
