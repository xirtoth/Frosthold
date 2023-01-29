using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold {



    public class Player
    {
        public Position Pos { get; set; }
        public string PlayerName { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        

        public Player(int x, int y, string name)
        {
            this.Pos = new Position(x, y);
            this.PlayerName = name;
            this.Health = 100;
            this.MaxHealth = 100;
        }

        //liikutetaan pelaajaa x ja y muuttujien mukaisesti
        public void MovePlayer(int x, int y)
        {
            this.Pos.x += x;
            this.Pos.y += y;
            //tarkastetaan että ei mennä ruudun yli
            if(Pos.x >= GameController.Instance.screen.Width-1)
            {
                Pos.x = GameController.Instance.screen.Width-1;
            }
            if(Pos.x <= 0)
            {
                Pos.x = 0;
            }
            if(Pos.y >= GameController.Instance.screen.Height-1)
            {
                Pos.y = GameController.Instance.screen.Height-1;
            }
            if(Pos.y <= 0)
            {
                Pos.y = 0;
            }
        }
    }
}
