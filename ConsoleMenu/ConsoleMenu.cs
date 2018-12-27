using System;
using System.IO;

namespace ConsoleMenu
{
    public class ConsoleMenu : Menu
    {
        private const string HelpInfo = "Your help info";
        
        public ConsoleMenu(string title) : base(title){}

        public override void Start()
        {
            IsRunning = true;
            while (IsRunning)
            {
                Display();
                int optionNumber;
                try
                {
                    optionNumber = GetChoosenOptionNumber();
                    Options[optionNumber].Start();
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Please, choose existing option.");
                    Update();
                    continue;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Incorrect option. Please, try again.");
                    Update();
                    continue;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Update();
                    continue;
                }
                catch (InvalidDataException e)
                {
                    Console.WriteLine(e.Message);
                    Update();
                    continue;
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                    Update();
                    continue;
                }
                if (Options[optionNumber].IsFinal)
                {
                    Stop();
                }
            }
        }
        
        protected override void Display()
        {
            Console.Clear();
            Console.WriteLine("Current part of program: " + Title);
            Console.WriteLine("Possible options:");
            
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine(i + 1 + "." + Options[i].Name);
            }
        }

        protected override int GetChoosenOptionNumber()
        {
            var s = Console.ReadLine();
            return int.Parse(s) - 1;
        }

        protected override void DisplayHelp() => DisplayMessage(HelpInfo);

        protected override void Update()
        {
            Console.WriteLine("To continue type anything");
            Console.ReadLine();
        }

        protected override void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}