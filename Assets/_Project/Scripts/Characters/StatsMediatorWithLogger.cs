using System;
using System.Collections.Generic;
using _Project.Scripts.StatsSystem;
using UnityEngine;
using Logger = Utilities.Logger;

namespace _Project.Scripts.Characters
{
    internal class StatsMediatorWithLogger : IStatsMediator
    {
        private readonly IStatsMediator _mediator;


        public StatsMediatorWithLogger(IStatsMediator mediator)
        {
            _mediator = mediator;
            OnModifierChange += OutputModifierChanged;
        }

        private void OutputModifierChanged(StatType obj)
        {
            Logger.Log($"{obj}", Color.clear);
        }

        public void AddBuff(StatBuff newBuff)
        {
            _mediator.AddBuff(newBuff);

            Logger.Log($"New buff {newBuff.Name} is added", Color.green);
        }

        public void RemoveBuff(StatBuff buff)
        {
            _mediator.RemoveBuff(buff);

            Logger.Log($"Buff {buff.Name} is removed", Color.red);
        }

        public void ClearBuffs()
        {
            _mediator.ClearBuffs();

            Logger.Log($"Add buffs are cleared", Color.blue);
        }

        public void PerformQuery(object sender, Query query)
        {
            Logger.Log($"New query is performed for: [{query.StatType} {query.Value}]", Color.yellow);
            _mediator.PerformQuery(sender, query);
            Logger.Log($"Updated values: [{query.StatType} {query.Value}]", Color.yellow);
        }

        public void Update(float deltaTime)
        {
            _mediator.Update(deltaTime);
        }

        public List<StatBuff> ActiveBuffs => _mediator.ActiveBuffs;
        public event Action<StatType> OnModifierChange;
    }
}