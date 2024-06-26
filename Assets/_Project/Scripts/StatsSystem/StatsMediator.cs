using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.StatsSystem
{
    public class StatsMediator
    {
        private readonly List<StatModifier> _listModifiers = new();

        public void PerformQuery(object sender, Query query)
        {
            foreach (var modifier in _listModifiers)
            {
                modifier.Handle(sender, query);
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            _listModifiers.Add(modifier);

            modifier.OnDisposed += _ => _listModifiers.Remove(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            modifier.Dispose();
            _listModifiers.Remove(modifier);
        }

        public void ClearModifiers()
        {
            foreach (var modifier in _listModifiers.ToList())
            {
                modifier.Dispose();
            }

            _listModifiers.Clear();
        }

        public void Update(float deltaTime)
        {
            foreach (var modifier in _listModifiers)
            {
                modifier.Update(deltaTime);
            }

            foreach (var modifier in _listModifiers.Where(m => m.MarkedForRemoval).ToList())
            {
                modifier.Dispose();
            }
        }
    }
}