namespace Frosthold
{
    public enum AnimationType
    {
        Ray,
        Explosion,
        Aura
    }

    public class Animation
    {
        public AnimationType AnimationType;
        public Position StartPos;
        public Position EndPos;
        private GameController gc = GameController.Instance;
        private AnimationType aura;
        private Position pos;

        public Animation(AnimationType anim, Position startPos, Position endPos)
        {
            AnimationType = anim;
            StartPos = startPos;
            EndPos = endPos;
        }

        public Animation()
        {
        }

        public Animation(AnimationType aura, Position pos)
        {
            this.AnimationType = aura;
            this.pos = pos;
        }

        public void Start()
        {
            List<Position> oldSteps = new List<Position>();
            switch (AnimationType)
            {
                case AnimationType.Ray:

                    if (EndPos == null)
                    {
                        return;
                    }
                    int deltaX = EndPos.x - StartPos.x;
                    int deltaY = EndPos.y - StartPos.y;
                    int steps = Math.Max(Math.Abs(deltaX), Math.Abs(deltaY));

                    for (int i = 0; i <= steps; i++)
                    {
                        int x = StartPos.x + (i * deltaX / steps);
                        int y = StartPos.y + (i * deltaY / steps);
                        oldSteps.Add(new Position(x, y));
                        Console.SetCursorPosition(x, y);
                        Console.Write("*");
                        Thread.Sleep(5);
                    }
                    /* for (int i = oldSteps.Count -1; i > 0; i--)
                     {
                         Console.SetCursorPosition(oldSteps[i].x, oldSteps[i].y);
                         Console.Write(" ");
                     }
                     break;*/
                    gc.screen.Clear();
                    gc.screen.PrintMap();
                    break;

                case AnimationType.Aura:

                    gc.screen.Clear();
                    gc.screen.DrawNewMap();
                    int playerX = gc.player.Pos.x;
                    int playerY = gc.player.Pos.y;
                    Console.SetCursorPosition(playerX, playerY);
                    //Console.Write("DEBUG");

                    for (int i = playerX - 1; i <= playerX + 1; i++)
                    {
                        for (int j = playerY - 1; j <= playerY + 1; j++)
                        {
                            Console.SetCursorPosition(i, j);

                            Console.BackgroundColor = ConsoleColor.Green;
                            switch (gc.map.MapArray[i - 1, j - 1])
                            {
                                case TileTypes.floor:
                                    Screen.Write(".");
                                    break;

                                case TileTypes.wall:
                                    Screen.Write("#");
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    Console.ResetColor();
                    break;

                default:
                    Console.Write("HGMMMMMMMMMMMMM");
                    return;
            }
        }
    }
}