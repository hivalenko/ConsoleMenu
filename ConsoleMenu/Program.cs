using System;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenu testMenu = new ConsoleMenu("Root test menu");
            ConsoleMenu testMenu1 = new ConsoleMenu("Root test menu 1");
            ConsoleMenu testMenu2 = new ConsoleMenu("Root test menu 2");

            object[] parametersS = {"string"};
            object[] parametersI = {3};
            object[] parametersIS = {"anotherstring", 8};
            
            testMenu1.Add("write 10 stirngs", new Action<string>(Write10Lines), parametersS, true);
            testMenu2.Add("write 10 ints", new Action<int>(Write10Ints), parametersI, false);
            testMenu2.Add("write 10 both", new Action<string, int>(Write10Both), parametersIS, false);
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

        public static void Write10Both(string s, int l)
        {
            for (int i = 10 - 1; i >= 0; i--)
            {
                Console.WriteLine(s + "       " +l);
            }

            Console.ReadLine();
        }
    }
}