namespace Code.Data.Buffs.Abstractions
{
    public interface ICharacterStats
    {
        int Health { get; }
        int Armor { get; }
        int Damage { get; }
        int Vampirism { get; }
    }
}