using Frosthold;
using System;

class NetHackScreen
{
    
    static void Main(string[] args)
    {
        GameController gc = new GameController();
        GameController.Instance = gc;
        gc.Init();
        gc.Start();
        
    }

   

    
}
