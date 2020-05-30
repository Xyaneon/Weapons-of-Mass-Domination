using System;
using System.Collections.Generic;
using System.Text;
using WMD.Game.Players;

namespace WMD.Game.Commands
{
    /// <summary>
    /// Represents the result of a player attacking another player.
    /// </summary>
    public class AttackPlayerResult: CommandResult
    {
        public AttackPlayerResult(Player player, GameState gameState) : base(player, gameState) { }
    }
}
