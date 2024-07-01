using System;
using System.Collections.Generic;
using _Project.Scripts.StatsSystem;

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
}