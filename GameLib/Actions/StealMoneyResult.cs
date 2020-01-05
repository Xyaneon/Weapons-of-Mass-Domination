namespace WMD.Game.Actions
{
    public class StealMoneyResult : ActionResult
    {
        public StealMoneyResult(Player player, decimal stolenAmount) : base(player)
        {
            StolenAmount = stolenAmount;
        }

        public decimal StolenAmount { get; }
    }
}
