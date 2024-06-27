using UnityEngine;

namespace _Project.Scripts.StatsSystem
{
    public interface IOperationStrategy
    {
        int Calculate(int value);
    }

    public class MultiplicationOperation : IOperationStrategy
    {
        private readonly int _value;

        public MultiplicationOperation(int value)
        {
            _value = value;
        }

        public int Calculate(int value) => value * _value;
    }

    public class DivisionOperation : IOperationStrategy
    {
        private readonly int _value;

        public DivisionOperation(int value)
        {
            _value = value;
        }

        //Might lose the precision, is it bad?
        public int Calculate(int value) => _value / value;
    }

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