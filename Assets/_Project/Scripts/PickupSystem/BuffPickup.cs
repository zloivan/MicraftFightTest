using _Project.Scripts.Characters;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.PickupSystem
{
    public class BuffPickup : Pickup
    {
        [SerializeField]
        private StatBuffSO _buffConfig;


        protected override void ApplyPickupEffect(Entity entity)
        {
            entity.StatsController.Mediator.AddBuff(_buffConfig.GetStatBuff());
        }
    }
}