using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Characters;
using _Project.Scripts.CombatSystem.abstractions;
using _Project.Scripts.StatsSystem;

namespace _Project.Scripts.CombatSystem
{
    public class CombatController : ICombatController
    {
        private readonly List<ICombatModifier> _modifiers = new();

        public void AddSupportedModifiers(IEnumerable<ICombatModifier> modifiers)
        {
            _modifiers.AddRange(modifiers);
        }

        public int CalculateDamage(IEntity attacker, IEntity defender)
        {
            var context = new CombatContext
            {
                Attacker = attacker,
                Defender = defender,
                BaseDamage = attacker.StatsController.GetStatByType(StatType.Damage) -
                             defender.StatsController.GetStatByType(StatType.Defence)
            };

            // Apply modifiers
            foreach (var modifier in _modifiers)
            {
                modifier.Modify(context);
            }

            return context.FinalDamage;
        }

        public CombatController(IEnumerable<ICombatModifier> modifiers = null)
        {
            _modifiers = modifiers?.ToList() ?? new List<ICombatModifier>();
        }

        public CombatController()
        {
        }
    }
}