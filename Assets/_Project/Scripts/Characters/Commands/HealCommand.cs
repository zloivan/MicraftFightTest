using _Project.Scripts.Characters;

namespace _Project.Scripts.CombatSystem.abstractions
{
    public class HealCommand : ICommand<IEntity>
    {
        private readonly IHealthProvider _target;
        private readonly int _healAmount;

        public HealCommand(IHealthProvider target, int healAmount)
        {
            _target = target;
            _healAmount = healAmount;
        }

        public void Execute()
        {
            _target.HealthController.Heal(_healAmount);
        }
    }
}