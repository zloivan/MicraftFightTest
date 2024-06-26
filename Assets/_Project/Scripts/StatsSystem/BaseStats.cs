using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.StatsSystem
{
    [CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats", order = 0)]
    public class BaseStats : ScriptableObject
    {
        // public int Damage = 10;
        // public int Defence = 20;
        // public int Health = 100;
        // public int Vampirism = 0;

        public List<StatTypeToValue> Base;
    }

    [Serializable]
    public struct StatTypeToValue
    {
        public StatType statType;
        public int value;
    }
}