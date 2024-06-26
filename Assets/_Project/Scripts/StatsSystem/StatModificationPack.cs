using System;

namespace _Project.Scripts.StatsSystem
{
    [Serializable]
    public struct StatModificationPack
    {
        public StatType StatType;

        public OperatorType OperatorType;

        public int Value;
    }
}