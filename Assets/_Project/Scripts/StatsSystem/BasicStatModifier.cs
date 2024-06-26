using System;

namespace _Project.Scripts.StatsSystem
{
    public class BasicStatModifier : StatModifier
    {
        private readonly StatType _statType;
        private readonly Func<int, int> _operation;

        public BasicStatModifier(StatType statType, float duration, Func<int, int> operation) : base(duration)
        {
            _statType = statType;
            _operation = operation;
        }

        public override void Handle(object sender, Query query)
        {
            if (query.StatType == _statType)
            {
                query.Value = _operation(query.Value);
            }
        }
    }
}