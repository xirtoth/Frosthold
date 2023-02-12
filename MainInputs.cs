namespace Frosthold
{
    //pelitilassa käytettäviä keybindejä
    public class MainInputs : KeyBinds
    {
        public GameController gc;
        private Screen screen;
        public Player player;

        public MainInputs(Player player, Screen screen) : base(new InputParser())
        {
            this.gc = GameController.Instance;
            this.screen = screen;
            this.player = player;
            AddKeys();
        }

        //lisätään keybindejä
        private void AddKeys()
        {
            ip.AddKey(ConsoleKey.RightArrow, () => gc.player.MovePlayer(1, 0));
            ip.AddKey(ConsoleKey.LeftArrow, () => gc.player.MovePlayer(-1, -0));
            ip.AddKey(ConsoleKey.UpArrow, () => gc.player.MovePlayer(0, -1));
            ip.AddKey(ConsoleKey.DownArrow, () => gc.player.MovePlayer(0, 1));
            ip.AddKey(ConsoleKey.Spacebar, () => gc.Inspect());
            ip.AddKey(ConsoleKey.K, () => gc.screen.PrintMap());
            ip.AddKey(ConsoleKey.Q, () => gc.running = false);
            ip.AddKey(ConsoleKey.I, () => player.inventory.PrintInventory());
            ip.AddKey(ConsoleKey.P, () => player.PickupItem());
            ip.AddKey(ConsoleKey.C, () => { gc.ani.AnimationType = AnimationType.Ray; gc.ani.StartPos = player.Pos; gc.ani.EndPos = gc.OldCursorPosition; gc.ani.Start(); });
            ip.AddKey(ConsoleKey.G, () => { gc.ChangeMap(0); });
            ip.AddKey(ConsoleKey.S, () => gc.SaveGameState("file.json"));
            ip.AddKey(ConsoleKey.NumPad7, () => gc.player.MovePlayer(-1, -1));
            ip.AddKey(ConsoleKey.NumPad8, () => gc.player.MovePlayer(0, -1));
            ip.AddKey(ConsoleKey.NumPad9, () => gc.player.MovePlayer(1, -1));
            ip.AddKey(ConsoleKey.NumPad4, () => gc.player.MovePlayer(-1, 0));
            ip.AddKey(ConsoleKey.NumPad6, () => gc.player.MovePlayer(1, 0));
            ip.AddKey(ConsoleKey.NumPad1, () => gc.player.MovePlayer(-1, 1));
            ip.AddKey(ConsoleKey.NumPad2, () => gc.player.MovePlayer(0, 1));
            ip.AddKey(ConsoleKey.NumPad3, () => gc.player.MovePlayer(1, 1));
        }
    }
}