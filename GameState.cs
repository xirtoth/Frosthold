using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public class Gamestate
    {
        public int Floor { get; set; }
        public Player PlayerState { get; set; }
        public List<Map> MapState { get; set; }

        public int Frames { get; set; }
    }
}