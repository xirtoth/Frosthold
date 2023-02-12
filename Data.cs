using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frosthold
{
    public static class Data
    {
        public static List<(string Name, string Symbol, ConsoleColor Color)> monsterList = new List<(string, string, ConsoleColor)>
{
    ("Minotaur", "M", ConsoleColor.Red),
    ("Medusa", "M", ConsoleColor.Green),
    ("Mothman", "M", ConsoleColor.Yellow),
    ("Manticore", "M", ConsoleColor.DarkMagenta),
    ("Chimera", "C", ConsoleColor.Cyan),
    ("Hydra", "H", ConsoleColor.DarkRed),
    ("Gorgon", "G", ConsoleColor.DarkGreen),
    ("Phoenix", "P", ConsoleColor.DarkYellow),
    ("Dragon", "D", ConsoleColor.DarkCyan),
    ("Vampire", "V", ConsoleColor.DarkGray)
};
    }
}