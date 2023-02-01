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
        public KeyBinds(InputParser ip)
        {
            this.ip = ip;
            this.gc = GameController.Instance;
            this.player = this.gc.player;

           
        }

      
    }
}
