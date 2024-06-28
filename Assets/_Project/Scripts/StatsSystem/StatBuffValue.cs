using System;

namespace _Project.Scripts.StatsSystem
{
    [Serializable]
    public struct StatBuffValue
    {
        public StatType StatType;

        public OperatorType OperatorType;

        public int Value;
    }
}