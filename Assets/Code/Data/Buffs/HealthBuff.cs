using Code.Data.Buffs.Abstractions;

namespace Code.Data.Buffs
{
    public class HealthBuff : BuffDecorator
    {
        private readonly int _additionalHealth;

        public HealthBuff(ICharacterStats characterStats, int additionalHealth) : base(characterStats)
        {
            _additionalHealth = additionalHealth;
        }

        public override int Health => CharacterStats.Health + _additionalHealth;
    }
}