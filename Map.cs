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

        public Map(int width, int height, int rooms)
        {
            this.Width = width;
            this.Height = height;
            this.Rooms = rooms;
            this.MapArray = new TileTypes[width + 1, height + 1];
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
            for(int i = 0; i < 5; i++)
            {
                en.Add(new Item("Potion" + 1, "Healing potion", 1, 1, "?", new Position(24, 24), ConsoleColor.Magenta));
            }
            
            return en;
        }

        //luodaan map itemi. ja määritellään seinät
        public void GenerateMap()
        {
            Random rand = new Random();
            for (int i = 0; i < Height; i++)
            {
                MapArray[i, 0] = TileTypes.wall;
                MapArray[i, Width - 1] = TileTypes.wall;
            }
            for (int i = 0; i < Width; i++)
            {
                MapArray[0, i] = TileTypes.wall;
                MapArray[Height - 1, i] = TileTypes.wall;
            }

            //tehdään huoneita mappiin. Katsotaan, jos huoneet ovat päällekkäisiä, jos näin on yritetään luoda uusi huone (hieman rikki)
            bool intersects = true;
            for (int i = 0; i < Rooms; i++)
            {
                Room room = new Room(rand.Next(3, 8), rand.Next(3, 8));
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
                MapArray[room.room.Y, x] = TileTypes.wall;
                MapArray[room.room.Y + room.room.Height - 1, x] = TileTypes.wall;
            }

            for (int y = room.room.Y; y < room.room.Y + room.room.Height; y++)
            {
                MapArray[y, room.room.X] = TileTypes.wall;
                MapArray[y, room.room.X + room.room.Width - 1] = TileTypes.wall;
            }
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