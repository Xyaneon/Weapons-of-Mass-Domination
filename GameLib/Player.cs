namespace WMD.Game
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            Money = 0;
        }

        public decimal Money { get; set; }

        public string Name { get; }
    }
}
