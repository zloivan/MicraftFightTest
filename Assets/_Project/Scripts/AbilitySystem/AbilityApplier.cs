using System.Collections.Generic;
using _Project.Scripts.AbilitySystem.abstractions;
using _Project.Scripts.Characters;

namespace _Project.Scripts.AbilitySystem
{
    public static class AbilityApplier
    {
        public static void Apply(IEntity user, IEnumerable<IEntity> targets, AbilityType abilityType)
        {
            if (!user.AbilityController.TryGetAbility<TargetableAbility>(abilityType, out var ability))
                return;

            ability.Targets = targets;
            user.EnqueueCommand(ability);
        }
    }
}