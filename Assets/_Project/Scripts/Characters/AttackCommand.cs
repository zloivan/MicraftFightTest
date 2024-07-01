using System;
using System.Collections.Generic;

namespace _Project.Scripts.Characters
{
    public class AttackCommand : ICommand<IEntity>
    {
        private List<IEntity> _targets;
        private Action<IEntity> _action;

        public void Execute()
        {
            foreach (var target in _targets)
            {
                _action?.Invoke(target);
            }
        }

        private AttackCommand()
        {
        }

        public class Builder
        {
            private readonly AttackCommand _command = new();

            public Builder(List<IEntity> targets = default)
            {
                _command._targets = targets ?? new List<IEntity>();
            }

            public Builder WithAction(Action<IEntity> action)
            {
                _command._action = action;
                return this;
            }

            public AttackCommand Build() => _command;
        }
    }
}