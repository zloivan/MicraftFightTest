using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Characters;
using _Project.Scripts.CombatSystem.abstractions;
using UnityEngine;

namespace _Project.Scripts.CombatSystem
{
    public class DamageProcessor : IDamageProcessor
    {
        private readonly List<ICombatModifier> _supportedModifiers;

        public DamageProcessor(IEnumerable<ICombatModifier> modifiers = null)
        {
            _supportedModifiers = modifiers?.ToList() ?? new List<ICombatModifier>();
        }

        public void AddSupportedModifiers(IEnumerable<ICombatModifier> modifiers)
        {
            _supportedModifiers.AddRange(modifiers);
        }

        public int CalculateDamage(IEntity attacker, IEntity defender)
        {
            var context = new CombatContext
            {
                Attacker = attacker,
                Defender = defender,
                BaseDamage = CalculateBase(attacker, defender)
            };

            // Apply modifiers
            foreach (var modifier in _supportedModifiers)
            {
                modifier.Modify(context);
            }

            return context.FinalDamage;
        }

        private int CalculateBase(IEntity attacker, IEntity defender)
        {
            //100 AD vs 25 DEF = 75 DAMAGE (25% from 100 AD)
            var multiplier = 1 - (float)Mathf.Clamp(defender.StatsController.Defense, 0, 100) /
                        100;
            
            var damageReduction = Mathf.FloorToInt(attacker.StatsController.Damage *
                                                   multiplier);

            return damageReduction;
        }
    }
}