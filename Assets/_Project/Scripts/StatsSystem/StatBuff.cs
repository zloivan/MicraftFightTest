using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace _Project.Scripts.StatsSystem
{
    public class StatBuff : IDisposable
    {
        public event Action<StatBuff> OnDisposed = delegate { };

        public string Name { get; private set; }
        public string IconAddress { get; private set; }
        public string Description { get; private set; }

        public List<StatModifier> Modifiers { get; }
        public bool MarkedForRemoval { get; private set; }

        //TODO Implement Timer
        //CountDownTime timer;

        private StatBuff(List<StatModifier> modifiers)
        {
            Modifiers = modifiers;
        }

        public void Update(float deltaTime)
        {
            //timer?.Tick(deltaTime);
        }

        public void Dispose()
        {
            MarkedForRemoval = false;
            OnDisposed?.Invoke(this);
        }


        public class Builder
        {
            private readonly StatBuff _buff;
            public float Duration { get; private set; }

            public Builder(List<StatModifier> modifiers)
            {
                _buff = new StatBuff(modifiers);
                //timer = new CountDownTime(duration);
                //timer.OnTimerStops += Dispose;
            }

            public Builder(StatModifier modifier)
            {
                _buff = new StatBuff(new List<StatModifier> { modifier });
                //timer = new CountDownTime(duration);
                //timer.OnTimerStops += Dispose;
            }

            public Builder WithName(string name)
            {
                _buff.Name = name;

                return this;
            }

            public Builder WithDescription(string description)
            {
                _buff.Description = description;
                return this;
            }

            public Builder WithIcon(string iconSpriteAddress)
            {
                _buff.IconAddress = iconSpriteAddress;
                return this;
            }


            public Builder WithTimer(float duration)
            {
                Duration = duration;
                return this;
            }

            public StatBuff BuildAndStartTimer()
            {
                if (Duration < 0)
                {
                    return _buff;
                }


                // buff.timer = new CountDownTime(Duration);
                // buff.timer.OnTimerStops += Dispose;
                // buff.timer.Start();

                return _buff;
            }
        }
    }
}