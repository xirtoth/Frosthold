using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public class InputParser
    {
        Dictionary<ConsoleKey, Action> keyMap;
        GameController gc = GameController.Instance;
        public InputParser()
        {
            keyMap = new Dictionary<ConsoleKey, Action>();
        }

        public void AddKey(ConsoleKey key, Action action)
        {
            keyMap.Add(key, action);
        }
        public void ParseInput(ConsoleKey key)
        {
           /* if (key.Key == ConsoleKey.RightArrow)
            {
                player.MovePlayer(1, 0);
            }

            if(key.Key == ConsoleKey.LeftArrow)
            {
                player.MovePlayer(-1, 0);
            }

            if(key.Key == ConsoleKey.Spacebar)
            {
                
                gc.Inspect();
            }*/

            if(keyMap.ContainsKey(key))
            {
                keyMap[key]();
            }

          
        }
    }
}
