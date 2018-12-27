using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class Option
    {
        private readonly List<MulticastDelegate> _actions;
        private readonly List<object[]> _parameters;
        public string Name { get; }
        public bool IsFinal { get; private set; }
        
        
        public Option(string name, MulticastDelegate action, object[] parameters)
        {
            Name = name;
            _actions = new List<MulticastDelegate>{ action };
            _parameters = new List<object[]>{ parameters };
            IsFinal = false;
        }

        public void Add(MulticastDelegate action, object[] parameters)
        {
            _actions.Add(action);
            _parameters.Add(parameters);
        }

        public void DeleteLast()
        {
            _actions.RemoveAt(_actions.Count - 1);
            _parameters.RemoveAt(_parameters.Count - 1);
        }
        
        public void SetFinal()
        {
            IsFinal = true;
        }
        
        public void Start()
        {
            foreach (var action in _actions)
            {
                action.DynamicInvoke(_parameters[_actions.IndexOf(action)]);
            }
        }
    }
}