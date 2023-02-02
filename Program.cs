using Frosthold;

internal class NetHackScreen
{
    private static void Main(string[] args)
    {
        GameController? gc = new GameController();
        //luodaan instance GameController classista, jotta voidaan käyttää tätä muissa luokissa lisäämättä site construktoriin. (Tässä voisi myös käyttää Singelton mutta täällä mennään nyt alkuun)
        //ongelmana tässä, että compiler huutaa varoitusta "Derefrence of a possibly null refrence". Mutta tässä tapauksessa tiedetään, että gc ei ole null.
        GameController.Instance = gc;
        gc.Init();
        gc.Start();
    }
}