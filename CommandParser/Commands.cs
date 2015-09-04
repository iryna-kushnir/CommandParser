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
            RemoveBrakets(ref keysAndValues);
            int l = keysAndValues.Count; 
            if (keysAndValues.Count == 0)
            {
                Console.WriteLine(
                    "You didn't specify keys and values for '-k' command. Use CommandParser.exe /? to see user help.");
            }
            else
            {
                for (var i = 0; i <= (l % 2 == 0 ? l - 2 : l - 3); i += 2)
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

        public static void SortAndPrint(List<string> list)
        {
            RemoveBrakets(ref list);
            int l = list.Count;
            if (list.Count == 0)
            {
                Console.WriteLine(
                    "You didn't specify keys and values for '-sort' command. Use CommandParser.exe /? to see user help.");
            }
            else
            {
                list.Sort();
                foreach (var item in
                list)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
            }
        }

        private static void RemoveBrakets(ref List<string> list)
        {
            int l = list.Count;
            if (l != 0)
            {
                if (list[0].StartsWith("[") && list.Last().EndsWith("]"))
                {
                    if (list[0] == "[") list.RemoveAt(0);
                    else
                    {
                        if (list[0].StartsWith("[")) list[0] = list[0].Substring(1);
                    }
                    if (list.Last() == "]") list.RemoveAt(list.Count - 1);
                    else
                    {
                        l = list.Count;
                        if (list.Last().EndsWith("]"))
                            list[l - 1] = list[l - 1].Remove(list[l - 1].Length - 1, 1);

                    }
                }
            }
        } 
    }
}