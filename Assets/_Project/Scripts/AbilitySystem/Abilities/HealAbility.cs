using System.Collections.Generic;
using _Project.Scripts.Characters;

namespace _Project.Scripts.CombatSystem.abstractions
{
    public class HealAbility : TargetableAbility
    {
        public int HealAmount { get; set; }

        public HealAbility(IEntity user, IEnumerable<IEntity> targets, int healAmount) : base(user, targets)
        {
            HealAmount = healAmount;
        }

        public HealAbility(IEntity user) : base(user)
        {
        }

        public override void Execute()
        {
            foreach (var target in Targets)
            {
                target.EnqueueCommand(new HealCommand(target, HealAmount));
            }
        }
    }
}