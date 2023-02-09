using System.Drawing;

namespace Frosthold
{
    public class Room
    {
        private int width { get; set; }
        private int height { get; set; }

        public Rectangle room { get; set; }

        private GameController gc = GameController.Instance;
        public Room(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        //tehdään uusi huone. (tällähetkellä testitilassa)
        public void GenerateRoom()
        {
            Random rand = new Random();
            int randomX = rand.Next(0, gc.MAP_WIDTH-width);
            int randomY = rand.Next(0, gc.MAP_HEIGHT-height);

            /*  for(int i = 0; i < width; i++)
              {
                  for(int k = 0; k < height; k++)
                  {
                  }
              }*/
            room = new Rectangle(randomX , randomY , width, height);
        }
    }
}