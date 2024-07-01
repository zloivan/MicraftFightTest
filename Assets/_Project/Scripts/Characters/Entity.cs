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
        private int _currentHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;

                if (_currentHealth > 0) 
                    return;
                Logger.Log($"Entity {gameObject.name} died.", Color.red);
                Destroy(gameObject);
            }
        }

        public Stats Stats { get; set; }

        private void Awake()
        {
            Stats = new Stats(LogMediator ? new StatsMediatorWithLogger(new StatsMediator()) : new StatsMediator(),
                _baseStats);

            CurrentHealth = Stats.GetStat(StatType.Health);

            Stats.SubscribeListenerToStatChange(StatType.Health, OnStatChange);
        }

        public void OnStatChange(int newMaxHealth)
        {
            CurrentHealth = newMaxHealth;
        }
        
        private void Update()
        {
            Stats.Mediator.Update(Time.deltaTime);
            ExecuteCommand();
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
    }
}