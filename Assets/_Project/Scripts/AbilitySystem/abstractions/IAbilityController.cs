using System.Collections.Generic;
using _Project.Scripts.CombatSystem.abstractions;

namespace _Project.Scripts.Characters
{
    public interface IAbilityController
    {
        public void AddAbility(IEnumerable<Ability> abilities);
        bool TryGetAbility<TAbility>(AbilityType abilityType, out TAbility ability) where TAbility : Ability;
    }
}