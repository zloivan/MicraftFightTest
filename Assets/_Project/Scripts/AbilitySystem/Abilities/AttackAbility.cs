using System.Collections.Generic;
using _Project.Scripts.Characters;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.CombatSystem.abstractions
{
    public class AttackAbility : TargetableAbility
    {
        private readonly ICombatController _combatController;

        public AttackAbility(IEntity user, IEnumerable<IEntity> targets, ICombatController combatController)
            : base(user, targets)
        {
            _combatController = combatController;
        }

        public AttackAbility(IEntity user, ICombatController combatController) : base(user)
        {
            _combatController = combatController;
        }

        public override void Execute()
        {
            var vamp = _user.StatsController.GetStatByType(StatType.Vampirism);
            foreach (var target in Targets)
            {
                var damage = _combatController.CalculateDamage(_user, target);

                target.EnqueueCommand(new ApplyDamageCommand(target, damage));
                var healAmount = Mathf.FloorToInt(damage * ((float)vamp / 100));
                _user.EnqueueCommand(new HealCommand(_user, healAmount));
            }
        }
    }
}