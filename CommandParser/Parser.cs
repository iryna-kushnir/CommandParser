using System;
using System.Collections.Generic;

namespace CommandParser
{
    /// <summary>
    ///     Contains methods for parsing command line arguments
    /// </summary>
    public static class Parser
    {
        /// <summary>
        ///     Method that goes through command line arguments and executes corresponding commands until -exit command happens
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Bool value that indicates if -exit command was among arguments</returns>
        public static bool ParseArgsAndExecuteCommands(string[] args)
        {
            if (args.Length == 0) Commands.ShowHelp();
            if (args.Length == 1 && (args[0] == "/?" || args[0] == "/help" || args[0] == "-help"))
            {
                Commands.ShowHelp();
            }
            for (var i = 0; i < args.Length; i++)
            {
                if (!Commands.AvailableCommands.Contains(args[i].ToLower()))
                {
                    Console.WriteLine(
                        "'{0}' command is not supported, use CommandParser.exe /? to see set of allowed commands",
                        args[i]);
                    continue;
                }
                switch (args[i].ToLower())
                {
                    case "-ping":
                    {
                        Commands.Ping();
                        break;
                    }
                    case "-print":
                    {
                        if (i + 1 == args.Length)
                            Commands.Print(null);
                        else
                        {
                            i++;
                            Commands.Print(args[i]);
                        }
                        break;
                    }
                    case "-k":
                    {
                        ChooseParamsAndInvokeBracketCommand(args, ref i, Commands.PrintKeyValue);
                        break;
                    }
                    case "-color":
                    {
                        if (i + 1 == args.Length)
                            Commands.ChangeTextColor(null);
                        else
                        {
                            i++;
                            Commands.ChangeTextColor(args[i]);
                        }
                        break;
                    }
                    case "-sort":
                    {
                        ChooseParamsAndInvokeBracketCommand(args, ref i, Commands.SortAndPrint);
                        break;
                    }
                    case "-exit":
                    {
                        Commands.Exit();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///     Finds the first occurrence of supported command at array of command line args starting from provided index
        /// </summary>
        /// <param name="startIndex">Index from which to start searching for first supported command occurrence</param>
        /// <param name="args">Command line arguments</param>
        /// <returns>Index where the first occurrence of supported command was found. If no command was found returns args length</returns>
        private static int FindIndexOfNextCommand(int startIndex, string[] args)
        {
            for (var i = startIndex; i < args.Length; i++)
            {
                if (Commands.AvailableCommands.Contains(args[i])) return i;
            }
            return args.Length;
        }

        /// <summary>
        ///     Finds the first occurrence of the closing bracket ] at array of command line args starting from provided index
        /// </summary>
        /// <param name="startIndex">Index from which to start searching for first supported command occurrence</param>
        /// <param name="args">Command line arguments</param>
        /// <returns>Index where the first occurrence of closing bracket ] was found. If no command was found returns args length</returns>
        private static int FindIndexOfNextClosingBracket(int startIndex, string[] args)
        {
            for (var i = startIndex; i < args.Length; i++)
            {
                if (args[i].EndsWith("]")) return i;
            }
            return args.Length;
        }

        /// <summary>
        ///     Selects parameters for -sort and -k commands from array of command line args and executes commands with selected
        ///     parameters. Pases either all the parameters within [] brackets or all the parameters before occurrence of next
        ///     command in case there is no brackets
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <param name="startIndex">Index from which to start looking for parameters</param>
        /// <param name="action">Command to be executed</param>
        private static void ChooseParamsAndInvokeBracketCommand(string[] args, ref int startIndex,
            Action<List<string>> action)
        {
            if (startIndex + 1 != args.Length && args[startIndex + 1].StartsWith("["))
            {
                var closingIndex = FindIndexOfNextClosingBracket(startIndex + 1, args);
                if (closingIndex == args.Length)
                {
                    var nextCommandIndex = FindIndexOfNextCommand(startIndex + 1, args);
                    action.Invoke(new List<string>(args).GetRange(startIndex + 1, nextCommandIndex - startIndex - 1));
                    startIndex = nextCommandIndex - 1;
                }
                else
                {
                    action.Invoke(
                        new List<string>(args).GetRange(startIndex + 1, closingIndex - startIndex));
                    startIndex = closingIndex;
                }
            }
            else
            {
                var nextCommandIndex = FindIndexOfNextCommand(startIndex + 1, args);
                action.Invoke(
                    new List<string>(args).GetRange(startIndex + 1, nextCommandIndex - startIndex - 1));
                startIndex = nextCommandIndex - 1;
            }
        }
    }
}