using System.Collections.Generic;
using _Project.Scripts.Characters;

namespace _Project.Scripts.CombatSystem.abstractions
{
    public abstract class Ability : ICommand<IEntity>
    {
        protected readonly IEntity _user;

        protected Ability(IEntity user)
        {
            _user = user;
        }

        public abstract void Execute();
    }

    public abstract class TargetableAbility : Ability
    {
        public IEnumerable<IEntity> Targets { get; set; }

        protected TargetableAbility(IEntity user, IEnumerable<IEntity> targets) : base(user)
        {
            Targets = targets;
        }

        protected TargetableAbility(IEntity user) : base(user)
        {
        }
    }
}