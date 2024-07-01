using _Project.Scripts.StatsSystem;

namespace _Project.Scripts.Characters
{
    public interface IEntity
    {
        Stats Stats { get;}
        int CurrentHealth { get; set; }
    }
}