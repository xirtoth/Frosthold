namespace Frosthold
{
    public class Monster : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        //perus constructori käytetään entity luokaa pohjana.
        public Monster(string name, string description, string mark, Position pos) : base(name, description, mark, pos)
        {
            //lisätään default valuet
        }

        //toinen constuctori, jossa määritellän myös health, maxHealth, damage(paljonko vahinkoa vihollinen tekee)
        public Monster(string name, string description, string mark, int health, int maxHealth, int damage, Position pos, ConsoleColor color) : base(name, description, mark, pos, color)
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
    }
}