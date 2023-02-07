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

        //tarkastetaan onko health 0, jos näin on kutsutaan Die() funktiota (ei vielä käytössä)
        private void CheckHealth()
        {
            if (Health <= 0)
            {
                Die();
            }
        }

        public override void TakeDamage(int hitDamage)
        {
            Health -= hitDamage;
            CheckHealth();
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

            this.Pos.x = Math.Max(1, Math.Min(GameController.Instance.screen.Width - 2, this.Pos.x + x));
            this.Pos.y = Math.Max(1, Math.Min(GameController.Instance.screen.Height - 2, this.Pos.y + y));
        }

        public void MoveTowardsPlayerWithRandomness()
        {
            Random rand = new Random();
            int xDiff = GameController.Instance.player.Pos.x - this.Pos.x;
            int yDiff = GameController.Instance.player.Pos.y - this.Pos.y;

            if (10.0 < GetDistanceFromPlayer())
            {
                MoveEntity(rand.Next(-1, 2), rand.Next(-1, 2));
            }
            else
            {
                if (Math.Abs(xDiff) > Math.Abs(yDiff))
                {
                    // Move horizontally

                    if (!CheckCollision(xDiff > 0 ? 1 : -1, 0))
                    {
                        MoveEntity(xDiff > 0 ? 1 : -1, 0);
                    }
                    else
                    {
                        int tries = 0;
                        while (CheckCollision(xDiff > 0 ? 1 : -1, 0) && tries < 5)
                        {
                            // Choose a new random direction
                            MoveEntity(rand.Next(-1, 2), rand.Next(-1, 2));
                            tries++;
                        }
                    }
                }
                else
                {
                    // Move vertically

                    if (!CheckCollision(0, yDiff > 0 ? 1 : -1))
                    {
                        MoveEntity(0, yDiff > 0 ? 1 : -1);
                    }
                    else
                    {
                        int tries = 0;
                        while (CheckCollision(0, yDiff > 0 ? 1 : -1) && tries < 5)
                        {
                            // Choose a new random direction
                            MoveEntity(rand.Next(-1, 2), rand.Next(-1, 2));
                            tries++;
                        }
                    }
                }
            }
        }

        public double GetDistanceFromPlayer()
        {
            int xDiff = GameController.Instance.player.Pos.x - Pos.x;
            int yDiff = GameController.Instance.player.Pos.y - Pos.y;
            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }
    }
}