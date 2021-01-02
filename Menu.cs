using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public abstract class Menu
    {
        private Menu _lastMenu;
        private readonly int _amountStartOpt;
        protected readonly string Title;
        protected bool IsRunning;
        protected readonly List<Option> Options;
        
        protected Menu(string title)
        {
            Title = title;
            Option help = new Option("Help", new Action(DisplayHelp), null);
            Option exit = new Option("Exit", new Action(Stop), null);
            Options = new List<Option>{ help, exit };
            _amountStartOpt = Options.Count;
            _lastMenu = null;
        }

        public void Add(Option option, bool isRoot)
        {
            Options.Insert(Options.Count - _amountStartOpt, option);
            if (isRoot)
            {
                Options.RemoveAt(Options.Count - 1);
                List<object> results = new List<object>();
                Options.Add(new Option("Exit", new Action(Stop), null, ref results));
            }
        }

        public void Add(string name, MulticastDelegate action, List<object> parameters, ref List<object> results,
            bool isFinal)
        {
            {
                var o = new Option(name, action, parameters, ref results);
                if (isFinal)
                {
                    o.SetFinal();
                    o.Add(new Action(Stop), null);
                }

                Add(o, false);
            }   
        }
        public void Add(string name, MulticastDelegate action, List<object> parameters, bool isFinal)
        {
            var o = new Option(name, action, parameters);
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
            var o = new Option(name, new Action(menu.Start), null);
            if (isFinal)
            {
                o.SetFinal();
                o.Add(new Action(Stop), null);
            }
            Add(o, false);
        }

        public void AddAction(MulticastDelegate action, ref List<object> parameters, bool isFinal)
        {
            var lastOptIndex = Options.Count - _amountStartOpt - 1;
            var option = Options[lastOptIndex];
            
            if (_lastMenu != null)
            {
                
                Delete(lastOptIndex);
                _lastMenu.Delete(_lastMenu.Options.Count - 1);
                Add(_lastMenu.Title, _lastMenu, isFinal);
                Options[Options.Count - _amountStartOpt -1].Add(action, parameters);
            }

            if (option.IsFinal) option.DeleteLast();
            option.Add(action, parameters);

            if (isFinal)
            {
                option.SetFinal();
                option.Add(new Action(Stop), null);
            }
        }

        //public List<object> GetResults(int optionId) => Options[optionId].results;
        
        public void AddAction(Menu menu, bool isFinal) => AddAction(new Action(menu.Start), null, isFinal);

        public void AddAction(MulticastDelegate action, List<object> parameters, bool isFinal) =>
            AddAction(action, ref parameters, isFinal);
        private void Delete(int index) => Options.RemoveAt(index);

        protected void Stop() => IsRunning = false;

        public abstract void Start();

        protected abstract void Update();

        protected abstract int GetChoosenOptionNumber();

        protected abstract void Display();

        protected abstract void DisplayHelp();
        
        protected abstract void DisplayMessage(string message);
    }
}