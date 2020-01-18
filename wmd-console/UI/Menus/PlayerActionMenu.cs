using WMD.Console.UI.Core;
using WMD.Game.Actions;

namespace WMD.Console.UI.Menus
{
    class PlayerActionMenu : Menu<PlayerActionKind>
    {
        public PlayerActionMenu() : base(CreateMenuOptions()) { }

        private static MenuOption<PlayerActionKind>[] CreateMenuOptions()
        {
            return new MenuOption<PlayerActionKind>[]
            {
                CreateMenuOption("Steal money", PlayerActionKind.StealMoney),
                CreateMenuOption("Purchase unclaimed land", PlayerActionKind.PurchaseUnclaimedLand),
                CreateMenuOption("Sell land", PlayerActionKind.SellLand),
                CreateMenuOption("Hire minions", PlayerActionKind.HireMinions),
                CreateMenuOption("Build/upgrade your secret base", PlayerActionKind.UpgradeSecretBase),
                CreateMenuOption("Skip turn", PlayerActionKind.Skip),
                CreateMenuOption("Resign", PlayerActionKind.Resign)
            };
        }

        private static MenuOption<PlayerActionKind> CreateMenuOption(string optionName, PlayerActionKind value)
        {
            return new MenuOption<PlayerActionKind>(optionName, value);
        }
    }
}
