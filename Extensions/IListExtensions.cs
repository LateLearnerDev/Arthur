using System.Collections.Generic;
using Godot;

namespace ArthurProject.Extensions
{
    public static class IListExtensions
    {
        public static T GetRandomItem<T>(this IList<T> list, RandomNumberGenerator rng)
        {
            rng.Randomize();
            var randomIndex = rng.RandiRange(0, list.Count - 1);
            return list[randomIndex];
        }
    }
}