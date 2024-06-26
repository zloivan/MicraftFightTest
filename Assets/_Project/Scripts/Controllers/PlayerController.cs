using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using _Project.Scripts.StatsAndBuffsSystem;
using _Project.Scripts.StatsAndBuffsSystem.Extensions;
using _Project.Scripts.UI.FloatingNumbers;
using _Project.Scripts.UI.Views;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.Controllers
{
    public class PlayerController : PlayerModel
    {
        public UnityEvent OnStatsChanged = new();
        public UnityEvent OnBuffsChanged = new();

        [SerializeField]
        private HealthBar _healthBar;


        public DamageNumbers FloatingNumbers;


        private ReadOnlyCollection<Stat> _baseStats;

        [SerializeField]
        private List<Stat> _currentStats = new();

        private int _maxHealth;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private List<Buff> _appliedBuffs = new();

        private static readonly int HealthAnimationHash = Animator.StringToHash("Health");
        private static readonly int AttackAnimationHash = Animator.StringToHash("Attack");

        public int Health
        {
            get => GetStatValue(StatsId.LifeID);
            private set
            {
                var clampedValue = Mathf.Clamp(value, 0, _maxHealth);
                UpdateStat(StatsId.LifeID, clampedValue);
                _animator.SetInteger(HealthAnimationHash, clampedValue);
                _healthBar.SetValue(Health, _maxHealth);
                OnStatsChanged.Invoke();
            }
        }

        public int Armor
        {
            get => GetStatValue(StatsId.ArmorID);
            private set
            {
                UpdateStat(StatsId.ArmorID, Mathf.Clamp(value, 0, 100));
                OnStatsChanged.Invoke();
            }
        }

        public int Damage
        {
            get => GetStatValue(StatsId.DamageID);
            private set
            {
                UpdateStat(StatsId.DamageID, value);
                OnStatsChanged.Invoke();
            }
        }

        public int Vampirism
        {
            get => GetStatValue(StatsId.LifeStealID);
            private set
            {
                UpdateStat(StatsId.LifeStealID, Mathf.Clamp(value, 0, 100));
                OnStatsChanged.Invoke();
            }
        }

        public bool IsDead { get; private set; }

        private void OnValidate()
        {
            ApplyBuffsToStats();
        }

        private void ApplyBuffsToStats()
        {
            if (_appliedBuffs == null || _appliedBuffs.Count == 0)
            {
                return;
            }

            _currentStats = _baseStats.DeepCopy().ToList();

            foreach (var buff in _appliedBuffs)
            {
                foreach (var modifier in buff.stats)
                {
                    switch (modifier.statId)
                    {
                        case StatsId.LifeID:
                            _maxHealth += (int)modifier.value;
                            Health = _maxHealth;
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

            OnStatsChanged.Invoke();
        }

        public void Initialize(Stat[] stats)
        {
            _baseStats = new ReadOnlyCollection<Stat>(stats.DeepCopy().ToList());
            _currentStats = _baseStats.DeepCopy().ToList();
            foreach (var stat in _baseStats)
            {
                if (stat.id == StatsId.LifeID)
                {
                    _maxHealth = (int)stat.value;
                    Health = _maxHealth;
                }
            }

            IsDead = false;
        }

        public void ApplyBuffs(Buff[] buffs)
        {
            _appliedBuffs.Clear();
            _appliedBuffs.AddRange(buffs);
            ApplyBuffsToStats();
            OnBuffsChanged.Invoke();
        }

        public void ClearBuffs()
        {
            _appliedBuffs.Clear();
            ApplyBuffsToStats();
            OnBuffsChanged.Invoke();
        }

        public List<Buff> GetBuffs()
        {
            return _appliedBuffs;
        }

        public IEnumerable<Stat> GetCurrentStats()
        {
            return _currentStats;
        }

        private int GetStatValue(int statId)
        {
            var stat = _currentStats.Find(s => s.id == statId);
            return stat != null ? (int)stat.value : 0;
        }

        private void UpdateStat(int statId, float newValue)
        {
            var stat = _currentStats.Find(s => s.id == statId);
            if (stat != null)
            {
                stat.value = newValue;
            }
        }

        private float CalculateEffectiveDamage(int attackerDamage, int targetArmor)
        {
            var damageReduction = targetArmor / 100f;
            return attackerDamage * (1 - damageReduction);
        }

        private void ApplyDamage(PlayerController target, float damage)
        {
            if (target.Health < damage)
            {
                damage = target.Health;
            }

            if (damage > 0)
            {
                target.FloatingNumbers.ShowFloatingText($"{(int)damage}", Color.red);
                target.Health -= (int)damage;
            }

            if (target.Health == 0)
            {
                target.OnDeath();
            }
        }

        private void ApplyVampirism(PlayerController attacker, float damageDealt)
        {
            var healthRecovered = damageDealt * (attacker.Vampirism / 100f);

            if (!(_maxHealth - Health > healthRecovered))
            {
                healthRecovered = _maxHealth - Health;
            }

            if (healthRecovered > 0)
            {
                attacker.FloatingNumbers.ShowFloatingText($"+{(int)healthRecovered}", Color.green);
                attacker.Health += (int)healthRecovered;
            }
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

    public class PlayerModel : MonoBehaviour
    {
        public void SetupView()
        {
            
        }
    }
}