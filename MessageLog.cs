using System;
using System.Collections.Generic;

namespace Frosthold
{
    public class MessageLog
    {
        private List<string> messages;
        private int maxMessages;
        private int mapWidth;

        public MessageLog(int mapWidth, int maxMessages = 10)
        {
            this.mapWidth = mapWidth;
            this.maxMessages = maxMessages;
            messages = new List<string>();
        }

        public void AddMessage(string message)
        {
            if (messages.Count == maxMessages)
            {
                messages.RemoveAt(0);
            }

            messages.Add(message);
        }

        public void PrintMessages()
        {
            for (int i = messages.Count - 1; i >= 0; i--)
            {
                var spaces = Console.WindowWidth - (mapWidth - 1) - messages[i].Length;
                Console.SetCursorPosition(mapWidth + 1, i);
                // Console.WriteLine(new string(' ', Console.WindowWidth - mapWidth));
                Console.WriteLine(messages[i] + new string(' ', spaces));
            }
        }
    }
}