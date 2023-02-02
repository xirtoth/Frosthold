using System.Numerics;
using System.Runtime.InteropServices;

namespace Frosthold
{
    public class Screen
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
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
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            
            Console.SetBufferSize(Console.LargestWindowWidth * 2, Console.LargestWindowHeight * 2);
            Console.SetWindowPosition(0, 0);
            IntPtr consoleWindow = GetConsoleWindow();
            if (consoleWindow != IntPtr.Zero)
            {
                ShowWindow(consoleWindow, 3);
            }
            PrintMap();
            PrintPlayer();



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
            for (int i = 1; i < map.MapArray.GetLength(0); i++)
            {
                for (int k = 1; k < map.MapArray.GetLength(1); k++)
                {
                    /* if (i == 0 || i == map.Width-1|| k == 0 || k == map.Height-1)
                     {
                         Console.SetCursorPosition(i, k);
                         Write("#");
                     }*/
                    if (map.MapArray[i, k] == TileTypes.wall)
                    {
                        Console.SetCursorPosition(i - 1, k - 1);
                        Write("#");
                    }

                    if (map.MapArray[i, k] == TileTypes.floor)
                    {
                        Console.SetCursorPosition(i - 1, k - 1);
                        Write(" ");
                    }
                    
                    if (map.MapArray[i, k] == TileTypes.entrance)
                    {
                        Console.SetCursorPosition(i - 1, k - 1);
                        Write(">", ConsoleColor.Green);
                    }
                    if (map.MapArray[i, k] == TileTypes.exit)
                    {
                        Console.SetCursorPosition(i - 1, k - 1);
                        Write("<", ConsoleColor.Green);
                    }


                }
            }
        }

        //tulostetaan jokanen entity ruudulle
        public void PrintEntities()
        {
            foreach (Entity e in entities)
            {
                Console.SetCursorPosition(e.Pos.x, e.Pos.y);
                Write(e.mark, e.color);
            }
        }

        //tyhjennetään ruutu ja tulostetaan kaikki tarvittava ruudulle.
        public void UpdateScreen()
        {
            //Clear();
            //PrintMap();
            
            PrintEntities();
            PrintPlayerStats();
            //PrintEnterAndExit();
            PrintPlayer();
            PrintEnterAndExit();
            
            


        }

        private void PrintEnterAndExit()
        {
            Console.SetCursorPosition(GameController.Instance.map.ExitPos.x, GameController.Instance.map.ExitPos.y);
            Write("<", GameController.Instance.map.ExitPos, ConsoleColor.Green);


            Console.SetCursorPosition(GameController.Instance.map.EntrancePos.x, GameController.Instance.map.EntrancePos.y);
            Write(">", GameController.Instance.map.EntrancePos, ConsoleColor.Green);
            
            
        }

        private void Write(string v, Position pos, ConsoleColor color)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Write(v, color);
        }

        //tulostetaan pelaajan nimi health(myöhemmin lisää) ruudun alareunaan
        public void PrintPlayerStats()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
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
            if (cursorLeft > Console.LargestWindowWidth || cursorTop <= 0 || cursorLeft <= 0 || cursorTop > Console.LargestWindowHeight)
            {
                return;
            }
            else
            {
                Console.SetCursorPosition(cursorLeft + x, cursorTop + y);
            }
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

        internal void RemoveMark(int x, int y)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
    }


}