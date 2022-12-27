namespace WMD.Game.State.Data.Players;

/// <summary>
/// Contains information that can be used to visually identify a <see cref="Player"/>.
/// </summary>
/// <param name="Name">The player's name.</param>
/// <param name="Color">The player's color as a <see cref="PlayerColor"/> value.</param>
/// <param name="IsHuman">Whether this is a human player (<see langword="true"/>) or a CPU player (<see langword="false"/>).</param>
public record PlayerIdentification(string Name, PlayerColor Color, bool IsHuman);
