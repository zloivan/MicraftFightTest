using System.Collections.Generic;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Characters.Test
{
    public class Charector : Entity
    {
        [SerializeField]
        private List<NewBuff> _buffs;

        [ContextMenu("Apply Buffs")]
        public void ApplyBuffs()
        {
            foreach (var buff in _buffs)
            {
                buff.Visit(this);
            }

            Debug.Log($"{Stats}");
        }

        [ContextMenu("Reset to Base")]
        public void TestResetStats()
        {
            ResetStats();
            
            Debug.Log($"{Stats}");
        }
    }
}