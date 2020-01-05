namespace WMD.Game.Actions
{
    public abstract class ActionResult
    {
        public ActionResult(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
    }
}
