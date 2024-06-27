using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.StatsSystem
{
    [CreateAssetMenu(fileName = "New Buff_DeBuff", menuName = "Stat System/Create New Buff", order = 0)]
    public class StatBuffSO : ScriptableObject
    {
        public List<StatModificationValue> StatsToChange;

        public float Duration;

        public Sprite Icon;

        public StatBuff GetStatBuff()
        {
            return new StatBuff(StatsToChange, Duration, Icon);
        }
    }

    public class StatBuff
    {
        public readonly List<StatModificationValue> StatsToChange;

        public readonly float Duration;

        public readonly Sprite Icon;

        public StatBuff(List<StatModificationValue> statsToChange, float duration, Sprite icon)
        {
            StatsToChange = statsToChange;
            Duration = duration;
            Icon = icon;
        }
    }
}