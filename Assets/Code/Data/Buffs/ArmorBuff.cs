using Code.Data.Buffs.Abstractions;

namespace Code.Data.Buffs
{
    public class ArmorBuff : BuffDecorator
    {
        private readonly int _additionalArmor;

        public ArmorBuff(ICharacterStats characterStats, int additionalArmor) : base(characterStats)
        {
            _additionalArmor = additionalArmor;
        }

        public override int Armor => CharacterStats.Armor + _additionalArmor;
    }
}