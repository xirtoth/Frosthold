using Newtonsoft.Json;

namespace Frosthold
{
    public class Item : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public int Amount { get; set; }

        public int Weight { get; set; }

        [JsonIgnore]
        public Action UseAction { get; set; }

        public Item(string name, string description, int amount, int weight, string mark, Action useAction, Position pos, ConsoleColor color) : base(name, description, mark, pos, false, color)
        {
            this.Name = Name;
            this.Description = Description;
            this.Amount = amount;
            this.Weight = weight;
            this.UseAction = useAction;
        }

        public override void MoveEntity(int x, int y)
        {
            this.Pos.x = this.Pos.x + 1;
        }

        public virtual void Attack(Entity e)
        {
        }

        public virtual void Attack(Player p, Entity attacker)
        {
        }

        public virtual void UseItem()
        {
            UseAction.Invoke();
        }
    }
}