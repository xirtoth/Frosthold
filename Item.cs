namespace Frosthold
{
    public class Item : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public int Amount { get; set; }

        public int Weight { get; set; }
        

        public Item(string name, string description, int amount, int weight, string mark, Position pos, ConsoleColor color) : base(name, description, mark, pos, false, color)
        {
            this.Name = Name;
            this.Description = Description;
            this.Amount = amount;
            this.Weight = weight;
        }

        public override void MoveEntity(int x, int y)
        {
            this.Pos.x = this.Pos.x + 1;
        }
        public virtual void Attack(Entity e)
        {

        }

        public virtual void UseItem()
        {
            GameController.Instance.screen.Write($"used {name}", ConsoleColor.Red);
        }
    }
}