using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollisionScript : MonoBehaviour
{
    public LogicManagerScript logic;
    public float deadZone = -5;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject
            .FindWithTag("Logic")
            .GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CoinCollision"))
        {
            Destroy(transform.parent.gameObject);
            logic.AddScore(1);
        }
        
    }
}
