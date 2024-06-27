using System;
using _Project.Scripts.Configs;
using _Project.Scripts.PickupSystem;
using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Characters.Test
{
    public class BuffApplierTest : MonoBehaviour
    {
        [SerializeField]
        private Entity _entity;

        private BuffApplier _applier;
        private IBuffProvider _buffProvider;

        private void Start()
        {
            _applier = new BuffApplier(ServiceLocator.For(this).Get<IStatModifierFactory>());
            _buffProvider = ServiceLocator.For(this).Get<IBuffProvider>();
        }

        [ContextMenu("Apply Buffs")]
        public void ApplyBuffs()
        {
            var buffsToApplie = _buffProvider.GetBuffs();
            
            _applier.ApplyBuffs(_entity, buffsToApplie);
            
            Debug.Log($"{_entity.Stats}");
        }

        [ContextMenu("Reset to Base")]
        public void TestResetStats()
        {
            _entity.Stats.Mediator.ClearModifiers();
            Debug.Log($"{_entity.Stats}");
        }
    }
}