using System;

namespace ConsoleMenu
{
    public static class IO
    {
        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void GetEmptyEnter()
        {
            Console.ReadLine();
        } 
        
        public static void Clean() => Console.Clear();

        public static string GetConsoleInput() => Console.ReadLine();
    }
}