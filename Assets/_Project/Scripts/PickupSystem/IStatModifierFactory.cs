using System;
using _Project.Scripts.StatsSystem;

namespace _Project.Scripts.PickupSystem
{
    public interface IStatModifierFactory
    {
        StatModifier Create(StatType statType, OperatorType operatorType, int value, float duration);
    }

    public class BasicStatModifierFactory : IStatModifierFactory
    {
        public StatModifier Create(StatType statType, OperatorType operatorType, int value, float duration)
        {
            IOperationStrategy operationStrategy = operatorType switch
            {
                OperatorType.Add => new AddOperation(value),
                OperatorType.Multiply => new MultiplicationOperation(value),
                OperatorType.Divide => new DivisionOperation(value),
                OperatorType.Percentage => new PercentageOperation(value),
                _ => throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null)
            };

            return new StatModifier(statType, duration, operationStrategy);
        }
    }
}