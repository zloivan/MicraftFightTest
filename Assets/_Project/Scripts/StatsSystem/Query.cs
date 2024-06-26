namespace _Project.Scripts.StatsSystem
{
    public class Query
    {
        public readonly StatType StatType;
        public int Value;

        public Query(StatType statType, int value)
        {
            StatType = statType;
            Value = value;
        }
    }
}