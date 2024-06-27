using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.StatsSystem
{
    [CreateAssetMenu(fileName = "New Buff_DeBuff", menuName = "Stat System/Create New Buff", order = 0)]
    public class StatBuff : ScriptableObject
    {
        public List<StatModificationValue> _statsToChange;

        public float _duration;

        public Sprite Icon;
    }
}