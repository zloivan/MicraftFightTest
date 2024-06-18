using Code.Data.Buffs.Abstractions;

namespace Code.Data.Buffs
{
    public class VampirismBuff : BuffDecorator
    {
        private readonly int _additionalVampirism;

        public VampirismBuff(ICharacterStats characterStats, int additionalVampirism) : base(characterStats)
        {
            _additionalVampirism = additionalVampirism;
        }

        public override int Vampirism => CharacterStats.Vampirism + _additionalVampirism;
    }
}