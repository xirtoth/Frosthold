namespace Frosthold
{
    public class InspectKeyBinds : KeyBinds

    {
        public GameController gc;

        //keybindit joita käytetään kun tarkastellaan ruudulla olevia entityjä
        public InspectKeyBinds() : base(new InputParser())
        {
            this.gc = GameController.Instance;

            AddBinds();
        }

        private void AddBinds()
        {
            this.ip.AddKey(ConsoleKey.Spacebar, () =>  gc.inspecting = false);

            ip.AddKey(ConsoleKey.RightArrow, () => gc.screen.MoveCursor(1, 0));
            ip.AddKey(ConsoleKey.LeftArrow, () => gc.screen.MoveCursor(-1, 0));
            ip.AddKey(ConsoleKey.UpArrow, () => gc.screen.MoveCursor(0, -1));
            ip.AddKey(ConsoleKey.DownArrow, () => gc.screen.MoveCursor(0, 1));
        }
    }
}