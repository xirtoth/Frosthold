using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
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

        private void AddKeys()
        {
            ip.AddKey(ConsoleKey.RightArrow, () => gc.player.MovePlayer(1, 0));
            ip.AddKey(ConsoleKey.LeftArrow, () => gc.player.MovePlayer(-1, -0));
            ip.AddKey(ConsoleKey.UpArrow, () => gc.player.MovePlayer(0, -1));
            ip.AddKey(ConsoleKey.DownArrow, () => gc.player.MovePlayer(0, 1));
            ip.AddKey(ConsoleKey.Spacebar, () => gc.Inspect());
            ip.AddKey(ConsoleKey.K, () => gc.screen.PrintMap());
            ip.AddKey(ConsoleKey.Q, () =>  Environment.Exit(0));
        }
    }
}
