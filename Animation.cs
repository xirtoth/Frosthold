namespace Frosthold
{
    public enum AnimationType
    {
        Ray,
        Explosion,
    }

    public class Animation
    {
        public AnimationType AnimationType;
        public Position StartPos;
        public Position EndPos;
        private GameController gc = GameController.Instance;

        public Animation(AnimationType anim, Position startPos, Position endPos)
        {
            AnimationType = anim;
            StartPos = startPos;
            EndPos = endPos;
        }

        public Animation()
        {
        }

        public void Start()
        {
            List<Position> oldSteps = new List<Position>();
            switch (AnimationType)
            {
                case AnimationType.Ray:

                    int deltaX = EndPos.x - StartPos.x;
                    int deltaY = EndPos.y - StartPos.y;
                    int steps = Math.Max(Math.Abs(deltaX), Math.Abs(deltaY));

                    for (int i = 0; i <= steps; i++)
                    {
                        int x = StartPos.x + (i * deltaX / steps);
                        int y = StartPos.y + (i * deltaY / steps);
                        oldSteps.Add(new Position(x,y));
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

                default:
                    return;
            }
        }
    }
}