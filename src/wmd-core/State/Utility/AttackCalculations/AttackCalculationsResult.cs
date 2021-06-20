namespace WMD.Game.State.Utility.AttackCalculations
{
    internal sealed record AttackCalculationsResult(
        int HenchmenAttackerLost,
        int HenchmenDefenderLost,
        int ReputationChangeForAttacker,
        int ReputationChangeForDefender,
        int LandAreaChangeForDefender
    );
}
