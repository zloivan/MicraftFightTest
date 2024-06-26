using System;
using System.Collections.Generic;

namespace _Project.Scripts.StatsSystem
{
    public class StatsMediator
    {
        private readonly LinkedList<StatModifier> _modifiers = new();

        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query query) => Queries?.Invoke(sender, query);

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.AddLast(modifier);
            Queries += modifier.Handle;

            modifier.OnDisposed += _ =>
            {
                _modifiers.Remove(modifier);
                Queries -= modifier.Handle;
            };
        }

        public void Update(float deltaTime)
        {
            var node = _modifiers.First;

            while (node != null)
            {
                var modifier = node.Value;
                modifier.Update(deltaTime);
                node = node.Next;
            }
            
            node = _modifiers.First;

            while (node != null)
            {
                var nextNode = node.Next;

                if (node.Value.MarkedForRemoval)
                {
                    node.Value.Dispose();
                }
                
                node = nextNode;
            }
        }
    }
}