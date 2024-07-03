using System.Collections.Generic;
using _Project.Scripts.Characters;

namespace _Project.Scripts.AbilitySystem.abstractions
{
    public abstract class Ability : ICommand<IEntity>
    {
        private readonly IEntity _user;
        private readonly AbilityType _abilityType;
        public AbilityType AbilityType => _abilityType;
        public IEntity User => _user;


        protected Ability(IEntity user, AbilityType abilityType)
        {
            _user = user;
            _abilityType = abilityType;
        }

        public abstract void Execute();
    }

    public abstract class TargetableAbility : Ability
    {
        public IEnumerable<IEntity> Targets { get; set; }

        protected TargetableAbility(IEntity user, IEnumerable<IEntity> targets, AbilityType abilityType) : base(user,
            abilityType)
        {
            Targets = targets;
        }

        protected TargetableAbility(IEntity user, AbilityType abilityType) : base(user, abilityType)
        {
        }
    }
}