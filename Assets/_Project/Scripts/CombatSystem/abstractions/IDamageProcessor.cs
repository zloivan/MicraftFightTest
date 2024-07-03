using System.Collections.Generic;
using _Project.Scripts.Characters;

namespace _Project.Scripts.CombatSystem.abstractions
{
    public interface IDamageProcessor
    {
        int CalculateDamage(IEntity user, IEntity target);
        void AddSupportedModifiers(IEnumerable<ICombatModifier> modifiers);
    }
}