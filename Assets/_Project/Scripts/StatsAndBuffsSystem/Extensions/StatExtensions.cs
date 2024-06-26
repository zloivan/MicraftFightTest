using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.StatsAndBuffsSystem.Extensions
{
    public static class StatExtensions
    {
        public static IEnumerable<Stat> DeepCopy(this IEnumerable<Stat> originalStats)
        {
            return originalStats.Select(stat => new Stat { id = stat.id, title = stat.title, value = stat.value, icon = stat.icon }).ToList();
        }
    }
}