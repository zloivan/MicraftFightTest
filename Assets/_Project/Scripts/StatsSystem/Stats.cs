using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Project.Scripts.StatsSystem
{
    public class Stats
    {
        public IStatsMediator Mediator { get; }
        private readonly Dictionary<StatType, int> _baseStats;
        private readonly Dictionary<StatType, List<Action<int>>> _callbacks = new();

        public Stats(IStatsMediator mediator, BaseStats baseStats)
        {
            Mediator = mediator;
            _baseStats = Enum.GetValues(typeof(StatType))
                .Cast<StatType>()
                .ToDictionary(stat => stat, stat => GetBaseValueStat(stat, baseStats));

            Mediator.OnModifierChange += MediatorOnOnModifierChange;
        }

        public int GetStatByType(StatType statType)
        {
            var q = new Query(statType, _baseStats[statType]);
            Mediator.PerformQuery(this, q);

            return q.Value;
        }

        public void SubscribeToStatChange(StatType statType, Action<int> callback)
        {
            if (!_callbacks.ContainsKey(statType))
            {
                _callbacks[statType] = new List<Action<int>>();
            }

            _callbacks[statType].Add(callback);
        }

        public void UnsubscribeFromStatChange(StatType statType, Action<int> callback)
        {
            if (_callbacks.ContainsKey(statType))
            {
                _callbacks[statType].Remove(callback);
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var stat in _baseStats)
            {
                result.AppendLine($"{stat.Key}: {GetStatByType(stat.Key)}");
            }

            return result.ToString();
        }


        private int GetBaseValueStat(StatType statType, BaseStats baseStats) //TODO Move that to baseStat Class
        {
            var statValue = baseStats.Base.FirstOrDefault(stat => stat.statType == statType).value;

            return statValue != 0 ? statValue : 0;
        }

        private void MediatorOnOnModifierChange(StatType statType)
        {
            NotifyStatChanged(statType, GetStatByType(statType));
        }
        
        private void NotifyStatChanged(StatType statType, int newValue)
        {
            if (!_callbacks.TryGetValue(statType, out var callback1)) 
                return;
            
            foreach (var callback in callback1)
            {
                callback?.Invoke(newValue);
            }
        }
    }
}