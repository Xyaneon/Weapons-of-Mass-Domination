namespace WMD.Game.State.Utility
{
    internal sealed record AttackCalculationsResult(
        int HenchmenAttackerLost,
        int HenchmenDefenderLost,
        int ReputationChangeForAttacker,
        int ReputationChangeForDefender
    );
}
