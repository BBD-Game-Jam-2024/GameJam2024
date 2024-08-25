using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionScript : MonoBehaviour
{
    public LogicManagerScript logic;
    private const float DefaultRadius = 2;

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
            playerCollider.radius = 0f;
            StartCoroutine(RevertRadiusAfterDelay(playerCollider, 15f));
            // MakeInvisible();
        }

        logic.StartInvincibilityTimer();
    }

    private IEnumerator RevertRadiusAfterDelay(CircleCollider2D playerCollider, float delay,
        float originalRadius = DefaultRadius)
    {
        yield return new WaitForSeconds(delay);
        playerCollider.radius = originalRadius;
        Destroy(gameObject);
    }
}