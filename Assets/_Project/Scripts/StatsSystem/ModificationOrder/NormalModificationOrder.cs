using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.StatsSystem.ModificationOrder.abstractions;
using _Project.Scripts.StatsSystem.OperationStrategies;

namespace _Project.Scripts.StatsSystem.ModificationOrder
{
    public class NormalModificationOrder : IStatModifierOder
    {
        public int Apply(IEnumerable<StatModifier> statModifiers, int baseValue)
        {
            var allModifiers = statModifiers.ToList();
            var addModifiers = allModifiers.Where(m => m.OperationStrategy is AddOperation);
            var multiplyModifiers = allModifiers.Where(m => m.OperationStrategy is MultiplicationOperation);
            var percentageModifiers = allModifiers.Where(m => m.OperationStrategy is PercentageOperation);


            //We want to increase by multiplication only base stats, not the entire stat value.
            baseValue = multiplyModifiers.Aggregate(baseValue,
                (current, multiplyModifier) => multiplyModifier.OperationStrategy.Calculate(current));

            // after multiplication, we want to add plain values
            baseValue = addModifiers.Aggregate(baseValue,
                (current, addModifier) => addModifier.OperationStrategy.Calculate(current));

            //after we want to increase everything by percentage
            baseValue = percentageModifiers.Aggregate(baseValue,
                (current, percentageModifier) => percentageModifier.OperationStrategy.Calculate(current));

            return baseValue;
        }
    }
}