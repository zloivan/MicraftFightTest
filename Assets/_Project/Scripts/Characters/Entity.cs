using System;
using System.Collections.Generic;
using _Project.Scripts.PickupSystem;
using _Project.Scripts.StatsSystem;
using UnityEngine;
using Logger = Utilities.Logger;

namespace _Project.Scripts.Characters
{
    public class Entity : MonoBehaviour, IVisitable, IEntity
    {
        [SerializeField]
        private BaseStats _baseStats;

        [SerializeField]
        private bool LogMediator;

        private readonly Queue<ICommand<IEntity>> _commands = new();

        public Stats Stats { get; set; }

        private HealthController _healthController;

        public int CurrentHealth => _healthController.CurrentHealth;

        private void Awake()
        {
            Logger.Log($"Awake called for {name}, {gameObject.name}", Color.blue);
            Stats = new Stats(LogMediator ? new StatsMediatorWithLogger(new StatsMediator()) : new StatsMediator(),
                _baseStats);

            _healthController = new HealthController(Stats.GetStatByType(StatType.Health));

            _healthController.OnDeath += HandleDeath;
            _healthController.OnMaxHealthChanged += HandleMaxHealthChanged;

            Stats.SubscribeToStatChange(StatType.Health, OnMaxHealthChanged);
        }

        private void HandleMaxHealthChanged(int newMaxHealth)
        {
            Logger.Log($"MaxHealth changed to {newMaxHealth}", Color.clear);
        }

        private void HandleDeath()
        {
            Logger.Log($"Entity {gameObject.name} died.", Color.red);
            Destroy(gameObject);
        }

        private void Update()
        {
            Stats.Mediator.Update(Time.deltaTime);
            ExecuteCommand();
        }

        private void OnMaxHealthChanged(int newMaxHealth)
        {
            _healthController.AdjustMaxHealth(newMaxHealth);
        }

        private void ExecuteCommand()
        {
            if (_commands.Count > 0)
            {
                _commands.Dequeue()?.Execute();
            }
        }

        public void EnqueueCammand(ICommand<IEntity> command)
        {
            _commands.Enqueue(command);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public void TakeDamage(int damage)//TODO TEMP
        {
            _healthController.TakeDamage(damage);
        }
    }
}