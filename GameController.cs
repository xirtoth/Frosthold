using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public class GameController
    {
        public static GameController? Instance { get; set; }
        Player player;
        public List<Entity> entities { get; set; }
        public int frames { get; set; }
        public Screen screen;
        private InputParser ip;
        public InspectKeyBinds ikb { get; set; }
        public MainInputs mkb { get; set; }
        public Map map;
        public KeyBinds mainKeys;
        public bool inspecting { get; set; }
        
        public bool running { get; set; }
        public GameController()
        {
            this.running = false;
            entities = new List<Entity>();
        }

        //Luodaan tarvittavat muuttujat
        public void Init()
        {

            this.inspecting = false;
            Random rnd = new Random();
            GenerateLevel();
            Entity e = new Entity("jorma", "jorma on iso paha mörkö", "J", new Position(10, 10), ConsoleColor.Red);
            entities.Add(e);
            
            entities.Add(new Monster("lohari", "Iso paha lohikäärme", "D", 100, 100, 100, new Position(15, 2), ConsoleColor.Magenta));
            for(int i = 0; i < 10; i++)
            {
                entities.Add(new Monster("keijo", "keijo on iso paha kissa", "K", 100, 100, 100, new Position(15, 15), (ConsoleColor)rnd.Next(Enum.GetValues(typeof(ConsoleColor)).Length)));
             
            }
            this.player = CreatePlayer();
            
            Map map = new Map(50, 50, 5);
            this.map = map;
            map.GenerateMap();
            screen = new Screen(50, 50, player, entities, map);
            this.running = true;
            this.frames = 0;
            this.mkb = new MainInputs(player, screen);
            this.ikb = new InspectKeyBinds(player, screen);
            
            this.mainKeys = new KeyBinds(this.ip, player);
            //ip.AddKey(ConsoleKey.K, () => player.MovePlayer(1, 1));
        }

        private void GenerateLevel()
        {
            Random rand = new Random();
            Map map = new Map(50, 50, 3);
            map.GenerateMap();
            
        }

        private static Player CreatePlayer()
        {
            Player player;
            while (true)
            {

                Console.Write("Give name: ");
                string name = Console.ReadLine();
                if (name != null && name.Length > 0 && name.Length <= 10)
                {
                    player = new Player(5, 5, name);
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
            while(running == true)
            {

                
                //player.MovePlayer(1, 0);
                //päivitetään ruutu
                screen.UpdateScreen();
                var input = ReadInput().Key;
                mkb.ip.ParseInput(input);
                //liikutetaan vihollisia
                MoveEnemies(entities);
                //testausta varten odotetaan sekuntti kunnes jatketaan looppia
                //Thread.Sleep(1000);
                

                frames++;
                
              /*  if(frames == 2)
                {
                    List<Entity> tempEntities = entities;
                    for(int i = entities.Count - 1; i >= 0; i--) 
                    {
                        if(entities[i] is Monster)
                        {

                            Monster monster = (Monster)entities[i];
                            monster.Die();
                            
                        }
                    }
                }*/
                

            }
        }

        //tätä voidaan käyttää, jos halutaan, että tiettyjen liikkeiden jälkeen tapahtuu jotain (tällähetkellä hain testaamis metodi)
        private void CheckRandomEvents()
        {
            Random rand = new Random();
            if(frames % 2 == 0)
            {
                entities[1].MoveEntity(rand.Next(-1,1), rand.Next(-1, 1));
            }
        }

        //käydään läpi entity lista, ja jos entity on Monster. Voidaan laittaa se tekemään erinäisiä toimintoja.
        private void MoveEnemies(List<Entity> entities)
        {
            Random rand = new Random();
            foreach(Entity e in entities)
            {
                if(e is Monster)
                {
                    Monster m = (Monster)e;
                    m.MoveEntity(rand.Next(-1,2), rand.Next(-1,2));
                }
            }
        }

        //pistetaan enity listasta
        public void RemoveEntity(Entity entity)
        {
            Console.Write("Removing " + entity.name + ".");
            entities.Remove(entity);
        }

        public void Inspect()
        {
            inspecting = true;
            Console.SetCursorPosition(player.Pos.x, player.Pos.y);
            Console.CursorVisible = true;
            while(inspecting)
            {
                
                var input = ReadInput();
                ikb.ip.ParseInput(input.Key);
               
                foreach (Entity e in entities)
                {
                    if (e.Pos.x == Console.CursorLeft && e.Pos.y == Console.CursorTop)
                    {
                        var cursorOldPosition = Console.GetCursorPosition();
                        Console.SetCursorPosition(25, 25);
                        Screen.Write(e.name + " " + e.description);
                        Console.SetCursorPosition(cursorOldPosition.Left, cursorOldPosition.Top);
                    }
                }
            }
        }
    }
}
