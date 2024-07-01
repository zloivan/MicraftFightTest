using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Project.Scripts.StatsSystem
{
    public class Stats
    {
        public IStatsMediator Mediator { get; }
        private readonly Dictionary<StatType, int> _stats;
        private readonly Dictionary<StatType, Action<int>> _callbacks = new();

        public Stats(IStatsMediator mediator, BaseStats baseStats)
        {
            Mediator = mediator;
            _stats = Enum.GetValues(typeof(StatType))
                .Cast<StatType>()
                .ToDictionary(stat => stat, stat => GetBaseValue(stat, baseStats));

            //Mediator.OnBuffNumberChanged += HandleBuffAdded;
        }

        private void HandleBuffAdded()
        {
            
        }


        public int GetStat(StatType statType)
        {
            var q = new Query(statType, _stats[statType]);
            Mediator.PerformQuery(this, q);
            return q.Value;
        }


        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var stat in _stats)
            {
                result.AppendLine($"{stat.Key}: {GetStat(stat.Key)}");
            }

            return result.ToString();
        }


        private int GetBaseValue(StatType statType, BaseStats baseStats)
        {
            var statValue = baseStats.Base.FirstOrDefault(stat => stat.statType == statType).value;

            return statValue != 0 ? statValue : 0;
        }


        public void SubscribeListenerToStatChange(StatType statType, Action<int> onStatChange)
        {
            if (!_callbacks.ContainsKey(statType))
            {
                _callbacks[statType] = null;
            }
            _callbacks[statType] += onStatChange;
        }

        
        private void NotifyStatChanged(StatType statType, int newValue)
        {
            if (_callbacks.TryGetValue(statType, out var callback))
            {
                callback?.Invoke(newValue);
            }
        }
        public void UnsubscribeListenerFromStatChange(StatType statType, Action<int> onStatChange)
        {
            if (_callbacks.ContainsKey(statType))
            {
                _callbacks[statType] -= onStatChange;
            }
        }
    }
}