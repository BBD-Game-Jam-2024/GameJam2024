using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionScript : MonoBehaviour
{
    public LogicManagerScript logic;
    private const float DefaultRadius = 0.065f;

    private bool _isActivated;

    // Start is called before the first frame update
    private void Start()
    {
        logic = GameObject
            .FindWithTag("Logic")
            .GetComponent<LogicManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || _isActivated) return;

        Debug.LogWarning(collision.gameObject.tag);
        _isActivated = true;
        var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();

        if (playerCollider != null)
        {

            Debug.LogWarning("Bubble collision script:");
            Debug.LogWarning(collision.gameObject.tag);
            _isActivated = true;
            // var playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();

            // if (playerCollider != null)
            // {
            //     playerCollider.radius = DefaultRadius; // removing this logic and instead just ignoring shark collisions
            //     StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
            //     MakeInvisible();
            // }
            // logic.StartInvincibilityTimer();

            // playerCollider.radius = 0f;
            // StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
            // MakeInvisible();
        }

        // logic.StartInvincibilityTimer();
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
    private IEnumerator RevertRadiusAfterDelay(CircleCollider2D playerCollider, float delay,
        float originalRadius = DefaultRadius)
    {
        yield return new WaitForSeconds(delay);
        
        _isActivated = false;
        playerCollider.radius = originalRadius;
        Destroy(gameObject);
    }
}