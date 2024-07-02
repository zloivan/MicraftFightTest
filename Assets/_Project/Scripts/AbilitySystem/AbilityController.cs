using System.Collections.Generic;
using _Project.Scripts.CombatSystem.abstractions;

namespace _Project.Scripts.Characters
{
    public class AbilityController : IAbilityController
    {
        public void AddAbility(IEnumerable<Ability> abilities)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetAbility<TAbility>(AbilityType abilityType, out TAbility ability) where TAbility : Ability
        {
            throw new System.NotImplementedException();
        }
    }
}