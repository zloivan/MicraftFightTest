using System.Collections.Generic;
using _Project.Scripts.Characters;

namespace _Project.Scripts.StatsSystem
{
    public class BuffApplier
    {
        private readonly IStatModifierFactory _statModifierFactory;

        private readonly Dictionary<StatBuff, IList<StatBuffValue>> _activeStatBuffs = new();
        public BuffApplier(IStatModifierFactory statModifierFactory)
        {
            _statModifierFactory = statModifierFactory;
        }

        private void ApplyBuff(Entity entity, StatBuff buffsToApply)
        {
            foreach (var modificationPack in buffsToApply.StatsToChange)
            {
                StatModifier modifier = _statModifierFactory.Create(
                    modificationPack.StatType,
                    modificationPack.OperatorType,
                    modificationPack.Value,
                    buffsToApply.Duration);

                entity.Stats.Mediator.AddModifier(modifier);
            }
        }

        public void ApplyBuffs(Entity entity, IEnumerable<StatBuff> buffsToApply)
        {
            foreach (var buff in buffsToApply)
            {
                ApplyBuff(entity, buff);
            }
        }
    }
}