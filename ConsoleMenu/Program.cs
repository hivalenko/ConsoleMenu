using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    delegate void MyFunc<T1>(ref T1 b);
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: add manager for menu
            var io = new IO();
            var testMenu = new MenuConsole("Root test menu", io);
            var testMenu1 = new MenuConsole("Root test menu 1", io);
            var testMenu2 = new MenuConsole("Root test menu 2", io);

            List<object> parametersS = new List<object>{"string"};
            List<object> parametersI = new List<object>{3};
            List<object> parametersIS = new List<object>{"anotherstring", 8};
            
            testMenu1.Add("write 10 stirngs", new Action<string>(Write10Lines), parametersS, true);
            testMenu2.Add("write 10 ints", new Action<int>(Write10Ints), parametersI, false);
            List<object> prevresults = new List<object>();
            testMenu2.Add("write 10 both", new Func<string, int, bool>(Write10Both), parametersIS, ref prevresults, false);
            testMenu2.AddAction(new MyFunc<bool>(IsPreviousTrue), ref prevresults, false);
            testMenu2.AddAction(new Action(final), null, false);
             
            testMenu1.AddAction(testMenu2, false);
            testMenu.Add("to menu 1", testMenu1, false);
            
            testMenu.Start();
        }

        public static void Write10Lines(string s)
        {
            for (int i = 10 - 1; i >= 0; i--)
            {
                Console.WriteLine(s);
            }

            Console.ReadLine();
        }

        public static void final()
        {
            Console.WriteLine("This is method final() added via addAction");
            Console.ReadLine();
        }
        public static void Write10Ints(int l)
        {
            for (int i = 10 - 1; i >= 0; i--)
            {
                Console.WriteLine(l);
            }

            Console.ReadLine();
        }

        public static void IsPreviousTrue(ref bool b)
        {
            Console.WriteLine(b);
            Console.ReadLine();
        }
        
        public static bool Write10Both(string s, int l)
        {
            for (int i = 10 - 1; i >= 0; i--)
            {
                Console.WriteLine(s + "       " +l);
            }
            
            Console.ReadLine();
            return true;
        }
    }
}