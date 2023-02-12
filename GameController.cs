//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Frosthold
{
    public class GameController
    {
        public int SCREEN_WIDTH = Console.LargestWindowWidth;
        public int SCREEN_HEIGHT = Console.LargestWindowHeight;
        public int MAP_WIDTH = 50;
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
        public List<Map> maps;

        // public KeyBinds mainKeys;
        public int floor { get; set; }

        public bool inspecting { get; set; }

        public bool running { get; set; }

        public Position OldCursorPosition { get; set; }
        public EventSystem events;
        public MessageLog messageLog;

        public GameController()
        {
            this.running = false;
            entities = new List<Entity>();
        }

        //Luodaan tarvittavat muuttujat
        public void Init()
        {
            if (System.IO.File.Exists("file.json"))
            {
                PrintIntroScreen();
                screen = new Screen(SCREEN_WIDTH, SCREEN_HEIGHT);
                screen.Write("There's saved game. Press any key to continue", ConsoleColor.Green);
                maps = new List<Map>();
                LoadGameState("file.json");
                entities = maps[floor].entities;

                Console.ReadKey(true);
                map = new Map(MAP_WIDTH, MAP_HEIGHT, 3);
                floor = floor;
                map = maps[floor];
                running = true;
                messageLog = new MessageLog(MAP_WIDTH, MAP_HEIGHT);
                this.mkb = new MainInputs(player, screen);
                this.ikb = new InspectKeyBinds();
                InitializeEvents();
                screen.Clear();
            }
            else
            {
                maps = new List<Map>();
                PrintIntroScreen();
                player = CreatePlayer();
                GenerateLevel();
                InitializeGamePlayVariables();

                InitializeEvents();

                PrintIntroScreen();

                GenerateLevel();
            }
            //ChangeMap(1);

            // this.mainKeys = new KeyBinds(this.ip);
            //ip.AddKey(ConsoleKey.K, () => player.MovePlayer(1, 1));
        }

        private void InitializeGamePlayVariables()
        {
            // maps = new List<Map>();
            this.floor = 1;
            this.inspecting = false;
            player.Pos = map.EntrancePos;
            screen = new Screen(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.running = true;
            this.frames = 0;
            this.mkb = new MainInputs(player, screen);
            this.ikb = new InspectKeyBinds();
            messageLog = new MessageLog(MAP_WIDTH, MAP_HEIGHT);
        }

        private void InitializeEvents()
        {
            events = new EventSystem();
            events.AddEvent(10, () => player.Health += 10, EventType.EveryXFrames);
            events.AddEvent(2, () => player.Health += 2, EventType.EveryXFrames);
            events.AddEvent(5, () => { Animation ani = new Animation(AnimationType.Aura, player.Pos); ani.Start(); }, EventType.AtFrame);
        }

        private void GenerateLevel()
        {
            Random rand = new Random();
            //Map map = new Map(50, 50, 3);
            Map map = new Map(MAP_WIDTH + 1, MAP_HEIGHT + 1, 2);
            map.GenerateMap();
            maps.Add(map);
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
            screen.DrawNewMap();
            while (running)
            {
                screen.UpdateScreen();
                var input = ReadInput().Key;
                mkb.ip.ParseInput(input);
                player.CheckCurrentPosition();

                //liikutetaan vihollisia
                MoveEnemies(entities);
                events.Update(frames);

                frames++;
            }
            screen.Clear();
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
                        entityInfo += $" Hp:{m.Health}/{m.MaxHealth} it is wielding {m.Inventory.Weapon.name}";
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

        public void ChangeMap(int direction)
        {
            if (direction == 1)
            {
                if (floor + 1 < maps.Count && maps[floor + 1] != null)
                {
                    map = maps[floor + 1];
                    entities = map.entities;
                    screen.DrawNewMap();
                    player.Pos = new Position(map.EntrancePos.x, map.EntrancePos.y - 1);
                    floor++;
                }
                else
                {
                    Random rand = new Random();
                    map2 = new Map(MAP_WIDTH + 1, MAP_HEIGHT + 1, rand.Next(1, 10));
                    map2.GenerateMap();
                    maps.Add(map2);
                    map = maps[maps.Count - 1];
                    entities = map.entities;
                    player.Pos = new Position(map.EntrancePos.x, map.EntrancePos.y - 1);
                    screen.Clear();
                    screen.DrawNewMap();
                    floor++;
                }
            }
            else if (floor > 0)
            {
                map = maps[floor - 1];
                player.Pos = new Position(map.ExitPos.x, map.ExitPos.y + 1);
                screen.Clear();
                screen.DrawNewMap();
                floor--;
            }
        }

        public void PrintIntroScreen()
        {
            Console.Write("88888888888                                           \r\n88                                             ,d     \r\n88                                             88     \r\n88aaaaa  8b,dPPYba,   ,adPPYba,   ,adPPYba,  MM88MMM  \r\n88\"\"\"\"\"  88P'   \"Y8  a8\"     \"8a  I8[    \"\"    88     \r\n88       88          8b       d8   `\"Y8ba,     88     \r\n88       88          \"8a,   ,a8\"  aa    ]8I    88,    \r\n88       88           `\"YbbdP\"'   `\"YbbdP\"'    \"Y888  \r\n                                                      \r\n                                                      \r\n                                                      \r\n88        88               88           88            \r\n88        88               88           88            \r\n88        88               88           88            \r\n88aaaaaaaa88   ,adPPYba,   88   ,adPPYb,88            \r\n88\"\"\"\"\"\"\"\"88  a8\"     \"8a  88  a8\"    `Y88            \r\n88        88  8b       d8  88  8b       88            \r\n88        88  \"8a,   ,a8\"  88  \"8a,   ,d88            \r\n88        88   `\"YbbdP\"'   88   `\"8bbdP\"Y8            \r\n                                               ");
            Console.WriteLine(Console.LargestWindowHeight + " " + Console.LargestWindowWidth);
        }

        public void SaveGameState(string fileName)
        {
            Gamestate state = new Gamestate
            {
                Floor = floor,
                Frames = frames,
                PlayerState = player,
                MapState = maps
            };

            var json = JsonConvert.SerializeObject(state, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            System.IO.File.WriteAllText(fileName, json);

            //  System.IO.File.WriteAllText(fileName, json);
        }

        /*   new JsonSerializerSettings()
           {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
              // PreserveReferencesHandling = PreserveReferencesHandling.Objects
           });*/

        public void LoadGameState(string filename)
        {
            var json = System.IO.File.ReadAllText(filename);
            Gamestate state = JsonConvert.DeserializeObject<Gamestate>(json);
            Console.WriteLine(floor);
            floor = state.Floor;
            frames = state.Floor;
            player = state.PlayerState;
            maps = state.MapState;
            map = state.MapState[floor];
            map.entities = state.MapState[floor].entities;
        }

        public void DeleteSaveFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}