﻿namespace Frosthold
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
            ip.AddKey(ConsoleKey.G, () => { gc.ChangeMap(); });
        }
    }
}