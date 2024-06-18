namespace Code.Data.Buffs.Abstractions
{
    public abstract class BuffDecorator : ICharacterStats
    {
        protected readonly ICharacterStats CharacterStats;

        protected BuffDecorator(ICharacterStats characterStats)
        {
            CharacterStats = characterStats;
        }

        public virtual int Health => CharacterStats.Health;
        public virtual int Armor => CharacterStats.Armor;
        public virtual int Damage => CharacterStats.Damage;
        public virtual int Vampirism => CharacterStats.Vampirism;
    }
}