using System;
using System.Collections.Generic;
using _Project.Scripts.Characters;
using _Project.Scripts.PickupSystem;
using UnityEngine;

namespace _Project.Scripts.StatsSystem
{
    [CreateAssetMenu(fileName = "New Buff_DeBuff", menuName = "Stat System/Create New Buff", order = 0)]
    public class NewBuff : ScriptableObject, IVisitor
    {
        [SerializeField]
        private List<StatModificationPack> _statsToChange;

        [SerializeField]
        private float _duration;

        public Sprite Icon;


        private void ApplyPickupEffect(Entity entity)
        {
            foreach (var statApplier in _statsToChange)
            {
                StatModifier modifier = statApplier.OperatorType switch
                {
                    OperatorType.Add => new BasicStatModifier(statApplier.StatType, _duration,
                        i => i + statApplier.Value),
                    OperatorType.Multiply => new BasicStatModifier(statApplier.StatType, _duration,
                        i => i * statApplier.Value),
                    _ => throw new ArgumentOutOfRangeException()
                };

                entity.Stats.Mediator.AddModifier(modifier);
            }
        }

        public void Visit<T>(T visitable) where T : Component, IVisitable
        {
            if (visitable is Entity entity)
            {
                ApplyPickupEffect(entity);
            }
        }
    }
}