using Code.Data.Buffs.Abstractions;

namespace Code.Character
{
    public class BaseCharacterStats : ICharacterStats
    {
        public int Health { get; }
        public int Armor { get; }
        public int Damage { get; }
        public int Vampirism { get; }

        public BaseCharacterStats(int health, int armor, int damage, int vampirism)
        {
            Health = health;
            Armor = armor;
            Damage = damage;
            Vampirism = vampirism;
        }
    }
}