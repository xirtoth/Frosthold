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


        public int Range { get; set; }
        public int Damage { get; set; }
        public WeaponType WeaponType;

        public Weapon(string name, string description, int damage, int range, string mark, WeaponType weapontype) : base(name, description, 1, 1, mark, new Position(1, 1), ConsoleColor.DarkRed)
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = weapontype;
        }

        public override void Attack()
        {
            switch (WeaponType)
            {
                case WeaponType.Melee:
                    MeleeAttack();
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

        private void MeleeAttack()
        {
            throw new NotImplementedException();
        }
    }


}
