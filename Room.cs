using System.Drawing;

namespace Frosthold
{
    public class Room
    {
        private int width { get; set; }
        private int height { get; set; }

        public Rectangle room { get; set; }

        public Room(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        //tehdään uusi huone. (tällähetkellä testitilassa)
        public void GenerateRoom()
        {
            Random rand = new Random();
            int randomX = rand.Next(0, 30);
            int randomY = rand.Next(0, 30);

            /*  for(int i = 0; i < width; i++)
              {
                  for(int k = 0; k < height; k++)
                  {
                  }
              }*/
            room = new Rectangle(randomX + 2, randomY + 2, width, height);
        }
    }
}