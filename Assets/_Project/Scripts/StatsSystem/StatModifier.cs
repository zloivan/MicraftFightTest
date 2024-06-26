using System;

namespace _Project.Scripts.StatsSystem
{
    public abstract class StatModifier : IDisposable
    {
        public bool MarkedForRemoval { get; private set; }
        public event Action<StatModifier> OnDisposed = delegate { };


        //TODO Implement Timer
        //CountDownTime timer;

        protected StatModifier(float duration)
        {
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

        public abstract void Handle(object sender, Query query);

        public void Dispose()
        {
            MarkedForRemoval = true;
            OnDisposed?.Invoke(this);
        }
    }
}