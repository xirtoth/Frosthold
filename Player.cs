namespace Frosthold
{
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
            if (CheckCollision(x, y))
            {
                return;
            }
            GameController.Instance.screen.RemoveMark(this.Pos.x, this.Pos.y);

            this.Pos.x += x;
            this.Pos.y += y;
            if(this.Pos.x == GameController.Instance.map.ExitPos.x && this.Pos.y == GameController.Instance.map.ExitPos.y)
            {
                GameController.Instance.ChangeMap();
            }
            //tarkastetaan että ei mennä ruudun yli
            if (Pos.x >= GameController.Instance.screen.Width - 1)
            {
                Pos.x = GameController.Instance.screen.Width - 1;
            }
            if (Pos.x <= 0)
            {
                Pos.x = 0;
            }
            if (Pos.y >= GameController.Instance.screen.Height - 1)
            {
                Pos.y = GameController.Instance.screen.Height - 1;
            }
            if (Pos.y <= 0)
            {
                Pos.y = 0;
            }
        }

        private bool CheckCollision(int x, int y)
        {
            foreach (Entity e in GameController.Instance.entities)
            {
                //katsotaan jos kohtaan mihin liikutaan on entity. (tähän lisätään myöhemmin damagen otto jne)
                if (this.Pos.x + x == e.Pos.x && this.Pos.y + y == e.Pos.y)
                {
                    GameController.Instance.RemoveEntity(e);
                    return true;
                }
            }

            int arrayWidth = GameController.Instance.map.MapArray.GetLength(0);
            int arrayHeight = GameController.Instance.map.MapArray.GetLength(1);

            //jos ruudussa on seinä
            if (this.Pos.x + x + 1 >= 0 && this.Pos.x + x + 1 < arrayWidth &&
                this.Pos.y + y + 1 >= 0 && this.Pos.y + y + 1 < arrayHeight &&
                GameController.Instance.map.MapArray[this.Pos.x + x + 1, this.Pos.y + y + 1] == TileTypes.wall)
            {
                return true;
            }

            return false;
        }
    }
}