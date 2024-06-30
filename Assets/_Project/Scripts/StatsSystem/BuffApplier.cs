using System.Collections.Generic;
using _Project.Scripts.Characters;

namespace _Project.Scripts.StatsSystem
{
    public class BuffApplier
    {
        public static void ApplyBuffs(Entity entity, IEnumerable<StatBuff> buffsToApply)
        {
            foreach (var buff in buffsToApply)
            {
                ApplyBuff(entity, buff);
            }
        }

        private static void ApplyBuff(Entity entity, StatBuff buffsToApply)
        {
            entity.Stats.Mediator.AddBuff(buffsToApply);
        }
    }
}