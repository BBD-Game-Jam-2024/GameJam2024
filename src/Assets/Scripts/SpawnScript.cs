using System.Collections.Generic;
using System.Linq;
using BaseScripts;
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
        if (_timer >= spawnRate)
        {
            SpawnItem();
            _timer = 0;
        }
        else
            _timer += Time.deltaTime;
    }

    private void SpawnItem()
    {
        var item = Utils.RandomElementByWeight(itemToSpawn, (_, val) => spawnRatio.ElementAtOrDefault(val));
        // If the item has a fixed height, use that else use random bounds
        var spawnHeight = item.TryGetComponent(out IFixedHeight fixedHeightItem)
            ? fixedHeightItem.GetHeightSpecified()
            : Random.Range(-heightOffset, heightOffset);

        var spawnPosition = transform.position + new Vector3(0, spawnHeight, 0);
        Instantiate(item, spawnPosition, Quaternion.identity);
    }
}