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
            if (args.Length == 1)
            {
                
            }
        }
    }
}
