namespace Frosthold
{
    public class Monster : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }

        private GameController gc = GameController.Instance;
        private static readonly Random rand = new();

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
            /* if (CheckIfPlayer(Math.Max(1, Math.Min(GameController.Instance.screen.Width - 2, this.Pos.x + x)), Math.Max(1, Math.Min(GameController.Instance.screen.Height - 2, this.Pos.y + y))))
             {
                 gc.screen.PrintDamageInfo($"{name} hits you for {Damage}. Damage");
                 gc.player.TakeDamage(Damage);
                 return;
             }*/

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
            if (GetDistanceFromPlayer() < 1.5)
            {
                gc.screen.PrintDamageInfo($"{name} hits you for {Damage} Damage");
                gc.player.TakeDamage(Damage);
                return;
            }

            int xDiff = GameController.Instance.player.Pos.x - this.Pos.x;
            int yDiff = GameController.Instance.player.Pos.y - this.Pos.y;
            int xMove = 0, yMove = 0;

            if (10.0 < GetDistanceFromPlayer())
            {
                xMove = rand.Next(-1, 2);
                yMove = rand.Next(-1, 2);
            }
            else
            {
                if (Math.Abs(xDiff) > Math.Abs(yDiff))
                {
                    xMove = xDiff > 0 ? 1 : -1;
                }
                else
                {
                    yMove = yDiff > 0 ? 1 : -1;
                }

                if (CheckCollision(xMove, yMove))
                {
                    int tries = 0;
                    while (CheckCollision(xMove, yMove) && tries < 5)
                    {
                        xMove = rand.Next(-1, 2);
                        yMove = rand.Next(-1, 2);
                        tries++;
                    }
                }
            }

            MoveEntity(xMove, yMove);
        }

        public double GetDistanceFromPlayer()
        {
            int xDiff = GameController.Instance.player.Pos.x - Pos.x;
            int yDiff = GameController.Instance.player.Pos.y - Pos.y;
            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        public bool CheckIfPlayer(int x, int y)
        {
            if (x == gc.player.Pos.x && y == gc.player.Pos.y)
            {
                return true;
            }
            return false;
        }
    }
}