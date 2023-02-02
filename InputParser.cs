namespace Frosthold
{
    public class InputParser
    {
        private Dictionary<ConsoleKey, Action> keyMap;
        private GameController gc = GameController.Instance;

        public InputParser()
        {
            keyMap = new Dictionary<ConsoleKey, Action>();
        }

        public void AddKey(ConsoleKey key, Action action)
        {
            keyMap.Add(key, action);
        }

        public void ParseInput(ConsoleKey key)
        {
            if (keyMap.ContainsKey(key))
            {
                keyMap[key]();
            }
        }
    }
}