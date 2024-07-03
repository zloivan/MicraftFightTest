using System.Collections.Generic;

namespace _Project.Scripts.AbilitySystem.abstractions
{
    public interface IAbilityModel
    {
        List<AbilityType> AvailableAbilities { get; }

        public void AddAbilities(IEnumerable<Ability> abilities);
        bool TryGetAbility<TAbility>(AbilityType abilityType, out TAbility searchedAbility) where TAbility : Ability;
    }
}