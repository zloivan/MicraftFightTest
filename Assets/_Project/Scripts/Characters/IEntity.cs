namespace _Project.Scripts.Characters
{
    public interface IEntity
    {
        int CurrentHealth { get; }
        void TakeDamage(int damage);
    }
}