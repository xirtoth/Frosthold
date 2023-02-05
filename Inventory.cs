using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    
    public class Inventory
    {
        public List<Item> inventory { get; set; }
        public Item Armour { get; set; }
        public Weapon Weapon { get; set; }
        public int size;
        public int currentSize;
        public int gold;
        public GameController gc = GameController.Instance;

        public Inventory(int size)
        {
            this.size = size;
            this.currentSize = 0;
            this.inventory = new List<Item>();
            this.gold = 50;
        }
    
        public void AddItem(Item item)
        {
            if(currentSize < size)
            {
                inventory.Add(item);
            }
            
        }

        public void PrintInventory()
        {
            gc.screen.Clear();
            Console.SetCursorPosition(0, 1);
            gc.screen.Write($"Equipped weapon: {Weapon.Name} (base damage {Weapon.Damage}) + strength modifier {gc.player.Strength} = {Weapon.Damage+gc.player.Strength} " + "\n", ConsoleColor.DarkYellow);
            gc.screen.Write("Inventory: ", ConsoleColor.DarkYellow);
            foreach(Item i in inventory)
            {
                Console.WriteLine(i.name + " " + i.description);
            }
            Console.ReadKey(true);
            gc.screen.DrawNewMap();
        }
    }

    
}
