using System.Collections.Generic;

namespace _Project.Scripts.StatsSystem.ModificationOrder.abstractions
{
    public interface IStatModifierOder
    {
        int Apply(IEnumerable<StatModifier> statModifiers, int baseValue);
    }
}