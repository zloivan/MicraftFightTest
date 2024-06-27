using System;
using _Project.Scripts.StatsSystem.OperationStrategies;
using _Project.Scripts.StatsSystem.OperationStrategies.abstractions;

namespace _Project.Scripts.StatsSystem
{
    public class BasicStatModifierFactory : IStatModifierFactory
    {
        public StatModifier Create(StatType statType, OperatorType operatorType, int value, float duration)
        {
            IOperationStrategy operationStrategy = operatorType switch
            {
                OperatorType.Add => new AddOperation(value),
                OperatorType.Multiply => new MultiplicationOperation(value),
                OperatorType.Percentage => new PercentageOperation(value),
                _ => throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null)
            };

            return new StatModifier(statType, duration, operationStrategy);
        }
    }
}