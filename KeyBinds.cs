namespace Frosthold
{
    public class KeyBinds
    {
        private Player player;

        private GameController gc;
        public InputParser ip { get; }

        public KeyBinds(InputParser ip)
        {
            this.ip = ip;
            this.gc = GameController.Instance;
            this.player = this.gc.player;
        }
    }
}