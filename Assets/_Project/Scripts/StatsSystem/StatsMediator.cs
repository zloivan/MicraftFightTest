using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using _Project.Scripts.StatsSystem.ModificationOrder;
using _Project.Scripts.StatsSystem.ModificationOrder.abstractions;
using Unity.VisualScripting;

namespace _Project.Scripts.StatsSystem
{
    public class StatsMediator : IStatsMediator
    {
        public event Action<StatType> OnModifierChange;
        
        private readonly ObservableCollection<StatModifier> _listModifiers = new();
        private readonly IStatModifierOder _statModifierOder = new NormalModificationOrder();
        private readonly Dictionary<StatType, IEnumerable<StatModifier>> _statTypeToModifiersCache = new();
        private readonly List<StatBuff> _activeBuffs = new();

        //No reference for collection is provided intentionally
        public List<StatBuff> ActiveBuffs => _activeBuffs.ToList();

        public StatsMediator()
        {
            _listModifiers.CollectionChanged += (_, args) =>
            {
                if (args.NewItems != null)
                {
                    foreach (StatModifier modifier in args.NewItems)
                    {
                        OnModifierChange?.Invoke(modifier.StatType);
                    }
                }

                if (args.OldItems != null)
                {
                    foreach (StatModifier modifier in args.OldItems)
                    {
                        OnModifierChange?.Invoke(modifier.StatType);
                    }
                }
            };
        }

        public void PerformQuery(object sender, Query query)
        {
            if (!_statTypeToModifiersCache.ContainsKey(query.StatType))
            {
                _statTypeToModifiersCache[query.StatType] =
                    _listModifiers.Where(m => m.StatType == query.StatType).ToList();
            }

            query.Value = _statModifierOder.Apply(_statTypeToModifiersCache[query.StatType], query.Value);
        }

        public void AddBuff(StatBuff newBuff)
        {
            _activeBuffs.Add(newBuff);
            
            foreach (var statType in newBuff.Modifiers.Select(m=>m.StatType).Distinct())
            {
                InvalidateCache(statType);
            }
            
            _listModifiers.AddRange(newBuff.Modifiers);

            
            newBuff.OnDisposed += RemoveBuff;
        }
        
        public void RemoveBuff(StatBuff buff)
        {
            if (!_activeBuffs.Contains(buff))
                return;
            
            foreach (var modifier in buff.Modifiers)
            {
                InvalidateCache(modifier.StatType);
                _listModifiers.Remove(modifier);
            }

            _activeBuffs.Remove(buff);
        }

        public void ClearBuffs()
        {
            var activeBuffs = _activeBuffs.ToList();
            foreach (var buff in activeBuffs)
            {
                buff.Dispose();
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var buff in _activeBuffs)
            {
                buff.Update(deltaTime);
            }

            foreach (var buff in _activeBuffs.Where(b => b.MarkedForRemoval).ToList())
            {
                buff.Dispose();
            }
        }

        private void InvalidateCache(StatType modifierType)
        {
            _statTypeToModifiersCache.Remove(modifierType);
        }
    }
}