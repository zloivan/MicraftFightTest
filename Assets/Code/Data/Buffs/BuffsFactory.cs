using System;
using System.Linq;
using Code.Data.Buffs.Abstractions;

namespace Code.Data.Buffs
{
    public class BuffFactory
    {
        public static ICharacterStats ApplyBuffs(ICharacterStats baseStats, Buff[] buffs)
        {
            return buffs.Aggregate(baseStats, (current, buff) => buff.stats.Aggregate(current, ApplyBuff));
        }

        private static ICharacterStats ApplyBuff(ICharacterStats characterStats, BuffStat buffStat)
        {
            return buffStat.statId switch
            {
                StatsId.LifeID => new HealthBuff(characterStats, (int)buffStat.value),
                StatsId.ArmorID => new ArmorBuff(characterStats, (int)buffStat.value),
                StatsId.DamageID => new DamageBuff(characterStats, (int)buffStat.value),
                StatsId.LifeStealID => new VampirismBuff(characterStats, (int)buffStat.value),
                _ => throw new ArgumentException("Unknown stat ID")
            };
        }
    }
}