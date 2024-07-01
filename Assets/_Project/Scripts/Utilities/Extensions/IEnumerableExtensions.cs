using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Utilities.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> source, int numberOfPicks)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (numberOfPicks < 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfPicks), "Number of picks cannot be negative.");

            var sourceList = source.ToList();
            if (numberOfPicks >= sourceList.Count) numberOfPicks = sourceList.Count;

            var randomIndices = new HashSet<int>();
            while (randomIndices.Count < numberOfPicks)
            {
                randomIndices.Add(UnityEngine.Random.Range(0, sourceList.Count));
            }

            foreach (var index in randomIndices)
            {
                yield return sourceList[index];
            }
        }
    }
}