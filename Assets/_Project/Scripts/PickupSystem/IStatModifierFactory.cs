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
            StatModifier modifier = operatorType switch
            {
                OperatorType.Add => new BasicStatModifier(statType, duration, i => i + value),
                OperatorType.Multiply => new BasicStatModifier(statType, duration, i => i * value),
                _ => throw new ArgumentOutOfRangeException()
            };

            return modifier;
        }
    }
}