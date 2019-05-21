using System;

namespace KainLabs.CliUtilities
{
    public static class KLConsole
    {
        public static void Clear()
        {
            Console.Clear();
        }

        public static int ReadInt()
        {
            return ReadInt(string.Empty);
        }

        public static int ReadInt(string prompt)
        {
            int returnValue = 0;

            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();
                if (int.TryParse(value, out returnValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invaild input");
                }
            }

            return returnValue;
        }

        public static void ReadKey()
        {
            ReadKey(string.Empty);
        }

        public static void ReadKey(string prompt)
        {
            Console.Write(prompt);
            Console.ReadKey();
        }

        public static string ReadString()
        {
            return ReadString(string.Empty);
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static void WriteLine()
        {
            WriteLine(string.Empty);
        }

        public static void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
    }
}