using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code.Character
{
    public class PlayerController : MonoBehaviour
    {
        private int _baseHealth;
        private int _baseArmor;
        private int _baseDamage;
        private int _baseVampirism;

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
        
        [SerializeField]
        private List<Buff> _appliedBuffs = new();

        private void OnValidate()
        {
            UpdateStats();
        }

        private void UpdateStats()
        {
            if (_appliedBuffs == null) 
                return;

            Health = _baseHealth;
            Armor = _baseArmor;
            Damage = _baseDamage;
            Vampirism = _baseVampirism;

            foreach (var buff in _appliedBuffs)
            {
                foreach (var modifier in buff.stats)
                {
                    switch (modifier.statId)
                    {
                        case StatsId.LifeID:
                            Health += (int)modifier.value;
                            break;
                        case StatsId.ArmorID:
                            Armor += (int)modifier.value;
                            break;
                        case StatsId.DamageID:
                            Damage += (int)modifier.value;
                            break;
                        case StatsId.LifeStealID:
                            Vampirism += (int)modifier.value;
                            break;
                    }
                }
            }
        }

        public void Initialize(Stat[] stats)
        {
            foreach (var stat in stats)
            {
                switch (stat.id)
                {
                    case StatsId.LifeID:
                        _baseHealth = (int)stat.value;
                        break;
                    case StatsId.ArmorID:
                        _baseArmor = (int)stat.value;
                        break;
                    case StatsId.DamageID:
                        _baseDamage = (int)stat.value;
                        break;
                    case StatsId.LifeStealID:
                        _baseVampirism = (int)stat.value;
                        break;
                }
            }

            UpdateStats();
        }

        public void ApplyBuffs(Buff[] buffs)
        {
            _appliedBuffs.Clear();
            _appliedBuffs.AddRange(buffs);
            UpdateStats();
        }

        public void ClearBuffs()
        {
            _appliedBuffs.Clear();
            UpdateStats();
        }
    }
}
