using System;

public class HealthController
{
    public event Action OnDeath;
    public event Action<int> OnMaxHealthChanged;
    private int _currentHealth;
    private int _maxHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }

    public int MaxHealth
    {
        get => _maxHealth;
        private set
        {
            _maxHealth = value;
            OnMaxHealthChanged?.Invoke(_maxHealth);
        }
    }

    public HealthController(int initialHealth)
    {
        _maxHealth = initialHealth;
        _currentHealth = initialHealth;
    }

    public void AdjustMaxHealth(int newMaxHealth)
    {
        var healthDifference = newMaxHealth - _maxHealth;

        if (healthDifference > 0)
        {
            // Increase current health if max health increased
            CurrentHealth += healthDifference;
        }
        else
        {
            // Adjust current health if it exceeds the new max health
            if (CurrentHealth > newMaxHealth)
            {
                CurrentHealth = newMaxHealth;
            }
        }

        MaxHealth = newMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void Heal(int amount)
    {
        CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth);
    }
}