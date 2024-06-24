using System.Collections.Generic;
using Data;
using UI.Scripts.Presenters;

namespace UI.Scripts
{
    public interface IGameView
    {
        void SetPresenter(GamePresenter presenter);
        
        void InitializePlayerStats(int playerId, List<Stat> stats, List<Buff> buffs);
        void UpdatePlayerStats(int playerId, List<Stat> stats);
        void UpdatePlayerBuffs(int playerId, List<Buff> buffs);
    }
}