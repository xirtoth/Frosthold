namespace Frosthold
{
    public enum TileTypes
    {
        floor, wall,
        door,
        entrance,
        exit
    }

    public class Map
    {
        public Position EntrancePos { get; set; }
        public Position ExitPos { get; set; }

        public List<Entity> entities { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        private int Rooms { get; set; }

        private List<Room> RoomsList { get; set; }
        public TileTypes[,] MapArray { get; set; }

        private GameController gc = GameController.Instance;

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
            Random rand = new Random();
            var count = rand.Next(1, 10);
            List<Entity> en = new List<Entity>();
            for (int i = 0; i < count; i++)
            {
                en.Add(new Monster("keijo" + i, "iso paha kissa", "K", 100, 100, 100, new Position(15, 15), (ConsoleColor)rand.Next(Enum.GetValues(typeof(ConsoleColor)).Length)));
            }
            for (int i = 0; i < 5; i++)
            {
                en.Add(new Item("Potion" + i, "Healing potion", 1, 1, "?", new Position(rand.Next(4, 40), rand.Next(4, 40)), ConsoleColor.Magenta));
            }
            en.Add(new Weapon("Testiase", "Hyvin testattava ase", 10, 1, "/", new Position(14, 14), WeaponType.Melee));
            return en;
        }

        //luodaan map itemi. ja määritellään seinät
        public void GenerateMap()
        {
            Random rand = new Random();
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
            //mapArray[room.room.X + room.room.Width, room.room.Y + room.room.Height] = TileTypes.wall;
        }

        //lisätään sisään- ja uloskäynti.
        public void AddEntranceAndExit()
        {
            Random rand = new Random();

            var xPos = rand.Next(2, Width);
            EntrancePos = new Position(xPos - 1, Height - 3);
            MapArray[xPos, Height - 2] = TileTypes.entrance;

            xPos = rand.Next(2, Width);
            ExitPos = new Position(xPos - 1, Height - Height + 1);
            MapArray[xPos, Height - Height + 2] = TileTypes.exit;
        }
    }
}