namespace WMD.Game.Actions
{
    /// <summary>
    /// Indicates which kind of action the player wants to take.
    /// </summary>
    public enum PlayerActionKind
    {
        /// <summary>
        /// Indicates that the current player is building a secret base.
        /// </summary>
        BuildSecretBase,
        /// <summary>
        /// Indicates that the current player is hiring henchmen.
        /// </summary>
        HireHenchmen,
        /// <summary>
        /// Indicates that the current player is purchasing unclaimed land.
        /// </summary>
        PurchaseUnclaimedLand,
        /// <summary>
        /// Indicates that the current player is resigning.
        /// </summary>
        Resign,
        /// <summary>
        /// Indicates that the current player is selling some of the land they control.
        /// </summary>
        SellLand,
        /// <summary>
        /// Indicates that the current player is skipping their turn.
        /// </summary>
        Skip,
        /// <summary>
        /// Indicates that the current player is stealing money.
        /// </summary>
        StealMoney,
        /// <summary>
        /// Indicates that the current player is upgrading their secret base.
        /// </summary>
        UpgradeSecretBase
    }
}
