using System.Collections.Generic;

namespace _Project.Scripts.AbilitySystem.abstractions
{
    public interface IAbilityController
    {
        public void AddAbilities(IEnumerable<Ability> abilities);
        bool TryGetAbility<TAbility>(AbilityType abilityType, out TAbility searchedAbility) where TAbility : Ability;
    }
}