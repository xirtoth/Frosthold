using Frosthold;
using System;

class NetHackScreen
{
    
    static void Main(string[] args)
    {
        
        GameController gc = new GameController();
        //luodaan instance GameController classista, jotta voidaan käyttää tätä muissa luokissa lisäämättä site construktoriin. (Tässä voisi myös käyttää Singelton mutta täällä mennään nyt alkuun)-
        GameController.Instance = gc;
        gc.Init();
        gc.Start();
        
    }

   

    
}
