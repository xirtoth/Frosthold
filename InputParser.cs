using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    internal class InputParser
    {
        public InputParser()
        {

        }

        public void ParseInput(ConsoleKeyInfo key, Player player)
        {
            if (key.Key == ConsoleKey.RightArrow)
            {
                player.MovePlayer(1, 0);
            }

            if(key.Key == ConsoleKey.LeftArrow)
            {
                player.MovePlayer(-1, 0);
            }
        }
    }
}
