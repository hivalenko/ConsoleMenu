using System;
using System.IO;
using static ConsoleMenu.IO;

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
                catch (ArgumentOutOfRangeException)
                {
                    PrintMessage("Please, choose existing option.");
                    Update();
                    continue;
                }
                catch (FormatException)
                {
                    PrintMessage("Incorrect option. Please, try again.");
                    Update();
                    continue;
                }
                catch (ArgumentException e)
                {
                    PrintMessage(e.Message);
                    Update();
                    continue;
                }
                catch (InvalidDataException e)
                {
                    PrintMessage(e.Message);
                    Update();
                    continue;
                }
                catch (InvalidOperationException e)
                {
                    PrintMessage(e.Message);
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
            Clean();
            PrintMessage("Current part of program: " + Title);
            PrintMessage("Possible options:");
            
            for (int i = 0; i < Options.Count; i++)
            {
                PrintMessage(i + 1 + "." + Options[i].Name);
            }
        }

        protected override int GetChoosenOptionNumber()
        {
            var s = GetConsoleInput();
            return int.Parse(s) - 1;
        }

        protected override void DisplayHelp() => DisplayMessage(HelpInfo);

        protected override void Update()
        {
            PrintMessage("To continue enter anything");
            GetEmptyEnter();
        }

        protected override void DisplayMessage(string message)
        {
            PrintMessage(message);
            GetEmptyEnter();
        }
    }
}