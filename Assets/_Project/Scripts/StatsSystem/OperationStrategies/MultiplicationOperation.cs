using _Project.Scripts.StatsSystem.OperationStrategies.abstractions;

namespace _Project.Scripts.StatsSystem.OperationStrategies
{
    public class MultiplicationOperation : IOperationStrategy
    {
        private readonly int _value;

        public MultiplicationOperation(int value)
        {
            _value = value;
        }

        public int Calculate(int value)
        {
            if (value < 0)
            {
                value = 0;
            }
            
            return _value * value;
        }
    }
}