using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject jellyFish;
    public GameObject powerUp;

    public float spawnRate = 2;
    private float _timer;
    public float heightOffset = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_timer < spawnRate)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            Spawn();
        }
    }

    void Spawn()
    {
        // Need to find the lowest point and highest point, below doesnt work idk why
        var lowestPoint = transform.position.y - heightOffset;
        var highestPoint = transform.position.y + heightOffset;
        // Debug.Log(lowestPoint);
        // Debug.Log(highestPoint);
        Instantiate(jellyFish, new Vector3(transform.position.x, Random.Range(-2, 2), 0), transform.rotation);
        _timer = 0;
    }
}
