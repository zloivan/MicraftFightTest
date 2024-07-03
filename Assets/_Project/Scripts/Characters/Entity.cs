using System;
using System.Collections.Generic;
using _Project.Scripts.AbilitySystem;
using _Project.Scripts.AbilitySystem.abstractions;
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
        private bool _logMediator;

        public IAbilityModel AbilityModel { get;private set;  }
        public IStatController StatsController { get; private set; }
        public IHealthController HealthController { get; private set; }


        private readonly Queue<ICommand<IEntity>> _commands = new();


        public void EnqueueCommand(ICommand<IEntity> command)
        {
            _commands.Enqueue(command);
        }

        public void Accept(IVisitor visitor) //TODO Implement visit of components
        {
            visitor.Visit(this);
        }

        private void Awake()
        {
            StatsController = new StatsController(
                _logMediator ? new StatsMediatorWithLogger(new StatsMediator()) : new StatsMediator(),
                _baseStats);

            HealthController = new HealthController(StatsController.MaxHealth);

            AbilityModel = new AbilityModel();
            //TODO ADD THAT TO HEALH CONTROLLER AS STAT PROVIDER
            HealthController.OnDeath += HandleDeath;
            StatsController.SubscribeToStatChange(StatType.MaxHealth, OnMaxHealthChanged);
        }

        private void Update()
        {
            StatsController.Mediator.Update(Time.deltaTime);
            ExecuteNextCommand();
        }

        private void ExecuteNextCommand()
        {
            if (_commands.Count > 0)
            {
                _commands.Dequeue()?.Execute();
            }
        }

        private void HandleDeath() //TODO Probably temp
        {
            Logger.Log($"Entity {gameObject.name} died.", Color.red);
            Destroy(gameObject);
        }

        private void OnMaxHealthChanged(int newMaxHealth) //TODO Probably temp
        {
            HealthController.AdjustMaxHealth(newMaxHealth);
        }
    }
}