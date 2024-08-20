using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] public List<GameObject> itemToSpawn;
    [SerializeField] public List<float> spawnRatio;

    [SerializeField] public float spawnRate = 2;
    [SerializeField] public float heightOffset = 2;
    private float _timer;

    private void Update()
    {
        if (_timer < spawnRate)
            _timer += Time.deltaTime;
        else
        {
            var randomHeight = Random.Range(0, heightOffset);
            var spawnPosition = transform.position + new Vector3(0, randomHeight, 0);
            var item = RandomElementByWeight(itemToSpawn, (_, val) => spawnRatio.ElementAtOrDefault(val));
            
            Instantiate(item, spawnPosition, Quaternion.identity);
            _timer = 0;
        }
    }

    // TODO: to a helper class
    private static T RandomElementByWeight<T>(IEnumerable<T> sequence, Func<T, int, float> weightSelector)
    {
        var enumerable = sequence.ToList();
        var totalWeight = enumerable.Select(weightSelector).Sum();
        var itemWeightIndex = (float)(new System.Random().NextDouble() * totalWeight);
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