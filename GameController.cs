namespace Frosthold
{
    public class GameController
    {
        public int SCREEN_WIDTH = Console.LargestWindowWidth;
        public int SCREEN_HEIGHT = Console.LargestWindowHeight;
        public int MAP_WIDTH = 100;
        public int MAP_HEIGHT = 24;
        public static GameController? Instance { get; set; }
        public Player? player;
        public List<Entity> entities { get; set; }
        public int frames { get; set; }
        public Screen? screen;

        public InspectKeyBinds? ikb { get; set; }
        public MainInputs? mkb { get; set; }
        public Map? map;
        public Map? map2;
        public Animation ani;

        // public KeyBinds mainKeys;
        public int floor { get; set; }

        public bool inspecting { get; set; }

        public bool running { get; set; }

        public Position OldCursorPosition { get; set; }

        public GameController()
        {
            this.running = false;
            entities = new List<Entity>();
        }

        //Luodaan tarvittavat muuttujat
        public void Init()
        {
            ani = new Animation();
            PrintIntroScreen();
            this.floor = 1;
            this.inspecting = false;
            Random rnd = new Random();

            this.player = CreatePlayer();
            GenerateLevel();

            player.Pos = map.EntrancePos;
            screen = new Screen(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.running = true;
            this.frames = 0;
            this.mkb = new MainInputs(player, screen);
            this.ikb = new InspectKeyBinds();

            // this.mainKeys = new KeyBinds(this.ip);
            //ip.AddKey(ConsoleKey.K, () => player.MovePlayer(1, 1));
        }

        private void GenerateLevel()
        {
            Random rand = new Random();
            //Map map = new Map(50, 50, 3);
            Map map = new Map(MAP_WIDTH + 1, MAP_HEIGHT + 1, 2);
            map.GenerateMap();
            this.map = map;
            this.entities = map.entities;
        }

        private static Player CreatePlayer()
        {
            Player player;
            while (true)
            {
                Console.Write("Give name: ");
                string? name = Console.ReadLine();
                if (name != null && name.Length > 0 && name.Length <= 10)
                {
                    player = new Player(5, 5, name, 15, 10, 5);
                    return player;
                }
                Console.WriteLine("Name must be between 1 and 10 letters.");
            }
        }

        //otetaan pelaajalta syöte ja palautetaan se ConsoleKeyInfo muodossa
        private static ConsoleKeyInfo ReadInput()
        {
            var key = Console.ReadKey(true);
            //Console.WriteLine(key.ToString());
            return key;
        }

        //pelin looppi
        public void Start()
        {
            while (running == true)
            {
                screen.UpdateScreen();
                var input = ReadInput().Key;
                mkb.ip.ParseInput(input);
                player.CheckCurrentPosition();

                //liikutetaan vihollisia
                MoveEnemies(entities);

                frames++;
            }
            screen.Clear();
        }

        //tätä voidaan käyttää, jos halutaan, että tiettyjen liikkeiden jälkeen tapahtuu jotain (tällähetkellä vain testaamis metodi)
        private void CheckRandomEvents()
        {
            Random rand = new Random();
            if (frames % 5 == 0)
            {
                player.Health += 5;
            }
        }

        //käydään läpi entity lista, ja jos entity on Monster. Voidaan laittaa se tekemään erinäisiä toimintoja.
        private void MoveEnemies(List<Entity> entities)
        {
            Random rand = new Random();
            foreach (Entity e in entities)
            {
                if (e.canMove)
                {
                    if (e.GetType() == typeof(Monster))
                    {
                        var en = (Monster)e;
                        en.MoveTowardsPlayerWithRandomness();
                    }
                }
                if (e.type == Entity.EntityType.item)
                {
                    e.MoveEntity(1, 1);
                }
            }
        }

        //poistetaan enity listasta
        public void RemoveEntity(Entity entity)
        {
            screen.RemoveMark(entity.Pos.x, entity.Pos.y);
            entities.Remove(entity);
        }

        public void Inspect()
        {
            inspecting = true;
            Console.SetCursorPosition(player.Pos.x, player.Pos.y);
            Console.CursorVisible = true;
            while (inspecting)
            {
                var input = ReadInput();
                ikb.ip.ParseInput(input.Key);
                var cursorOldPosition = Console.GetCursorPosition();
                OldCursorPosition = new Position(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);

                var entityAtCursor = entities.FirstOrDefault(e => e.Pos.x == Console.CursorLeft && e.Pos.y == Console.CursorTop);

                if (entityAtCursor != null)
                {
                    var entityInfo = $"{entityAtCursor.name} {entityAtCursor.description}";
                    if (entityAtCursor is Monster m)
                    {
                        entityInfo += $" Hp:{m.Health}/{m.MaxHealth}";
                    }
                    screen.PrintEntityInfo(entityInfo);
                }
                else
                {
                    screen.PrintEntityInfo("");
                }
                Console.SetCursorPosition(cursorOldPosition.Left, cursorOldPosition.Top);
            }
        }

        internal void ChangeMap()
        {
            Random rand = new Random();
            map2 = new Map(MAP_WIDTH + 1, MAP_HEIGHT + 1, rand.Next(1, 10));
            map2.GenerateMap();
            this.map = map2;
            this.entities = map2.entities;
            this.player.Pos = map2.EntrancePos;

            screen.DrawNewMap();
            floor++;
        }

        public void PrintIntroScreen()
        {
            Console.Write("88888888888                                           \r\n88                                             ,d     \r\n88                                             88     \r\n88aaaaa  8b,dPPYba,   ,adPPYba,   ,adPPYba,  MM88MMM  \r\n88\"\"\"\"\"  88P'   \"Y8  a8\"     \"8a  I8[    \"\"    88     \r\n88       88          8b       d8   `\"Y8ba,     88     \r\n88       88          \"8a,   ,a8\"  aa    ]8I    88,    \r\n88       88           `\"YbbdP\"'   `\"YbbdP\"'    \"Y888  \r\n                                                      \r\n                                                      \r\n                                                      \r\n88        88               88           88            \r\n88        88               88           88            \r\n88        88               88           88            \r\n88aaaaaaaa88   ,adPPYba,   88   ,adPPYb,88            \r\n88\"\"\"\"\"\"\"\"88  a8\"     \"8a  88  a8\"    `Y88            \r\n88        88  8b       d8  88  8b       88            \r\n88        88  \"8a,   ,a8\"  88  \"8a,   ,d88            \r\n88        88   `\"YbbdP\"'   88   `\"8bbdP\"Y8            \r\n                                               ");
            Console.WriteLine(Console.LargestWindowHeight + " " + Console.LargestWindowWidth);
        }
    }
}