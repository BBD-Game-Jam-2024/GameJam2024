using System;
using System.Collections.Generic;
using System.Linq;

public static class Utils
{
    public static T RandomElementByWeight<T>(IEnumerable<T> sequence, Func<T, int, float> weightSelector)
    {
        var enumerable = sequence.ToList();
        var totalWeight = enumerable.Select(weightSelector).Sum();
        var itemWeightIndex = (float)(new Random().NextDouble() * totalWeight);
        float currentWeightIndex = 0;

        foreach (var item in enumerable.Select((item, index) =>
                     new { Value = item, Weight = weightSelector(item, index) }))
        {
            currentWeightIndex += item.Weight;
            if (currentWeightIndex >= itemWeightIndex)
                return item.Value;
        }

        return default;
    }
}