namespace WMD.Game.State.Data.Planets;

/// <summary>
/// Represents Earth as a playable planet.
/// </summary>
public record Earth : Planet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Earth"/> class.
    /// </summary>
    /// <remarks>
    /// This creates a relatively realistic depiction of Earth using
    /// figures from https://en.wikipedia.org/wiki/Earth .
    /// </remarks>
    public Earth() : base("Earth", 148940000, 510072000, 361132000, 7000000000) { }
}
