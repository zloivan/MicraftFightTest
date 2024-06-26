namespace _Project.Scripts.StatsSystem
{
    public class Stats
    {
        private readonly BaseStats _baseStats;
        public StatsMediator Mediator { get; }

        public int Damage
        {
            get
            {
                var q = new Query(StatType.Damage, _baseStats.Damage);

                Mediator.PerformQuery(this, q);
                
                return q.Value;
            }
        }

        public int Defence
        {
            get
            {
                var q = new Query(StatType.Defence, _baseStats.Defence);

                Mediator.PerformQuery(this, q);
                
                return q.Value;
            }
        }

        public int Health
        {
            get
            {
                var q = new Query(StatType.Health, _baseStats.Health);

                Mediator.PerformQuery(this, q);
                
                return q.Value;
            }
        }

        public int Vampirism
        {
            get
            {
                var q = new Query(StatType.Vampirism, _baseStats.Vampirism);

                Mediator.PerformQuery(this, q);
                
                return q.Value;
            }
        }

        public Stats(StatsMediator mediator, BaseStats baseStats)
        {
            _baseStats = baseStats;
            Mediator = mediator;
        }

        public override string ToString()
        {
            return
                $"{nameof(Damage)}: {Damage}, {nameof(Defence)}: {Defence}, {nameof(Health)}: {Health}, {nameof(Vampirism)}: {Vampirism}";
        }
    }
}