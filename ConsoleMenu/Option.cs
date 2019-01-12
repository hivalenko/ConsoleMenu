using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class Option
    {
        private readonly List<MulticastDelegate> _actions;
        private List<List<object>> _parameters;
        public List<object> _results;
        public string Name { get; }
        public bool IsFinal { get; private set; }
        
        public Option(string name, MulticastDelegate action, List<object> parameters)
        {
            Name = name;
            _actions = new List<MulticastDelegate>{ action };
            _parameters = new List<List<object>>();
            _parameters.Add(parameters);
            IsFinal = false;
        }
        
        public Option(string name, MulticastDelegate action, List<object> parameters, ref List<object> results)
        {
            Name = name;
            _actions = new List<MulticastDelegate>{ action };
            _parameters = new List<List<object>>();
            _parameters.Add(parameters);
            _results = results;
            IsFinal = false;
        }

        public void Add(MulticastDelegate action, List<object> parameters)
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
                int number = _actions.IndexOf(action);
                var parameter = _parameters[number];
                object result = null;
                if (parameter != null)
                {
                    result = action.DynamicInvoke(parameter.ToArray());
                }
                else
                {
                    action.DynamicInvoke(null);
                }
                if(result != null)
                _results.Insert(_actions.IndexOf(action),result);
            }
        }
    }
}