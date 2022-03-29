using System;
using PowerArgs;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Args.InvokeAction<Methods>(args.Length == 0 ? new[] { "-h" } : args);

        }
    }
}
