namespace _Project.Scripts.StatsSystem
{
    public class AddOperation : IOperationStrategy
    {
        private readonly int _value;
        
        
        public AddOperation(int value)
        {
            _value = value;
        }
        
        public int Calculate(int value) => _value + value;
    }
}