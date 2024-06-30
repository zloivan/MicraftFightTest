using System;
using _Project.Scripts.StatsSystem.OperationStrategies.abstractions;

namespace _Project.Scripts.StatsSystem
{
    public class StatModifier
    {
        public StatType StatType { get; private set; }

        public IOperationStrategy OperationStrategy { get; private set; }

        public StatModifier(StatType statType, IOperationStrategy operationStrategy)
        {
            StatType = statType;
            OperationStrategy = operationStrategy;
        }
    }
}