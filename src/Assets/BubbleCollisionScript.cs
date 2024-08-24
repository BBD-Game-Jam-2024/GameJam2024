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
        
        _isActivated = false;
        playerCollider.radius = originalRadius;
        Destroy(gameObject);
    }
}
