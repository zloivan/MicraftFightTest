using _Project.Scripts.Characters;
using _Project.Scripts.ServiceLocatorSystem;
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
            var modifier = ServiceLocator.For(this).Get<IStatModifierFactory>()
                .Create(_statType, _operatorType, _value, _duration);

            entity.Stats.Mediator.AddModifier(modifier);
        }
    }
}