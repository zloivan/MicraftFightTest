using _Project.Scripts.Characters;

namespace _Project.Scripts.CombatSystem.abstractions
{
    public class ApplyDamageCommand :  ICommand<IEntity>
    {
        private readonly IHealthProvider _target;
        private readonly int _damage;

        public ApplyDamageCommand(IHealthProvider target, int damage)
        {
            _target = target;
            _damage = damage;
        }

        public void Execute()
        {
            _target.HealthController.TakeDamage(_damage);
        }
    }
}