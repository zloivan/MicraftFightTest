using System;
using _Project.Scripts.Characters;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.PickupSystem
{
    public class StatModifierPickup : Pickup
    {
        [SerializeField]
        private StatType _statType;

        [SerializeField]
        private OperatorType _operatorType;

        [SerializeField]
        private int _value;

        [SerializeField]
        private float _duration;

        protected override void ApplyPickupEffect(Entity entity)
        {
            StatModifier modifier = _operatorType switch
            {
                OperatorType.Add => new BasicStatModifier(_statType, _duration, i => i + _value),
                OperatorType.Multiply => new BasicStatModifier(_statType, _duration, i => i * _value),
                _ => throw new ArgumentOutOfRangeException()
            };

            entity.Stats.Mediator.AddModifier(modifier);
        }
    }
}