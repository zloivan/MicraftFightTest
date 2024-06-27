using _Project.Scripts.StatsSystem.OperationStrategies.abstractions;
using UnityEngine;

namespace _Project.Scripts.StatsSystem.OperationStrategies
{
    public class PercentageOperation : IOperationStrategy
    {
        private readonly int _value;

        public PercentageOperation(int value)
        {
            _value = value;
        }

        public int Calculate(int value)
        {
            if (value < 0)
            {
                value = 0;
            }


            return Mathf.RoundToInt(_value * (float)value / 100);
        }
    }
}