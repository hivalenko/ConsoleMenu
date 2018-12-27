using System;

namespace ConsoleMenu
{
    public class IO
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void GetEmptyEnter()
        {
            Console.ReadLine();
        } 
        
        public void Clean() => Console.Clear();

        public string GetConsoleInput() => Console.ReadLine();
    }
}