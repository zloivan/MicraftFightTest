using System.Collections.Generic;
using _Project.Scripts.StatsAndBuffsSystem;
using _Project.Scripts.UI.Presenters;

namespace _Project.Scripts.UI
{
    public interface IGameView
    {
        void SetPresenter(GamePresenter presenter);
        
        void InitializePlayerStats(int playerId, List<Stat> stats, List<Buff> buffs);
        void UpdatePlayerStats(int playerId, List<Stat> stats);
        void UpdatePlayerBuffs(int playerId, List<Buff> buffs);
    }
}