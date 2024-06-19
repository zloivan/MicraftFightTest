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
        private int _maxHealth;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private int _armor;

        [SerializeField]
        private int _vampirism;

        [SerializeField]
        private List<Buff> _appliedBuffs = new();

        [SerializeField]
        private int _health;

        private static readonly int HealthAnimationHash = Animator.StringToHash("Health");
        private static readonly int AttackAnimationHash = Animator.StringToHash("Attack");

        public int Health
        {
            get => _health;
            private set
            {
                _health = Mathf.Clamp(value, 0, _maxHealth);
                _animator.SetInteger(HealthAnimationHash, _health);
            }
        }

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

        public bool IsDead { get; private set; }

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
                            _maxHealth += (int)modifier.value; // Increase max health by the buff value
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
                        _maxHealth = _baseHealth;
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

        private float CalculateEffectiveDamage(int attackerDamage, int targetArmor)
        {
            var damageReduction = targetArmor / 100f;
            return attackerDamage * (1 - damageReduction);
        }

        private void ApplyDamage(PlayerController target, float damage)
        {
            target.Health -= (int)damage;
            
            if (target.Health == 0)
            {
                target.OnDeath();
            }
        }

        private void ApplyVampirism(PlayerController attacker, float damageDealt)
        {
            var healthRecovered = damageDealt * (attacker.Vampirism / 100f);
            attacker.Health += (int)healthRecovered;
            attacker.Health = Mathf.Clamp(attacker.Health, 0, attacker._maxHealth);
        }


        public void Attack(PlayerController target)
        {
            if (IsDead || target.IsDead) return;

            _animator.SetTrigger(AttackAnimationHash);
            var effectiveDamage = CalculateEffectiveDamage(Damage, target.Armor);
            ApplyDamage(target, effectiveDamage);
            ApplyVampirism(this, effectiveDamage);
        }

        private void OnDeath()
        {
            IsDead = true;
        }
    }
}