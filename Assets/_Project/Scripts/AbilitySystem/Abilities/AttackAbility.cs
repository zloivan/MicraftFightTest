using _Project.Scripts.AbilitySystem.abstractions;
using _Project.Scripts.Characters;
using _Project.Scripts.Characters.Commands;
using _Project.Scripts.CombatSystem.abstractions;
using UnityEngine;

namespace _Project.Scripts.AbilitySystem.Abilities
{
    public class AttackAbility : TargetableAbility
    {
        private readonly IDamageProcessor _damageProcessor;

        public AttackAbility(IEntity user, IDamageProcessor damageProcessor, AbilityType abilityType) : base(
            user,
            abilityType)
        {
            _damageProcessor = damageProcessor;
        }

        public override void Execute()
        {
            var vamp = User.StatsController.Vampirism;
            foreach (var target in Targets)
            {
                var damage = _damageProcessor.CalculateDamage(User, target);

                target.EnqueueCommand(new ApplyDamageCommand(target, damage));
                var healAmount = Mathf.FloorToInt(damage * ((float)vamp / 100));
                User.EnqueueCommand(new HealCommand(User, healAmount));
            }
        }
    }
}