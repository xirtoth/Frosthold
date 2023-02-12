using Newtonsoft.Json;

namespace Frosthold
{
    public class Inventory
    {
        public List<Item> inventory { get; set; }

        public Item Armour { get; set; }

        public Weapon Weapon { get; set; }

        public int size { get; set; }
        public int currentSize { get; set; }
        public int gold { get; set; }

        [JsonIgnore]
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
            if (currentSize < size)
            {
                inventory.Add(item);
            }
        }

        public void PrintInventory()
        {
            int counter = 1;
            gc.screen.Clear();
            Console.SetCursorPosition(0, 1);
            gc.screen.Write($"Equipped weapon: {Weapon.Name} (base damage {Weapon.Damage}) + strength modifier {gc.player.Strength} = {Weapon.Damage + gc.player.Strength} " + "\n", ConsoleColor.DarkYellow);
            gc.screen.Write("\nInventory:\n", ConsoleColor.DarkYellow);
            foreach (Item i in inventory)
            {
                Console.WriteLine($"({(char)(counter + 96)}) [{i.mark}] {i.name} {i.description}");
                counter++;
            }
            Console.WriteLine("\nCommands: u = use, w = wear, d = drop");
            Console.WriteLine("\nEnter the letter corresponding to the item you want to use, wear, or drop:");
            string selectedItemString = Console.ReadLine().ToLower();
            if (selectedItemString.Length == 2)
            {
                char selectedCommand = selectedItemString[0];
                char selectedItem = selectedItemString[1];
                int selectedIndex = selectedItem - 96;
                if (selectedIndex > 0 && selectedIndex <= inventory.Count)
                {
                    if (selectedCommand == 'u')
                    {
                        inventory[selectedIndex - 1].UseItem();
                        Console.WriteLine($"Item {inventory[selectedIndex - 1].name} used.");
                        //inventory[selectedIndex - 1].UseItem();
                    }
                    else if (selectedCommand == 'w')
                    {
                        if (inventory[selectedIndex - 1] is Weapon)
                        {
                            if (Weapon != null)
                            {
                                inventory.Add(Weapon);
                            }
                            Weapon = (Weapon)inventory[selectedIndex - 1];
                            inventory.Remove((Weapon)inventory[selectedIndex - 1]);
                            Console.WriteLine($"Weapon {inventory[selectedIndex - 1].name} equipped.");
                            gc.messageLog.AddMessage($"{inventory[selectedIndex - 1].name} equipped.");
                        }
                        else
                        {
                            Console.WriteLine("Item is not a weapon and cannot be worn.");
                        }
                    }
                    else if (selectedCommand == 'd')
                    {
                        Console.WriteLine($"Item {inventory[selectedIndex - 1].name} dropped.");
                        inventory.Remove(inventory[selectedIndex - 1]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Please enter a valid command letter (u, w, d). for example command ua uses item a. command da drops item a");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please enter a valid letter.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter exactly two letters (command + item letter).");
            }
            Console.ReadKey(true);

            gc.screen.Clear();
            gc.screen.DrawNewMap();
        }
    }
}