using System;

namespace CommandParser
{
    /// <summary>
    ///     Class that contains Main for CommandParser
    /// </summary>
    public class CommandParserMain
    {
        /// <summary>
        ///     Starting point of CommandParser. Calls method that parses command line arguments and executes corresponding
        ///     commands.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static void Main(string[] args)
        {
            var exit = Parser.ParseArgsAndExecuteCommands(args);
            while (!exit)
            {
                exit = Parser.ParseArgsAndExecuteCommands(Console.ReadLine().Split(' '));
            }
        }
    }
}