using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public class Monster : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        
        public Monster(string name, string description, string mark, Position pos) : base(name, description, mark, pos)
        {
        }

        public Monster(string name, string description, string mark, int health, int maxHealth, int damage, Position pos, ConsoleColor color) : base(name, description, mark, pos, color)
        {
            this.Health = health;
            this.MaxHealth = maxHealth;
            this.Damage = damage;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            CheckHealth();
        }

        private void CheckHealth()
        {
            if(Health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GameController.Instance.RemoveEntity(this);
        }
    }
}
