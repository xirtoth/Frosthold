using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public static class WeaponGenerator
    {
        private static List<Weapon> commonWeapons = new List<Weapon>
        {
            new Weapon("Short sword", "small rusty sword", 3, 1, "/", WeaponType.Melee),
            new Weapon("Long sword", "rusty long sword", 5, 1, "/", WeaponType.Melee),
            new Weapon("Mace", "rusty mace", 4, 1, "/", WeaponType.Melee)


        };

        public static Weapon GetRandomCommonWeapon()
        {
            Random rand = new Random();
            int index = rand.Next(commonWeapons.Count);
            return commonWeapons[index];

        }
    
    }
}
