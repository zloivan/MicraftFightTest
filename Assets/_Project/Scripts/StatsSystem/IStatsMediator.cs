using System;
using System.Collections.Generic;

namespace _Project.Scripts.StatsSystem
{
    public interface IStatsMediator
    {
        void AddBuff(StatBuff newBuff);
        void RemoveBuff(StatBuff buff);

        void ClearBuffs();
        void PerformQuery(object sender, Query query);
        void Update(float deltaTime);
        List<StatBuff> ActiveBuffs { get; }
        
    }
}