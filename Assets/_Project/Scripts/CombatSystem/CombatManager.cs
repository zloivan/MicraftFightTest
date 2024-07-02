using System.Collections.Generic;
using _Project.Scripts.Characters;
using _Project.Scripts.CombatSystem.abstractions;
using _Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace _Project.Scripts.CombatSystem
{
    public class CombatManager
    {
        private CombatManager(ICombatController combatSystem, ICombatModifiersProvider combatModifiersProvider)
        {
            combatSystem.AddSupportedModifiers(combatModifiersProvider.GetAvailableModifiers());
        }

        public static void InitiateCombatAction(IEntity user, IEnumerable<IEntity> targets, AbilityType abilityType)
        {
            if (!user.AbilityController.TryGetAbility<TargetableAbility>(abilityType, out var ability))
                return;

            ability.Targets = targets;
            user.EnqueueCommand(ability);
        }
    }
}