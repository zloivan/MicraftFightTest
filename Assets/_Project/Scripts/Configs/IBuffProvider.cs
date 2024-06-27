using System.Collections.Generic;
using _Project.Scripts.StatsSystem;

namespace _Project.Scripts.Configs
{
    public interface IBuffProvider
    {
        IEnumerable<StatBuff> GetBuffs();
    }
}