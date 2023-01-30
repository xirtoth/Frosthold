using System.Security.Cryptography.X509Certificates;

namespace Frosthold
{
    public class Screen
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private Player player { get; set; }
        private List<Entity> entities { get; set; }

        private Map map { get; set; }
        public Screen(int width, int height, Player player, List<Entity> entities, Map map)
        {
            this.Width = width;
            this.Height = height;
            this.player = player;
            this.entities = entities;
            this.map = map;
            Console.CursorVisible = false;
            
            Console.SetWindowSize(100,50);
            Console.SetBufferSize(100,50);
            Console.SetWindowPosition(0, 0);
            


        }

        //asetetaan kursori pelaajan sijaintiin ja tulostetaan @
        public void PrintPlayer()
        {
            Console.SetCursorPosition(player.Pos.x, player.Pos.y);
            Write("@");
        }

        //käydään läpi map array ja tulostetaan # jokaiseen kohtaa jossa on seinä (ei toimi täysin)
        public void PrintMap()
        {
           // Console.WriteLine(map.Width + map.Height);
            for(int i = 1; i < map.Width; i++)
            {
                for (int k = 1; k < map.Height; k++)
                {
                    /* if (i == 0 || i == map.Width-1|| k == 0 || k == map.Height-1)
                     {
                         Console.SetCursorPosition(i, k);
                         Write("#");
                     }*/
                    if (map.MapArray[i,k] == TileTypes.wall)
                    {
                        Console.SetCursorPosition(i-1, k-1);
                        Write("#");
                    }
                } 
            }  
        }
        
        //tulostetaan jokanen entity ruudulle
        public void PrintEntities()
        {
            foreach(Entity e in entities)
            {
                Console.SetCursorPosition(e.Pos.x, e.Pos.y);
                Write(e.mark, e.color);
            }
        }

        //tyhjennetään ruutu ja tulostetaan kaikki tarvittava ruudulle.
        public void UpdateScreen()
        {
            Clear();
            PrintMap();
            PrintPlayer();
            PrintEntities();
            PrintPlayerStats();
        }

        //tulostetaan pelaajan nimi health(myöhemmin lisää) ruudun alareunaan
        public void PrintPlayerStats()
        {
            Console.SetCursorPosition(0, Console.WindowHeight-1);
            Write(player.PlayerName + " " + player.Health + "/" + player.MaxHealth);
        }
        //tyhjennetään ruutu
        public void Clear()
        {
            Console.Clear();
        }

        public void MoveCursor(int x, int y)
        {
            Console.CursorVisible = true;
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;
            Console.SetCursorPosition(cursorLeft + x, cursorTop + y);
        }

        //tulostetaan teksti ruudulle tietyllä värillä
        public void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        //tulostetaan teksti ruudulle normaalilla värillä
        public static void Write(string text)
        {
            
            Console.Write(text);
        }
    }

  
}