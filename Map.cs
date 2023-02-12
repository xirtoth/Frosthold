using Newtonsoft.Json;

namespace Frosthold
{
    public enum TileTypes
    {
        floor,
        wall,
        door,
        entrance,
        exit,
        openDoor
    }

    public class Map
    {
        private List<(string Name, string Symbol, ConsoleColor Color)> monsterList = new List<(string, string, ConsoleColor)>
{
    ("Minotaur", "M", ConsoleColor.Red),
    ("Medusa", "M", ConsoleColor.Green),
    ("Mothman", "M", ConsoleColor.Yellow),
    ("Manticore", "M", ConsoleColor.DarkMagenta),
    ("Chimera", "C", ConsoleColor.Cyan),
    ("Hydra", "H", ConsoleColor.DarkRed),
    ("Gorgon", "G", ConsoleColor.DarkGreen),
    ("Phoenix", "P", ConsoleColor.DarkYellow),
    ("Dragon", "D", ConsoleColor.DarkCyan),
    ("Vampire", "V", ConsoleColor.DarkGray)
};

        public Position EntrancePos { get; set; }
        public Position ExitPos { get; set; }

        public List<Entity> entities { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        private int Rooms { get; set; }

        private List<Room> RoomsList { get; set; }
        public TileTypes[,] MapArray { get; set; }

        private GameController gc = GameController.Instance;

        private static readonly Random rand = new();

        public Map(int width, int height, int rooms)
        {
            Console.WriteLine($"{width} {height}");
            this.Width = width;
            this.Height = height;
            this.Rooms = rooms;
            this.MapArray = new TileTypes[gc.MAP_WIDTH + 1, gc.MAP_HEIGHT + 1];
            this.RoomsList = new List<Room>();
            this.entities = CreateEntities();
        }

        private List<Entity>? CreateEntities()
        {
            var count = rand.Next(1, 10);
            List<Entity> en = new List<Entity>();
            for (int i = 0; i < count; i++)
            {
                var randomIndex = rand.Next(0, monsterList.Count);
                var randomMonster = Data.monsterList[randomIndex];
                en.Add(new Monster(randomMonster.Name, "", randomMonster.Symbol, 100, 100, 2, new Position(rand.Next(1, Width - 2), rand.Next(1, Height - 10)), randomMonster.Color));
            }
            for (int i = 0; i < 5; i++)
            {
                en.Add(new Item("Potion" + i, "Healing potion", 1, 1, "?", () => gc.messageLog.AddMessage("used Healing potion"), new Position(rand.Next(1, Width - 2), rand.Next(1, Height - 2)), ConsoleColor.Magenta));
            }
            en.Add(new Weapon("Testiase", "Hyvin testattava ase", 10, 1, "/", new Position(14, 14), WeaponType.Melee));
            return en;
        }

        //luodaan map itemi. ja määritellään seinät
        public void GenerateMap()
        {
            //Random rand = new Random();
            for (int i = 0; i <= Width - 1; i++)
            {
                MapArray[i, 0] = TileTypes.wall;
                MapArray[i, Height - 1] = TileTypes.wall;
            }
            for (int i = 0; i < Height - 1; i++)
            {
                MapArray[0, i] = TileTypes.wall;
                MapArray[Width - 1, i] = TileTypes.wall;
            }

            //tehdään huoneita mappiin. Katsotaan, jos huoneet ovat päällekkäisiä, jos näin on yritetään luoda uusi huone (hieman rikki)
            bool intersects = true;
            for (int i = 0; i < Rooms; i++)
            {
                Room room = new Room(rand.Next(3, 10), rand.Next(3, 10));
                room.GenerateRoom();
                RoomsList.Add(room);
                intersects = true;
                while (intersects)
                {
                    intersects = false;
                    for (int j = 0; j < RoomsList.Count; j++)
                    {
                        if (i == j) continue;
                        if (room.room.IntersectsWith(RoomsList[j].room))
                        {
                            room.GenerateRoom();
                            intersects = true;
                            break;
                        }
                    }
                }

                CopyToMapArray(MapArray, room);
            }
            AddEntranceAndExit();
        }

        //kopiodaan huoneen arvot mapArrray muuttujaan
        public void CopyToMapArray(TileTypes[,] mapArray, Room room)
        {
            for (int x = room.room.X; x < room.room.X + room.room.Width; x++)
            {
                MapArray[x, room.room.Y] = TileTypes.wall;
                //Console.WriteLine($"{x} : {room.room.Top+room.room.Height} {room.room.Left}");
                MapArray[x, room.room.Y + room.room.Height] = TileTypes.wall;
            }

            for (int y = room.room.Y; y < room.room.Y + room.room.Height; y++)
            {
                MapArray[room.room.X + room.room.Width, room.room.Y + room.room.Height] = TileTypes.wall;
                MapArray[room.room.X + room.room.Width, y] = TileTypes.wall;
                MapArray[room.room.X, y] = TileTypes.wall;
            }
            MapArray[room.room.Left + 2, room.room.Bottom] = TileTypes.door;
            MapArray[room.room.Left + 2, room.room.Top] = TileTypes.door;
        }

        //lisätään sisään- ja uloskäynti.
        public void AddEntranceAndExit()
        {
            Random rand = new Random();

            var xPos = rand.Next(2, Width - 2);
            var yPos = Height - 4;
            EntrancePos = new Position(xPos, yPos);
            MapArray[xPos, yPos] = TileTypes.entrance;

            xPos = rand.Next(2, Width - 2);
            yPos = 2;
            ExitPos = new Position(xPos, yPos);
            MapArray[xPos, 2] = TileTypes.exit;
        }
    }
}