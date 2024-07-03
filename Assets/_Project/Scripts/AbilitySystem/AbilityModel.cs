using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AbilitySystem.abstractions;

namespace _Project.Scripts.AbilitySystem
{
    public class AbilityModel : IAbilityModel
    {
        private readonly Dictionary<AbilityType, Ability> _abilities = new();

        public List<AbilityType> AvailableAbilities => _abilities.Select(ability => ability.Key).ToList();

        public void AddAbilities(IEnumerable<Ability> abilities)
        {
            foreach (var ability in abilities)
            {
                _abilities[ability.AbilityType] = ability;
            }
        }

        public bool TryGetAbility<TAbility>(AbilityType abilityType, out TAbility searchedAbility)
            where TAbility : Ability
        {
            if (_abilities.TryGetValue(abilityType, out var abilityByType))
            {
                if (abilityByType is TAbility ability)
                {
                    searchedAbility = ability;
                    return true;
                }
            }

            searchedAbility = null;
            return false;
        }
    }
}