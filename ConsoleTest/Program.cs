using KainLabs.CliUtilities;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NLog.LogManager.LoadConfiguration("NLog.config");


            Console.WriteLine("Hello World!");
            var menu = new Menu();
            menu.Run();
        }
    }
}
