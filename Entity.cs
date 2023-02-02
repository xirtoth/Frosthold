namespace Frosthold
{
    public class Entity
    {
        public string name { get; set; }
        public string description { get; set; }

        public int health;
        public int maxHealth;

        public enum EntityType
        {
            monster, npc, item
        }

        public EntityType type { get; set; }

        public string mark { get; set; }
        public Position Pos { get; set; }

        public ConsoleColor color;

        public Entity(string name, string description, string mark, Position pos)
        {
            this.name = name;
            this.description = description;
            this.Pos = pos;
            this.mark = mark;
            this.color = ConsoleColor.White;
            this.type = EntityType.monster;
        }

        public Entity(string name, string description, string mark, Position pos, ConsoleColor color)
        {
            this.name = name;
            this.description = description;
            this.Pos = pos;
            this.mark = mark;
            this.color = color;
            this.type = EntityType.monster;
        }

        //liikutetaan entityä
        public void MoveEntity(int x, int y)
        {
            var oldPos = this.Pos;
            //tarkastetaan onko esteitä (tätä voi parantaa)
            if (CheckCollision(x, y))
            {
                return;
            }
            //asetetaan entityn x ja y arvo lisäämällä parametrinä saadut x ja y. tarkastetaan myös, että ei mennä ruudun ulkopuolelle
            GameController.Instance.screen.RemoveMark(oldPos.x, oldPos.y);

            this.Pos.x += x;
            this.Pos.y += y;

            if (Pos.x >= GameController.Instance.screen.Width - 2)
            {
                Pos.x = GameController.Instance.screen.Width - 2;
            }
            if (Pos.x <= 1)
            {
                Pos.x = 1;
            }
            if (Pos.y >= GameController.Instance.screen.Height - 2)
            {
                Pos.y = GameController.Instance.screen.Height - 2;
            }
            if (Pos.y <= 1)
            {
                Pos.y = 1;
            }
        }

        //asetetaan entityn piste
        public void SetEntityPosition(int x, int y)
        {
            this.Pos.x = x;
            this.Pos.y = y;
        }

        //tarkastetaan onko ruudussa johon ollaan liikkumassa toinen entity. Palatetaan true jos näin on (Tähän tarvii paljon lisää tarkastuksia tulevaisuudessa)
        private bool CheckCollision(int x, int y)
        {
            foreach (Entity e in GameController.Instance.entities)
            {
                if (this.Pos.x + x == e.Pos.x && this.Pos.y + y == e.Pos.y)
                {
                    return true;
                }
                if (GameController.Instance.map.MapArray[this.Pos.x + x + 1, this.Pos.y + y + 1] == TileTypes.wall)
                {
                    return true;
                }

                if (this.Pos.x + x == GameController.Instance.player.Pos.x && this.Pos.y + y == GameController.Instance.player.Pos.y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}