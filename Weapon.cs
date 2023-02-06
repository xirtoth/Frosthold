using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public enum WeaponType
    {
        Ranged,
        Melee
    }
    public class Weapon : Item
    {

        private static readonly Random rand = new Random();

        public int Range { get; set; }
        public int Damage { get; set; }
        public WeaponType WeaponType;
        private GameController gc = GameController.Instance;

        public Weapon(string name, string description, int damage, int range, string mark, WeaponType weapontype) : base(name, description, 1, 1, mark, new Position(1, 1), ConsoleColor.DarkRed)
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = weapontype;
        }

        public override void Attack(Entity e)
        {
            switch (WeaponType)
            {
                case WeaponType.Melee:
                    MeleeAttack(e);
                    break;
                case WeaponType.Ranged:
                    RangedAttack();
                    break;
                default:
                    break;
            }
        }

        private void RangedAttack()
        {
            throw new NotImplementedException();
        }

        private void MeleeAttack(Entity e)
        {
            var en = (Monster)e;
            var hitChance = 50 + (gc.player.Dexterity - 10) * 5;

            if (rand.Next(0, 100) < hitChance)
            {
                en.TakeDamage(gc.player.Strength + Damage);
                gc.screen.PrintDamageInfo($"you hit {en.name} for {gc.player.Strength + Damage} it has {en.Health} hp left");
            }
            else
            {
                gc.screen.PrintDamageInfo($"you miss {en.name}");
            }
        }

    }


}
