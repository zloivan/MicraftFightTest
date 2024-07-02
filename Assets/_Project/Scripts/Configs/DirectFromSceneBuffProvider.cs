using System.Collections.Generic;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    public class DirectFromSceneBuffProvider : MonoBehaviour, IBuffProvider
    {
        [SerializeField]
        private List<StatBuffSO> _buffs;

        public IEnumerable<StatBuff> GetBuffs()
        {
            foreach (var buffSO in _buffs)
            {
                yield return buffSO.GetStatBuff();
            }
        }
    }
}