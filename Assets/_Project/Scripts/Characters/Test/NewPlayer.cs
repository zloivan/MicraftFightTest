using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Characters.Test
{
    public class Charector : Entity
    {
        [SerializeField]
        private List<NewBuff> _buffs;

        [SerializeField]
        private NewBuff _buffToRemove;


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
            ClearModifiers();

            Debug.Log($"{Stats}");
        }


        [ContextMenu("Remove Buff")]
        public void RemoveBuff()
        {
            var toRemove = _buffs.Where(b => b == _buffToRemove).ToList();
            
            foreach (var buffToRemove in toRemove)
            {
                RemoveBuff(buffToRemove);
                _buffs.Remove(buffToRemove);
            }

            _buffToRemove = null;
            
            Debug.Log($"{Stats}");
        }

        private void RemoveBuff(NewBuff buffToRemove)
        {
            buffToRemove.RemoveBuffEffect();
        }
    }
}