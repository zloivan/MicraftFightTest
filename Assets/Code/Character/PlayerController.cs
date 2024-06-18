using Code.Data;
using Code.Data.Buffs;
using Code.Data.Buffs.Abstractions;
using UnityEngine;

namespace Code.Character
{
    public class PlayerController : MonoBehaviour
    {
        private ICharacterStats _baseStats;
        private ICharacterStats _currentStats;

        [SerializeField]
        private int _armor;

        [SerializeField]
        private int _vampirism;

        [field: SerializeField]
        public int Health { get; private set; }

        public int Armor
        {
            get => _armor;
            private set => _armor = Mathf.Clamp(value, 0, 100);
        }

        [field: SerializeField]
        public int Damage { get; private set; }

        public int Vampirism
        {
            get => _vampirism;
            private set => _vampirism = Mathf.Clamp(value, 0, 100);
        }

        private void UpdateStats()
        {
            Health = _currentStats.Health;
            Armor = _currentStats.Armor;
            Damage = _currentStats.Damage;
            Vampirism = _currentStats.Vampirism;
        }

        public void Initialize(Stat[] stats)
        {
            int health = 0, armor = 0, damage = 0, vampirism = 0;

            foreach (var stat in stats)
            {
                switch (stat.id)
                {
                    case StatsId.LifeID:
                        health = (int)stat.value;
                        break;
                    case StatsId.ArmorID:
                        armor = (int)stat.value;
                        break;
                    case StatsId.DamageID:
                        damage = (int)stat.value;
                        break;
                    case StatsId.LifeStealID:
                        vampirism = (int)stat.value;
                        break;
                }
            }

            _baseStats = new BaseCharacterStats(health, armor, damage, vampirism);
            _currentStats = _baseStats;

            UpdateStats();
        }

        public void ApplyBuffs(Buff[] buffs)
        {
            _currentStats = BuffFactory.ApplyBuffs(_baseStats, buffs);
            UpdateStats();
        }
    }
}
