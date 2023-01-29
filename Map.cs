using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{

    public enum TileTypes
    {
        floor, wall
    }
    public class Map
    {
      
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
            this.MapArray = new TileTypes[width+1, height+1];
            this.RoomsList = new List<Room>();
        }
        public void GenerateMap()
        {
            Random rand = new Random();
            for (int i = 0; i < Width; i++)
            {
                MapArray[1, i] = TileTypes.wall;
                MapArray[Width, i] = TileTypes.wall;
            }
            for (int i = 0; i < Height; i++)
            {
                MapArray[i, 1] = TileTypes.wall;
                MapArray[Height, i] = TileTypes.wall;
            }

            for (int i = 0; i < Rooms; i++)
            {
                Room room = new Room(3, 6);
                room.GenerateRoom();
                RoomsList.Add(room);
                bool intersects = false;
                while (intersects)
                {
                    intersects = false;
                    for (int j = 0; j < RoomsList.Count; j++)
                    {
                        if (i == j) continue;
                        if (room.room.IntersectsWith(RoomsList[j].room))
                        {
                            room.GenerateRoom();
                            intersects = false;
                            break;
                        }
                    }
                }
                CopyToMapArray(MapArray, room);
            }
        }

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
           // Console.WriteLine(mapArray);
        }

    }
}
