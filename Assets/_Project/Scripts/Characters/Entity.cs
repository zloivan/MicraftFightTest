using _Project.Scripts.PickupSystem;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Characters
{
    public class Entity : MonoBehaviour, IVisitable
    {
        [SerializeField]
        private BaseStats _baseStats;

        public Stats Stats { get; set; }

        private void Awake()
        {
            ResetStats();
        }

        private void Update()
        {
            Stats.Mediator.Update(Time.deltaTime);
        }

        public void ResetStats()
        {
            Stats = new Stats(new StatsMediator(), _baseStats);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}