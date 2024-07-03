using System;
using _Project.Scripts.AbilitySystem.Abilities;
using _Project.Scripts.AbilitySystem.abstractions;
using _Project.Scripts.Characters;
using _Project.Scripts.CombatSystem.abstractions;

namespace _Project.Scripts.AbilitySystem
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IDamageProcessor _damageProcessor;

        public AbilityFactory(IDamageProcessor damageProcessor)
        {
            _damageProcessor = damageProcessor;
        }

        public Ability Create(IEntity user, AbilityType type)
        {
            Ability ability = type switch
            {
                AbilityType.BasicAttack => new AttackAbility(user, _damageProcessor, AbilityType.BasicAttack),
                AbilityType.Heal => new HealAbility(user, AbilityType.Heal),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return ability;
        }
    }

    public interface IAbilityFactory
    {
        Ability Create(IEntity user, AbilityType type);
    }
}