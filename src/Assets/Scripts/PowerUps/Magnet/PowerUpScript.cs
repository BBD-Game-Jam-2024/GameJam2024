using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float moveSpeed = 0.001f;
    private const float DefaultRadius = 2;
    public float deadZone = -5;
    private bool _isActivated = false;

    void Update()
    {
        transform.position += (Vector3.left) * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CoinCollision") && !_isActivated)
        {
            _isActivated = true;
            var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();
            
            if (playerCollider != null)
            {
                playerCollider.radius = 5;
                Debug.Log("Collider radius increased to: " + playerCollider.radius);
                StartCoroutine(RevertRadiusAfterDelay(playerCollider, 10f));
                MakeInvisible();
            }
        }
    }

    private void MakeInvisible()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        var collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private IEnumerator RevertRadiusAfterDelay(CircleCollider2D playerCollider, float delay, float originalRadius = DefaultRadius)
    {
        yield return new WaitForSeconds(delay);
        playerCollider.radius = originalRadius;
        Destroy(gameObject);
    }
}
