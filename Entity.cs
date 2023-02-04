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
        public bool canMove { get; set; }

        public ConsoleColor color;

        public Entity(string name, string description, string mark, Position pos, bool canMove)
        {
            this.name = name;
            this.description = description;
            this.Pos = pos;
            this.mark = mark;
            this.color = ConsoleColor.White;
            this.type = EntityType.monster;
            this.canMove = canMove;
        }

        public Entity(string name, string description, string mark, Position pos, bool canMove, ConsoleColor color)
        {
            this.name = name;
            this.description = description;
            this.Pos = pos;
            this.mark = mark;
            this.color = color;
            this.type = EntityType.monster;
            this.canMove = canMove;
        }

        //liikutetaan entityä
        public virtual void MoveEntity(int x, int y)
        {
            
            
        }

        //asetetaan entityn piste
        public void SetEntityPosition(int x, int y)
        {
            this.Pos.x = x;
            this.Pos.y = y;
        }

        //tarkastetaan onko ruudussa johon ollaan liikkumassa toinen entity. Palatetaan true jos näin on (Tähän tarvii paljon lisää tarkastuksia tulevaisuudessa)
        public bool CheckCollision(int x, int y)
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