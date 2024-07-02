using System;
using System.Collections.Generic;
using _Project.Scripts.CombatSystem.abstractions;
using _Project.Scripts.StatsSystem;


namespace _Project.Scripts.Characters
{
    public interface IEntity : IStatProvider, IHealthProvider, IAbilityControllerProvider
    {
        void EnqueueCommand(ICommand<IEntity> command);
    }

    public interface IAbilityControllerProvider
    {
        IAbilityController AbilityController { get; }
    }

    public interface IStatProvider
    {
        IStatController StatsController { get; }
    }

    public interface IHealthProvider
    {
        IHealthController HealthController { get; }
    }

    public interface IStatController
    {
        void SubscribeToStatChange(StatType health, Action<int> onMaxHealthChanged);
        int GetStatByType(StatType health);
        IStatsMediator Mediator { get; }
    }
    
    public interface IHealthController
    {
        void TakeDamage(int damage);
        int CurrentHealth { get; }
        int MaxHealth { get; }
        void AdjustMaxHealth(int newMaxHealth);
        void Heal(int amount);
        event Action OnDeath;
    }

    public interface ICombatModifiersProvider
    {
        IEnumerable<ICombatModifier> GetAvailableModifiers();
    }
}