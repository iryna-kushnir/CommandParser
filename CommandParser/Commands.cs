using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandParser
{
    public static class Commands
    {
        public static List<string> AwailableCommands = new List<string>
        {
            "/?",
            "/help",
            "-help",
            "-k",
            "-ping",
            "-print",
            "-color",
            "-sort"
        };

        public static void ShowHelp()
        {
            Console.WriteLine("Help!!!!!!");
        }

        public static void Ping()
        {
            Console.WriteLine("Pinging...");
            Console.Beep();
        }

        public static void Print(string message)
        {
            if (message == null)
                Console.WriteLine(
                    "You didn't specify message for '-print' command. Use CommandParser.exe /? to see user help.");
            else
                Console.WriteLine(message);
        }

        public static void PrintKeyValue(List<string> keysAndValues)
        {

            if (keysAndValues.Count == 0 || (keysAndValues[0] == "[" && keysAndValues.Last() == "]"))
            {
                Console.WriteLine(
                    "You didn't specify keys and values for '-k' command. Use CommandParser.exe /? to see user help.");
            }
            else
            {
                if (keysAndValues[0] == "[") keysAndValues.RemoveAt(0);
                if (keysAndValues[0].StartsWith("[")) keysAndValues[0] = keysAndValues[0].Substring(1);
                if (keysAndValues.Last() == "]") keysAndValues.RemoveAt(keysAndValues.Count - 1);
                var l = keysAndValues.Count;
                if (keysAndValues.Last().EndsWith("]")) keysAndValues[l - 1] = keysAndValues[l - 1].Remove(l - 1, 1);
                for (var i = 0; i <= (l%2 == 0 ? l - 2 : l - 3); i += 2)
                {
                    Console.WriteLine("{0} - {1}", keysAndValues[i], keysAndValues[i + 1]);
                }
                if (l%2 == 1)
                    Console.WriteLine("{0} - null", keysAndValues[l - 1]);
            }
        }

        public static void ChangeTextColor(string color)
        {
            ConsoleColor parsedColor;
            if (Enum.TryParse(color, true, out parsedColor))
            {
                Console.ForegroundColor = parsedColor;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("'{0}' is not a color.", color);
            }
        }
    }
}