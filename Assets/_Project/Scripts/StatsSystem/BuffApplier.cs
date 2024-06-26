using System.Collections.Generic;
using _Project.Scripts.Characters;
using _Project.Scripts.PickupSystem;

namespace _Project.Scripts.StatsSystem
{
    public class BuffApplier
    {
        private readonly IStatModifierFactory _statModifierFactory;

        public BuffApplier(IStatModifierFactory statModifierFactory)
        {
            _statModifierFactory = statModifierFactory;
        }

        public void ApplyBuff(Entity entity, StatBuff buffsToApply)
        {
            foreach (var modificationPack in buffsToApply._statsToChange)
            {
                var modifier = _statModifierFactory.Create(
                    modificationPack.StatType,
                    modificationPack.OperatorType,
                    modificationPack.Value,
                    buffsToApply._duration);

                entity.Stats.Mediator.AddModifier(modifier);
            }
        }

        public void ApplyBuffEffect(Entity entity, IEnumerable<StatBuff> buffsToApply)
        {
            foreach (var buff in buffsToApply)
            {
                ApplyBuff(entity, buff);
            }
        }
       
    }
}