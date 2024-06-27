using System;
using UnityEngine;

namespace _Project.Scripts.StatsSystem
{
    public class StatModifier : IDisposable
    {
        public event Action<StatModifier> OnDisposed = delegate { };
        
        private readonly StatType _statType;
        private readonly IOperationStrategy _operationStrategy;
        
        public bool MarkedForRemoval { get; private set; }

        //TODO Implement Timer
        //CountDownTime timer;

        public StatModifier(StatType statType, float duration, IOperationStrategy operationStrategy)
        {
            _statType = statType;
            _operationStrategy = operationStrategy;

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

        public void Handle(object sender, Query query)
        {
            if (query.StatType == _statType)
            {
                query.Value = _operationStrategy.Calculate(query.Value);
            }
        }

        public void Dispose()
        {
            MarkedForRemoval = true;
            OnDisposed?.Invoke(this);
        }
    }
}