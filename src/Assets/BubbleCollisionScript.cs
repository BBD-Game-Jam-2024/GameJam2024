using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionScript : MonoBehaviour
{
    public LogicManagerScript logic;
    public float moveSpeed = 0.001f;
    private const float DefaultRadius = 2;
    public float deadZone = -5;
    private bool _isActivated = false;
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
        if (collision.gameObject.tag == "Player" && !_isActivated)
        {

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

    }
    private IEnumerator RevertRadiusAfterDelay(CircleCollider2D playerCollider, float delay, float originalRadius = DefaultRadius)
    {
        yield return new WaitForSeconds(delay);
        playerCollider.radius = originalRadius;
        Destroy(gameObject);
    }
}
