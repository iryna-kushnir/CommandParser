using System;
using System.Collections.Generic;

namespace CommandParser
{
    public static class Parser
    {
        public static void ParseArgsAndExecuteCommands(string[] args)
        {
            if (args.Length == 0) Commands.ShowHelp();
            if (args.Length == 1 && (args[0] == "/?" || args[0] == "/help" || args[0] == "-help"))
            {
                Commands.ShowHelp();
            }
            for (var i = 0; i < args.Length; i++)
            {
                if (!Commands.AwailableCommands.Contains(args[i]))
                {
                    Console.WriteLine(
                        "'{0}' command is not supported, use CommandParser.exe /? to see set of allowed commands",
                        args[i]);
                    continue;
                }
                switch (args[i])
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
                        //if (i + 1 != args.Length && args[i + 1].StartsWith("["))
                        //{ 
                          
                        //    var closingIndex = FindIndexOfNextClosingBracket(i + 1, args);
                        //    if (closingIndex == args.Length)
                        //    {
                        //        var nextCommandIndex = FindIndexOfNextCommand(i + 1, args);
                        //        Commands.PrintKeyValue(
                        //            new List<string>(args).GetRange(i + 1, nextCommandIndex - i - 1));
                        //        i = nextCommandIndex - 1;
                        //    }
                        //    else
                        //    {
                                
                        //        Commands.PrintKeyValue(
                        //            new List<string>(args).GetRange(i + 1, closingIndex - i));
                        //        i = closingIndex;
                        //    }

                        //}
                        //else
                        //{
                        //    var nextCommandIndex = FindIndexOfNextCommand(i + 1, args);
                        //    Commands.PrintKeyValue(
                        //        new List<string>(args).GetRange(i + 1, nextCommandIndex - i - 1));
                        //    i = nextCommandIndex - 1;
                           
                        //}
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
                        //if (i + 1 != args.Length && args[i + 1].StartsWith("["))
                        //{

                        //    var closingIndex = FindIndexOfNextClosingBracket(i + 1, args);
                        //    if (closingIndex == args.Length)
                        //    {
                        //        var nextCommandIndex = FindIndexOfNextCommand(i + 1, args);
                        //        Commands.SortAndPrint(
                        //            new List<string>(args).GetRange(i + 1, nextCommandIndex - i - 1));
                        //        i = nextCommandIndex - 1;
                        //    }
                        //    else
                        //    {

                        //        Commands.SortAndPrint(
                        //            new List<string>(args).GetRange(i + 1, closingIndex - i));
                        //        i = closingIndex;
                        //    }

                        //}
                        //else
                        //{
                        //    var nextCommandIndex = FindIndexOfNextCommand(i + 1, args);
                        //    Commands.SortAndPrint(
                        //        new List<string>(args).GetRange(i + 1, nextCommandIndex - i - 1));
                        //    i = nextCommandIndex - 1;

                        //}
                        break;
                    }
                }
            }
        }

        private static int FindIndexOfNextCommand(int startIndex, string[] args)
        {
            for (var i = startIndex; i < args.Length; i++)
            {
                if (Commands.AwailableCommands.Contains(args[i])) return i;
            }
            return args.Length;
        }

        private static int FindIndexOfNextClosingBracket(int startIndex, string[] args)
        {
            for (var i = startIndex; i < args.Length; i++)
            {
                
                if (args[i].EndsWith("]")) return i;
            }
            return args.Length;
        }

        private static void ChooseParamsAndInvokeBracketCommand(string[] args, ref int startIndex, Action<List<string>> action)
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