using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public abstract class Menu
    {
        public Menu _lastMenu;
        private int AmountStartOpt;
        public string Title;
        public bool IsRunning;
        public List<Option> Options;
        
        public Menu(string title)
        {
            Title = title;
            Option help = new Option("Help", new Action(DisplayHelp), null);
            Option exit = new Option("Exit", new Action(Stop), null);
            Options = new List<Option>{ help, exit };
            AmountStartOpt = Options.Count;
            _lastMenu = null;
        }

        public void Add(Option option, bool isRoot)
        {
            Options.Insert(Options.Count - AmountStartOpt, option);
            if (isRoot)
            {
                Options.RemoveAt(Options.Count - 1);
                Options.Add(new Option("Exit", new Action(Stop), null));
            }
        }

        public void Add(string name, MulticastDelegate action, object[] parameters, bool isFinal)
        {
            Option o = new Option(name, action, parameters);
            if (isFinal)
            {
                o.SetFinal();
                o.Add(new Action(Stop), null);
            }

            Add(o, false);
        }

        public void Add(string name, Menu menu, bool isFinal)
        {
            _lastMenu = menu;
            Option o = new Option(name, new Action(menu.Start), null);
            if (isFinal)
            {
                o.SetFinal();
                o.Add(new Action(Stop), null);
            }
            Add(o, false);
        }

        public void AddAction(MulticastDelegate action, object[] parameters, bool isFinal)
        {
            int lastOptIndex = Options.Count - AmountStartOpt - 1;
            Option option = Options[lastOptIndex];
            
            if (_lastMenu != null)
            {
                
                Delete(lastOptIndex);
                _lastMenu.Delete(_lastMenu.Options.Count - 1);
                Add(_lastMenu.Title, _lastMenu, isFinal);
                Options[Options.Count - AmountStartOpt -1].Add(action, parameters);
            }

            if (option.IsFinal) option.DeleteLast();
            option.Add(action, parameters);

            if (isFinal)
            {
                option.SetFinal();
                option.Add(new Action(Stop), null);
            }
        }

        public void AddAction(Menu menu, bool isFinal) => AddAction(new Action(menu.Start), null, isFinal);
        
        public void Delete(int index) => Options.RemoveAt(index);

        public void Stop() => IsRunning = false;

        public abstract void Start();

        protected abstract void Update();

        protected abstract int GetChoosenOptionNumber();

        protected abstract void Display();

        protected abstract void DisplayHelp();
        
        protected abstract void DisplayMessage(string message);
    }
}