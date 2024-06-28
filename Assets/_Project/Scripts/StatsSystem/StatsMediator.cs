using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.StatsSystem.ModificationOrder;
using _Project.Scripts.StatsSystem.ModificationOrder.abstractions;

namespace _Project.Scripts.StatsSystem
{
    public class StatsMediator
    {
        public event Action<List<StatModifier>> OnModifiersChanged;
        
        private readonly List<StatModifier> _listModifiers = new();
        private readonly IStatModifierOder _statModifierOder = new NormalModificationOrder();
        private readonly Dictionary<StatType, IEnumerable<StatModifier>> _statTypeToModifiersCache = new();

        public List<StatModifier> ListModifiers => _listModifiers;

        public void PerformQuery(object sender, Query query)
        {
            if (!_statTypeToModifiersCache.ContainsKey(query.StatType))
            {
                _statTypeToModifiersCache[query.StatType] =
                    _listModifiers.Where(m => m.StatType == query.StatType).ToList();
            }

            query.Value = _statModifierOder.Apply(_statTypeToModifiersCache[query.StatType], query.Value);
        }

        public void AddModifier(StatModifier modifier)
        {
            _listModifiers.Add(modifier);
            InvalidateCache(modifier.StatType);
            OnModifiersChanged?.Invoke(_listModifiers);
            
            modifier.OnDisposed += _ => _listModifiers.Remove(modifier);
            modifier.OnDisposed += m => InvalidateCache(m.StatType);
            modifier.OnDisposed += m => OnModifiersChanged?.Invoke(_listModifiers);
        }

        private void InvalidateCache(StatType modifierType)
        {
            _statTypeToModifiersCache.Remove(modifierType);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (_listModifiers.Contains(modifier) == false)
            {
                return;
            }

            modifier.Dispose();
            _listModifiers.Remove(modifier);
        }

        public void ClearModifiers()
        {
            if (ListModifiers.Count == 0)
            {
                return;
            }

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