using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public class KeyBinds
    {
        Player player;
        Dictionary<ConsoleKey, Action> keyMap;
        GameController gc;
        public InputParser ip { get; }
        public KeyBinds(InputParser ip, Player player)
        {
            this.ip = ip;
            this.player = player;
            this.gc = GameController.Instance;
            AddBinds();
        }

        private void AddBinds()
        {

           
        }
    }
}
