using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandParser
{
    public static class Parser
    {
        public static void ParseArgsAndExecuteCommands(string [] args)
        {
            if (args.Length == 0) Commands.ShowHelp();
            if (args.Length == 1 && (args[0] == "/?" || args[0] == "/help" || args[0] == "-help"))
            {
                Commands.ShowHelp();
            }
            for (int i = 0; i < args.Length; i++)
            {

                if (!Commands.AwailableCommands.Contains(args[i])) {Console.WriteLine("'{0}' command is not supported, use CommandParser.exe /? to see set of allowed commands", args[i]); continue;}
                switch (args[i])
                {
                    case "-ping": {Commands.Ping(); break;}
                    case "-print":
                    {
						if (i + 1 == args.Length)
							Commands.Print (null);
                        else
                        {
                            i++;
                            Commands.Print(args[i]);
                        }
                        break;
                    }
                    case "-k":
                    {
                        int nextCommandIndex = FindIndexOfNextCommand(i + 1, args);
                        Commands.PrintKeyValue(new List<string>(args).GetRange(i + 1, nextCommandIndex - i - 1).ToArray());
                        i = nextCommandIndex - 1;
                        break;
                    }
                }
    
                
            }
        }

        public static int FindIndexOfNextCommand(int startIndex, string[] args)
        {
            for (int i = startIndex; i < args.Length; i++)
            {
                if (Commands.AwailableCommands.Contains(args[i])) return i;
            }
            return args.Length;
        }
    }
}
