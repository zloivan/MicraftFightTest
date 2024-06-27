using System;
using _Project.Scripts.StatsSystem.OperationStrategies.abstractions;

namespace _Project.Scripts.StatsSystem
{
    public class StatModifier : IDisposable
    {
        public event Action<StatModifier> OnDisposed = delegate { };

        public bool MarkedForRemoval { get; private set; }

        public StatType StatType { get; private set; }

        public IOperationStrategy OperationStrategy { get; private set; }

        //TODO Implement Timer
        //CountDownTime timer;

        public StatModifier(StatType statType, float duration, IOperationStrategy operationStrategy)
        {
            StatType = statType;
            OperationStrategy = operationStrategy;

            if (duration < 0)
            {
                return;
            }

            //timer = new CountDownTime(duiration);
            //timer.OnTimerStops+=Dispose;
            //timer.Start();
        }

        public void Update(float deltaTime)
        {
            //timer?.Tick(deltaTime);
        }

        public void Dispose()
        {
            MarkedForRemoval = true;
            OnDisposed?.Invoke(this);
        }
    }
}