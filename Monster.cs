namespace Frosthold
{
    public class Monster : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        //perus constructori käytetään entity luokaa pohjana.
        public Monster(string name, string description, string mark, Position pos) : base(name, description, mark, pos, true)
        {
            //lisätään default valuet
        }

        //toinen constuctori, jossa määritellän myös health, maxHealth, damage(paljonko vahinkoa vihollinen tekee)
        public Monster(string name, string description, string mark, int health, int maxHealth, int damage, Position pos, ConsoleColor color) : base(name, description, mark, pos, true, color)
        {
            this.Health = health;
            this.MaxHealth = maxHealth;
            this.Damage = damage;
        }

        //funktio jolla otetaan vahinkoa. (ei vielä käytössä)
        public void TakeDamage(int amount)
        {
            Health -= amount;
            CheckHealth();
        }

        //tarkastetaan onko health 0, jos näin on kutsutaan Die() funktiota (ei vielä käytössä)
        private void CheckHealth()
        {
            if (Health <= 0)
            {
                Die();
            }
        }

        //GameControllerin instancesta kutsutaan RemoveEntity metodia ja annetaan parametriks tämä instance.
        public void Die()
        {
            GameController.Instance.RemoveEntity(this);
        }

        public override void MoveEntity(int x, int y)
        {
            var oldPos = this.Pos;
            //tarkastetaan onko esteitä (tätä voi parantaa)
            if (base.CheckCollision(x, y))
            {
                return;
            }
            //poistetaan merkki ruudusta josta liikuttiin
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
    }
}