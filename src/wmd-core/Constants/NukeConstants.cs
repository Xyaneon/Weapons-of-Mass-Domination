namespace WMD.Game.Constants;

/// <summary>
/// Provides constants related to nukes.
/// </summary>
public static class NukeConstants
{
    /// <summary>
    /// The base probability that a given nuke turns out to be a dud.
    /// </summary>
    public const double BaseDudProbability = 0.1;

    /// <summary>
    /// The maximum level for nukes research a player may attain.
    /// </summary>
    public const int MaxNukeResearchLevel = 10;

    /// <summary>
    /// The price for manufacturing a single nuke.
    /// </summary>
    public const decimal ManufacturingPrice = 1000.0M;

    /// <summary>
    /// The cost for gaining another level of nukes research.
    /// </summary>
    public const decimal NukeResearchLevelCost = 500;

    /// <summary>
    /// The amount by which a player's reputation changes for not successfully landing any hits with their nukes.
    /// </summary>
    public const int ReputationChangeAmountForNoSuccessfulNukeHits = -10;
    
    /// <summary>
    /// The amount by which a player's reputation changes for successfully landing hits with their nukes.
    /// </summary>
    public const int ReputationChangeAmountForSuccessfulNukeHits = 10;
}
