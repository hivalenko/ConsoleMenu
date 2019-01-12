using System;
using System.IO;
using System.Reflection;

namespace ConsoleMenu
{
    public class MenuConsole : Menu
    {
        private const string HelpInfo = "Your help info";
        private readonly IO _io;

        public MenuConsole(string title, IO io) : base(title)
        {
            _io = io;
        }

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
                    _io.PrintMessage("Please, choose existing option.");
                    Update();
                    continue;
                }
                catch (FormatException)
                {
                    _io.PrintMessage("Incorrect option. Please, try again.");
                    Update();
                    continue;
                }
                catch (ArgumentException e)
                {
                    _io.PrintMessage(e.Message);
                    Update();
                    continue;
                }
                catch (InvalidDataException e)
                {
                    _io.PrintMessage(e.Message);
                    Update();
                    continue;
                }
                catch (InvalidOperationException e)
                {
                    _io.PrintMessage(e.Message);
                    Update();
                    continue;
                }
                /*catch (TargetInvocationException e)
                {
                    _io.PrintMessage(e.Message);
                    Update();
                    continue;
                }*/
                if (Options[optionNumber].IsFinal)
                {
                    Stop();
                }
            }
        }
        
        protected override void Display()
        {
            _io.Clean();
            _io.PrintMessage("Current part of program: " + Title);
            _io.PrintMessage("Possible options:");
            
            for (int i = 0; i < Options.Count; i++)
            {
                _io.PrintMessage(i + 1 + "." + Options[i].Name);
            }
        }

        protected override int GetChoosenOptionNumber()
        {
            var s = _io.GetConsoleInput();
            return int.Parse(s) - 1;
        }

        protected override void DisplayHelp() => DisplayMessage(HelpInfo);

        protected override void Update()
        {
            _io.PrintMessage("To continue enter anything");
            _io.GetEmptyEnter();
        }

        protected override void DisplayMessage(string message)
        {
            _io.PrintMessage(message);
            _io.GetEmptyEnter();
        }
    }
}