using Code.Data.Buffs.Abstractions;

namespace Code.Data.Buffs
{
    public class DamageBuff : BuffDecorator
    {
        private readonly int _additionalDamage;

        public DamageBuff(ICharacterStats characterStats, int additionalDamage) : base(characterStats)
        {
            _additionalDamage = additionalDamage;
        }

        public override int Damage => CharacterStats.Damage + _additionalDamage;
    }
}