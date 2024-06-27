using System;
using System.Collections.Generic;
using _Project.Scripts.PickupSystem;
using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Characters.Test
{
    public class BuffApplierTest : MonoBehaviour
    {
        [SerializeField]
        private List<StatBuff> _buffsToApplie;

        [SerializeField]
        private StatBuff _buffToRemove;

        [SerializeField]
        private Entity _entity;

        private BuffApplier _applier;

        private void Start()
        {
            _applier = new BuffApplier(ServiceLocator.For(this).Get<IStatModifierFactory>());
        }

        [ContextMenu("Apply Buffs")]
        public void ApplyBuffs()
        {
            _applier.ApplyBuffs(_entity, _buffsToApplie);
            
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