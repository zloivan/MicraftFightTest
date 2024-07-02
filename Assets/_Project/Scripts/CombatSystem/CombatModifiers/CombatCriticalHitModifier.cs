using _Project.Scripts.CombatSystem.abstractions;
using _Project.Scripts.StatsSystem;

namespace _Project.Scripts.CombatSystem.CombatModifiers
{
    public class CombatCriticalHitModifier : ICombatModifier
    {
        public void Modify(CombatContext context)
        {
            var critChance = context.Attacker.StatsController.GetStatByType(StatType.CritChance);
            
            if (UnityEngine.Random.Range(1, 101) <= critChance)
            {
                context.IsCritical = true;
                context.FinalDamage = context.BaseDamage * context.Attacker.StatsController.GetStatByType(StatType.CritDamage) / 100;
            }
            else
            {
                context.FinalDamage = context.BaseDamage;
            }
        }
    }
}