using System.Runtime.CompilerServices;

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

        public void MoveEntity(int x, int y)
        {
            if (CheckCollision(x, y))
            {
                return;
            }
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

        public void SetEntityPosition(int x, int y)
        {
            this.Pos.x = x;
            this.Pos.y = y;
        }

        private bool CheckCollision(int x, int y)
        {
            foreach(Entity e in GameController.Instance.entities)
            {
                if(this.Pos.x + x == e.Pos.x && this.Pos.y + y == e.Pos.y)
                {
                    return true;
                }
                if (GameController.Instance.map.MapArray[this.Pos.x + x + 1, this.Pos.y + y + 1] == TileTypes.wall)
                {
                    
                    return true;
                }
            }
            return false;
        }


    }
}