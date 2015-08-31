using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandParser
{
    public static class Commands
    {
        public static List<string> AwailableCommands = new List<string>{"/?", "/help", "-help", "-k", "-ping", "-print"}; 

        public static void Ping()
        {
            Console.WriteLine("Pinging...");
            Console.Beep();
        }

        public static void ShowHelp()
        {
            Console.WriteLine("Help!!!!!!");
        }

        public static void Print(string message)
        {
            Console.WriteLine(message);
        }

        public static void PrintKeyValue(string [] keysAndValues)
        {
            int l = keysAndValues.Length;
            if (l == 0)
            {
                Console.WriteLine("null");
            }
            else 
            {
                for (int i = 0; i <= (l%2==0? l-2:l-3); i += 2)
                {
                    Console.WriteLine("{0} - {1}", keysAndValues[i], keysAndValues[i + 1]);
                }
                if (l%2 == 1) Console.WriteLine("{0} - null", keysAndValues[l-1]);
            }
            
        }
    }
}
