using System.Collections.Generic;
using Data;

namespace Utility
{
    public static class Utility
    {
        public static IEnumerable<Stat> DeepCopyStats(this IEnumerable<Stat> originalStats)
        {
            var copiedStats = new List<Stat>();
            foreach (var stat in originalStats)
            {
                copiedStats.Add(new Stat
                {
                    id = stat.id,
                    title = stat.title,
                    value = stat.value,
                    icon = stat.icon
                });
            }
            return copiedStats;
        }
    }
}