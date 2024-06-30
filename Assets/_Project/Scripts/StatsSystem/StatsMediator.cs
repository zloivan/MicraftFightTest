using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.StatsSystem.ModificationOrder;
using _Project.Scripts.StatsSystem.ModificationOrder.abstractions;

namespace _Project.Scripts.StatsSystem
{
    public class StatsMediator : IStatsMediator
    {
        private readonly List<StatModifier> _listModifiers = new();
        private readonly IStatModifierOder _statModifierOder = new NormalModificationOrder();
        private readonly Dictionary<StatType, IEnumerable<StatModifier>> _statTypeToModifiersCache = new();
        private readonly List<StatBuff> _activeBuffs = new();

        //No reference for collection is provided
        public List<StatBuff> ActiveBuffs => _activeBuffs.ToList();

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
            foreach (var modifier in newBuff.Modifiers)
            {
                _listModifiers.Add(modifier);
                InvalidateCache(modifier.StatType);
            }

            
            newBuff.OnDisposed += RemoveBuff;
        }
        
        public void RemoveBuff(StatBuff buff)
        {
            if (!_activeBuffs.Contains(buff))
                return;
            
            foreach (var modifier in buff.Modifiers)
            {
                _listModifiers.Remove(modifier);
                InvalidateCache(modifier.StatType);
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

        private void InvalidateCache(StatType modifierType)
        {
            _statTypeToModifiersCache.Remove(modifierType);
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
    }
}