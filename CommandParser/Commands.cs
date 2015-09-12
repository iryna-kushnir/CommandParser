using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandParser
{
    /// <summary>
    ///     Contains methods that execute all the supported commands
    /// </summary>
    public static class Commands
	{
        /// <summary>
        ///     The list of supported commands
        /// </summary>
        private static List<string> _awailableCommands = new List<string> {
			"/?",
			"/help",
			"-help",
			"-k",
			"-ping",
			"-print",
			"-color",
			"-sort",
			"-exit"
		};

		/// <summary>
		/// 	Gets the awailable commands
		/// </summary>
		/// <value>The awailable commands</value>
		public static List<string> AwailableCommands{
			get{ 
				return _awailableCommands;
			}
		}

        /// <summary>
        ///     Prints user help to console - description of all supported commands. Corresponds to /?, /help and -help commands
        /// </summary>
        public static void ShowHelp()
        {
            Console.WriteLine(@"Welcome to CommandParser! 
            
Here is the list of supported commands:
/? - Shows user help
/help - Shows user help
-help - Shows user help
-k [key value] - Prints parameters as key-value pairs. Prints all the parameters within [] brackets or if there is no brackets takes all the parameters until occurrence of the next command
-ping - Makes a pinging sound
-print <message> - Prints provided message
-color <color_name> - Changes text color to color_name
-sort [values to sort] - Sorts and prints provided values. Sorts all the values within [] brackets or if there is no brackets takes all the values until occurrence of the next command
-exit - exits CommandParser

Usage example: CommandParser.exe -k [key1 value1 key2 value2]

You can pass a few commands simultaneously and they will be executed in turn!");
        }

        /// <summary>
        ///     Beeps at console. Corresponds to -ping command
        /// </summary>
        public static void Ping()
        {
            Console.WriteLine("Pinging...");
            Console.Beep();
        }

        /// <summary>
        ///     Prints provided message to console. Corresponds to -print command
        /// </summary>
        /// <param name="message">Message to be printed to console</param>
        public static void Print(string message)
        {
            if (message == null)
                Console.WriteLine(
                    "You didn't specify message for '-print' command. Use CommandParser.exe /? to see user help.");
            else
                Console.WriteLine(message);
        }

        /// <summary>
        ///     Prints items at provided list as pairs of keys and values. E.g.:
        ///     key1 - value1
        ///     key2 - value2
        ///     Corresponds to -k command
        /// </summary>
        /// <param name="keysAndValues">List of keys and values to be printed</param>
        public static void PrintKeyValue(List<string> keysAndValues)
        {
            RemoveBrakets(keysAndValues);
            var l = keysAndValues.Count;
            if (keysAndValues.Count == 0)
            {
                Console.WriteLine(
                    "You didn't specify keys and values for '-k' command. Use CommandParser.exe /? to see user help.");
            }
            else
            {
                for (var i = 0; i <= (l%2 == 0 ? l - 2 : l - 3); i += 2)
                {
                    Console.WriteLine("{0} - {1}", keysAndValues[i], keysAndValues[i + 1]);
                }
                if (l%2 == 1)
                    Console.WriteLine("{0} - null", keysAndValues[l - 1]);
            }
        }

        /// <summary>
        ///     Changes color of text in console to provided one. If there was provided invalid color name, the text color gets
        ///     changed back to white. Corresponds to -color command
        /// </summary>
        /// <param name="color">Desired color of text in console</param>
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

        /// <summary>
        ///     Sorts and prints to console provided list. Corresponds to -sort command
        /// </summary>
        /// <param name="list"></param>
        public static void SortAndPrint(List<string> list)
        {
            RemoveBrakets(list);
            var l = list.Count;
            if (list.Count == 0)
            {
                Console.WriteLine(
                    "You didn't specify keys and values for '-sort' command. Use CommandParser.exe /? to see user help.");
            }
            else
            {
				List<long> numbers = new List<long> ();
				foreach (var item in list)
				{
					long parsingResult = 0;
					if (long.TryParse (item, out parsingResult)) {
						numbers.Add (parsingResult);
					} else
						break;
				}
				if (numbers.Count == list.Count) 
				{
					numbers.Sort ();
					foreach (var number in numbers) 
					{
						Console.Write ("{0} ", number);
					}
					Console.WriteLine ();
				} else 
				{
					list.Sort ();
					foreach (var item in list) 
					{
						Console.Write ("{0} ", item);
					}
					Console.WriteLine ();
				}
            }
        }

		/// <summary>
		/// 	Prints exiting CommandParser text
		/// </summary>
		public static void Exit()
		{
			Console.WriteLine ("Exiting CommandParser...");
		}

        /// <summary>
        ///     Removes [] brackets from the provided list of parameters (if both starting [ and closing ] brackets surround the
        ///     list)
        /// </summary>
        /// <param name="list">List of parameters to remove [] from</param>
        private static void RemoveBrakets(List<string> list)
        {
            var l = list.Count;
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