namespace CommandParser
{
    public class CommandParserMain
    {
        private static void Main(string[] args)
        {
            Parser.ParseArgsAndExecuteCommands(args);
        }
    }
}