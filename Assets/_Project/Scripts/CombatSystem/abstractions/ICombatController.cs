using System.Collections.Generic;
using _Project.Scripts.Characters;
using _Project.Scripts.CombatSystem.abstractions;

namespace _Project.Scripts.CombatSystem
{
    public interface ICombatController
    {
        int CalculateDamage(IEntity user, IEntity target);
        void AddSupportedModifiers(IEnumerable<ICombatModifier> modifiers);
    }
}