using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.StatsSystem;
using _Project.Scripts.StatsSystem.OperationStrategies;
using _Project.Scripts.StatsSystem.OperationStrategies.abstractions;

namespace _Project.Scripts.PickupSystem
{
    internal interface IStatBuffFactory
    {
        StatBuff Create(StatBuffContainer modifiersDescription);
        StatBuff Create(StatBuffContainer modifiersDescription, float duration);

        StatBuff Create(StatBuffContainer modifiersDescription, float duration, string name);
        StatBuff Create(StatBuffContainer modifiersDescription, float duration, string name, string description);

        StatBuff Create(StatBuffContainer modifiersDescription,
            float duration,
            string name,
            string description,
            string iconAddress);
    }

    [Serializable]
    public struct StatBuffContainer
    {
        public List<BuffModifiersDescription> ModifiersDescriptions;
    }

    [Serializable]
    public struct BuffModifiersDescription
    {
       public StatType StatBuff;
       public OperatorType OperatorBuff;
       public int Value;
    }

    public class StatBuffFactory : IStatBuffFactory
    {
        public StatBuff Create(StatBuffContainer buffs)
        {
            var modifierList = buffs.ModifiersDescriptions.Select(buff => StatModifier(buff.StatBuff, buff.OperatorBuff, buff.Value))
                .ToList();

            return new StatBuff.Builder(modifierList)
                .BuildAndStartTimer();
        }

        public StatBuff Create(StatBuffContainer buffs, float duration)
        {
            var modifierList = buffs.ModifiersDescriptions.Select(buff => StatModifier(buff.StatBuff, buff.OperatorBuff, buff.Value))
                .ToList();

            return new StatBuff.Builder(modifierList)
                .WithTimer(duration)
                .BuildAndStartTimer();
        }

        public StatBuff Create(StatBuffContainer buffs, float duration, string name)
        {
            var modifierList = buffs.ModifiersDescriptions.Select(buff => StatModifier(buff.StatBuff, buff.OperatorBuff, buff.Value))
                .ToList();

            return new StatBuff.Builder(modifierList)
                .WithName(name)
                .BuildAndStartTimer();
        }

        public StatBuff Create(StatBuffContainer buffs, float duration, string name, string description)
        {
            var modifierList = buffs.ModifiersDescriptions.Select(buff => StatModifier(buff.StatBuff, buff.OperatorBuff, buff.Value))
                .ToList();

            return new StatBuff.Builder(modifierList)
                .WithName(name)
                .WithDescription(description)
                .BuildAndStartTimer();
        }

        public StatBuff Create(StatBuffContainer buffs,
            float duration,
            string name,
            string description,
            string iconAddress)
        {
            var modifierList = buffs.ModifiersDescriptions.Select(buff => StatModifier(buff.StatBuff, buff.OperatorBuff, buff.Value))
                .ToList();

            return new StatBuff.Builder(modifierList)
                .WithName(name)
                .WithDescription(description)
                .WithIcon(iconAddress)
                .BuildAndStartTimer();
        }


        private static StatModifier StatModifier(StatType statType, OperatorType operatorType, int value)
        {
            var operation = OperationStrategy(operatorType, value);

            var modifier = new StatModifier(statType, operation);
            return modifier;
        }

        private static IOperationStrategy OperationStrategy(OperatorType operatorType, int value)
        {
            IOperationStrategy operation = operatorType switch
            {
                OperatorType.Add => new AddOperation(value),
                OperatorType.Multiply => new MultiplicationOperation(value),
                OperatorType.Percentage => new PercentageOperation(value),
                _ => throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null)
            };
            return operation;
        }
    }
}