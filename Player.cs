namespace Frosthold
{
    public class Player
    {
        public Position Pos { get; set; }
        public string PlayerName { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public Inventory inventory;

        private GameController gc = GameController.Instance;

        public Player(int x, int y, string name, int str, int dex, int intt)
        {
            this.Pos = new Position(x, y);
            this.PlayerName = name;
            this.Health = 100;
            this.MaxHealth = 100;
            this.inventory = new Inventory(10);
            Strength = str;
            Dexterity = dex;
            Intelligence = intt;

            inventory.Weapon = WeaponGenerator.GetRandomCommonWeapon();
        }

        //liikutetaan pelaajaa x ja y muuttujien mukaisesti
        /* public void MovePlayer(int x, int y)
         {
             if (CheckCollision(x, y))
             {
                 return;
             }
             GameController.Instance.screen.RemoveMark(this.Pos.x, this.Pos.y);

             this.Pos.x += x;
             this.Pos.y += y;

             if (this.Pos.x == GameController.Instance.map.ExitPos.x && this.Pos.y == GameController.Instance.map.ExitPos.y)
             {
                 GameController.Instance.ChangeMap();
             }
             //tarkastetaan että ei mennä ruudun yli
             if (Pos.x >= GameController.Instance.screen.Width - 1)
             {
                 Pos.x = GameController.Instance.screen.Width - 1;
             }
             if (Pos.x <= 0)
             {
                 Pos.x = 0;
             }
             if (Pos.y >= GameController.Instance.screen.Height - 1)
             {
                 Pos.y = GameController.Instance.screen.Height - 1;
             }
             if (Pos.y <= 0)
             {
                 Pos.y = 0;
             }
         }*/

        public void MovePlayer(int x, int y)
        {
            if (CheckCollision(x, y))
            {
                return;
            }
            GameController.Instance.screen.RemoveMark(this.Pos.x, this.Pos.y);

            this.Pos.x += x;
            this.Pos.y += y;

            if (this.Pos.x == GameController.Instance.map.ExitPos.x && this.Pos.y == GameController.Instance.map.ExitPos.y)
            {
                GameController.Instance.ChangeMap();
            }
            //tarkastetaan että ei mennä ruudun yli
            this.Pos.x = Math.Min(Math.Max(this.Pos.x, 0), GameController.Instance.screen.Width - 1);
            this.Pos.y = Math.Min(Math.Max(this.Pos.y, 0), GameController.Instance.screen.Height - 1);
        }

        private bool CheckCollision(int x, int y)
        {
            foreach (Entity e in GameController.Instance.entities)
            {
                //katsotaan jos kohtaan mihin liikutaan on entity.
                if (this.Pos.x + x == e.Pos.x && this.Pos.y + y == e.Pos.y)
                {
                    if (e.GetType() == typeof(Monster))
                    {
                        var en = (Monster)e;
                        inventory.Weapon.Attack(e);
                    }
                    else if (e.GetType() == typeof(Item) || e.GetType() == typeof(Weapon))
                    {
                        return false;
                    }

                    //GameController.Instance.RemoveEntity(e);
                    return true;
                }
            }

            int arrayWidth = GameController.Instance.map.MapArray.GetLength(0);
            int arrayHeight = GameController.Instance.map.MapArray.GetLength(1);

            //jos ruudussa on seinä
            if (this.Pos.x + x + 1 >= 0 && this.Pos.x + x + 1 < arrayWidth &&
                this.Pos.y + y + 1 >= 0 && this.Pos.y + y + 1 < arrayHeight &&
                GameController.Instance.map.MapArray[this.Pos.x + x + 1, this.Pos.y + y + 1] == TileTypes.wall)
            {
                return true;
            }

            return false;
        }

        public void CheckCurrentPosition()
        {
            foreach (Entity e in gc.entities)
            {
                if (Pos.x == e.Pos.x && Pos.y == e.Pos.y && e is Item item)
                {
                    gc.screen.PrintDamageInfo($"Theres {e.name} laying on ground here. (p to pickup)");
                }
            }
        }

        public void PickupItem()
        {
            List<Entity> tempList = gc.entities.ToList();
            foreach (Entity e in tempList)
            {
                if (Pos.x == e.Pos.x && Pos.y == e.Pos.y && e is Item item)
                {
                    gc.player.inventory.AddItem(item);
                    gc.screen.PrintDamageInfo($"Picked up {item.name}");
                    gc.RemoveEntity(item);
                }
            }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (Health <= 0)
            {
                gc.running = false;
            }
        }
    }
}