using _Project.Scripts.AbilitySystem.abstractions;
using _Project.Scripts.Characters;
using _Project.Scripts.Characters.Commands;

namespace _Project.Scripts.AbilitySystem.Abilities
{
    public class HealAbility : TargetableAbility
    {
        public int HealAmount { get; set; }

        public HealAbility(IEntity user, AbilityType abilityType) : base(user, abilityType)
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