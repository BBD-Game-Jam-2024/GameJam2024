using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollisionScript : MonoBehaviour
{
    public LogicManagerScript logic;
    public float deadZone = -5;
    
    void Start()
    {
        logic = GameObject
            .FindWithTag("Logic")
            .GetComponent<LogicManagerScript>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.LogWarning("Should now be here");
            Destroy(transform.parent.gameObject);
            logic.AddScore(1);
        }
        
    }
}
