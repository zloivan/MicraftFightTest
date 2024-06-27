namespace _Project.Scripts.StatsSystem
{
    public interface IStatModifierFactory
    {
        StatModifier Create(StatType statType, OperatorType operatorType, int value, float duration);
    }
}