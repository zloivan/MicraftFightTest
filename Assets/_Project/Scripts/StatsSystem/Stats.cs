using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Project.Scripts.StatsSystem
{
    public class Stats
    {
        //private readonly BaseStats _baseStats;
        public IStatsMediator Mediator { get; }

        // public int Damage
        // {
        //     get
        //     {
        //         var q = new Query(StatType.Damage, _baseStats.Damage);
        //
        //         Mediator.PerformQuery(this, q);
        //
        //         return q.Value;
        //     }
        // }
        //
        // public int Defence
        // {
        //     get
        //     {
        //         var q = new Query(StatType.Defence, _baseStats.Defence);
        //
        //         Mediator.PerformQuery(this, q);
        //
        //         return q.Value;
        //     }
        // }
        //
        // public int Health
        // {
        //     get
        //     {
        //         var q = new Query(StatType.Health, _baseStats.Health);
        //
        //         Mediator.PerformQuery(this, q);
        //
        //         return q.Value;
        //     }
        // }
        //
        // public int Vampirism
        // {
        //     get
        //     {
        //         var q = new Query(StatType.Vampirism, _baseStats.Vampirism);
        //
        //         Mediator.PerformQuery(this, q);
        //
        //         return q.Value;
        //     }
        // }

        private readonly Dictionary<StatType, int> _stats;

        public Stats(IStatsMediator mediator, BaseStats baseStats)
        {
            // _baseStats = baseStats;
            Mediator = mediator;

            // Initialize the _stats dictionary with values from _baseStats.Base
            _stats = Enum.GetValues(typeof(StatType))
                .Cast<StatType>()
                .ToDictionary(stat => stat, stat => GetBaseValue(stat, baseStats));
        }

        private int GetBaseValue(StatType statType, BaseStats baseStats)
        {
            // Find the corresponding base value for the given stat type
            var statValue = baseStats.Base.FirstOrDefault(stat => stat.statType == statType).value;

            // If not found in the list, return a default value (could be zero or any other default)
            return statValue != 0 ? statValue : 0;
        }

        private bool useOld = false;

        public override string ToString()
        {
            if (useOld)
            {
                //return ToStringOld();
            }

            return NewToStriong();
        }

        public int GetStat(StatType statType)
        {
            var q = new Query(statType, _stats[statType]);
            Mediator.PerformQuery(this, q);
            return q.Value;
        }

        private string NewToStriong()
        {
            var result = new StringBuilder();
            foreach (var stat in _stats)
            {
                result.AppendLine($"{stat.Key}: {GetStat(stat.Key)}");
            }

            return result.ToString();
        }

        // private string ToStringOld()
        // {
        //     return
        //         $"{nameof(Damage)}: {Damage}, {nameof(Defence)}: {Defence}, {nameof(Health)}: {Health}, {nameof(Vampirism)}: {Vampirism}";
        // }
    }
}