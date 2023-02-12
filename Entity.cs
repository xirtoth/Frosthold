using Newtonsoft.Json;

namespace Frosthold
{
    public class Entity
    {
        public string name { get; set; }
        public string description { get; set; }

        public int health { get; set; }
        public int maxHealth { get; set; }

        public enum EntityType
        {
            monster, npc, item
        }

        public EntityType type { get; set; }

        public string mark { get; set; }
        public Position Pos { get; set; }
        public bool canMove { get; set; }

        public ConsoleColor color;

        private GameController gc = GameController.Instance;

        public Entity(string name, string description, string mark, Position pos, bool canMove)
        {
            this.name = name;
            this.description = description;
            this.Pos = pos;
            this.mark = mark;
            this.color = ConsoleColor.White;
            //this.type = EntityType.monster;
            this.canMove = canMove;
        }

        [JsonConstructor]
        public Entity(string name, string description, string mark, Position pos, bool canMove, ConsoleColor color)
        {
            this.name = name;
            this.description = description;
            this.Pos = pos;
            this.mark = mark;
            this.color = color;
            // this.type = EntityType.monster;
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
            foreach (Entity e in gc.entities)
            {
                if (e != this && this.Pos.x + x == e.Pos.x && this.Pos.y + y == e.Pos.y)
                {
                    return true;
                }
            }

            return gc.map.MapArray[this.Pos.x + x + 1, this.Pos.y + y + 1] == TileTypes.wall || gc.map.MapArray[this.Pos.x + x + 1, this.Pos.y + y + 1] == TileTypes.door
                || (this.Pos.x + x == gc.player.Pos.x && this.Pos.y + y == gc.player.Pos.y);
        }

        public virtual void TakeDamage(int hitDamage)
        {
            health -= hitDamage;
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (health <= 0)
            {
                gc.messageLog.AddMessage($"{name} dies.");
                RemoveEntity(this);
            }
        }

        public void RemoveEntity(Entity entity)
        {
            gc.RemoveEntity(entity);
        }
    }
}