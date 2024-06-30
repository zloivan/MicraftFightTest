using _Project.Scripts.PickupSystem;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Characters
{
    public class Entity : MonoBehaviour, IVisitable
    {
        [SerializeField]
        private BaseStats _baseStats;

        [SerializeField]
        private bool LogMediator;

        public Stats Stats { get; set; }

        private void Awake()
        {
            IStatsMediator mediator = new StatsMediator();

            if (LogMediator)
            {
                mediator = new StatsMediatorWithLogger(mediator);
            }

            Stats = new Stats(mediator, _baseStats);
        }

        private void Update()
        {
            Stats.Mediator.Update(Time.deltaTime);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}