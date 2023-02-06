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

        private GameController gc;

        public Screen(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.player = player;
            this.entities = entities;

            IntPtr consoleWindow = GetConsoleWindow();
           
            if (consoleWindow != IntPtr.Zero)
            {
                ShowWindow(consoleWindow, 3);
            }
            gc = GameController.Instance;
            Console.CursorVisible = false;
            Console.WindowHeight = Height;
            Console.WindowWidth = Width;

            Console.SetWindowPosition(0, 0);

            Console.SetBufferSize(Width+1, Height+1);
            
          
            Clear();
            PrintMap();
            PrintPlayer();
        }

        //asetetaan kursori pelaajan sijaintiin ja tulostetaan @
        public void PrintPlayer()
        {
            Console.SetCursorPosition(gc.player.Pos.x, gc.player.Pos.y);
            Write("@");
        }

        //käydään läpi map array ja tulostetaan # jokaiseen kohtaa jossa on seinä (ei toimi täysin)
        public void PrintMap()
        {
            // Console.WriteLine(map.Width + map.Height);
            for (int i = 1; i < gc.map.MapArray.GetLength(0); i++)
            {
                for (int k = 1; k < gc.map.MapArray.GetLength(1); k++)
                {
                    /* if (i == 0 || i == map.Width-1|| k == 0 || k == map.Height-1)
                     {
                         Console.SetCursorPosition(i, k);
                         Write("#");
                     }*/
                    if (gc.map.MapArray[i, k] == TileTypes.wall)
                    {
                        Console.SetCursorPosition(i - 1, k - 1);
                        Write("#");
                    }

                    if (gc.map.MapArray[i, k] == TileTypes.floor)
                    {
                        Console.SetCursorPosition(i - 1, k - 1);
                        Write(".");
                    }

                    if (gc.map.MapArray[i, k] == TileTypes.entrance)
                    {
                        Console.SetCursorPosition(i - 1, k - 1);
                        Write(">", ConsoleColor.Green);
                    }
                    if (gc.map.MapArray[i, k] == TileTypes.exit)
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
            foreach (Entity e in gc.entities)
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
            PrintEnterAndExit();
            PrintEntities();
            PrintPlayerStats();
            //PrintEnterAndExit();
            PrintPlayer();
            //PrintEnterAndExit();
        }

        private void PrintEnterAndExit()
        {
            //Console.SetCursorPosition(GameController.Instance.map.ExitPos.x, GameController.Instance.map.ExitPos.y);
            Write("<", gc.map.ExitPos, ConsoleColor.Yellow);

            //Console.SetCursorPosition(GameController.Instance.map.EntrancePos.x, GameController.Instance.map.EntrancePos.y);
            Write("!", gc.map.EntrancePos, ConsoleColor.Yellow);
        }

        private void Write(string text, Position pos, ConsoleColor color)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Write(text, color);
        }

        //tulostetaan pelaajan nimi health(myöhemmin lisää) ruudun alareunaan
        public void PrintPlayerStats()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Write($"{gc.player.PlayerName} {gc.player.Health}/{gc.player.MaxHealth} Gold: {gc.player.inventory.gold} Floor: {gc.floor}");
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

        //tulostetaan teksti ruudulle tietyllä värillä.
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

        internal void DrawNewMap()
        {
            /*Clear();

            PrintEnterAndExit();
            //PrintEntities();
            PrintPlayerStats();
            //PrintEnterAndExit() ;
            PrintMap();
            PrintPlayer();*/
            Clear();
            PrintMap();
            PrintPlayer();
        }

        public void ChangeMap(Map map)
        {
            this.map = map;
        }

        public void PrintEntityInfo(string v)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Write(v, ConsoleColor.Red);
        }

        public void PrintDamageInfo(string text)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Write(text, ConsoleColor.Green);
        }
    }
}