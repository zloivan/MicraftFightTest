using System;
using _Project.Scripts.Characters;
using _Project.Scripts.CombatSystem;
using _Project.Scripts.CombatSystem.abstractions;

namespace _Project.Scripts.AbilitySystem
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly ICombatController _combatController;

        public AbilityFactory(ICombatController combatController)
        {
            _combatController = combatController;
        }
        
        public Ability Create(IEntity user ,AbilityType type)
        {
            Ability ability = type switch
            {
                AbilityType.BasicAttack => new AttackAbility(user, _combatController),
                AbilityType.Heal => new HealAbility(user),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return ability;
        }
    }

    public interface IAbilityFactory
    {
        Ability Create(IEntity user ,AbilityType type);
    }
}