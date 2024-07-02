using _Project.Scripts.Characters;

namespace _Project.Scripts.CombatSystem
{
    public class CombatContext
    {
        public IEntity Attacker { get; set; }
        public IEntity Defender { get; set; }
        public int BaseDamage { get; set; }
        public int FinalDamage { get; set; }
        public bool IsCritical { get; set; }
        // Add more properties as needed
    }
}