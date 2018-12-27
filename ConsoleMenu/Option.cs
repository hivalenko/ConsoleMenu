using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class Option
    {
        public List<MulticastDelegate> Actions { get; private set; }
        public List<object[]> Parameters { get; private set; }
        public string Name { get; private set; }
        public bool IsFinal;
        
        
        public Option(string name, MulticastDelegate action, object[] parameters)
        {
            Name = name;
            Actions = new List<MulticastDelegate>{ action };
            Parameters = new List<object[]>{ parameters };
            IsFinal = false;
        }

        public void Add(MulticastDelegate action, object[] parameters)
        {
            Actions.Add(action);
            Parameters.Add(parameters);
        }

        public void DeleteLast()
        {
            Actions.RemoveAt(Actions.Count - 1);
            Parameters.RemoveAt(Parameters.Count - 1);
        }
        
        public void SetFinal()
        {
            IsFinal = true;
        }
        
        public void Start()
        {
            foreach (var action in Actions)
            {
                action.DynamicInvoke(Parameters[Actions.IndexOf(action)]);
            }
        }
    }
}