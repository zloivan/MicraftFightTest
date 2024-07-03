namespace _Project.Scripts.Characters.Commands
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